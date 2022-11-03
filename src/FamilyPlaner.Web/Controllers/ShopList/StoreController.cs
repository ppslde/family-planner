using FamilyPlaner.Application.Common.Models;
using FamilyPlaner.Application.ShopList.Shops.Create;
using FamilyPlaner.Application.ShopList.Stores.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FamilyPlaner.Web.Controllers.ShopList;

[Route("shoplist")]
public class StoreController : BaseApiController
{
    [HttpGet("store")]
    public async Task<ActionResult<PaginatedList<StoreModel>>> GetShopStores([FromQuery] GetShopStoresPagedQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost("store")]
    public async Task<ActionResult<Guid>> CreateStore(CreateShopCommand command)
    {
        return await Mediator.Send(command);
    }
}
