using FamilyPlaner.Application.Common.Mappings;
using FamilyPlaner.Domain.Entities;

namespace FamilyPlaner.Application.FamilyMembers.Create
{
    public record CreatedFamilyMemberModel : IMapFrom<FamilyMember>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
    }
}
