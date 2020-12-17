using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SampleApiCoreCqrs.Infrastructure.Interfaces;

namespace SampleApiCoreCqrs.Application.Commands.AccountCommand.Register
{
    public class RegisterAccountCommandHandler : IRequestHandler<RegisterAccountCommand, string>
    {
        private readonly IRepositoryUnitWork _repositoryUnitWork;
        private readonly IAccountRepository _accountRepository;
        //private readonly IAccountMailService _srvMail;

        public RegisterAccountCommandHandler(
            IRepositoryUnitWork repositoryUnitWork,
            IAccountRepository accountRepository)
            //IAccountMailService srvMail)
        {
            _repositoryUnitWork = repositoryUnitWork;
            _accountRepository = accountRepository;
            //_srvMail = srvMail;
        }

        public async Task<string> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
        {
            Thread.Sleep(501);

            int.Parse("a");

            return await Task.FromResult("passou");
        }
    }
}
