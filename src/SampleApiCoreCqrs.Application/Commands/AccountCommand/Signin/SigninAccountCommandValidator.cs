using System;
using FluentValidation;
using SampleApiCoreCqrs.Application.Common.Validators;

namespace SampleApiCoreCqrs.Application.Commands.AccountCommand.Signin
{
    public class SigninAccountCommandValidator : AbstractValidator<SigninAccountCommand>
    {
        public SigninAccountCommandValidator()
        {
            RuleFor(v => v.Email)
                .NotNull()
                .NotEmpty().WithMessage("Campo obrigatório")
                .EmailAddress()
                .WithMessage("E-mail inválido");

            RuleFor(v => v.Password)
                .NotNull()
                .NotEmpty().WithMessage("Campo obrigatório")
                .SetValidator(new Md5Validator()).WithMessage("Utilize criptografia MD5");
        }
    }
}
