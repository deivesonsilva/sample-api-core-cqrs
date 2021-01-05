using SampleApiCoreCqrs.Infrastructure.Entities;

namespace SampleApiCoreCqrs.Application.Common.Interfaces
{
    public interface IAccountMailService
    {
        void SendRegisterAsync(Account entity, string verifyCode);
        void SendResetPasswordAsync(Account entity, string resetPassword);
    }
}
