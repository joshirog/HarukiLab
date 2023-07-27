using Haru.Api.Commons.Bases;
using Haru.Api.Commons.Constants;
using Haru.Api.Domains.Entities;
using Haru.Api.Features.V1.Intranet.Authentication.SignUp;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Haru.Api.Features.V1.Intranet.Account.Resend;

public class ResendCommand : IRequest<ResponseBase<bool>>
{
    public ResendCommand(string userId)
    {
        UserId = userId;
    }

    public string UserId { get; }
}

public class ResendHandler : IRequestHandler<ResendCommand,  ResponseBase<bool>>
{
    private readonly IMediator _mediator;
    private readonly UserManager<User> _userManager;

    public ResendHandler(IMediator mediator, UserManager<User> userManager)
    {
        _mediator = mediator;
        _userManager = userManager;
    }
        
    public async Task<ResponseBase<bool>> Handle(ResendCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);

        if (user is null)
            return Response.Fail(ResponseConstant.MessageFail, false);
        
        await _mediator.Publish(new SignUpNotification{ UserName = user.UserName }, cancellationToken);
            
        return Response.Ok(ResponseConstant.MessageConfirm, true);
    }
}