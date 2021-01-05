using FluentValidation;
using SampleApiCoreCqrs.Application.Common.Validators;

namespace SampleApiCoreCqrs.Application.Commands.AccountCommand.ChangePassword
{
    public class ChangePasswordAccountCommandValidator : AbstractValidator<ChangePasswordAccountCommand>
    {
        public ChangePasswordAccountCommandValidator()
        {
            RuleFor(v => v.Email)
                .NotNull()
                .NotEmpty().WithMessage("Campo obrigatório");

            RuleFor(v => v.Email)
                .EmailAddress()
                .WithMessage("E-mail inválido");

            RuleFor(v => v.ResetPassword)
                .NotNull()
                .NotEmpty().WithMessage("Campo obrigatório")
                .SetValidator(new Md5Validator()).WithMessage("Utilize criptografia MD5");

            RuleFor(v => v.NewPassword)
                .NotNull()
                .NotEmpty().WithMessage("Campo obrigatório")
                .SetValidator(new Md5Validator()).WithMessage("Utilize criptografia MD5");
        }
    }
}
