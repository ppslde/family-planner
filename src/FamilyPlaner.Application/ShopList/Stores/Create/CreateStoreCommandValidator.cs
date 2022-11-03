using FamilyPlaner.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FamilyPlaner.Application.ShopList.Stores.Create;

internal class CreateStoreCommandValidator : AbstractValidator<CreateStoreCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateStoreCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(64).WithMessage("Name must not exceed 64 characters.")
            .MustAsync(BeUniqueTitle).WithMessage("The specified Name already exists.");

        RuleFor(v => v.ShopId)
            .MustAsync(BeExistingShop).WithMessage("Store not found");
    }

    private async Task<bool> BeUniqueTitle(string name, CancellationToken cancellationToken)
    {
        return await _context
                .Stores
                .AllAsync(l => l.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase), cancellationToken);
    }

    private async Task<bool> BeExistingShop(Guid id, CancellationToken cancellationToken)
    {
        return await _context
                .Shops
                .AnyAsync(c => c.Id == id, cancellationToken);
    }
}
