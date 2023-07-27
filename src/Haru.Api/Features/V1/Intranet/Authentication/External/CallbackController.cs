using Haru.Api.Commons.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Haru.Api.Features.V1.Intranet.Authentication.External;

public class CallbackController : ApiControllerBase
{
    [HttpGet("/api/v{version:apiVersion}/intranet/authentication/callback")]
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Intranet")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "External callback authentication login", Description = "External callback authentication login")]
    public async Task<IActionResult> Callback(CallbackCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}