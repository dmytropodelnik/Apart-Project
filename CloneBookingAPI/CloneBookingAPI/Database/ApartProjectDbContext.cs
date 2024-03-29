﻿using CloneBookingAPI.Database.Configurations;
using CloneBookingAPI.Database.Configurations.Review;
using CloneBookingAPI.Database.Configurations.Services;
using CloneBookingAPI.Database.Configurations.Suggestions;
using CloneBookingAPI.Database.Configurations.UserData;
using CloneBookingAPI.Database.Configurations.ViewModels;
using CloneBookingAPI.Database.Models;
using CloneBookingAPI.Database.Models.Review;
using CloneBookingAPI.Database.Models.Services;
using CloneBookingAPI.Database.Models.Suggestions;
using CloneBookingAPI.Database.Models.UserData;
using CloneBookingAPI.Database.Models.ViewModels;
using CloneBookingAPI.Services.Database.Configurations;
using CloneBookingAPI.Services.Database.Configurations.Location;
using CloneBookingAPI.Services.Database.Configurations.Payment;
using CloneBookingAPI.Services.Database.Configurations.Review;
using CloneBookingAPI.Services.Database.Configurations.Suggestions;
using CloneBookingAPI.Services.Database.Configurations.UserData;
using CloneBookingAPI.Services.Database.Configurations.UserProfile;
using CloneBookingAPI.Services.Database.Models;
using CloneBookingAPI.Services.Database.Models.Location;
using CloneBookingAPI.Services.Database.Models.Payment;
using CloneBookingAPI.Services.Database.Models.Review;
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
        public DbSet<Guest> Guests { get; set; }
        public DbSet<StayBooking> StayBookings { get; set; }
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
        public DbSet<Suggestion> Suggestions { get; set; }
        public DbSet<CardType> CardTypes { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<BookedPeriod> BookedDates { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<SuggestionStatus> SuggestionStatuses { get; set; }
        public DbSet<BookingCategoryType> BookingCategoryTypes { get; set; }
        public DbSet<MailLetter> MailLetters { get; set; }
        public DbSet<LettersReceiver> LettersReceivers { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<BookingStatus> BookingStatuses { get; set; }
        public DbSet<CustomerInfo> CustomerInfos { get; set; }

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

        /// <summary>s
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
            modelBuilder.Entity<Suggestion>()
                .HasMany(s => s.Images)
                .WithMany(i => i.Suggestions)
                .UsingEntity<SuggestionFileModel>(
                    j => j
                        .HasOne(pt => pt.Image)
                        .WithMany(t => t.SuggestionsFileModels)
                        .HasForeignKey(pt => pt.ImageId),
                    j => j
                        .HasOne(pt => pt.Suggestion)
                        .WithMany(p => p.SuggestionsFileModels)
                        .HasForeignKey(pt => pt.SuggestionId),
                    j =>
                    {
                        j.HasKey(t => new { t.SuggestionId, t.ImageId });
                    });

            modelBuilder.Entity<Apartment>()
                .HasMany(a => a.BookedPeriods)
                .WithMany(p => p.Apartments)
                .UsingEntity<ApartmentBookedPeriod>(
                    j => j
                        .HasOne(bp => bp.BookedPeriod)
                        .WithMany(a => a.ApartmentsBookedPeriods)
                        .HasForeignKey(pt => pt.BookedPeriodId),
                    j => j
                        .HasOne(pt => pt.Apartment)
                        .WithMany(p => p.ApartmentsBookedPeriods)
                        .HasForeignKey(pt => pt.ApartmentId),
                    j =>
                    {
                        j.HasKey(t => new { t.ApartmentId, t.BookedPeriodId });
                    });

            modelBuilder.Entity<Suggestion>()
                .HasMany(s => s.Facilities)
                .WithMany(f => f.Suggestions)
                .UsingEntity<SuggestionFacility>(
                    j => j
                        .HasOne(sf => sf.Facility)
                        .WithMany(s => s.SuggestionsFacilities)
                        .HasForeignKey(pt => pt.FacilityId),
                    j => j
                        .HasOne(pt => pt.Suggestion)
                        .WithMany(f => f.SuggestionsFacilities)
                        .HasForeignKey(pt => pt.SuggestionId),
                    j =>
                    {
                        j.HasKey(t => new { t.SuggestionId, t.FacilityId });
                    });

            modelBuilder.Entity<Suggestion>()
                .HasMany(s => s.SuggestionRules)
                .WithMany(f => f.Suggestions)
                .UsingEntity<SuggestionSuggestionRule>(
                    j => j
                        .HasOne(sf => sf.SuggestionRule)
                        .WithMany(s => s.SuggestionsSuggestionRules)
                        .HasForeignKey(pt => pt.SuggestionRuleId),
                    j => j
                        .HasOne(pt => pt.Suggestion)
                        .WithMany(f => f.SuggestionsSuggestionRules)
                        .HasForeignKey(pt => pt.SuggestionId),
                    j =>
                    {
                        j.HasKey(t => new { t.SuggestionId, t.SuggestionRuleId });
                    });

            modelBuilder.Entity<Apartment>()
                .HasMany(s => s.Facilities)
                .WithMany(f => f.Apartments)
                .UsingEntity<ApartmentFacility>(
                    j => j
                        .HasOne(sf => sf.Facility)
                        .WithMany(s => s.ApartmentsFacilities)
                        .HasForeignKey(pt => pt.FacilityId),
                    j => j
                        .HasOne(pt => pt.Apartment)
                        .WithMany(f => f.ApartmentsFacilities)
                        .HasForeignKey(pt => pt.ApartmentId),
                    j =>
                    {
                        j.HasKey(t => new { t.ApartmentId, t.FacilityId });
                    });

            modelBuilder.Entity<StayBooking>()
                .HasMany(b => b.Guests)
                .WithMany(g => g.StayBookings)
                .UsingEntity<StayBookingGuest>(
                    j => j
                        .HasOne(b => b.Guest)
                        .WithMany(g => g.StayBookingsGuests)
                        .HasForeignKey(bg => bg.GuestId),
                    j => j
                        .HasOne(g => g.StayBooking)
                        .WithMany(b => b.StayBookingsGuests)
                        .HasForeignKey(bg => bg.StayBookingId),
                    j =>
                    {
                        j.HasKey(t => new { t.StayBookingId, t.GuestId });
                    });

            modelBuilder.Entity<StayBooking>()
                .HasMany(b => b.Apartments)
                .WithMany(g => g.StayBookings)
                .UsingEntity<StayBookingApartment>(
                    j => j
                        .HasOne(b => b.Apartment)
                        .WithMany(g => g.StayBookingsApartments)
                        .HasForeignKey(bg => bg.ApartmentId),
                    j => j
                        .HasOne(g => g.StayBooking)
                        .WithMany(b => b.StayBookingsApartments)
                        .HasForeignKey(bg => bg.StayBookingId),
                    j =>
                    {
                        j.HasKey(t => new { t.StayBookingId, t.ApartmentId });
                    });

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
            modelBuilder.ApplyConfiguration(new BookingCategoriesConfiguration());
            modelBuilder.ApplyConfiguration(new FacilitiesConfiguration());
            modelBuilder.ApplyConfiguration(new FacilityTypesConfiguration());
            modelBuilder.ApplyConfiguration(new LanguagesConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationsConfiguration());
            modelBuilder.ApplyConfiguration(new ServicesCategoriesConfiguration());
            modelBuilder.ApplyConfiguration(new FileModelConfiguration());
            modelBuilder.ApplyConfiguration(new GuestsConfiguration());
            modelBuilder.ApplyConfiguration(new SuggestionsConfiguration());
            modelBuilder.ApplyConfiguration(new SuggestionHighlightsConfiguration());
            modelBuilder.ApplyConfiguration(new SuggestionReviewGradesConfiguration());
            modelBuilder.ApplyConfiguration(new SuggestionRulesConfiguration());
            modelBuilder.ApplyConfiguration(new SuggestionRuleTypesConfiguration());
            modelBuilder.ApplyConfiguration(new SurroundingObjectsConfiguration());
            modelBuilder.ApplyConfiguration(new SurroundingObjectTypesConfiguration());
            modelBuilder.ApplyConfiguration(new BookingPricesConfiguration());
            modelBuilder.ApplyConfiguration(new InterestPlacesConfiguration());
            modelBuilder.ApplyConfiguration(new ContactDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new BookedPeriodsConfiguration());
            modelBuilder.ApplyConfiguration(new ApartmentsConfiguration());
            modelBuilder.ApplyConfiguration(new ApartmentsBookedPeriodsConfiguration());
            modelBuilder.ApplyConfiguration(new SuggestionsFileModelsConfiguration());
            modelBuilder.ApplyConfiguration(new BookingCategoryTypesConfiguration());
            modelBuilder.ApplyConfiguration(new SuggestionStatusesConfiguration());
            modelBuilder.ApplyConfiguration(new MailLettersConfiguration());
            modelBuilder.ApplyConfiguration(new SuggestionsFacilitiesConfiguration());
            modelBuilder.ApplyConfiguration(new SuggestionsSuggestionRulesConfiguration());
            modelBuilder.ApplyConfiguration(new ApartmentsFacilitiesConfiguration());
            modelBuilder.ApplyConfiguration(new BookingStatusesConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerInfosConfiguration());
            modelBuilder.ApplyConfiguration(new StayBookingsGuestsConfiguration());
            modelBuilder.ApplyConfiguration(new StayBookingsApartmentsConfiguration());
        }
    }
}