using FamilyPlaner.Application.Common.Models;
using FamilyPlaner.Application.ShopList.Categories.Create;
using FamilyPlaner.Application.ShopList.Categories.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FamilyPlaner.Web.Controllers.ShopList;

[Route("shoplist")]
public class CategoryController : BaseApiController
{
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
}
