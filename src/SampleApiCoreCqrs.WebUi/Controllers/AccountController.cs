using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleApiCoreCqrs.Application.Commands.AccountCommand.ChangePassword;
using SampleApiCoreCqrs.Application.Commands.AccountCommand.Register;
using SampleApiCoreCqrs.Application.Commands.AccountCommand.ResetPassword;
using SampleApiCoreCqrs.Application.Commands.AccountCommand.Signin;
using SampleApiCoreCqrs.Application.Commands.AccountCommand.VerifyCode;
using SampleApiCoreCqrs.Application.Common.Library;
using SampleApiCoreCqrs.WebUi.Helpers;

namespace SampleApiCoreCqrs.WebUi.Controllers
{
    public class AccountController : ApiController
    {
        /// <summary>
        /// Used to create a new account for user and admin. Send a verify code to the email
        /// </summary>
        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterAccountCommand command)
        {
            var res = await Mediator.Send(command);
            return GetActionResult((Response)res);
        }

        /// <summary>
        /// Used to signin, return a new token
        /// </summary>
        [HttpPost]
        [Route("signin")]
        [AllowAnonymous]
        public async Task<ActionResult<SigninAccountDto>> Signin(SigninAccountCommand command)
        {
            var res = await Mediator.Send(command);
            return GetActionResult((Response<SigninAccountDto>)res);
        }

        /// <summary>
        /// Used to verify code when you use "register"
        /// </summary>
        [HttpPost]
        [Route("verifycode")]
        [AllowAnonymous]
        public async Task<ActionResult<VerifyCodeAccountDto>> VerifyCode(VerifyCodeAccountCommand command)
        {
            var res = await Mediator.Send(command);
            return GetActionResult((Response<VerifyCodeAccountDto>)res);
        }

        /// <summary>
        /// Used to recorver the password. Send a temporary password to the email
        /// </summary>
        [HttpPost]
        [Route("resetpassword")]
        [AllowAnonymous]
        public async Task<ActionResult<SigninAccountDto>> ResetPassword(ResetPasswordAccountCommand command)
        {
            var res = await Mediator.Send(command);
            return GetActionResult((Response)res);
        }

        /// <summary>
        /// Used to create the new password when you use "resetpassword"
        /// </summary>
        [HttpPost]
        [Route("changepassword")]
        [AllowAnonymous]
        public async Task<ActionResult<ChangePasswordAccountDto>> ChangePassword(ChangePasswordAccountCommand command)
        {
            var res = await Mediator.Send(command);
            return GetActionResult((Response<ChangePasswordAccountDto>)res);
        }
    }
}
