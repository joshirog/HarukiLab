using FluentValidation;

namespace Haru.Api.Features.V1.Intranet.Account.Recovery;

public class RecoveryValidator : AbstractValidator<RecoveryCommand>
{
    public RecoveryValidator()
    {
        RuleFor(v => v.Email)
            .EmailAddress()
            .NotEmpty();
    }
}