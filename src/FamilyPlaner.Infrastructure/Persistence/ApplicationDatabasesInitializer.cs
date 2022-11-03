using FamilyPlaner.Domain.Authorization;
using FamilyPlaner.Domain.Entities;
using FamilyPlaner.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyPlaner.Infrastructure.Persistence;

internal class ApplicationDatabasesInitializer
{
    private readonly ILogger<ApplicationDatabasesInitializer> _logger;
    private readonly ApplicationDbContext _applicationDb;
    private readonly IdentityDbContext _identityDb;

    public ApplicationDatabasesInitializer(ILogger<ApplicationDatabasesInitializer> logger, ApplicationDbContext applicationDb, IdentityDbContext identityDb)
    {
        _logger = logger;
        _applicationDb = applicationDb;
        _identityDb = identityDb;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_applicationDb.Database.IsRelational())
                await _applicationDb.Database.MigrateAsync();


            if (_identityDb.Database.IsRelational())
                await _identityDb.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        FamilyMember jdoe = new()
        {
            Name = "John Doe",
            Email = "jdoe@localhost",
            DayOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Roles = FamilyMemberRoles.Administrator | FamilyMemberRoles.Relative
        };

        FamilyMemberCredentials creds = new()
        {
            Hash = IdentityService.CreatePasswordHash("!Admin1"),
            FamilyMember = jdoe,
        };

        if (!await _identityDb.FamilyMembers.AnyAsync(m => m.Name.ToLower() == jdoe.Name.ToLower() && m.Email.ToLower().ToLower() == jdoe.Email.ToLower()))
        {
            await _identityDb.Credentials.AddAsync(creds);
            await _identityDb.SaveChangesAsync();
        }
    }
}
