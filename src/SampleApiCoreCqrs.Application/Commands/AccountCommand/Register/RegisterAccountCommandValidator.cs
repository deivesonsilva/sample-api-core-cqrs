using FluentValidation;

namespace SampleApiCoreCqrs.Application.Commands.AccountCommand.Register
{
    public class RegisterAccountCommandValidator : AbstractValidator<RegisterAccountCommand>
    {
        public RegisterAccountCommandValidator()
        {
            RuleFor(v => v.FirstName)
                .NotNull()
                .NotEmpty().WithMessage("Campo obrigatório");

            RuleFor(v => v.SecondName)
                .NotNull()
                .NotEmpty().WithMessage("Campo obrigatório");

            RuleFor(v => v.Email)
                .NotNull()
                .NotEmpty().WithMessage("Campo obrigatório");

            RuleFor(v => v.Password)
                .NotNull()
                .NotEmpty().WithMessage("Campo obrigatório");
        }
    }
}
