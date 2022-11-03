using FamilyPlaner.Application.Common.Models;
using FamilyPlaner.Domain.Authorization;
using FamilyPlaner.Domain.Entities;

namespace FamilyPlaner.Application.Common.Interfaces;

public interface IIdentityService
{
	Task<Guid> AuthenticateUser(string userName, string password);
	Task<Result<Guid>> CreateUserAsync(string userName, string password, string email, DateOnly dayOfBirth);
	Task<FamilyMember> GetUserInfoAsync(Guid userId);

	Task<string> GetUserNameAsync(Guid userId);
	Task<bool> IsInRoleAsync(Guid userId, FamilyMemberRoles role);
	Task<bool> AuthorizeAsync(Guid userId, FamilyMemberPolicies policy);

	Task<Result<bool>> DeleteUserAsync(Guid userId);
	string GenerateUserToken(FamilyMember userInfo);
	Task<bool> IsUserNameUnique(string username, CancellationToken cancellationToken);
	Task<bool> IsEmailUnique(string email, CancellationToken cancellationToken);
}
