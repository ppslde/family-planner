using FamilyPlaner.Application.Common.Interfaces;
using FamilyPlaner.Application.Common.Models;
using FamilyPlaner.Domain.Authorization;
using FamilyPlaner.Domain.Common;
using FamilyPlaner.Domain.Entities;
using FamilyPlaner.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FamilyPlaner.Infrastructure.Identity;

class IdentityService : IIdentityService
{
    private readonly IdentityDbContext _identityDbContext;
    private readonly JwtSettings _jwtSettings;

    public IdentityService(IdentityDbContext identityDbContext, IOptions<JwtSettings> jwtSettings)
    {
        _identityDbContext = identityDbContext;
        _jwtSettings = jwtSettings.Value;
    }

    public string GenerateUserToken(FamilyMember userInfo)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret /*builder.Configuration["Jwt:Key"]*/));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userInfo.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, userInfo.Name),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                // the JTI is used for our refresh token which we will be convering in the next video
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

        claims.AddRange(userInfo.Roles.GetFlags().Select(r => new Claim(ClaimTypes.Role, $"{r}")));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(6),
            Audience = _jwtSettings.Audience,
            Issuer = _jwtSettings.Issuer,
            IssuedAt = DateTime.UtcNow,
            SigningCredentials = credentials
        };

        JwtSecurityTokenHandler jwtTokenHandler = new();
        SecurityToken token = jwtTokenHandler.CreateToken(tokenDescriptor);
        string jwtToken = jwtTokenHandler.WriteToken(token);

        return jwtToken;
    }

    public async Task<Result<Guid>> CreateUserAsync(string userName, string password, string email, DateOnly dayOfBirth)
    {
        if (await _identityDbContext
                    .Credentials
                    .Include(c => c.FamilyMember)
                    .Where(c => c.FamilyMember.Name.ToLower() == userName.ToLower() || c.FamilyMember.Email.ToLower() == userName.ToLower())
                    .Where(c => c.FamilyMember.Name.ToLower() == email.ToLower() || c.FamilyMember.Email.ToLower() == email.ToLower())
                    .AnyAsync())
            return Result<Guid>.Failure(new[] { "User already exists" });

        FamilyMemberCredentials newCreds = new()
        {
            Hash = CreatePasswordHash(password),
            FamilyMember = new()
            {
                Name = userName,
                Email = email,
                DayOfBirth = dayOfBirth
            }
        };

        await _identityDbContext.Credentials.AddAsync(newCreds);
        await _identityDbContext.SaveChangesAsync();

        return Result<Guid>.Success(newCreds.FamilyMember.Id);
    }

    public async Task<Guid> AuthenticateUser(string userName, string password)
    {
        var pwdHash = CreatePasswordHash(password);

        Guid userId = await _identityDbContext
                                .Credentials
                                .Include(c => c.FamilyMember)
                                .Where(c => c.FamilyMember.Name.ToLower() == userName.ToLower() || c.FamilyMember.Email.ToLower() == userName.ToLower())
                                .Where(c => c.Hash == pwdHash)
                                .Select(c => c.FamilyMember.Id)
                                .SingleOrDefaultAsync();
        return userId;
    }

    public async Task<FamilyMember> GetUserInfoAsync(Guid userId)
    {
        FamilyMember user = await _identityDbContext
                         .FamilyMembers
                         .Where(m => m.Id.Equals(userId))
                         .AsNoTracking()
                         .SingleAsync();

        return user;
    }

    internal static string CreatePasswordHash(string input)
    {
        byte[] inputBytes = Encoding.ASCII.GetBytes(input);
        byte[] hashBytes = MD5.HashData(inputBytes);

        return Convert.ToHexString(hashBytes);
    }

    public async Task<bool> IsUserNameUnique(string username, CancellationToken cancellationToken)
    {
        return await _identityDbContext
                        .FamilyMembers
                        .AllAsync(m => m.Name.ToLower() != username.ToLower(), cancellationToken);
    }

    public async Task<bool> IsEmailUnique(string email, CancellationToken cancellationToken)
    {
        return await _identityDbContext
                        .FamilyMembers
                        .AllAsync(m => m.Email.ToLower() != email.ToLower(), cancellationToken);
    }

    public async Task<string> GetUserNameAsync(Guid userId)
    {
        return "";
    }

    public async Task<bool> IsInRoleAsync(Guid userId, FamilyMemberRoles role)
    {
        FamilyMemberRoles userRoles = await _identityDbContext
                                                .FamilyMembers
                                                .Where(m => m.Id == userId)
                                                .Select(m => m.Roles)
                                                .SingleOrDefaultAsync();

        return userRoles.HasFlag(role);
    }

    public async Task<bool> AuthorizeAsync(Guid userId, FamilyMemberPolicies policy)
    {
        FamilyMemberRoles userRoles = await _identityDbContext
                                                .FamilyMembers
                                                .Where(m => m.Id == userId)
                                                .Select(m => m.Roles)
                                                .SingleOrDefaultAsync();

        return FamilyMemberContracts
                .Policies[policy]
                .GetFlags()
                .Any(p => userRoles.HasFlag(p));
    }


    public async Task<Result<bool>> DeleteUserAsync(Guid userId)
    {
        return null;
    }
}
