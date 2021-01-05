using MediatR;
using SampleApiCoreCqrs.Application.Common.Interfaces;

namespace SampleApiCoreCqrs.Application.Commands.AccountCommand.ChangePassword
{
    public class ChangePasswordAccountCommand : IRequest<IResponse<ChangePasswordAccountDto>>
    {
        public string Email { get; set; }
        public string ResetPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
