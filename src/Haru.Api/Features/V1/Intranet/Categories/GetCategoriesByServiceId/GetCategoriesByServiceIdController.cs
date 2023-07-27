using Haru.Api.Commons.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Haru.Api.Features.V1.Intranet.Categories.GetCategoriesByServiceId;

public class GetCategoriesByServiceIdController : ApiControllerBase
{
    [AllowAnonymous]
    [HttpGet("/api/v{version:apiVersion}/intranet/categories")]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Intranet")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Get categories by service id", Description = "Get categories by service id")]
    public async Task<IActionResult> GetCategoriesByServiceId(Guid serviceId)
    {
        return Ok(await Mediator.Send(new GetCategoriesByServiceIdQuery{ ServiceId = serviceId }));
    }
}