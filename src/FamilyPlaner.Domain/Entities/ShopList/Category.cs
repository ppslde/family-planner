using FamilyPlaner.Domain.Common;

namespace FamilyPlaner.Domain.Entities.ShopList;

public class Category : Entity
{
    public string Name { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
    public ICollection<StoreCategory> Stores { get; set; } = new List<StoreCategory>();
}