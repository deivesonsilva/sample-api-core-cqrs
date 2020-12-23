using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SampleApiCoreCqrs.Application.Common.Interfaces;
using SampleApiCoreCqrs.Application.Common.Library;
using SampleApiCoreCqrs.Infrastructure.Entities;
using SampleApiCoreCqrs.Infrastructure.Interfaces;

namespace SampleApiCoreCqrs.Application.Commands.AccountCommand.Register
{
    public class RegisterAccountCommandHandler : IRequestHandler<RegisterAccountCommand, IResponse>
    {
        private readonly IRepositoryUnitWork _repositoryUnitWork;
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountMailService _srvMail;

        public RegisterAccountCommandHandler(
            IRepositoryUnitWork repositoryUnitWork,
            IAccountRepository accountRepository,
            IAccountMailService srvMail)
        {
            _repositoryUnitWork = repositoryUnitWork;
            _accountRepository = accountRepository;
            _srvMail = srvMail;
        }

        public async Task<IResponse> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
        {
            Account account = await _accountRepository.GetAsync(p =>
                p.Email.Equals(request.Email));

            if (account != null && account.IsConfirmed)
                return Response.Bad("E-mail já cadastrado");

            if (account != null && !account.IsConfirmed)
                return Response.Bad("E-mail aguardando confirmação");

            string verifyCode = Functions.GenerateCode();

            account = new Account
            {
                FirstName = request.FirstName,
                SecondName = request.SecondName,
                IsConfirmed = false,
                Email = request.Email,
                Password = request.Password,
                VerifyCode = verifyCode.GenerateMd5(),
                CreatedAt = DateTime.Now
            };

            await _accountRepository.CreateAsync(account);
            await _repositoryUnitWork.CompleteAsync();
            _srvMail.SendRegisterAsync(account, verifyCode);
            return Response.Ok();
        }
    }
}
