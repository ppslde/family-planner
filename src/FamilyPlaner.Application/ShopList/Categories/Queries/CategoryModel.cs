using FamilyPlaner.Application.Common.Mappings;
using FamilyPlaner.Domain.Entities.ShopList;

namespace FamilyPlaner.Application.ShopList.Categories.Queries;

public record CategoryModel : IMapFrom<Category>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
