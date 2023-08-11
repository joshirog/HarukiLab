using Haru.Api.Commons.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Haru.Api.Features.V1.Intranet.Authentication.TwoFactor;

public class TwoFactorController : ApiControllerBase
{
    [HttpPost("/api/v{version:apiVersion}/intranet/authentication/twostep")]
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Intranet")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Two step authenticator 2Factor", Description = "Two step authenticator 2Factor")]
    public async Task<IActionResult> TwoStep([FromBody] TwoFactorCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
    
    [HttpGet("/api/v{version:apiVersion}/intranet/authentication/twostep")]
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Intranet")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Two step authenticator 2Factor", Description = "Two step authenticator 2Factor")]
    public async Task<IActionResult> TwoStep(Guid userId)
    {
        return Ok(await Mediator.Send(new TwoFactorCommand(userId)));
    }
}