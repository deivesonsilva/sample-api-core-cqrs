using System;
namespace SampleApiCoreCqrs.Infrastructure.Entities
{
    public class Account : Entity
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsConfirmed { get; set; }
        public string VerifyCode { get; set; }
        public int Type { get; set; }
        public string ResetPassword { get; set; }
        public Guid? AccountMasterId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastSignin { get; set; }
    }
}
