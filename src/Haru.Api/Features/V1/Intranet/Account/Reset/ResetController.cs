using Haru.Api.Commons.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Haru.Api.Features.V1.Intranet.Account.Reset;

public class ResetController : ApiControllerBase
{
    [HttpPost("/api/v{version:apiVersion}/intranet/account/reset")]
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Intranet")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Reset a new password account", Description = "Reset a new password account")]
    public async Task<IActionResult> Reset([FromBody] ResetCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
    
    [HttpGet("/api/v{version:apiVersion}/intranet/account/reset")]
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Intranet")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Reset a new password account", Description = "Reset a new password account")]
    public async Task<IActionResult> Reset(string userId, string token)
    {
        return Ok(await Mediator.Send(new ResetCommand(userId, token)));
    }
}