﻿using CloneBookingAPI.Services.Database.Configurations;
using CloneBookingAPI.Services.Database.Configurations.Flights;
using CloneBookingAPI.Services.Database.Configurations.Location;
using CloneBookingAPI.Services.Database.Configurations.Payment;
using CloneBookingAPI.Services.Database.Configurations.Review;
using CloneBookingAPI.Services.Database.Configurations.UserProfile;
using CloneBookingAPI.Services.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace CloneBookingAPI.Services.Database
{
    public class CloneBookingDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public CloneBookingDbContext(DbContextOptions<CloneBookingDbContext> options) : base(options)
        {
            // If database already exists then delete it
            ConnectToDatabase();
        }

        /// <summary>
        ///     Try to connect to database. If fail then create new database.
        /// </summary>
        private void ConnectToDatabase()
        {
            if (Database.CanConnect())
            {
                Database.EnsureDeleted();
            }

            // Create database
            Database.EnsureCreated();
        }

        /// <summary>
        ///     Output in debug queries which are sent by EF in Output window (Menu Visual Studio: View -> Output).
        /// </summary>
        /// <param name="optionsBuilder">Builder options.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Method DbContextOptionsBuilder.LogTo was added only from EF Core 5.0.
            optionsBuilder.LogTo(s => System.Diagnostics.Debug.WriteLine(s));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FlightClassTypesConfiguration());
            modelBuilder.ApplyConfiguration(new AddressesConfiguration());
            modelBuilder.ApplyConfiguration(new AirportsConfiguration());
            modelBuilder.ApplyConfiguration(new CitiesConfiguration());
            modelBuilder.ApplyConfiguration(new CountriesConfiguration());
            modelBuilder.ApplyConfiguration(new DistrictsConfiguration());
            modelBuilder.ApplyConfiguration(new RegionsConfiguration());
            modelBuilder.ApplyConfiguration(new CardTypesConfiguration());
            modelBuilder.ApplyConfiguration(new CurrenciesConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentTypesConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewCategoriesConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewsConfiguration());
            modelBuilder.ApplyConfiguration(new FavoritesConfiguration());
            modelBuilder.ApplyConfiguration(new GendersConfiguration());
            modelBuilder.ApplyConfiguration(new RolesConfiguration());
            modelBuilder.ApplyConfiguration(new UserProfilesConfiguration());
            modelBuilder.ApplyConfiguration(new UsersConfiguration());
            modelBuilder.ApplyConfiguration(new AreaInfosConfiguration());
            modelBuilder.ApplyConfiguration(new AreaInfoTypesConfiguration());
            modelBuilder.ApplyConfiguration(new BookingsConfiguration());
            modelBuilder.ApplyConfiguration(new CategoriesConfiguration());
            modelBuilder.ApplyConfiguration(new FacilitiesConfiguration());
            modelBuilder.ApplyConfiguration(new FacilityTypesConfiguration());
            modelBuilder.ApplyConfiguration(new LanguagesConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationsConfiguration());
            modelBuilder.ApplyConfiguration(new ServicesConfiguration());
            modelBuilder.ApplyConfiguration(new TripsConfiguration());
        }
    }
}
