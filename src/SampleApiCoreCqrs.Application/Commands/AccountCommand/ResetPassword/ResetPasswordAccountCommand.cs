using MediatR;
using SampleApiCoreCqrs.Application.Common.Interfaces;

namespace SampleApiCoreCqrs.Application.Commands.AccountCommand.ResetPassword
{
    public class ResetPasswordAccountCommand : IRequest<IResponse>
    {
        public string Email { get; set; }
    }
}
