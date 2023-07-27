using FluentValidation;

namespace Haru.Api.Features.V1.Intranet.Authentication.SignIn;

public class SignInValidator : AbstractValidator<SignInCommand>
{
    public SignInValidator()
    {
        RuleFor(v => v.UserName)
            .MaximumLength(200)
            .NotEmpty()
            .EmailAddress();
            
        RuleFor(v => v.Password)
            .MaximumLength(200)
            .NotEmpty();
    }
}