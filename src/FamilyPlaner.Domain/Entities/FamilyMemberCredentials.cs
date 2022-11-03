using FamilyPlaner.Domain.Common;

namespace FamilyPlaner.Domain.Entities;

public class FamilyMemberCredentials : Entity
{
    public string Hash { get; set; } = "";
    public FamilyMember FamilyMember { get; set; } = null!;
}
