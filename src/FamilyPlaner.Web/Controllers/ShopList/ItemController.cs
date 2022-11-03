using FamilyPlaner.Application.ShopList.Items.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FamilyPlaner.Web.Controllers.ShopList;

[Route("shoplist")]
public class ItemController : BaseApiController
{
    [HttpGet("item")]
    public async Task<ActionResult<ICollection<ShopListItemModel>>> GetListItems()
    {
        return await Mediator.Send(new GetShopListItemsQuery());
    }
}
