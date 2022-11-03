using FamilyPlaner.Domain.Authorization;
using FamilyPlaner.Domain.Common;

namespace FamilyPlaner.Domain.Entities;

public class FamilyMember : Entity
{
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public DateOnly DayOfBirth { get; set; } = DateOnly.MinValue;
    public FamilyMemberRoles Roles { get; set; } = FamilyMemberRoles.None;
}