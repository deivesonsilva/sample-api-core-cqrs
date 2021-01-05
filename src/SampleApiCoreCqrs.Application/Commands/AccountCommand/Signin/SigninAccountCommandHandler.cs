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

namespace SampleApiCoreCqrs.Application.Commands.AccountCommand.Signin
{
    public class SigninAccountCommandHandler : IRequestHandler<SigninAccountCommand, IResponse<SigninAccountDto>>
    {
        private readonly IRepositoryUnitWork _repositoryUnitWork;
        private readonly IAccountRepository _accountRepository;
        private readonly IOptions<TokenConfiguration> _configToken;

        public SigninAccountCommandHandler(
            IRepositoryUnitWork repositoryUnitWork,
            IAccountRepository accountRepository,
            IOptions<TokenConfiguration> configToken)
        {
            _repositoryUnitWork = repositoryUnitWork;
            _accountRepository = accountRepository;
            _configToken = configToken;
        }

        public async Task<IResponse<SigninAccountDto>> Handle(SigninAccountCommand request, CancellationToken cancellationToken)
        {
            Account account = await _accountRepository.GetAsync(p => p.Email.Equals(request.Email));

            if (account == null)
                return Response<SigninAccountDto>.Bad("E-mail não encontrado");

            if (!string.IsNullOrEmpty(account.ResetPassword) && !account.ResetPassword.Equals(request.Password))
                return Response<SigninAccountDto>.Bad("Senha temporaria inválida");

            if (string.IsNullOrEmpty(account.ResetPassword) && !account.Password.Equals(request.Password))
                return Response<SigninAccountDto>.Bad("Senha inválida");

            if (!account.IsConfirmed)
                return Response<SigninAccountDto>.Bad("E-mail aguardando confirmação");

            account.LastSignin = DateTime.Now;
            _accountRepository.Update(account);
            await _repositoryUnitWork.CompleteAsync();

            SigninAccountDto res = null;
            DateTime dateExpiration = DateTime.UtcNow.AddMinutes(_configToken.Value.Minutes);

            if (!string.IsNullOrEmpty(account.ResetPassword))
            {
                res = new SigninAccountDto
                {
                    DateExpiration = null,
                    Token = null,
                    IsReset = true,
                    IsVerifyCode = false,
                    Type = null
                };
                return Response<SigninAccountDto>.Ok(res);
            }

            if (!string.IsNullOrEmpty(account.VerifyCode))
            {
                res = new SigninAccountDto
                {
                    DateExpiration = null,
                    Token = null,
                    IsReset = false,
                    IsVerifyCode = true,
                    Type = null
                };
                return Response<SigninAccountDto>.Ok(res);
            }

            res = new SigninAccountDto
            {
                DateExpiration = dateExpiration,
                Token = Functions.GenerateToken(account, dateExpiration, _configToken.Value.Key),
                IsReset = false,
                IsVerifyCode = false,
                Type = account.Type
            };

            return Response<SigninAccountDto>.Ok(res);
        }
    }
}
