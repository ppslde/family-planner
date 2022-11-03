using FamilyPlaner.Domain.Common;

namespace FamilyPlaner.Domain.Entities.ShopList
{
    public class Shop : Entity
    {
        public string Name { get; set; }
        public ICollection<Store> Stores { get; set; } = new List<Store>();
    }
}
