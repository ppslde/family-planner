namespace FamilyPlaner.Domain.Authorization;

[Flags]
public enum FamilyMemberRoles
{
    None = -1,
    Guest = 1,
    Friend = 2,
    Relative = 4,
    GrandParent = 8,
    Children = 16,
    Parent = 32,
    Administrator = 128
}
