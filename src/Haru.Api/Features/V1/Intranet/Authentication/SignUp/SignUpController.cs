using Haru.Api.Commons.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Haru.Api.Features.V1.Intranet.Authentication.SignUp;

public class SignUpController : ApiControllerBase
{
    [HttpPost("/api/v{version:apiVersion}/intranet/authentication/signup")]
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "Intranet")]
    [Produces("application/json")]
    [SwaggerOperation(Summary = "Create new user", Description = "Create new user")]
    public async Task<IActionResult> SignUp([FromBody] SignUpCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}