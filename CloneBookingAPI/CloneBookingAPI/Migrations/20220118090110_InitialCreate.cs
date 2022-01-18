using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloneBookingAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AreaInfoTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaInfoTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FacilityTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilityTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlightClassTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightClassTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AmountInUserCurrency = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountInUS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TAX = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ResortFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DamageDeposit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CancellationPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PromoCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PercentDiscount = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromoCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReviewMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SuggestionRuleTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuggestionRuleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SurroundingObjectTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurroundingObjectTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AreaInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AreaInfoTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AreaInfos_AreaInfoTypes_AreaInfoTypeId",
                        column: x => x.AreaInfoTypeId,
                        principalTable: "AreaInfoTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreditCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cardholder = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CVC = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    CardTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditCards_CardTypes_CardTypeId",
                        column: x => x.CardTypeId,
                        principalTable: "CardTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Countries_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FavoriteId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    LanguageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Favorites_FavoriteId",
                        column: x => x.FavoriteId,
                        principalTable: "Favorites",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SuggestionRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuggestionRuleTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuggestionRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuggestionRules_SuggestionRuleTypes_SuggestionRuleTypeId",
                        column: x => x.SuggestionRuleTypeId,
                        principalTable: "SuggestionRuleTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentTypeId = table.Column<int>(type: "int", nullable: true),
                    CreditCardId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_CreditCards_CreditCardId",
                        column: x => x.CreditCardId,
                        principalTable: "CreditCards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Payments_PaymentTypes_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Addresses_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EmitterId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_EmitterId",
                        column: x => x.EmitterId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Airports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Airports_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Airports_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Districts_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Districts_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Regions_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Regions_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Suggestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    ServiceCategoryId = table.Column<int>(type: "int", nullable: true),
                    BookingCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suggestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suggestions_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Suggestions_Categories_BookingCategoryId",
                        column: x => x.BookingCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Suggestions_Services_ServiceCategoryId",
                        column: x => x.ServiceCategoryId,
                        principalTable: "Services",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Suggestions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GenderId = table.Column<int>(type: "int", maxLength: 50, nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: true),
                    LanguageId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    ImageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfiles_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserProfiles_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserProfiles_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserProfiles_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserProfiles_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserProfiles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AirportTaxiBookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirportTaxiBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AirportTaxiBookings_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AttractionBookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttractionBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttractionBookings_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CarRentalBookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarRentalBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarRentalBookings_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FlightBookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlightBookings_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Facilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: true),
                    FacilityTypeId = table.Column<int>(type: "int", nullable: true),
                    SuggestionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Facilities_FacilityTypes_FacilityTypeId",
                        column: x => x.FacilityTypeId,
                        principalTable: "FacilityTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Facilities_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Facilities_Suggestions_SuggestionId",
                        column: x => x.SuggestionId,
                        principalTable: "Suggestions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FavoriteSuggestion",
                columns: table => new
                {
                    FavoritesId = table.Column<int>(type: "int", nullable: false),
                    SuggestionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteSuggestion", x => new { x.FavoritesId, x.SuggestionsId });
                    table.ForeignKey(
                        name: "FK_FavoriteSuggestion_Favorites_FavoritesId",
                        column: x => x.FavoritesId,
                        principalTable: "Favorites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteSuggestion_Suggestions_SuggestionsId",
                        column: x => x.SuggestionsId,
                        principalTable: "Suggestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageSuggestion",
                columns: table => new
                {
                    ImagesId = table.Column<int>(type: "int", nullable: false),
                    SuggestionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageSuggestion", x => new { x.ImagesId, x.SuggestionsId });
                    table.ForeignKey(
                        name: "FK_ImageSuggestion_Images_ImagesId",
                        column: x => x.ImagesId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImageSuggestion_Suggestions_SuggestionsId",
                        column: x => x.SuggestionsId,
                        principalTable: "Suggestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReviewCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    SuggestionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewCategories_Suggestions_SuggestionId",
                        column: x => x.SuggestionId,
                        principalTable: "Suggestions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoomTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomsLeft = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SuggestionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomTypes_Suggestions_SuggestionId",
                        column: x => x.SuggestionId,
                        principalTable: "Suggestions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StayBookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsForWork = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsRequestedAirportShuttle = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsRequestedRentingCar = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SpecialRequests = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PaymentId = table.Column<int>(type: "int", nullable: true),
                    PriceId = table.Column<int>(type: "int", nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    CheckIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PromoCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    UniqueNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ServiceCategoryId = table.Column<int>(type: "int", nullable: true),
                    BookingCategoryId = table.Column<int>(type: "int", nullable: true),
                    SuggestionId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CartId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StayBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StayBookings_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StayBookings_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StayBookings_Categories_BookingCategoryId",
                        column: x => x.BookingCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StayBookings_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StayBookings_Prices_PriceId",
                        column: x => x.PriceId,
                        principalTable: "Prices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StayBookings_Services_ServiceCategoryId",
                        column: x => x.ServiceCategoryId,
                        principalTable: "Services",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StayBookings_Suggestions_SuggestionId",
                        column: x => x.SuggestionId,
                        principalTable: "Suggestions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StayBookings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SuggestionSuggestionRule",
                columns: table => new
                {
                    SuggestionRulesId = table.Column<int>(type: "int", nullable: false),
                    SuggestionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuggestionSuggestionRule", x => new { x.SuggestionRulesId, x.SuggestionsId });
                    table.ForeignKey(
                        name: "FK_SuggestionSuggestionRule_SuggestionRules_SuggestionRulesId",
                        column: x => x.SuggestionRulesId,
                        principalTable: "SuggestionRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SuggestionSuggestionRule_Suggestions_SuggestionsId",
                        column: x => x.SuggestionsId,
                        principalTable: "Suggestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SurroundingObjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurroundingObjectTypeId = table.Column<int>(type: "int", nullable: true),
                    SuggestionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurroundingObjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurroundingObjects_Suggestions_SuggestionId",
                        column: x => x.SuggestionId,
                        principalTable: "Suggestions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SurroundingObjects_SurroundingObjectTypes_SurroundingObjectTypeId",
                        column: x => x.SurroundingObjectTypeId,
                        principalTable: "SurroundingObjectTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SuggestionReviewGrades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ReviewCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuggestionReviewGrades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuggestionReviewGrades_ReviewCategories_ReviewCategoryId",
                        column: x => x.ReviewCategoryId,
                        principalTable: "ReviewCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    SuggestionId = table.Column<int>(type: "int", nullable: true),
                    BookingId = table.Column<int>(type: "int", nullable: true),
                    ReviewedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MessageId = table.Column<int>(type: "int", nullable: true),
                    LikesAmount = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    DislikesAmount = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_ReviewMessages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "ReviewMessages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_StayBookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "StayBookings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_Suggestions_SuggestionId",
                        column: x => x.SuggestionId,
                        principalTable: "Suggestions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sleeps = table.Column<int>(type: "int", nullable: false),
                    RoomSize = table.Column<int>(type: "int", nullable: false),
                    IsSmokingAllowed = table.Column<bool>(type: "bit", nullable: false),
                    IsSuite = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    RoomTypeId = table.Column<int>(type: "int", nullable: true),
                    AmountInUserCurrency = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountInUS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StayBookingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_RoomTypes_RoomTypeId",
                        column: x => x.RoomTypeId,
                        principalTable: "RoomTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rooms_StayBookings_StayBookingId",
                        column: x => x.StayBookingId,
                        principalTable: "StayBookings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StayBookingTempUser",
                columns: table => new
                {
                    BookingsId = table.Column<int>(type: "int", nullable: false),
                    GuestsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StayBookingTempUser", x => new { x.BookingsId, x.GuestsId });
                    table.ForeignKey(
                        name: "FK_StayBookingTempUser_StayBookings_BookingsId",
                        column: x => x.BookingsId,
                        principalTable: "StayBookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StayBookingTempUser_TempUsers_GuestsId",
                        column: x => x.GuestsId,
                        principalTable: "TempUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SuggestionSuggestionReviewGrade",
                columns: table => new
                {
                    SuggestionReviewGradesId = table.Column<int>(type: "int", nullable: false),
                    SuggestionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuggestionSuggestionReviewGrade", x => new { x.SuggestionReviewGradesId, x.SuggestionsId });
                    table.ForeignKey(
                        name: "FK_SuggestionSuggestionReviewGrade_SuggestionReviewGrades_SuggestionReviewGradesId",
                        column: x => x.SuggestionReviewGradesId,
                        principalTable: "SuggestionReviewGrades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SuggestionSuggestionReviewGrade_Suggestions_SuggestionsId",
                        column: x => x.SuggestionsId,
                        principalTable: "Suggestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SuggestionHighlights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: true),
                    SuggestionId = table.Column<int>(type: "int", nullable: true),
                    RoomId = table.Column<int>(type: "int", nullable: true),
                    RoomId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuggestionHighlights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuggestionHighlights_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SuggestionHighlights_Rooms_RoomId1",
                        column: x => x.RoomId1,
                        principalTable: "Rooms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SuggestionHighlights_RoomTypes_RoomId",
                        column: x => x.RoomId,
                        principalTable: "RoomTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SuggestionHighlights_Suggestions_SuggestionId",
                        column: x => x.SuggestionId,
                        principalTable: "Suggestions",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Category" },
                values: new object[,]
                {
                    { 1, "Hotels" },
                    { 2, "Places of interest" },
                    { 3, "Homes" },
                    { 4, "Apartments" },
                    { 5, "Resorts" },
                    { 6, "Villas" },
                    { 7, "Hostels" },
                    { 8, "B&Bs" },
                    { 9, "Guest houses" },
                    { 10, "Unique places to stay" }
                });

            migrationBuilder.InsertData(
                table: "ReviewCategories",
                columns: new[] { "Id", "Category", "SuggestionId" },
                values: new object[,]
                {
                    { 1, "Staff", null },
                    { 2, "Facilities", null },
                    { 3, "Cleanliness", null },
                    { 4, "Comfort", null },
                    { 5, "Value for money", null },
                    { 6, "Location", null },
                    { 7, "Free WiFi", null },
                    { 8, "Pets allowed", null },
                    { 9, "Air conditioning", null },
                    { 10, "Private bathroom", null },
                    { 11, "City view", null },
                    { 12, "Private bathroom", null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "admin" },
                    { 2, "user" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Category" },
                values: new object[,]
                {
                    { 1, "Stays" },
                    { 2, "Flights" },
                    { 3, "Car rentals" },
                    { 4, "Attractions" },
                    { 5, "Airport taxis" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CityId",
                table: "Addresses",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CountryId",
                table: "Addresses",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Airports_AddressId",
                table: "Airports",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Airports_ImageId",
                table: "Airports",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_AirportTaxiBookings_CartId",
                table: "AirportTaxiBookings",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_AreaInfos_AreaInfoTypeId",
                table: "AreaInfos",
                column: "AreaInfoTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AttractionBookings_CartId",
                table: "AttractionBookings",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CarRentalBookings_CartId",
                table: "CarRentalBookings",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_ImageId",
                table: "Cities",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_ImageId",
                table: "Countries",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_CardTypeId",
                table: "CreditCards",
                column: "CardTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_AddressId",
                table: "Districts",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_ImageId",
                table: "Districts",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_FacilityTypeId",
                table: "Facilities",
                column: "FacilityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_ImageId",
                table: "Facilities",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_SuggestionId",
                table: "Facilities",
                column: "SuggestionId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteSuggestion_SuggestionsId",
                table: "FavoriteSuggestion",
                column: "SuggestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightBookings_CartId",
                table: "FlightBookings",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageSuggestion_SuggestionsId",
                table: "ImageSuggestion",
                column: "SuggestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_EmitterId",
                table: "Notifications",
                column: "EmitterId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CreditCardId",
                table: "Payments",
                column: "CreditCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentTypeId",
                table: "Payments",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_AddressId",
                table: "Regions",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_ImageId",
                table: "Regions",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewCategories_SuggestionId",
                table: "ReviewCategories",
                column: "SuggestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BookingId",
                table: "Reviews",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_MessageId",
                table: "Reviews",
                column: "MessageId",
                unique: true,
                filter: "[MessageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_SuggestionId",
                table: "Reviews",
                column: "SuggestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomTypeId",
                table: "Rooms",
                column: "RoomTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_StayBookingId",
                table: "Rooms",
                column: "StayBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTypes_SuggestionId",
                table: "RoomTypes",
                column: "SuggestionId");

            migrationBuilder.CreateIndex(
                name: "IX_StayBookings_AddressId",
                table: "StayBookings",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_StayBookings_BookingCategoryId",
                table: "StayBookings",
                column: "BookingCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StayBookings_CartId",
                table: "StayBookings",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_StayBookings_PaymentId",
                table: "StayBookings",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_StayBookings_PriceId",
                table: "StayBookings",
                column: "PriceId",
                unique: true,
                filter: "[PriceId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StayBookings_ServiceCategoryId",
                table: "StayBookings",
                column: "ServiceCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StayBookings_SuggestionId",
                table: "StayBookings",
                column: "SuggestionId");

            migrationBuilder.CreateIndex(
                name: "IX_StayBookings_UserId",
                table: "StayBookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StayBookingTempUser_GuestsId",
                table: "StayBookingTempUser",
                column: "GuestsId");

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionHighlights_ImageId",
                table: "SuggestionHighlights",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionHighlights_RoomId",
                table: "SuggestionHighlights",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionHighlights_RoomId1",
                table: "SuggestionHighlights",
                column: "RoomId1");

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionHighlights_SuggestionId",
                table: "SuggestionHighlights",
                column: "SuggestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionReviewGrades_ReviewCategoryId",
                table: "SuggestionReviewGrades",
                column: "ReviewCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionRules_SuggestionRuleTypeId",
                table: "SuggestionRules",
                column: "SuggestionRuleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Suggestions_AddressId",
                table: "Suggestions",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Suggestions_BookingCategoryId",
                table: "Suggestions",
                column: "BookingCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Suggestions_ServiceCategoryId",
                table: "Suggestions",
                column: "ServiceCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Suggestions_UserId",
                table: "Suggestions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionSuggestionReviewGrade_SuggestionsId",
                table: "SuggestionSuggestionReviewGrade",
                column: "SuggestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionSuggestionRule_SuggestionsId",
                table: "SuggestionSuggestionRule",
                column: "SuggestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_SurroundingObjects_SuggestionId",
                table: "SurroundingObjects",
                column: "SuggestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SurroundingObjects_SurroundingObjectTypeId",
                table: "SurroundingObjects",
                column: "SurroundingObjectTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_AddressId",
                table: "UserProfiles",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_CurrencyId",
                table: "UserProfiles",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_GenderId",
                table: "UserProfiles",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_ImageId",
                table: "UserProfiles",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_LanguageId",
                table: "UserProfiles",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UserId",
                table: "UserProfiles",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_FavoriteId",
                table: "Users",
                column: "FavoriteId",
                unique: true,
                filter: "[FavoriteId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_LanguageId",
                table: "Users",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Airports");

            migrationBuilder.DropTable(
                name: "AirportTaxiBookings");

            migrationBuilder.DropTable(
                name: "AreaInfos");

            migrationBuilder.DropTable(
                name: "AttractionBookings");

            migrationBuilder.DropTable(
                name: "CarRentalBookings");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Facilities");

            migrationBuilder.DropTable(
                name: "FavoriteSuggestion");

            migrationBuilder.DropTable(
                name: "FlightBookings");

            migrationBuilder.DropTable(
                name: "FlightClassTypes");

            migrationBuilder.DropTable(
                name: "ImageSuggestion");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "PromoCodes");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "StayBookingTempUser");

            migrationBuilder.DropTable(
                name: "SuggestionHighlights");

            migrationBuilder.DropTable(
                name: "SuggestionSuggestionReviewGrade");

            migrationBuilder.DropTable(
                name: "SuggestionSuggestionRule");

            migrationBuilder.DropTable(
                name: "SurroundingObjects");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "AreaInfoTypes");

            migrationBuilder.DropTable(
                name: "FacilityTypes");

            migrationBuilder.DropTable(
                name: "ReviewMessages");

            migrationBuilder.DropTable(
                name: "TempUsers");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "SuggestionReviewGrades");

            migrationBuilder.DropTable(
                name: "SuggestionRules");

            migrationBuilder.DropTable(
                name: "SurroundingObjectTypes");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "RoomTypes");

            migrationBuilder.DropTable(
                name: "StayBookings");

            migrationBuilder.DropTable(
                name: "ReviewCategories");

            migrationBuilder.DropTable(
                name: "SuggestionRuleTypes");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropTable(
                name: "Suggestions");

            migrationBuilder.DropTable(
                name: "CreditCards");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "CardTypes");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Images");
        }
    }
}
