using FamilyPlaner.Application.Common.Models;
using FamilyPlaner.Application.ShopList.Products.Create;
using FamilyPlaner.Application.ShopList.Products.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FamilyPlaner.Web.Controllers.ShopList;

[Route("shoplist")]
public class ProductController : BaseApiController
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
}
