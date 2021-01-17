using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OmdbApi.Configurations;


namespace OmdbApi.Extensions
{
    public static class ConfigurationExtension
    {
        public static IServiceCollection RegisterConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailConfiguration>(configuration.GetSection("EmailConfiguration"));

            return services;
        }
    }
}
