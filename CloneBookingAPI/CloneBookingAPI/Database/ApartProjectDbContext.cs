using CloneBookingAPI.Database.Configurations;
using CloneBookingAPI.Database.Configurations.Suggestions;
using CloneBookingAPI.Database.Models;
using CloneBookingAPI.Database.Models.Suggestions;
using CloneBookingAPI.Services.Database.Configurations;
using CloneBookingAPI.Services.Database.Configurations.Flights;
using CloneBookingAPI.Services.Database.Configurations.Location;
using CloneBookingAPI.Services.Database.Configurations.Payment;
using CloneBookingAPI.Services.Database.Configurations.Review;
using CloneBookingAPI.Services.Database.Configurations.Services;
using CloneBookingAPI.Services.Database.Configurations.Suggestions;
using CloneBookingAPI.Services.Database.Configurations.UserData;
using CloneBookingAPI.Services.Database.Configurations.UserProfile;
using CloneBookingAPI.Services.Database.Models;
using CloneBookingAPI.Services.Database.Models.Flights;
using CloneBookingAPI.Services.Database.Models.Location;
using CloneBookingAPI.Services.Database.Models.Payment;
using CloneBookingAPI.Services.Database.Models.Review;
using CloneBookingAPI.Services.Database.Models.Services;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using CloneBookingAPI.Services.Database.Models.UserData;
using CloneBookingAPI.Services.Database.Models.UserProfile;
using Microsoft.EntityFrameworkCore;

namespace CloneBookingAPI.Services.Database
{
    public class ApartProjectDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<ContactDetails> ContactDetails { get; set; }
        public DbSet<TempUser> TempUsers { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<StayBooking> StayBookings { get; set; }
        public DbSet<FlightBooking> FlightBookings { get; set; }
        public DbSet<CarRentalBooking> CarRentalBookings { get; set; }
        public DbSet<AttractionBooking> AttractionBookings { get; set; }
        public DbSet<AirportTaxiBooking> AirportTaxiBookings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<BookingPrice> BookingPrices { get; set; }
        public DbSet<PromoCode> PromoCodes { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewCategory> ReviewCategories { get; set; }
        public DbSet<ReviewMessage> ReviewMessages { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<FileModel> Files { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<FacilityType> FacilityTypes { get; set; }
        public DbSet<BookingCategory> BookingCategories { get; set; }
        public DbSet<InterestPlace> InterestPlaces { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<SurroundingObject> SurroundingObjects { get; set; }
        public DbSet<SurroundingObjectType> SurroundingObjectTypes { get; set; }
        public DbSet<SuggestionRule> SuggestionRules { get; set; }
        public DbSet<SuggestionRuleType> SuggestionRuleTypes { get; set; }
        public DbSet<SuggestionReviewGrade> SuggestionReviewGrades { get; set; }
        public DbSet<SuggestionHighlight> SuggestionHighlights { get; set; }
        public DbSet<FlightClassType> FlightClassTypes { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<CardType> CardTypes { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Bed> Beds { get; set; }
        public DbSet<BedType> BedTypes { get; set; }
        public ApartProjectDbContext(DbContextOptions<ApartProjectDbContext> options) : base(options)
        {
            //// if database already exists then delete it
            //if (Database.CanConnect())
            //{
            //    Database.EnsureDeleted();
            //}

            //// create database
            //Database.EnsureCreated();
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
            modelBuilder.ApplyConfiguration(new CreditCardsConfiguration());
            modelBuilder.ApplyConfiguration(new CurrenciesConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentTypesConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentsConfiguration());
            modelBuilder.ApplyConfiguration(new PromoCodesConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewCategoriesConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewsConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewMessagesConfiguration());
            modelBuilder.ApplyConfiguration(new FavoritesConfiguration());
            modelBuilder.ApplyConfiguration(new GendersConfiguration());
            modelBuilder.ApplyConfiguration(new RolesConfiguration());
            modelBuilder.ApplyConfiguration(new UserProfilesConfiguration());
            modelBuilder.ApplyConfiguration(new UsersConfiguration());
            modelBuilder.ApplyConfiguration(new AreaInfosConfiguration());
            modelBuilder.ApplyConfiguration(new AreaInfoTypesConfiguration());
            modelBuilder.ApplyConfiguration(new StayBookingsConfiguration());
            modelBuilder.ApplyConfiguration(new FlightBookingsConfiguration());
            modelBuilder.ApplyConfiguration(new CarRentalBookingsConfiguration());
            modelBuilder.ApplyConfiguration(new AttractionBookingsConfiguration());
            modelBuilder.ApplyConfiguration(new AirportTaxiBookingsConfiguration());
            modelBuilder.ApplyConfiguration(new BookingCategoriesConfiguration());
            modelBuilder.ApplyConfiguration(new FacilitiesConfiguration());
            modelBuilder.ApplyConfiguration(new FacilityTypesConfiguration());
            modelBuilder.ApplyConfiguration(new LanguagesConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationsConfiguration());
            modelBuilder.ApplyConfiguration(new ServicesCategoriesConfiguration());
            modelBuilder.ApplyConfiguration(new FlightBookingsConfiguration());
            modelBuilder.ApplyConfiguration(new FileModelConfiguration());
            modelBuilder.ApplyConfiguration(new CartsConfiguration());
            modelBuilder.ApplyConfiguration(new TempUsersConfiguration());
            modelBuilder.ApplyConfiguration(new RoomsConfiguration());
            modelBuilder.ApplyConfiguration(new RoomTypesConfiguration());
            modelBuilder.ApplyConfiguration(new SuggestionsConfiguration());
            modelBuilder.ApplyConfiguration(new SuggestionHighlightsConfiguration());
            modelBuilder.ApplyConfiguration(new SuggestionReviewGradesConfiguration());
            modelBuilder.ApplyConfiguration(new SuggestionRulesConfiguration());
            modelBuilder.ApplyConfiguration(new SuggestionRuleTypesConfiguration());
            modelBuilder.ApplyConfiguration(new SurroundingObjectsConfiguration());
            modelBuilder.ApplyConfiguration(new SurroundingObjectTypesConfiguration());
            modelBuilder.ApplyConfiguration(new BookingPricesConfiguration());
            modelBuilder.ApplyConfiguration(new InterestPlacesConfiguration());
            modelBuilder.ApplyConfiguration(new BedsConfiguration());
            modelBuilder.ApplyConfiguration(new BedTypesConfiguration());
            modelBuilder.ApplyConfiguration(new ContactDetailsConfiguration());
        }
    }
}