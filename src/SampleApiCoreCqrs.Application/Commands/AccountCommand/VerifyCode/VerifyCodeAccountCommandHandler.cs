using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using SampleApiCoreCqrs.Application.Common.Interfaces;
using SampleApiCoreCqrs.Application.Common.Library;
using SampleApiCoreCqrs.Application.Common.Model;
using SampleApiCoreCqrs.Infrastructure.Entities;
using SampleApiCoreCqrs.Infrastructure.Interfaces;

namespace SampleApiCoreCqrs.Application.Commands.AccountCommand.VerifyCode
{
    public class VerifyCodeAccountCommandHandler : IRequestHandler<VerifyCodeAccountCommand, IResponse<VerifyCodeAccountDto>>
    {
        private readonly IRepositoryUnitWork _repositoryUnitWork;
        private readonly IAccountRepository _accountRepository;
        private readonly IOptions<TokenConfiguration> _configToken;

        public VerifyCodeAccountCommandHandler(
            IRepositoryUnitWork repositoryUnitWork,
            IAccountRepository accountRepository,
            IOptions<TokenConfiguration> configToken)
        {
            _repositoryUnitWork = repositoryUnitWork;
            _accountRepository = accountRepository;
            _configToken = configToken;
        }

        public async Task<IResponse<VerifyCodeAccountDto>> Handle(VerifyCodeAccountCommand request, CancellationToken cancellationToken)
        {
            Account account = await _accountRepository.GetAsync(p => p.Email.Equals(request.Email));

            if (account == null)
                return Response<VerifyCodeAccountDto>.Bad("E-mail não encontrado");

            if (account.IsConfirmed)
                return Response<VerifyCodeAccountDto>.Bad("E-mail já confirmado");

            if (!account.Password.Equals(request.Password))
                return Response<VerifyCodeAccountDto>.Bad("Senha inválida");

            if (!account.VerifyCode.Equals(request.Code))
                return Response<VerifyCodeAccountDto>.Bad("Código inválido");

            DateTime dateExpiration = DateTime.UtcNow.AddMinutes(_configToken.Value.Minutes);

            VerifyCodeAccountDto res = new VerifyCodeAccountDto
            {
                DateExpiration = dateExpiration,
                Token = Functions.GenerateToken(account, dateExpiration, _configToken.Value.Key),
                IsReset = false,
                IsVerifyCode = false,
                Type = account.Type
            };

            account.IsConfirmed = true;
            account.VerifyCode = null;
            account.ResetPassword = null;
            _accountRepository.Update(account);
            await _repositoryUnitWork.CompleteAsync();

            return Response<VerifyCodeAccountDto>.Ok(res);
        }
    }
}
