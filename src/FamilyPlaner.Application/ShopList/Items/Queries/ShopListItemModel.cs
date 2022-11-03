using FamilyPlaner.Application.Common.Mappings;
using FamilyPlaner.Domain.Entities.ShopList;

namespace FamilyPlaner.Application.ShopList.Items.Queries;

public record ShopListItemModel : IMapFrom<ShopListItem>
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public Guid CreatedBy { get; set; }
}
