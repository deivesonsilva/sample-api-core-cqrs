using FluentValidation;

namespace SampleApiCoreCqrs.Application.Commands.AccountCommand.VerifyCode
{
    public class VerifyCodeAccountCommandValidator : AbstractValidator<VerifyCodeAccountCommand>
    {
        public VerifyCodeAccountCommandValidator()
        {
            RuleFor(v => v.Email)
                .NotNull()
                .NotEmpty().WithMessage("Campo obrigatório");

            RuleFor(v => v.Email)
                .EmailAddress()
                .WithMessage("E-mail inválido");

            RuleFor(v => v.Password)
                .NotNull()
                .NotEmpty().WithMessage("Campo obrigatório");

            RuleFor(v => v.Code)
                .NotNull()
                .NotEmpty().WithMessage("Campo obrigatório");
        }
    }
}
