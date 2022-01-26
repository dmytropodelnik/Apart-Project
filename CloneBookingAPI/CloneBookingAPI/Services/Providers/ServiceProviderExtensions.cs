using CloneBookingAPI.Interfaces;
using CloneBookingAPI.Services.Generators;
using Microsoft.Extensions.DependencyInjection;

namespace CloneBookingAPI.Services.Providers
{
    public static class ServiceProviderExtensions
    {
        public static void AddCodeGeneratorService(this IServiceCollection services)
        {
            services.AddSingleton<IGenerator, CodeGenerator>();
        }

        public static void AddCodesRepositoryService(this IServiceCollection services)
        {
            services.AddSingleton<CodesRepository>();
        }
    }
}
