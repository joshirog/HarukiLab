using Haru.Api.Commons.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Haru.Api.Features.V1.Intranet.Account.Resend;

public class ResendController : ApiControllerBase
{
    [HttpPost("/api/v{version:apiVersion}/intranet/account/resend")]
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Intranet")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Resend change password account", Description = "Resend change password account")]
    public async Task<IActionResult> Resend([FromBody] ResendCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
    
    [HttpGet("/api/v{version:apiVersion}/intranet/account/resend")]
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Intranet")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Resend change password account", Description = "Resend change password account")]
    public async Task<IActionResult> Resend(string userId)
    {
        return Ok(await Mediator.Send(new ResendCommand(userId)));
    }
}