using FamilyPlaner.Application.Common.Models;
using FamilyPlaner.Application.ShopList.Shops.Create;
using FamilyPlaner.Application.ShopList.Shops.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FamilyPlaner.Web.Controllers.ShopList;

[Route("shoplist")]
public class ShopController : BaseApiController
{
    [HttpGet("shop")]
    public async Task<ActionResult<PaginatedList<ShopModel>>> GetShopss([FromQuery] GetShopsPagedQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost("shop")]
    public async Task<ActionResult<Guid>> CreateShop(CreateShopCommand command)
    {
        return await Mediator.Send(command);
    }
}
