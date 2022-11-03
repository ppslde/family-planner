using FamilyPlaner.Domain.Common;

namespace FamilyPlaner.Domain.Entities.ShopList;

public class ShopListItem : Entity
{
    public uint Quantity { get; set; } = 1;
    public Product Product { get; set; }

}
