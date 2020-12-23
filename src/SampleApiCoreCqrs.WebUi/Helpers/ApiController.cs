using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SampleApiCoreCqrs.Application.Common.Enums;
using SampleApiCoreCqrs.Application.Common.Interfaces;

namespace SampleApiCoreCqrs.WebUi.Helpers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class ApiController : ControllerBase
    {
        private IMediator _mediator = null;

        protected IMediator Mediator => _mediator ?? HttpContext.RequestServices.GetService<IMediator>();

        protected ActionResult GetActionResult(IResponse response)
        {
            if (response.Type == ResponseType.Bad)
                return BadRequest(response.Error);

            if (response.Type == ResponseType.OkEmpty)
                return Ok();

            return BadRequest("ActionResult not implemented");
        }

        protected ActionResult<TValue> GetActionResult<TValue>(IResponse<TValue> response)
        {
            if (response.Type == ResponseType.OkObject)
                return Ok(response.Value);

            if (response.Type == ResponseType.Bad)
                return BadRequest(response.Error);

            return BadRequest("ActionResult not implemented");
        }
    }
}
