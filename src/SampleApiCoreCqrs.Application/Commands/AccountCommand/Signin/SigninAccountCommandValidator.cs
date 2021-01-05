using System;
using FluentValidation;

namespace SampleApiCoreCqrs.Application.Commands.AccountCommand.Signin
{
    public class SigninAccountCommandValidator : AbstractValidator<SigninAccountCommand>
    {
        public SigninAccountCommandValidator()
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

            RuleFor(v => v.Password)
                .MinimumLength(6)
                .WithMessage("A senha deve ter no mínimo 6 caracteres");
        }
    }
}
