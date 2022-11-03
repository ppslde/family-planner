using FamilyPlaner.Application.Common.Interfaces;
using FamilyPlaner.Application.Common.Security;
using FamilyPlaner.Domain.Authorization;
using FamilyPlaner.Domain.Entities.ShopList;
using MediatR;

namespace FamilyPlaner.Application.ShopList.Shops.Create;

[Authorize(Policy = FamilyMemberPolicies.BelongsToHousehold, Roles = FamilyMemberRoles.Administrator)]
public record CreateShopCommand : IRequest<Guid>
{
    public string? Name { get; init; }
}

public class CreateShopCommandHandler : IRequestHandler<CreateShopCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateShopCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateShopCommand request, CancellationToken cancellationToken)
    {
        Shop newShop = new()
        {
            Name = request.Name
        };

        await _context.Shops.AddAsync(newShop, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return newShop.Id;
    }
}