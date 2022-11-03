using FamilyPlaner.Application.Common.Exceptions;
using FamilyPlaner.Application.Common.Interfaces;
using FamilyPlaner.Application.Common.Security;
using FamilyPlaner.Domain.Authorization;
using FamilyPlaner.Domain.Entities.ShopList;
using MediatR;

namespace FamilyPlaner.Application.ShopList.Products.Create;

[Authorize(Policy = FamilyMemberPolicies.BelongsToHousehold, Roles = FamilyMemberRoles.Administrator)]
public record CreateProductCommand : IRequest<Guid>
{
    public string? Name { get; init; }
    public Guid CategoryId { get; set; }
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {

        Category? category = await _context.Categories.FindAsync(new object?[] { request.CategoryId }, cancellationToken);

        if (category == null)
            throw new NotFoundException(nameof(Category), request.CategoryId);

        Product newProduct = new()
        {
            Category = category,
            Name = request.Name
        };

        await _context.Products.AddAsync(newProduct, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);


        return newProduct.Id;
    }
}