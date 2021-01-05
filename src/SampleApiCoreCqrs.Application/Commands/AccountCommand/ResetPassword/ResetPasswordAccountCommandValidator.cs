using FluentValidation;

namespace SampleApiCoreCqrs.Application.Commands.AccountCommand.ResetPassword
{
    public class ResetPasswordAccountCommandValidator : AbstractValidator<ResetPasswordAccountCommand>
    {
        public ResetPasswordAccountCommandValidator()
        {
            RuleFor(v => v.Email)
                .NotNull()
                .NotEmpty().WithMessage("Campo obrigatório");

            RuleFor(v => v.Email)
                .EmailAddress()
                .WithMessage("E-mail inválido");
        }
    }
}
