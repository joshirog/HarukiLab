using Haru.Api.Commons.Bases;
using Haru.Api.Features.v1.Services.GetServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Haru.Api.Features.v1.Categories.GetCategoriesByServiceId;

public class GetCategoriesByServiceIdController : ApiControllerBase
{
    [AllowAnonymous]
    [HttpGet("/api/v{version:apiVersion}/categories")]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Categories")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Get categories by service id", Description = "Get categories by service id")]
    public async Task<IActionResult> GetCategoriesByServiceId(Guid serviceId)
    {
        return Ok(await Mediator.Send(new GetCategoriesByServiceIdQuery{ ServiceId = serviceId }));
    }
}