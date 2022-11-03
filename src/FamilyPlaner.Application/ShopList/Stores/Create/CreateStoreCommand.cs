using FamilyPlaner.Application.Common.Exceptions;
using FamilyPlaner.Application.Common.Interfaces;
using FamilyPlaner.Application.Common.Security;
using FamilyPlaner.Domain.Authorization;
using FamilyPlaner.Domain.Entities.ShopList;
using MediatR;

namespace FamilyPlaner.Application.ShopList.Stores.Create;

[Authorize(Policy = FamilyMemberPolicies.BelongsToHousehold, Roles = FamilyMemberRoles.Administrator)]
public record CreateStoreCommand : IRequest<Guid>
{
    public string? Name { get; init; }
    public Guid ShopId { get; set; }
}

public class CreateStoreCommandHandler : IRequestHandler<CreateStoreCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateStoreCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
    {

        Shop? shop = await _context.Shops.FindAsync(new object?[] { request.ShopId }, cancellationToken);

        if (shop == null)
            throw new NotFoundException(nameof(Shop), request.ShopId);

        Store newStore = new()
        {
            Shop = shop,
            Name = request.Name
        };

        await _context.Stores.AddAsync(newStore, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);


        return newStore.Id;
    }
}