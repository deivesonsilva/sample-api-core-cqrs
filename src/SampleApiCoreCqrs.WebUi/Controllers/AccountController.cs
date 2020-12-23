using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleApiCoreCqrs.Application.Commands.AccountCommand.Register;
using SampleApiCoreCqrs.Application.Common.Library;
using SampleApiCoreCqrs.WebUi.Helpers;

namespace SampleApiCoreCqrs.WebUi.Controllers
{
    public class AccountController : ApiController
    {
        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterAccountCommand command)
        {
            var res = await Mediator.Send(command);
            return GetActionResult((Response)res);
        }
    }
}
