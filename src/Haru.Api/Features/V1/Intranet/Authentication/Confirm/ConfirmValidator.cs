using FluentValidation;

namespace Haru.Api.Features.V1.Intranet.Authentication.Confirm;

public class ConfirmValidator : AbstractValidator<ConfirmCommand>
{
    public ConfirmValidator()
    {
        RuleFor(v => v.UserId)
            .MaximumLength(36)
            .NotEmpty();
    }
}