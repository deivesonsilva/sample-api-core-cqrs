using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using SampleApiCoreCqrs.Application.Common.Interfaces;

namespace SampleApiCoreCqrs.Application.Behaviours
{
    public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger;
        private readonly ICurrentUserService _currentUserService;

        public UnhandledExceptionBehaviour(
            ICurrentUserService currentUserService,
            ILogger<TRequest> logger)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                string commandName = typeof(TRequest).Name;
                string commandValue = JsonSerializer.Serialize(request);
                string accountId = _currentUserService.AccountId.ToString() ?? string.Empty;

                _logger.LogError(ex, "[SampleApi]-[Log Error] : {accountId} - {command} => {requestValue}", accountId, commandName, commandValue);
                throw ex;
            }
        }
    }
}
