using FamilyPlaner.Application.Common.Mappings;
using FamilyPlaner.Domain.Entities.ShopList;

namespace FamilyPlaner.Application.ShopList.Shops.Queries;

public record ShopModel : IMapFrom<Shop>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
