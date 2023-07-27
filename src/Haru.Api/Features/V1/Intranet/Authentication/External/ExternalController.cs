using Haru.Api.Commons.Bases;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Haru.Api.Features.V1.Intranet.Authentication.External;

public class ExternalController : ApiControllerBase
{
    [HttpPost("/api/v{version:apiVersion}/intranet/authentication/external")]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Intranet")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "External authentication login", Description = "External authentication login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> External([FromBody] ExternalCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
    
    [HttpGet("/api/v{version:apiVersion}/intranet/authentication/external")]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Intranet")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "External schemes authentication login", Description = "External schemes authentication login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> External()
    {
        return Ok(await Mediator.Send(new ExternalQuery()));
    }
}