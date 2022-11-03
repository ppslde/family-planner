using FamilyPlaner.Application.Common.Interfaces;
using FamilyPlaner.Domain.Entities.ShopList;
using FamilyPlaner.Infrastructure.Common;
using FamilyPlaner.Infrastructure.Persistence.Interceptors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FamilyPlaner.Infrastructure.Persistence;

internal class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IMediator _mediator;
    private readonly EntitySaveChangesInterceptor _saveChangesInterceptor;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator mediator, EntitySaveChangesInterceptor saveChangesInterceptor) : base(options)
    {
        _mediator = mediator;
        _saveChangesInterceptor = saveChangesInterceptor;
    }

    public DbSet<ShopListItem> ShopListItems => Set<ShopListItem>();
    public DbSet<Shop> Shops => Set<Shop>();
    public DbSet<Store> Stores => Set<Store>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_saveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }
}
