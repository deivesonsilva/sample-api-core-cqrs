using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace SampleApiCoreCqrs.WebUi.Helpers
{
    public static class Configure
    {
        public static void RegisterAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            //Add a default AuthorizeFilter to all endpoints
            services.AddRazorPages()
                .AddMvcOptions(options => options.Filters.Add(new AuthorizeFilter()));
        }

        public static void RegisterSwagger(this IServiceCollection services)
        {
            string scheme = "Bearer";

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Sample Api Core CQRS",
                    Version = "v1",
                    Description = ""
                });

                string xmlFile = $"{ System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                options.AddSecurityDefinition(scheme, new OpenApiSecurityScheme
                {
                    Description = "Copy 'Bearer' + token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = scheme
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = scheme
                            },
                            Scheme = "oauth2",
                            Name = scheme,
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });
        }

        public static void RegisterLogging(this IServiceCollection services)
        {
            services.AddLogging(configure =>
            {
                configure.ClearProviders();
                configure.AddConsole();
                configure.AddDebug();
            });
        }

        public static void RegisterFilter(this IServiceCollection services)
        {
            services.AddControllers(config =>
            {
                config.Filters.Add(typeof(ApiExceptionFilter));
            });
        }
    }
}
