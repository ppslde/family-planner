using FluentValidation;

namespace FamilyPlaner.Application.ShopList.Products.Queries;

internal class GetCatagoryProductsPagedQueryValidator : AbstractValidator<GetCatagoryProductsPagedQuery>
{
    public GetCatagoryProductsPagedQueryValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Category id is required.");

        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}
