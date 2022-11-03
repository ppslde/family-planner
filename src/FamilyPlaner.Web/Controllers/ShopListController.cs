using FamilyPlaner.Application.Common.Models;
using FamilyPlaner.Application.ShopList.Categories.Create;
using FamilyPlaner.Application.ShopList.Categories.Queries;
using FamilyPlaner.Application.ShopList.Products.Create;
using FamilyPlaner.Application.ShopList.Products.Queries;
using FamilyPlaner.Application.ShopList.Shops.Create;
using FamilyPlaner.Application.ShopList.Shops.Queries;
using FamilyPlaner.Application.ShopList.Stores.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FamilyPlaner.Web.Controllers;

[Authorize]
public class ShopListController : BaseApiController
{
    [HttpGet("product")]
    public async Task<ActionResult<PaginatedList<ProductModel>>> GetCategoryProducts([FromQuery] GetCatagoryProductsPagedQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost("product")]
    public async Task<ActionResult<Guid>> CreateProduct(CreateProductCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpGet("category")]
    public async Task<ActionResult<PaginatedList<CategoryModel>>> GetCategoryProducts([FromQuery] GetCatagoriesPagedQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost("category")]
    public async Task<ActionResult<Guid>> CreateProduct(CreateCategoryCommand command)
    {
        return await Mediator.Send(command);
    }

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
