using FluentValidation;
using SampleApiCoreCqrs.Application.Common.Validators;

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
                .NotEmpty().WithMessage("Campo obrigatório")
                .EmailAddress()
                .WithMessage("E-mail inválido");

            RuleFor(v => v.Password)
                .NotNull()
                .NotEmpty().WithMessage("Campo obrigatório")
                .SetValidator(new Md5Validator()).WithMessage("Utilize criptografia MD5");

            RuleFor(v => v.Type)
                .NotNull()
                .NotEmpty().WithMessage("Campo obrigatório")
                .IsInEnum()
                .WithMessage("Tipo inválido");
        }
    }
}
