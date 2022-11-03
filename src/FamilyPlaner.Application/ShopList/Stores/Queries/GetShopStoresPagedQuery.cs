using AutoMapper;
using AutoMapper.QueryableExtensions;
using FamilyPlaner.Application.Common.Interfaces;
using FamilyPlaner.Application.Common.Mappings;
using FamilyPlaner.Application.Common.Models;
using FamilyPlaner.Application.Common.Security;
using FamilyPlaner.Domain.Authorization;
using MediatR;
using System.Data;

namespace FamilyPlaner.Application.ShopList.Stores.Queries;

[Authorize(Roles = FamilyMemberRoles.Administrator)]
[Authorize(Policy = FamilyMemberPolicies.BelongsToHousehold)]
public record GetShopStoresPagedQuery : IRequest<PaginatedList<StoreModel>>
{
    public Guid ShopId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetShopStoresPagedQueryHandler : IRequestHandler<GetShopStoresPagedQuery, PaginatedList<StoreModel>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetShopStoresPagedQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<StoreModel>> Handle(GetShopStoresPagedQuery request, CancellationToken cancellationToken)
    {
        return await _context
            .Stores
            .Where(x => x.Shop.Id == request.ShopId)
            .OrderBy(x => x.Name)
            .ProjectTo<StoreModel>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}