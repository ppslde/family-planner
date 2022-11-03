using FamilyPlaner.Domain.Entities;
using FamilyPlaner.Infrastructure.Common;
using FamilyPlaner.Infrastructure.Persistence.Interceptors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FamilyPlaner.Infrastructure.Persistence
{
    internal class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
    {
        public DateOnlyConverter()
            : base(d => d.ToDateTime(TimeOnly.MinValue),
                   d => DateOnly.FromDateTime(d))
        { }
    }

    internal class IdentityDbContext : DbContext
    {
        private readonly IMediator _mediator;
        private readonly EntitySaveChangesInterceptor _saveChangesInterceptor;

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options, IMediator mediator, EntitySaveChangesInterceptor saveChangesInterceptor) : base(options)
        {
            _mediator = mediator;
            _saveChangesInterceptor = saveChangesInterceptor;
        }

        public DbSet<FamilyMember> FamilyMembers => Set<FamilyMember>();
        public DbSet<FamilyMemberCredentials> Credentials => Set<FamilyMemberCredentials>();

        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {
            builder
                .Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter>()
                .HaveColumnType("datetime");

            builder
                .Properties<Enum>()
                .HaveConversion<string>();

            base.ConfigureConventions(builder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
               .Entity<FamilyMember>()
               .HasQueryFilter(m => !m.Deleted);

            builder.Entity<FamilyMember>()
                .Property(b => b.Name)
                .HasMaxLength(64)
                .IsRequired();

            builder.Entity<FamilyMember>()
                .Property(t => t.Email)
                .HasMaxLength(128)
                .IsRequired();

            builder
                .Entity<FamilyMemberCredentials>()
                .HasQueryFilter(c => !c.Deleted);

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
}
