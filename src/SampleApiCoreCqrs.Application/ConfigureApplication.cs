using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleApiCoreCqrs.Application.Behaviours;
using SampleApiCoreCqrs.Application.Common.Model;

namespace SampleApiCoreCqrs.Application
{
    public static class ConfigureApplication
    {
        public static void RegisterApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TokenConfiguration>(x => configuration.GetSection("TokenConfiguration").Bind(x));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        }
    }
}
