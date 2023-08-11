using Haru.Api.Commons.Bases;
using Haru.Api.Commons.Constants;
using MediatR;

namespace Haru.Api.Features.V1.Intranet.Authentication.Otp;

public class OtpCommand : IRequest<ResponseBase<bool>>
{
    public OtpCommand(string userId, string returnUrl)
    {
        UserId = userId;
        ReturnUrl = returnUrl;
    }

    public string UserId { get; }

    public string ReturnUrl { get; }
}

public class OtpHandler : IRequestHandler<OtpCommand, ResponseBase<bool>>
{
    private readonly IMediator _mediator;

    public OtpHandler(IMediator mediator)
    {
        _mediator = mediator;
    }
        
    public async Task<ResponseBase<bool>> Handle(OtpCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Publish(new OtpNotification { UserId = request.UserId, ReturnUrl = request.ReturnUrl}, cancellationToken);

        return Response.Ok(ResponseConstant.MessageSuccessOpt, true);
    }
}