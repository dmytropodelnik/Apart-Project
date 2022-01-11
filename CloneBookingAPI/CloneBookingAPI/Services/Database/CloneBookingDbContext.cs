using Microsoft.EntityFrameworkCore;

namespace CloneBookingAPI.Services.Database
{
    public class CloneBookingDbContext : DbContext
    {
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
            //modelBuilder.ApplyConfiguration(new UsersConfiguration());
            //modelBuilder.ApplyConfiguration(new UserProfilesConfiguration());
            //modelBuilder.ApplyConfiguration(new RecordsConfiguration());
        }
    }
}
