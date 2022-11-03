using FamilyPlaner.Domain.Common;

namespace FamilyPlaner.Domain.Entities.ShopList;

public class Product : Entity
{
    public string Name { get; set; }
    public Category Category { get; set; }

}
