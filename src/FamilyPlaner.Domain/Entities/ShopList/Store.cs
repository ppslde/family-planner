using FamilyPlaner.Domain.Common;

namespace FamilyPlaner.Domain.Entities.ShopList;

public class Store : Entity
{
    public string Name { get; set; }
    public Shop Shop { get; set; }
    public ICollection<StoreCategory> Products { get; set; } = new LinkedList<StoreCategory>();
}
