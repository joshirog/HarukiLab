using Haru.Api.Commons.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Haru.Api.Features.v1.Services.GetServices;

public class GetServicesController : ApiControllerBase
{
    [AllowAnonymous]
    [HttpGet("/api/v{version:apiVersion}/services")]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Services")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Get all services", Description = "Get all services")]
    public async Task<IActionResult> GetServices()
    {
        return Ok(await Mediator.Send(new GetServicesQuery()));
    }
}