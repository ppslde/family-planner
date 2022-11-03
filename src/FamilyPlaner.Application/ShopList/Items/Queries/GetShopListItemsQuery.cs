using AutoMapper;
using FamilyPlaner.Application.Common.Interfaces;
using FamilyPlaner.Application.Common.Mappings;
using FamilyPlaner.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyPlaner.Application.ShopList.Items.Queries;

[Authorize(Policy = Domain.Authorization.FamilyMemberPolicies.BelongsToHousehold)]
public record GetShopListItemsQuery : IRequest<List<ShopListItemModel>>
{
}

public class GetShopListItemsQueryHandler : IRequestHandler<GetShopListItemsQuery, List<ShopListItemModel>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetShopListItemsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ShopListItemModel>> Handle(GetShopListItemsQuery request, CancellationToken cancellationToken)
    {

        return await _context
            .ShopListItems
            .Include(i => i.Product)
            .OrderBy(i => i.Product.Name)
            .AsNoTracking()
            .ProjectToListAsync<ShopListItemModel>(_mapper.ConfigurationProvider);
    }
}