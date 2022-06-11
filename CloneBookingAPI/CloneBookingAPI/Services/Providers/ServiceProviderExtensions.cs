using CloneBookingAPI.Controllers.Search.Filtering;
using CloneBookingAPI.Controllers.Search.Pagination;
using CloneBookingAPI.Controllers.Search.Sorting;
using CloneBookingAPI.Interfaces;
using CloneBookingAPI.Services.Cleaners;
using CloneBookingAPI.Services.Files;
using CloneBookingAPI.Services.Generators;
using CloneBookingAPI.Services.Interfaces;
using CloneBookingAPI.Services.PromoCodes;
using CloneBookingAPI.Services.Repositories;
using CloneBookingAPI.Services.Timers;
using Microsoft.Extensions.DependencyInjection;

namespace CloneBookingAPI.Services.Providers
{
    public static class ServiceProviderExtensions
    {
        public static void AddCodeGeneratorService(this IServiceCollection services)
        {
            services.AddSingleton<IGenerator, CodeGenerator>();
        }
        public static void AddRegistrationCodesRepositoryService(this IServiceCollection services)
        {
            services.AddSingleton<RegistrationCodesRepository>();
        }
        public static void AddJwtRepositoryService(this IServiceCollection services)
        {
            services.AddSingleton<JwtRepository>();
        }
        public static void AddSaltGeneratorService(this IServiceCollection services)
        {
            services.AddSingleton<SaltGenerator>();
        }
        public static void AddSuggestionIdGeneratorService(this IServiceCollection services)
        {
            services.AddScoped<SuggestionIdGenerator>();
        }
        public static void AddFileUploaderService(this IServiceCollection services)
        {
            services.AddTransient<FileUploader>();
        }
        public static void AddPromocodeGeneratorService(this IServiceCollection services)
        {
            services.AddTransient<PromoCodeGenerator>();
        }
        public static void AddFilterService(this IServiceCollection services)
        {
            services.AddTransient<SuggestionsFilter>();
        }
        public static void AddSortingService(this IServiceCollection services)
        {
            services.AddTransient<SuggestionsSorter>();
        }
        public static void AddPaginatorService(this IServiceCollection services)
        {
            services.AddTransient<SuggestionsPaginator>();
        }
        public static void AddResetPasswordCodesRepositoryService(this IServiceCollection services)
        {
            services.AddSingleton<ResetPasswordCodesRepository>();
        }
        public static void AddChangingEmailCodesRepositoryService(this IServiceCollection services)
        {
            services.AddSingleton<ChangingEmailCodesRepository>();
        }
        public static void AddEnterCodesRepositoryService(this IServiceCollection services)
        {
            services.AddSingleton<EnterCodesRepository>();
        }
        public static void AddDeleteUserCodesRepositoryService(this IServiceCollection services)
        {
            services.AddSingleton<DeleteUserCodesRepository>();
        }
        public static void AddSubscriptionCodesRepositoryService(this IServiceCollection services)
        {
            services.AddSingleton<SubscriptionCodesRepository>();
        }
        public static void AddChangingEmailCodeCleanerService(this IServiceCollection services)
        {
            services.AddTransient<ChangingEmailCodeCleaner>();
        }
        public static void AddDeleteUserCodeCleanerService(this IServiceCollection services)
        {
            services.AddTransient<DeleteUserCodeCleaner>();
        }
        public static void AddEnterCodeCleanerService(this IServiceCollection services)
        {
            services.AddTransient<EnterCodeCleaner>();
        }
        public static void AddSubscriptionCodeCleanerService(this IServiceCollection services)
        {
            services.AddTransient<SubscriptionCodeCleaner>();
        }
        public static void AddJwtCodeCleanerService(this IServiceCollection services)
        {
            services.AddSingleton<JwtCodeCleaner>();
        }
        public static void AddRegistrationCodeCleanerService(this IServiceCollection services)
        {
            services.AddTransient<RegistrationCodeCleaner>();
        }
        public static void AddPromoCodesApplierService(this IServiceCollection services)
        {
            services.AddTransient<PromoCodesApplier>();
        }
    }
}
