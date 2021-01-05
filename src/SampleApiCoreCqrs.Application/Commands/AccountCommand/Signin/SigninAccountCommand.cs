using MediatR;
using SampleApiCoreCqrs.Application.Common.Interfaces;

namespace SampleApiCoreCqrs.Application.Commands.AccountCommand.Signin
{
    public class SigninAccountCommand : IRequest<IResponse<SigninAccountDto>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
