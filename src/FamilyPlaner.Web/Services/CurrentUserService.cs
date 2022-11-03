using FamilyPlaner.Application.Common.Interfaces;
using System.Security.Claims;

namespace FamilyPlaner.Web.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId => TryGetUserId();

    private Guid TryGetUserId()
    {
        if (!Guid.TryParse(_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            userId = Guid.Empty;

        return userId;
    }
}
