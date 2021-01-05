using FluentValidation;
using SampleApiCoreCqrs.Application.Common.Validators;

namespace SampleApiCoreCqrs.Application.Commands.AccountCommand.VerifyCode
{
    public class VerifyCodeAccountCommandValidator : AbstractValidator<VerifyCodeAccountCommand>
    {
        public VerifyCodeAccountCommandValidator()
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

            RuleFor(v => v.Code)
                .NotNull()
                .NotEmpty().WithMessage("Campo obrigatório")
                .SetValidator(new Md5Validator()).WithMessage("Utilize criptografia MD5");
        }
    }
}
