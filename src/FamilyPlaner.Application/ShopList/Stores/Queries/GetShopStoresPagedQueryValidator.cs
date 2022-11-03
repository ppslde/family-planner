using FluentValidation;

namespace FamilyPlaner.Application.ShopList.Stores.Queries;

internal class GetShopStoresPagedQueryValidator : AbstractValidator<GetShopStoresPagedQuery>
{
    public GetShopStoresPagedQueryValidator()
    {
        RuleFor(x => x.ShopId)
            .NotEmpty().WithMessage("Shop id is required.");

        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}
