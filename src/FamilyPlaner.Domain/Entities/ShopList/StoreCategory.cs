using FamilyPlaner.Domain.Common;

namespace FamilyPlaner.Domain.Entities.ShopList;

public class StoreCategory : Entity
{
    public Store Store { get; set; }
    public Category Category { get; set; }
    public int Order { get; set; }
}
