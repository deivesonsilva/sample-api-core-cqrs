using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleApiCoreCqrs.Application.Common.Model;

namespace SampleApiCoreCqrs.Application
{
    public static class ConfigureApplication
    {
        public static void RegisterApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TokenConfiguration>(x => configuration.GetSection("TokenConfiguration").Bind(x));



            
        }
    }
}
