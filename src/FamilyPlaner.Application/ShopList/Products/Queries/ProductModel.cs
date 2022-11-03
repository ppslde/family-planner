using FamilyPlaner.Application.Common.Mappings;
using FamilyPlaner.Domain.Entities.ShopList;

namespace FamilyPlaner.Application.ShopList.Products.Queries;

public record ProductModel : IMapFrom<Product>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CategoryId { get; set; }
}
