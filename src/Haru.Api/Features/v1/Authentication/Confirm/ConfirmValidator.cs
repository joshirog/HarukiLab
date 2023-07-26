using FluentValidation;

namespace Haru.Api.Features.v1.Authentication.Confirm;

public class ConfirmValidator : AbstractValidator<ConfirmCommand>
{
    public ConfirmValidator()
    {
        RuleFor(v => v.UserId)
            .MaximumLength(36)
            .NotEmpty();
    }
}