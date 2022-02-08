using CloneBookingAPI.Interfaces;
using CloneBookingAPI.Services.Generators;
using CloneBookingAPI.Services.Repositories;
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
        public static void AddJwtRepositoryService(this IServiceCollection services)
        {
            //services.AddSingleton<JwtRepository>();
        }
        public static void AddSaltGeneratorService(this IServiceCollection services)
        {
            services.AddSingleton<SaltGenerator>();
        }
    }
}
