using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace SampleApiCoreCqrs.WebUi.Helpers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class ApiController : ControllerBase
    {
        private IMediator _mediator = null;

        protected IMediator Mediator => _mediator ?? HttpContext.RequestServices.GetService<IMediator>();
    }
}
