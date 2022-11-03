namespace FamilyPlaner.Domain.Authorization;

public static class FamilyMemberContracts
{
    public static Dictionary<FamilyMemberPolicies, FamilyMemberRoles> Policies => new()
    {
        [FamilyMemberPolicies.BelongsToFamily] = FamilyMemberRoles.Relative | FamilyMemberRoles.GrandParent | FamilyMemberRoles.Children | FamilyMemberRoles.Parent,
        [FamilyMemberPolicies.BelongsToHousehold] = FamilyMemberRoles.Children | FamilyMemberRoles.Parent
    };
}
