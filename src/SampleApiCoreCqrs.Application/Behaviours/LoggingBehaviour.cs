using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using SampleApiCoreCqrs.Application.Common.Interfaces;

namespace SampleApiCoreCqrs.Application.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserService _currentUserService;

        public LoggingBehaviour(
            ILogger<TRequest> logger,
            ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            string commandName = typeof(TRequest).Name;
            string accountId = _currentUserService.AccountId.ToString() ?? string.Empty;
            string commandValue = JsonSerializer.Serialize(request);

            _logger.LogInformation("[SampleApi]-[Log Request] : {accountId} - {command} => {requestValue}",
                accountId, commandName, commandValue);
        }
    }
}
