using Haru.Api.Commons.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Haru.Api.Features.V1.Intranet.Authentication.SignIn;

public class SignInController : ApiControllerBase
{
    [HttpPost("/api/v{version:apiVersion}/intranet/authentication/signin")]
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Intranet")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Authentication user", Description = "Authentication user")]
    public async Task<IActionResult> SignIn([FromBody] SignInCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}