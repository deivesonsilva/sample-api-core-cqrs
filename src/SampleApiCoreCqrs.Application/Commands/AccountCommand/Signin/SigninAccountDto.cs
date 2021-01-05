using System;
namespace SampleApiCoreCqrs.Application.Commands.AccountCommand.Signin
{
    public class SigninAccountDto
    {
        public DateTime? DateExpiration { get; set; }
        public string Token { get; set; }
        public int? Type { get; set; }
        public bool IsReset { get; set; }
        public bool IsVerifyCode { get; set; }
    }
}
