using FluentValidation;

namespace Haru.Api.Features.V1.Intranet.Authentication.Otp;

public class OtpValidator : AbstractValidator<OtpCommand>
{
    public OtpValidator()
    {
        RuleFor(v => v.UserId)
            .NotEmpty();
    }
}