using CloneBookingAPI.Interfaces;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using CloneBookingAPI.Services.Providers;
using System;

namespace CloneBookingAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
                    .AddJsonFile("emailsendersettings.json")
                    .AddConfiguration(configuration);

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(provider => Configuration);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                                .AddJwtBearer(options =>
                                {
                                    options.RequireHttpsMetadata = false;
                                    options.TokenValidationParameters = new TokenValidationParameters
                                    {
                                        // укзывает, будет ли валидироваться издатель при валидации токена
                                        ValidateIssuer = true,
                                        // строка, представляющая издателя
                                        ValidIssuer = AuthOptions.ISSUER,

                                        // будет ли валидироваться потребитель токена
                                        ValidateAudience = true,
                                        // установка потребителя токена
                                        ValidAudience = AuthOptions.AUDIENCE,
                                        // будет ли валидироваться время существования
                                        ValidateLifetime = true,

                                        // установка ключа безопасности
                                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                                        // валидация ключа безопасности
                                        ValidateIssuerSigningKey = true,                                       
                                    };
                                });

            // получаем строку подключения из файла конфигурации
            string connection = Configuration.GetConnectionString("DefaultConnection");
            // добавляем контекст Apart в качестве сервиса в приложение
            services.AddDbContext<ApartProjectDbContext>(options =>
                options.UseSqlServer(connection));

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddCors();
            // services.AddSwaggerGen(c =>
            // {
            //     c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApartProjectAPI", Version = "v1" });
            // });

            services.AddJwtRepositoryService();
            services.AddCodesRepositoryService();
            services.AddCodeGeneratorService();
            services.AddSaltGeneratorService();
            services.AddSuggestionIdGeneratorService();
            services.AddFileUploaderService();
            services.AddPromocodeGeneratorService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // app.UseSwagger();
                // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CloneBookingAPI v1"));
            }

            app.UseCors(builder =>
                builder
                    //.AllowCredentials()
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            );

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always
            });

            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
