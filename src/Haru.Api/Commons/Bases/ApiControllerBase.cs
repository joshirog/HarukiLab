using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Haru.Api.Commons.Bases;

[ApiController]
[Authorize]
[Route( "api/[controller]" )]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>()!;
}