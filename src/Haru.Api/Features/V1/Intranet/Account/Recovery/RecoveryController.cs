using Haru.Api.Commons.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Haru.Api.Features.V1.Intranet.Account.Recovery;

public class RecoveryController : ApiControllerBase
{
    [HttpPost("/api/v{version:apiVersion}/intranet/account/recovery")]
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Intranet")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Recovery password account", Description = "Recovery password account")]
    public async Task<IActionResult> Recovery([FromBody] RecoveryCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}