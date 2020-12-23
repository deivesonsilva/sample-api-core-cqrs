namespace SampleApiCoreCqrs.Application.Common.Model
{
    public class EmailConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
}
