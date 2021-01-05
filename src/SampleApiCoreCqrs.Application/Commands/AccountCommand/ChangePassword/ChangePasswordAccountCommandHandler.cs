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

namespace SampleApiCoreCqrs.Application.Commands.AccountCommand.ChangePassword
{
    public class ChangePasswordAccountCommandHandler : IRequestHandler<ChangePasswordAccountCommand, IResponse<ChangePasswordAccountDto>>
    {
        private readonly IRepositoryUnitWork _repositoryUnitWork;
        private readonly IAccountRepository _accountRepository;
        private readonly IOptions<TokenConfiguration> _configToken;

        public ChangePasswordAccountCommandHandler(
            IRepositoryUnitWork repositoryUnitWork,
            IAccountRepository accountRepository,
            IOptions<TokenConfiguration> configToken)
        {
            _repositoryUnitWork = repositoryUnitWork;
            _accountRepository = accountRepository;
            _configToken = configToken;
        }

        public async Task<IResponse<ChangePasswordAccountDto>> Handle(ChangePasswordAccountCommand request, CancellationToken cancellationToken)
        {
            Account account = await _accountRepository.GetAsync(p => p.Email.Equals(request.Email));

            if (account == null)
                return Response<ChangePasswordAccountDto>.Bad("E-mail não encontrado");

            if (string.IsNullOrEmpty(account.ResetPassword))
                return Response<ChangePasswordAccountDto>.Bad("Senha temporaria não encontrada");

            if (!account.ResetPassword.Equals(request.ResetPassword))
                return Response<ChangePasswordAccountDto>.Bad("Senha temporaria invalida");

            if (account.Password.Equals(request.NewPassword))
                return Response<ChangePasswordAccountDto>.Bad("A nova senha não pode ser igual a antiga");

            DateTime dateExpiration = DateTime.UtcNow.AddMinutes(_configToken.Value.Minutes);
            ChangePasswordAccountDto res = new ChangePasswordAccountDto
            {
                DateExpiration = dateExpiration,
                Token = Functions.GenerateToken(account, dateExpiration, _configToken.Value.Key),
                IsReset = false,
                IsVerifyCode = false,
                Type = account.Type
            };

            account.ResetPassword = null;
            account.IsConfirmed = true;
            account.VerifyCode = null;
            account.Password = request.NewPassword;

            _accountRepository.Update(account);
            await _repositoryUnitWork.CompleteAsync();

            return Response<ChangePasswordAccountDto>.Ok(res);
        }
    }
}
