using FamilyPlaner.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FamilyPlaner.Application.ShopList.Products.Create;

internal class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateProductCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(64).WithMessage("Name must not exceed 64 characters.")
            .MustAsync(BeUniqueTitle).WithMessage("The specified name already exists.");

        RuleFor(v => v.CategoryId)
            .MustAsync(CategoryExists).WithMessage("Category not found");
    }

    private async Task<bool> BeUniqueTitle(string name, CancellationToken cancellationToken)
    {
        return await _context
                .Products
                .AllAsync(l => l.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase), cancellationToken);
    }

    private async Task<bool> CategoryExists(Guid categoryId, CancellationToken cancellationToken)
    {
        return await _context
                .Categories
                .AnyAsync(c => c.Id == categoryId, cancellationToken);
    }
}
