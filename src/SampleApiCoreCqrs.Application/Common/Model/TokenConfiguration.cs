namespace SampleApiCoreCqrs.Application.Common.Model
{
    public class TokenConfiguration
    {
        public string Key { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Minutes { get; set; }
    }
}
