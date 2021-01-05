using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleApiCoreCqrs.Application.Behaviours;
using SampleApiCoreCqrs.Application.Common.Interfaces;
using SampleApiCoreCqrs.Application.Common.Model;
using SampleApiCoreCqrs.Application.Common.Services;

namespace SampleApiCoreCqrs.Application
{
    public static class ConfigureApplication
    {
        public static void RegisterApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TokenConfiguration>(x => configuration.GetSection("TokenConfiguration").Bind(x));
            services.Configure<EmailConfiguration>(x => configuration.GetSection("EmailConfiguration").Bind(x));
            services.AddTransient<IAccountMailService, AccountMailService>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}