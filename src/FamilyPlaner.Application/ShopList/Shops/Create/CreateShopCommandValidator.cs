using FamilyPlaner.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FamilyPlaner.Application.ShopList.Shops.Create;

internal class CreateShopCommandValidator : AbstractValidator<CreateShopCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateShopCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(64).WithMessage("Name must not exceed 64 characters.")
            .MustAsync(BeUniqueTitle).WithMessage("The specified name already exists.");
    }

    private async Task<bool> BeUniqueTitle(string name, CancellationToken cancellationToken)
    {
        return await _context
                .Shops
                .AllAsync(l => l.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase), cancellationToken);
    }
}
