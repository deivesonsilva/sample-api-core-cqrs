using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleApiCoreCqrs.Application.Commands.AccountCommand.Register;
using SampleApiCoreCqrs.WebUi.Helpers;

namespace SampleApiCoreCqrs.WebUi.Controllers
{
    public class AccountController : ApiController
    {
        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<string> Register(RegisterAccountCommand command)
        {
            var res = await Mediator.Send(command);
            return res;
        }
    }
}
