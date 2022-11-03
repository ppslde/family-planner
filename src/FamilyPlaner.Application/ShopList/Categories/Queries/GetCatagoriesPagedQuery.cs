using AutoMapper;
using AutoMapper.QueryableExtensions;
using FamilyPlaner.Application.Common.Interfaces;
using FamilyPlaner.Application.Common.Mappings;
using FamilyPlaner.Application.Common.Models;
using FamilyPlaner.Application.Common.Security;
using FamilyPlaner.Domain.Authorization;
using MediatR;
using System.Data;

namespace FamilyPlaner.Application.ShopList.Categories.Queries;

[Authorize(Roles = FamilyMemberRoles.Administrator)]
[Authorize(Policy = FamilyMemberPolicies.BelongsToHousehold)]
public record GetCatagoriesPagedQuery : IRequest<PaginatedList<CategoryModel>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 50;
}

public class GetCatagoriesPagedQueryHandler : IRequestHandler<GetCatagoriesPagedQuery, PaginatedList<CategoryModel>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCatagoriesPagedQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<CategoryModel>> Handle(GetCatagoriesPagedQuery request, CancellationToken cancellationToken)
    {
        return await _context
            .Categories
            .OrderBy(x => x.Name)
            .ProjectTo<CategoryModel>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}