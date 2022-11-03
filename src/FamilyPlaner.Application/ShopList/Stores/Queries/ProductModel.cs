using FamilyPlaner.Application.Common.Mappings;
using FamilyPlaner.Domain.Entities.ShopList;

namespace FamilyPlaner.Application.ShopList.Stores.Queries;

public record StoreModel : IMapFrom<Store>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid ShopId { get; set; }
}
