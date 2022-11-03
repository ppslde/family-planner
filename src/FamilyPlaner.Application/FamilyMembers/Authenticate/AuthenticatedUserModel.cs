using FamilyPlaner.Application.Common.Mappings;
using FamilyPlaner.Domain.Entities;

namespace FamilyPlaner.Application.FamilyMembers.Authenticate;

public class AuthenticatedUserModel
{
    public UserInfoModel UserInfo { get; set; } = null!;
    public string AccessToken { get; set; } = "";
}

public class UserInfoModel : IMapFrom<FamilyMember>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
}
