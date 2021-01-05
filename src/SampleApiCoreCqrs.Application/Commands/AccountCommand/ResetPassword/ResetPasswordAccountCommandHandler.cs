using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SampleApiCoreCqrs.Application.Common.Interfaces;
using SampleApiCoreCqrs.Application.Common.Library;
using SampleApiCoreCqrs.Infrastructure.Entities;
using SampleApiCoreCqrs.Infrastructure.Interfaces;

namespace SampleApiCoreCqrs.Application.Commands.AccountCommand.ResetPassword
{
    public class ResetPasswordAccountCommandHandler : IRequestHandler<ResetPasswordAccountCommand, IResponse>
    {
        private readonly IRepositoryUnitWork _repositoryUnitWork;
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountMailService _srvMail;

        public ResetPasswordAccountCommandHandler(
            IRepositoryUnitWork repositoryUnitWork,
            IAccountRepository accountRepository,
            IAccountMailService srvMail)
        {
            _repositoryUnitWork = repositoryUnitWork;
            _accountRepository = accountRepository;
            _srvMail = srvMail;
        }

        public async Task<IResponse> Handle(ResetPasswordAccountCommand request, CancellationToken cancellationToken)
        {
            Account account = await _accountRepository.GetAsync(p => p.Email.Equals(request.Email));

            if (account == null)
                return Response.Bad("E-mail não encontrado");

            string resetPassword = Functions.GenerateCode();
            account.ResetPassword = resetPassword.GenerateMd5();
            _accountRepository.Update(account);
            await _repositoryUnitWork.CompleteAsync();
            _srvMail.SendResetPasswordAsync(account, resetPassword);

            return Response.Ok();
        }
    }
}
