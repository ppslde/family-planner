using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FamilyPlaner.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class BaseApiController : ControllerBase
{
    private ISender _mediator = null!;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}