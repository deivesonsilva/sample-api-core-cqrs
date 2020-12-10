using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleApiCoreCqrs.Infrastructure.Interfaces;
using SampleApiCoreCqrs.Infrastructure.Repositories;

namespace SampleApiCoreCqrs.Infrastructure
{
    public static class ConfigureInfrastructure
    {
        public static void RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(options =>
                    options.UseMySql(
                        configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IRepositoryUnitWork, RepositoryUnitWork>();
            services.AddTransient<IAccountRepository, AccountRepository>();
        }

        public static void ConfigureUpdateDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<RepositoryContext>())
                {
                    //context.Database.Migrate();
                    context.Database.EnsureCreated();
                }
            }
        }
    }
}
