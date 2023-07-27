using Haru.Api.Commons.Bases;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Haru.Api.Features.V1.Intranet.Account.SignOut;

public class SignOutController : ApiControllerBase
{
    [HttpPost("/api/v{version:apiVersion}/intranet/account/signout")]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Intranet")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Sign out account session", Description = "Sign out account session")]
    public async Task<IActionResult> SignOut([FromBody] SignOutCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}