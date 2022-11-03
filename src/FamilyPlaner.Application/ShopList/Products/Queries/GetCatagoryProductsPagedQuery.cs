using AutoMapper;
using AutoMapper.QueryableExtensions;
using FamilyPlaner.Application.Common.Interfaces;
using FamilyPlaner.Application.Common.Mappings;
using FamilyPlaner.Application.Common.Models;
using FamilyPlaner.Application.Common.Security;
using FamilyPlaner.Domain.Authorization;
using MediatR;
using System.Data;

namespace FamilyPlaner.Application.ShopList.Products.Queries;

[Authorize(Roles = FamilyMemberRoles.Administrator)]
[Authorize(Policy = FamilyMemberPolicies.BelongsToHousehold)]
public record GetCatagoryProductsPagedQuery : IRequest<PaginatedList<ProductModel>>
{
    public Guid CategoryId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetCatagoryProductsPagedQueryHandler : IRequestHandler<GetCatagoryProductsPagedQuery, PaginatedList<ProductModel>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCatagoryProductsPagedQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ProductModel>> Handle(GetCatagoryProductsPagedQuery request, CancellationToken cancellationToken)
    {
        return await _context
            .Products
            .Where(x => x.Category.Id == request.CategoryId)
            .OrderBy(x => x.Name)
            .ProjectTo<ProductModel>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}