using FamilyPlaner.Application.Common.Interfaces;
using FamilyPlaner.Application.Common.Security;
using FamilyPlaner.Domain.Authorization;
using FamilyPlaner.Domain.Entities.ShopList;
using MediatR;

namespace FamilyPlaner.Application.ShopList.Categories.Create;

[Authorize(Policy = FamilyMemberPolicies.BelongsToHousehold, Roles = FamilyMemberRoles.Administrator)]
public record CreateCategoryCommand : IRequest<Guid>
{
    public string? Name { get; init; }
}

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category newCategory = new()
        {
            Name = request.Name
        };

        await _context.Categories.AddAsync(newCategory, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return newCategory.Id;
    }
}