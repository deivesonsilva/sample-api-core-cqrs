using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using SampleApiCoreCqrs.Application.Common.Model.ResponseModel;

namespace SampleApiCoreCqrs.WebUi.Helpers
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ApiExceptionFilter> _logger;

        public ApiExceptionFilter(
            ILogger<ApiExceptionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            HandleUnknownException(context);
            return base.OnExceptionAsync(context);
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            var result = new ResponseError
            {
                Message = context.Exception.Message,
                Value = context.Exception
            };

            context.Result = new ObjectResult(result)
            {
                StatusCode = StatusCodes.Status400BadRequest
            };

            _logger.LogError(context.Exception, "[SampleApi]-[Log Error]: ");
            context.ExceptionHandled = true;
        }
    }
}