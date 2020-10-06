using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;

namespace Api.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection ApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));

            services.AddHttpClient<IStackOverflowService, StackOverflowService>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri(config["StackOverflowBaseUri"]);
                    c.DefaultRequestHeaders.Add("Accept", "application/json");
                })
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                });

            var assembliesToScan = new[] {
                Assembly.GetExecutingAssembly(),
                Assembly.Load("Core"),
                Assembly.Load("Infrastructure")
            };

            services.RegisterAssemblyPublicNonGenericClasses(assembliesToScan)
                .Where(x => x.Name.EndsWith("Service"))
                .IgnoreThisInterface<IStackOverflowService>()
                .AsPublicImplementedInterfaces(ServiceLifetime.Transient);

            // services.AddScoped<ISkillTagService, SkillTagService>();

            return services;
        }
    }
}
