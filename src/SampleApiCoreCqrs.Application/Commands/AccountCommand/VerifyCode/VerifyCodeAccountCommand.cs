using MediatR;
using SampleApiCoreCqrs.Application.Common.Interfaces;

namespace SampleApiCoreCqrs.Application.Commands.AccountCommand.VerifyCode
{
    public class VerifyCodeAccountCommand : IRequest<IResponse<VerifyCodeAccountDto>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Code { get; set; }
    }
}
