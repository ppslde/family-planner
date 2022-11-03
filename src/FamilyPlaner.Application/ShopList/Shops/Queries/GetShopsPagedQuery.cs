using AutoMapper;
using AutoMapper.QueryableExtensions;
using FamilyPlaner.Application.Common.Interfaces;
using FamilyPlaner.Application.Common.Mappings;
using FamilyPlaner.Application.Common.Models;
using FamilyPlaner.Application.Common.Security;
using FamilyPlaner.Domain.Authorization;
using MediatR;
using System.Data;

namespace FamilyPlaner.Application.ShopList.Shops.Queries;

[Authorize(Roles = FamilyMemberRoles.Administrator)]
[Authorize(Policy = FamilyMemberPolicies.BelongsToHousehold)]
public record GetShopsPagedQuery : IRequest<PaginatedList<ShopModel>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 50;
}

public class GetShopsPagedQueryHandler : IRequestHandler<GetShopsPagedQuery, PaginatedList<ShopModel>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetShopsPagedQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ShopModel>> Handle(GetShopsPagedQuery request, CancellationToken cancellationToken)
    {
        return await _context
            .Shops
            .OrderBy(x => x.Name)
            .ProjectTo<ShopModel>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}