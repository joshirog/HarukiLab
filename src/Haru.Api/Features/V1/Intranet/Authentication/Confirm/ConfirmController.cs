using Haru.Api.Commons.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Haru.Api.Features.V1.Intranet.Authentication.Confirm;

public class ConfirmController : ApiControllerBase
{
    [HttpGet("/api/v{version:apiVersion}/intranet/authentication/confirm")]
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Intranet")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Confirm activate account", Description = "Confirm activate account")]
    public async Task<IActionResult> Confirm([FromBody] ConfirmCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}