using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace CloneBookingAPI.Services.Database
{
    public class ApartProjectContextFactory : IDesignTimeDbContextFactory<ApartProjectDbContext>
    {
        public ApartProjectDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApartProjectDbContext>();

            // get configuration from file appsettings.json
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build();

            // get connection string from file appsettings.json
            string connectionString = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString, opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(1).TotalSeconds));
            return new ApartProjectDbContext(optionsBuilder.Options);
        }
    }
}
