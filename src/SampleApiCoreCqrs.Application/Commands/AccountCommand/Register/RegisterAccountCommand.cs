using MediatR;

namespace SampleApiCoreCqrs.Application.Commands.AccountCommand.Register
{
    public class RegisterAccountCommand : IRequest<string>
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Type { get; set; }
    }
}
