using FamilyPlaner.Application.FamilyMembers.Authenticate;
using FamilyPlaner.Application.FamilyMembers.Create;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FamilyPlaner.Web.Controllers;

[Authorize]
public class FamilyMemberController : BaseApiController
{
    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<ActionResult<AuthenticatedUserModel>> Authenticate(AuthenticateFamilyMemberCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost]
    public async Task<ActionResult<CreatedFamilyMemberModel>> Create(CreateFamilyMemberCommand command)
    {
        return await Mediator.Send(command);
    }
}
