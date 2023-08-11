using Haru.Api.Commons.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Haru.Api.Features.V1.Intranet.Authentication.Otp;

public class OtpController : ApiControllerBase
{
    [HttpGet("/api/v{version:apiVersion}/intranet/authentication/otp")]
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Intranet")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Otp 2 factor authentication", Description = "Otp 2 factor authentication")]
    public async Task<IActionResult> Otp([FromBody] OtpCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}