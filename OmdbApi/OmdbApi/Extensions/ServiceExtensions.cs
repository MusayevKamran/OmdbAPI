using Microsoft.Extensions.DependencyInjection;
using OmdbApi.Interfaces;
using OmdbApi.Services;


namespace OmdbApi.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {

            services.AddScoped<IOmdbApi, OmdbApiService>();
            services.AddScoped<IEmail, EmailService>();

            return services;
        }
    }
}
