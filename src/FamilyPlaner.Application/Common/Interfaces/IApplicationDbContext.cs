using FamilyPlaner.Domain.Entities.ShopList;
using Microsoft.EntityFrameworkCore;

namespace FamilyPlaner.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<ShopListItem> ShopListItems { get; }
    DbSet<Product> Products { get; }
    DbSet<Category> Categories { get; }
    DbSet<Shop> Shops { get; }
    DbSet<Store> Stores { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
