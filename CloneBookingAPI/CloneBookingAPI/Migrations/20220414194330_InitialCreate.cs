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
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaInfoTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BedTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BedTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookedDates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOut = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookedDates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookingPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AmountInUserCurrency = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountInUSD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TAX = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ResortFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DamageDeposit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CancellationPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingPrices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    BankCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlightClassTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PromoCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PercentDiscount = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    GeneratingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCategories", x => x.Id);
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
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
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
                name: "Beds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    BedTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beds_BedTypes_BedTypeId",
                        column: x => x.BedTypeId,
                        principalTable: "BedTypes",
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
                    CardNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CVC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditCards_CardTypes_CardTypeId",
                        column: x => x.CardTypeId,
                        principalTable: "CardTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingCategories_Files_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_Countries_Files_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Files",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Districts_Files_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Files",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FacilityTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilityTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacilityTypes_Files_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Files",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SaltHash = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SuggestionRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SuggestionRuleTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuggestionRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuggestionRules_SuggestionRuleTypes_SuggestionRuleTypeId",
                        column: x => x.SuggestionRuleTypeId,
                        principalTable: "SuggestionRuleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentTypeId = table.Column<int>(type: "int", nullable: false),
                    CreditCardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_CreditCards_CreditCardId",
                        column: x => x.CreditCardId,
                        principalTable: "CreditCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_PaymentTypes_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    ImageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cities_Files_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Files",
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
                    FacilityTypeId = table.Column<int>(type: "int", nullable: true)
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
                        name: "FK_Facilities_Files_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Files",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorites_Users_UserId",
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
                    ImageId = table.Column<int>(type: "int", nullable: true),
                    EmitterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Files_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Files",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notifications_Users_EmitterId",
                        column: x => x.EmitterId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    ImageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Regions_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Regions_Files_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Files",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AirportTaxiBookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirportTaxiBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AirportTaxiBookings_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AirportTaxiBookings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AttractionBookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttractionBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttractionBookings_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AttractionBookings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CarRentalBookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarRentalBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarRentalBookings_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CarRentalBookings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FlightBookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlightBookings_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FlightBookings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    DistrictId = table.Column<int>(type: "int", nullable: true),
                    RegionId = table.Column<int>(type: "int", nullable: true),
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Addresses_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Addresses_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Addresses_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
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
                        name: "FK_Airports_Files_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Files",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Suggestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UniqueCode = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: ""),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StarsRating = table.Column<int>(type: "int", nullable: false),
                    Progress = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsParkingAvailable = table.Column<bool>(type: "bit", nullable: false),
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
                        name: "FK_Suggestions_BookingCategories_BookingCategoryId",
                        column: x => x.BookingCategoryId,
                        principalTable: "BookingCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Suggestions_ServiceCategories_ServiceCategoryId",
                        column: x => x.ServiceCategoryId,
                        principalTable: "ServiceCategories",
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
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 4, 14, 22, 43, 28, 815, DateTimeKind.Local).AddTicks(2477)),
                    Nationality = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GenderId = table.Column<int>(type: "int", nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: true),
                    LanguageId = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
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
                        name: "FK_UserProfiles_Files_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Files",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserProfiles_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
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
                name: "Apartments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PriceInUserCurrency = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceInUSD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RoomsAmount = table.Column<int>(type: "int", nullable: false),
                    GuestsLimit = table.Column<int>(type: "int", nullable: false),
                    BathroomsAmount = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SuggestionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Apartments_Suggestions_SuggestionId",
                        column: x => x.SuggestionId,
                        principalTable: "Suggestions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BedSuggestion",
                columns: table => new
                {
                    BedsId = table.Column<int>(type: "int", nullable: false),
                    SuggestionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BedSuggestion", x => new { x.BedsId, x.SuggestionsId });
                    table.ForeignKey(
                        name: "FK_BedSuggestion_Beds_BedsId",
                        column: x => x.BedsId,
                        principalTable: "Beds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BedSuggestion_Suggestions_SuggestionsId",
                        column: x => x.SuggestionsId,
                        principalTable: "Suggestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SuggestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactDetails_Suggestions_SuggestionId",
                        column: x => x.SuggestionId,
                        principalTable: "Suggestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FacilitySuggestion",
                columns: table => new
                {
                    FacilitiesId = table.Column<int>(type: "int", nullable: false),
                    SuggestionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilitySuggestion", x => new { x.FacilitiesId, x.SuggestionsId });
                    table.ForeignKey(
                        name: "FK_FacilitySuggestion_Facilities_FacilitiesId",
                        column: x => x.FacilitiesId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacilitySuggestion_Suggestions_SuggestionsId",
                        column: x => x.SuggestionsId,
                        principalTable: "Suggestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "InterestPlaces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Place = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    SuggestionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterestPlaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InterestPlaces_Suggestions_SuggestionId",
                        column: x => x.SuggestionId,
                        principalTable: "Suggestions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LanguageSuggestion",
                columns: table => new
                {
                    LanguagesId = table.Column<int>(type: "int", nullable: false),
                    SuggestionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageSuggestion", x => new { x.LanguagesId, x.SuggestionsId });
                    table.ForeignKey(
                        name: "FK_LanguageSuggestion_Languages_LanguagesId",
                        column: x => x.LanguagesId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LanguageSuggestion_Suggestions_SuggestionsId",
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
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grade = table.Column<double>(type: "float", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    SuggestionId = table.Column<int>(type: "int", nullable: false),
                    ReviewedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReviewMessageId = table.Column<int>(type: "int", nullable: false),
                    LikesAmount = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    DislikesAmount = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_ReviewMessages_ReviewMessageId",
                        column: x => x.ReviewMessageId,
                        principalTable: "ReviewMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Suggestions_SuggestionId",
                        column: x => x.SuggestionId,
                        principalTable: "Suggestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
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
                    IsFinished = table.Column<bool>(type: "bit", nullable: false),
                    SpecialRequests = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PaymentId = table.Column<int>(type: "int", nullable: false),
                    PriceId = table.Column<int>(type: "int", nullable: false),
                    CheckIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PromoCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    UniqueNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SuggestionId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    BookingCategoryId = table.Column<int>(type: "int", nullable: true),
                    CartId = table.Column<int>(type: "int", nullable: true),
                    ServiceCategoryId = table.Column<int>(type: "int", nullable: true)
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
                        name: "FK_StayBookings_BookingCategories_BookingCategoryId",
                        column: x => x.BookingCategoryId,
                        principalTable: "BookingCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StayBookings_BookingPrices_PriceId",
                        column: x => x.PriceId,
                        principalTable: "BookingPrices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StayBookings_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StayBookings_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StayBookings_ServiceCategories_ServiceCategoryId",
                        column: x => x.ServiceCategoryId,
                        principalTable: "ServiceCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StayBookings_Suggestions_SuggestionId",
                        column: x => x.SuggestionId,
                        principalTable: "Suggestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StayBookings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SuggestionsFileModels",
                columns: table => new
                {
                    SuggestionId = table.Column<int>(type: "int", nullable: false),
                    ImageId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuggestionsFileModels", x => new { x.SuggestionId, x.ImageId });
                    table.ForeignKey(
                        name: "FK_SuggestionsFileModels_Files_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SuggestionsFileModels_Suggestions_SuggestionId",
                        column: x => x.SuggestionId,
                        principalTable: "Suggestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    SurroundingObjectTypeId = table.Column<int>(type: "int", nullable: false),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApartmentsBookedPeriods",
                columns: table => new
                {
                    ApartmentId = table.Column<int>(type: "int", nullable: false),
                    BookedPeriodId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApartmentsBookedPeriods", x => new { x.ApartmentId, x.BookedPeriodId });
                    table.ForeignKey(
                        name: "FK_ApartmentsBookedPeriods_Apartments_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApartmentsBookedPeriods_BookedDates_BookedPeriodId",
                        column: x => x.BookedPeriodId,
                        principalTable: "BookedDates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApartmentsRoomTypes",
                columns: table => new
                {
                    ApartmentId = table.Column<int>(type: "int", nullable: false),
                    RoomTypeId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApartmentsRoomTypes", x => new { x.ApartmentId, x.RoomTypeId });
                    table.ForeignKey(
                        name: "FK_ApartmentsRoomTypes_Apartments_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApartmentsRoomTypes_RoomTypes_RoomTypeId",
                        column: x => x.RoomTypeId,
                        principalTable: "RoomTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SuggestionReviewGrades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<double>(type: "float", maxLength: 10, nullable: false),
                    ReviewCategoryId = table.Column<int>(type: "int", nullable: false),
                    SuggestionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuggestionReviewGrades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuggestionReviewGrades_ReviewCategories_ReviewCategoryId",
                        column: x => x.ReviewCategoryId,
                        principalTable: "ReviewCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SuggestionReviewGrades_Suggestions_SuggestionId",
                        column: x => x.SuggestionId,
                        principalTable: "Suggestions",
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
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    RoomTypeId = table.Column<int>(type: "int", nullable: false),
                    PriceInUserCurrency = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceInUSD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StayBookingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_RoomTypes_RoomTypeId",
                        column: x => x.RoomTypeId,
                        principalTable: "RoomTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    GuestsId = table.Column<int>(type: "int", nullable: false),
                    StayBookingsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StayBookingTempUser", x => new { x.GuestsId, x.StayBookingsId });
                    table.ForeignKey(
                        name: "FK_StayBookingTempUser_StayBookings_StayBookingsId",
                        column: x => x.StayBookingsId,
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
                name: "SuggestionHighlights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    FileId = table.Column<int>(type: "int", nullable: true),
                    SuggestionId = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: true),
                    RoomTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuggestionHighlights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuggestionHighlights_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SuggestionHighlights_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SuggestionHighlights_RoomTypes_RoomTypeId",
                        column: x => x.RoomTypeId,
                        principalTable: "RoomTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SuggestionHighlights_Suggestions_SuggestionId",
                        column: x => x.SuggestionId,
                        principalTable: "Suggestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BedTypes",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Twin beds" },
                    { 2, "Queen bed" },
                    { 3, "King bed" },
                    { 4, "Single bed" },
                    { 5, "Double bed" },
                    { 6, "Large bed" },
                    { 7, "Extra-large double bed" },
                    { 8, "Bunk bed" },
                    { 9, "Sofa bed" }
                });

            migrationBuilder.InsertData(
                table: "BookedDates",
                columns: new[] { "Id", "DateIn", "DateOut" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2022, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2022, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2022, 4, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2022, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, new DateTime(2022, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 5, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, new DateTime(2022, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, new DateTime(2022, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, new DateTime(2022, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, new DateTime(2022, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, new DateTime(2022, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, new DateTime(2022, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, new DateTime(2022, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, new DateTime(2022, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, new DateTime(2022, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, new DateTime(2022, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, new DateTime(2022, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 11, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, new DateTime(2022, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19, new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 20, new DateTime(2022, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 21, new DateTime(2023, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 22, new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 23, new DateTime(2023, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 24, new DateTime(2023, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 25, new DateTime(2023, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 26, new DateTime(2023, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 27, new DateTime(2023, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 28, new DateTime(2023, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 29, new DateTime(2023, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 30, new DateTime(2023, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Abbreviation", "BankCode", "Value" },
                values: new object[,]
                {
                    { 1, "USD", "$", "USA Dollar" },
                    { 2, "EUR", "€", "Euro" },
                    { 3, "RUB", "₽", "Рубль" }
                });

            migrationBuilder.InsertData(
                table: "FacilityTypes",
                columns: new[] { "Id", "ImageId", "Type" },
                values: new object[,]
                {
                    { 1, null, "Most popular facilities" },
                    { 2, null, "Outdoors" },
                    { 3, null, "Entertainment & Family Services" },
                    { 4, null, "Outdoor swimming pool" },
                    { 5, null, "Activities" },
                    { 6, null, "Cleaning Services" },
                    { 7, null, "Spa" },
                    { 8, null, "Business Facilities" },
                    { 9, null, "Safety & security" },
                    { 10, null, "Food & Drink" },
                    { 11, null, "Languages Spoken" },
                    { 12, null, "Parking" },
                    { 13, null, "Internet" },
                    { 14, null, "Front Desk Services" },
                    { 15, null, "General" }
                });

            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "Id", "Name", "Path" },
                values: new object[,]
                {
                    { 1, "YSJu1Nj3YEKYuc+MC4TSlRWPvtybYtnfFT6dHXPt5k=.png", "/files/YSJu1Nj3YEKYuc+MC4TSlRWPvtybYtnfFT6dHXPt5k=.png" },
                    { 2, "N6TTPt7raEBYeOcLAFMjL1mWb5ip+Kt9YXHddBJ5+A=.png", "/files/N6TTPt7raEBYeOcLAFMjL1mWb5ip+Kt9YXHddBJ5+A=.png" },
                    { 3, "AyFnP96S870Z5dpsREvobcCqkk9WxRqZaqrlwDD+aU=.png", "/files/AyFnP96S870Z5dpsREvobcCqkk9WxRqZaqrlwDD+aU=.png" },
                    { 4, "PMPvpvziKwjaiBm+NJOnumepEhpdkEpmwZX+y8uGxM=.png", "/files/PMPvpvziKwjaiBm+NJOnumepEhpdkEpmwZX+y8uGxM=.png" },
                    { 5, "JonaGiXS6Fy5vFAhFXV6yi4gF0QAjlGfAKt1tLfNH4=.png", "/files/JonaGiXS6Fy5vFAhFXV6yi4gF0QAjlGfAKt1tLfNH4=.png" },
                    { 6, "hunLamNewdFFiIJuD2jTaHhnD62OU2EwqFp4lao+qNY=.png", "/files/hunLamNewdFFiIJuD2jTaHhnD62OU2EwqFp4lao+qNY=.png" },
                    { 7, "U4h32cGZhMmt59xHBhyIkx+2qJ6x2cs+23U9c94ugcc=.png", "/files/U4h32cGZhMmt59xHBhyIkx+2qJ6x2cs+23U9c94ugcc=.png" },
                    { 8, "89NTq1CzGc3AnN8+m2RNyIzkfP33nOm96VYeeoJmbBQ=.png", "/files/89NTq1CzGc3AnN8+m2RNyIzkfP33nOm96VYeeoJmbBQ=.png" },
                    { 9, "H8v3dNgasrnuIthKRth17pFIb+3bfi7j09YDODmk4l4=.png", "/files/H8v3dNgasrnuIthKRth17pFIb+3bfi7j09YDODmk4l4=.png" },
                    { 10, "pBWV7R2w3omzP+Kv5PxT627LiqV3evixcVyN4B9NovI=.png", "/files/pBWV7R2w3omzP+Kv5PxT627LiqV3evixcVyN4B9NovI=.png" },
                    { 11, "9FXre4iyTJ6PR2kItwfneUxvLFxi8bN402K2VcsZ4dE=.png", "/files/9FXre4iyTJ6PR2kItwfneUxvLFxi8bN402K2VcsZ4dE=.png" },
                    { 12, "1rpHDSXaRvlmmbFewuByZ5l02dD0lY99Nfm5TYu9Vdo=.png", "/files/1rpHDSXaRvlmmbFewuByZ5l02dD0lY99Nfm5TYu9Vdo=.png" },
                    { 13, "+IAx1JqqRxZudz0ReeJAGz76F5LKfzo5paBh7HCSBc=.png", "/files/+IAx1JqqRxZudz0ReeJAGz76F5LKfzo5paBh7HCSBc=.png" },
                    { 14, "FnsrvbI5NPfBBa+wcpa85l1orRZIdwppeOkm1EA5RiA=.png", "/files/FnsrvbI5NPfBBa+wcpa85l1orRZIdwppeOkm1EA5RiA=.png" },
                    { 15, "MwDbR+apJTYsCV9tUP4WPs8W0EqEiqG5m5wrca5SZsw=.png", "/files/MwDbR+apJTYsCV9tUP4WPs8W0EqEiqG5m5wrca5SZsw=.png" },
                    { 16, "1cDGXpJthPlZ+7Jk7++tTOYNDO0NdvQp6vtYFcpEeE=.png", "/files/1cDGXpJthPlZ+7Jk7++tTOYNDO0NdvQp6vtYFcpEeE=.png" },
                    { 17, "8jPLR62nAIKQNMYx1TmBvtK57mq6PB6RnTVrtAm5SZg=.webp", "/files/booking-categories/8jPLR62nAIKQNMYx1TmBvtK57mq6PB6RnTVrtAm5SZg=.webp" },
                    { 18, "PWKTeFbPU3hKtS0NS88R+ZkYbZpEHZcRTUxo21bdWQ=.jpg", "/files/booking-categories/PWKTeFbPU3hKtS0NS88R+ZkYbZpEHZcRTUxo21bdWQ=.jpg" },
                    { 19, "WvBwu6I7aVKY4M1+UrxtVQ4FBqZoWjiuvwRg393+wc0=.jpg", "/files/booking-categories/WvBwu6I7aVKY4M1+UrxtVQ4FBqZoWjiuvwRg393+wc0=.jpg" },
                    { 20, "DpIc5BjZEwT+N6kuEHdQ67EE7gWBrjZBnTlywfWAlo=.jpg", "/files/booking-categories/DpIc5BjZEwT+N6kuEHdQ67EE7gWBrjZBnTlywfWAlo=.jpg" },
                    { 21, "ccbGiIK5RdTaNH3FKaLHCFXp9vvNDxNGGefGtA6s0a0=.jpg", "/files/booking-categories/ccbGiIK5RdTaNH3FKaLHCFXp9vvNDxNGGefGtA6s0a0=.jpg" },
                    { 22, "SVGx5iqYWcxNoJZGFNAFc9+ZtYXSTWWGdUQdg3DQD2g=.jpg", "/files/booking-categories/SVGx5iqYWcxNoJZGFNAFc9+ZtYXSTWWGdUQdg3DQD2g=.jpg" },
                    { 23, "h3DP1vCkB2kLPb2jyy6YpkMi3mJM6IYW1Y07gXNndLI=.jpg", "/files/booking-categories/h3DP1vCkB2kLPb2jyy6YpkMi3mJM6IYW1Y07gXNndLI=.jpg" },
                    { 24, "WFBGRejuSFOJ9hY0jm5BSEIX7Zd6vPfGkoQsP3C8A=.jpg", "/files/booking-categories/WFBGRejuSFOJ9hY0jm5BSEIX7Zd6vPfGkoQsP3C8A=.jpg" },
                    { 25, "AWKGQV1ZympDIFSlVlvgX8m2XN8bU8hqPoHuqp6aSsk=.jpg", "/files/booking-categories/AWKGQV1ZympDIFSlVlvgX8m2XN8bU8hqPoHuqp6aSsk=.jpg" },
                    { 26, "nL4slBus5cwN7eAjhpHr6LHedLqV0WpgwZ64Xt7NsbE=.jpg", "/files/booking-categories/nL4slBus5cwN7eAjhpHr6LHedLqV0WpgwZ64Xt7NsbE=.jpg" },
                    { 27, "jFNYJ0G0n69IMehflOLhqJN6L7mvtyvEhWYosFbCD0o=.jpg", "/files/booking-categories/jFNYJ0G0n69IMehflOLhqJN6L7mvtyvEhWYosFbCD0o=.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "Id", "Name", "Path" },
                values: new object[,]
                {
                    { 28, "PIKzYCNQ6eJX21q3tEUYIOspOHy7TBAhZjc4yCgXb0=.jpg", "/files/booking-categories/PIKzYCNQ6eJX21q3tEUYIOspOHy7TBAhZjc4yCgXb0=.jpg" },
                    { 29, "wSjvY0V4thEVQdc2yc2HuHn3geZZuoJUUiKDp94TmI=.jpg", "/files/booking-categories/wSjvY0V4thEVQdc2yc2HuHn3geZZuoJUUiKDp94TmI=.jpg" },
                    { 30, "aCULIjuxi3WuMDOpb4N2nzB2OlXNhu7qtPetG34cWNw=.jpg", "/files/booking-categories/aCULIjuxi3WuMDOpb4N2nzB2OlXNhu7qtPetG34cWNw=.jpg" },
                    { 31, "cameLbbE7MwXMEWSpWQb5xelqkRtnd89WSHdQa748=.jpg", "/files/booking-categories/cameLbbE7MwXMEWSpWQb5xelqkRtnd89WSHdQa748=.jpg" },
                    { 32, "s+zPgNsDGpzOmiGpErxaD25x38t08YakaWMh4oy+xko=.jpg", "/files/booking-categories/s+zPgNsDGpzOmiGpErxaD25x38t08YakaWMh4oy+xko=.jpg" },
                    { 33, "vEsdBlDZSonr9G4nrPC+ADdB05nS3WtD1Vdtg2vEdls=.jpg", "/files/booking-categories/vEsdBlDZSonr9G4nrPC+ADdB05nS3WtD1Vdtg2vEdls=.jpg" },
                    { 34, "xlO12jVkOIhTl6V0CAR3uC0clz782hFr60m90ZMoEtc=.jpg", "/files/booking-categories/xlO12jVkOIhTl6V0CAR3uC0clz782hFr60m90ZMoEtc=.jpg" },
                    { 35, "YclkZnp1Xn6wxk1eUtjno9cHcS43eefuqhDzSzSUDFU=.jpg", "/files/booking-categories/YclkZnp1Xn6wxk1eUtjno9cHcS43eefuqhDzSzSUDFU=.jpg" },
                    { 36, "v1uHZOLG2gjVU1d6EcFeWv61i5U6EJFBWzovIbiTWfc=.jpg", "/files/booking-categories/v1uHZOLG2gjVU1d6EcFeWv61i5U6EJFBWzovIbiTWfc=.jpg" },
                    { 37, "28gfiUeA8lD6E5eO4eHc31vvFGarpWk7DoAGwzDd4=.jpg", "/files/booking-categories/28gfiUeA8lD6E5eO4eHc31vvFGarpWk7DoAGwzDd4=.jpg" },
                    { 38, "38ULX8oMuuiBBrWmybcDv9mE02zeXuKGyKwxJ0wbbvc=.jpg", "/files/booking-categories/38ULX8oMuuiBBrWmybcDv9mE02zeXuKGyKwxJ0wbbvc=.jpg" },
                    { 39, "k60eWzKITm0qHXThVzz+VFtcmDwS+Geqw4qpVaVntww=.jpg", "/files/booking-categories/k60eWzKITm0qHXThVzz+VFtcmDwS+Geqw4qpVaVntww=.jpg" },
                    { 40, "UtWw0Kd9VsqxkDAyjdo1kR4pTXgRYtgr8bnIafjm4KA=.jpg", "/files/booking-categories/UtWw0Kd9VsqxkDAyjdo1kR4pTXgRYtgr8bnIafjm4KA=.jpg" },
                    { 41, "2cJ9bLYWaRmntd32hJMfHkGSAKl44mL6skW7xBgxSQ=.jpg", "/files/cities/2cJ9bLYWaRmntd32hJMfHkGSAKl44mL6skW7xBgxSQ=.jpg" },
                    { 42, "r25bYhQuTGlu2Q3XLxx5AjBOymSwaFoGrE3NkSt3g=.jpg", "/files/cities/r25bYhQuTGlu2Q3XLxx5AjBOymSwaFoGrE3NkSt3g=.jpg" },
                    { 43, "AO9WI6rkbYft1QE5Ht7Bea3FacTX8koOpW34qDB1Hxs=.jpg", "/files/cities/AO9WI6rkbYft1QE5Ht7Bea3FacTX8koOpW34qDB1Hxs=.jpg" },
                    { 44, "qf+01PybqRDOrxpESro3QvN7bLPIzPHlXtQzXYaRJQ=.jpg", "/files/cities/qf+01PybqRDOrxpESro3QvN7bLPIzPHlXtQzXYaRJQ=.jpg" },
                    { 45, "ACCQwXjlZ0HyF0ZEK+9JeiwKmHfbRapg2+hCfoR+GQ=.webp", "/files/cities/ACCQwXjlZ0HyF0ZEK+9JeiwKmHfbRapg2+hCfoR+GQ=.webp" },
                    { 46, "zzfyT2YznMbRTX3WMQGcmRvdHWpKFPtZBYakvJyJeFQ=.jpg", "/files/cities/zzfyT2YznMbRTX3WMQGcmRvdHWpKFPtZBYakvJyJeFQ=.jpg" },
                    { 47, "wUR7kBl2k5hbmSez3shxGijGAy7he9XsySVgVmThu38=.jpg", "/files/cities/wUR7kBl2k5hbmSez3shxGijGAy7he9XsySVgVmThu38=.jpg" },
                    { 48, "iGX8wogugyA1tS2KsT5wiPJtOm72QVPKbKbaTkwC54c=.webp", "/files/cities/iGX8wogugyA1tS2KsT5wiPJtOm72QVPKbKbaTkwC54c=.webp" },
                    { 49, "ZMGW71dUcALX3Fmi27UwsbPDRCOdeW7gpVKOPUyRZBs=.jpg", "/files/cities/ZMGW71dUcALX3Fmi27UwsbPDRCOdeW7gpVKOPUyRZBs=.jpg" },
                    { 50, "ja9zowNm1GdRAwBxY+kdzv9o6vP9Bi3PqNaBVl2xuQ=.png", "/files/cities/ja9zowNm1GdRAwBxY+kdzv9o6vP9Bi3PqNaBVl2xuQ=.png" },
                    { 51, "Qhc5DZhf0xTviHaF2EJ7Y5ppUtPDWfw3xiGHP0y8k8c=.jpg", "/files/cities/Qhc5DZhf0xTviHaF2EJ7Y5ppUtPDWfw3xiGHP0y8k8c=.jpg" },
                    { 52, "MnDxHSrTUYmyX8Tsx0qUVRDL33fRQaVaE0+ihCGDoM=.jpg", "/files/cities/MnDxHSrTUYmyX8Tsx0qUVRDL33fRQaVaE0+ihCGDoM=.jpg" },
                    { 53, "Yp6XJTcaYN3pDZncxFOSJSsqdXfX0b4yg6zNr9cgg=.jpg", "/files/cities/Yp6XJTcaYN3pDZncxFOSJSsqdXfX0b4yg6zNr9cgg=.jpg" },
                    { 54, "", "/files/cities/" },
                    { 55, "", "/files/cities/" },
                    { 56, "", "/files/cities/" },
                    { 57, "", "/files/cities/" },
                    { 58, "", "/files/cities/" },
                    { 59, "", "/files/cities/" },
                    { 60, "", "/files/cities/" },
                    { 61, "", "/files/cities/" },
                    { 62, "", "/files/cities/" },
                    { 63, "", "/files/cities/" },
                    { 64, "", "/files/cities/" },
                    { 65, "", "/files/cities/" },
                    { 66, "", "/files/cities/" },
                    { 67, "", "/files/cities/" },
                    { 68, "", "/files/cities/" },
                    { 69, "", "/files/cities/" }
                });

            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "Id", "Name", "Path" },
                values: new object[,]
                {
                    { 70, "", "/files/cities/" },
                    { 71, "", "/files/cities/" },
                    { 72, "", "/files/cities/" },
                    { 73, "", "/files/cities/" },
                    { 74, "", "/files/cities/" },
                    { 75, "", "/files/cities/" },
                    { 76, "", "/files/cities/" },
                    { 77, "", "/files/cities/" },
                    { 78, "", "/files/cities/" },
                    { 79, "", "/files/cities/" },
                    { 80, "", "/files/cities/" },
                    { 81, "", "/files/cities/" },
                    { 82, "", "/files/cities/" },
                    { 83, "", "/files/cities/" },
                    { 84, "", "/files/cities/" },
                    { 85, "", "/files/cities/" },
                    { 86, "", "/files/cities/" },
                    { 87, "", "/files/cities/" },
                    { 88, "", "/files/cities/" }
                });

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "Id", "Gender" },
                values: new object[,]
                {
                    { 1, "Male" },
                    { 2, "Female" }
                });

            migrationBuilder.InsertData(
                table: "InterestPlaces",
                columns: new[] { "Id", "Place", "SuggestionId" },
                values: new object[,]
                {
                    { 1, "Hotels", null },
                    { 2, "Places of interest", null },
                    { 3, "Homes", null },
                    { 4, "Apartments", null },
                    { 5, "Resorts", null },
                    { 6, "Villas", null },
                    { 7, "Hostels", null },
                    { 8, "B&Bs", null },
                    { 9, "Guest houses", null },
                    { 10, "Unique places to stay", null },
                    { 11, "Vacation Homes", null },
                    { 12, "Serviced Apartments", null },
                    { 13, "Glamping", null },
                    { 14, "Cottages", null },
                    { 15, "Cabins", null },
                    { 16, "Motels", null },
                    { 17, "Ryokans", null },
                    { 18, "Riads", null },
                    { 19, "Resort Villages", null },
                    { 20, "Homestays", null },
                    { 21, "Campgrounds", null }
                });

            migrationBuilder.InsertData(
                table: "InterestPlaces",
                columns: new[] { "Id", "Place", "SuggestionId" },
                values: new object[,]
                {
                    { 22, "Country Houses", null },
                    { 23, "Farm Stays", null },
                    { 24, "Boats", null },
                    { 25, "Luxury Tents", null },
                    { 26, "Self-catering Accommodations", null },
                    { 27, "Tiny houses", null }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Language" },
                values: new object[,]
                {
                    { 1, "English" },
                    { 2, "Ukrainian" },
                    { 3, "Russian" },
                    { 4, "German" },
                    { 5, "Polish" },
                    { 6, "Arabic" },
                    { 7, "Italian" },
                    { 8, "Spanish" }
                });

            migrationBuilder.InsertData(
                table: "PromoCodes",
                columns: new[] { "Id", "Code", "ExpirationDate", "GeneratingDate", "IsActive", "PercentDiscount" },
                values: new object[] { 1, "test", new DateTime(2022, 4, 17, 19, 43, 28, 815, DateTimeKind.Utc).AddTicks(635), new DateTime(2022, 4, 15, 19, 43, 28, 815, DateTimeKind.Utc).AddTicks(625), true, 20 });

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
                    { 2, "user" },
                    { 3, "temp user" }
                });

            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Small Double Room" },
                    { 2, "Standard Double Room" },
                    { 3, "Standard Twin Room" },
                    { 4, "Superior Twin Room" },
                    { 5, "Studio" },
                    { 6, "Standard Quadruple Room" },
                    { 7, "Apartment" },
                    { 8, "Double Room with Balcony" },
                    { 9, "Double Room with Extra Bed" },
                    { 10, "Contempo Double Room" },
                    { 11, "Double Room" },
                    { 12, "Deluxe Double Room" }
                });

            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "Id", "Title" },
                values: new object[] { 13, "Classic Suite" });

            migrationBuilder.InsertData(
                table: "ServiceCategories",
                columns: new[] { "Id", "Category" },
                values: new object[,]
                {
                    { 1, "Stays" },
                    { 2, "Flights" },
                    { 3, "Car rentals" },
                    { 4, "Attractions" },
                    { 5, "Airport taxis" }
                });

            migrationBuilder.InsertData(
                table: "SuggestionRuleTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Cancellation/prepayment" },
                    { 2, "Children & Beds" },
                    { 3, "Age restriction" },
                    { 4, "Pets" },
                    { 5, "Groups" },
                    { 6, "Cards accepted at this hotel" }
                });

            migrationBuilder.InsertData(
                table: "SurroundingObjectTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "What's nearby" },
                    { 2, "Beaches in the Neighborhood" },
                    { 3, "Closest Airports" },
                    { 4, "Top attractions" },
                    { 5, "Public transit" },
                    { 6, "Restaurants & cafes" },
                    { 7, "Natural Beauty" }
                });

            migrationBuilder.InsertData(
                table: "Beds",
                columns: new[] { "Id", "Amount", "BedTypeId", "Description" },
                values: new object[,]
                {
                    { 1, 0, 1, "test1" },
                    { 2, 0, 2, "test2" },
                    { 3, 0, 3, "test3" },
                    { 4, 0, 4, "test4" }
                });

            migrationBuilder.InsertData(
                table: "BookingCategories",
                columns: new[] { "Id", "Category", "ImageId" },
                values: new object[,]
                {
                    { 1, "Hotels", 17 },
                    { 2, "Apartments", 18 },
                    { 3, "Resorts", 19 },
                    { 4, "Villas", 20 },
                    { 5, "Hostels", 21 },
                    { 6, "B&Bs", 22 },
                    { 7, "Guest Houses", 23 },
                    { 8, "Vacation Homes", 24 },
                    { 9, "Serviced Apartments", 25 },
                    { 10, "Glamping", 26 },
                    { 11, "Cottages", 27 },
                    { 12, "Cabins", 28 },
                    { 13, "Motels", 29 },
                    { 14, "Ryokans", 30 },
                    { 15, "Riads", 31 },
                    { 16, "Resort Villages", 32 },
                    { 17, "Homestays", 33 },
                    { 18, "Campgrounds", 34 },
                    { 19, "Country Houses", 35 },
                    { 20, "Farm Stays", 36 },
                    { 21, "Boats", 37 },
                    { 22, "Luxury Tents", 38 },
                    { 23, "Selfcatering Accommodation", 39 },
                    { 24, "Tiny Houses", 40 }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "ImageId", "Title" },
                values: new object[,]
                {
                    { 1, 2, "Ukraine" },
                    { 2, 1, "USA" },
                    { 3, 3, "UK" },
                    { 4, 1, "Belarus" },
                    { 5, 1, "Bosnia and Herzegovina" },
                    { 6, 1, "Algeria" },
                    { 7, 1, "Chad" },
                    { 8, 1, "Czech Republic" },
                    { 9, 1, "French Polynesia" },
                    { 10, 5, "Hungary" },
                    { 11, 1, "Gabon" },
                    { 12, 1, "Iraq" },
                    { 13, 1, "Madagascar" },
                    { 14, 1, "Namibia" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "ImageId", "Title" },
                values: new object[,]
                {
                    { 15, 1, "Lithuania" },
                    { 16, 1, "Niue" },
                    { 17, 1, "Nigeria" },
                    { 18, 1, "Oman" },
                    { 19, 1, "Saint Kitts and Nevis" },
                    { 20, 1, "San Marino" },
                    { 21, 1, "Russia" },
                    { 22, 1, "Seychelles" },
                    { 23, 1, "Sri Lanka" },
                    { 24, 1, "Senegal" },
                    { 25, 1, "Trinidad and Tobago" },
                    { 26, 1, "Uruguay" },
                    { 27, 1, "Uzbekistan" },
                    { 28, 1, "Montenegro" },
                    { 29, 1, "Saint Barts" },
                    { 30, 1, "Albania" },
                    { 31, 1, "Andorra" },
                    { 32, 1, "Botswana" },
                    { 33, 1, "Burundi" },
                    { 34, 1, "Denmark" },
                    { 35, 1, "China" },
                    { 36, 1, "Cape Verde" },
                    { 37, 1, "Ecuador" },
                    { 38, 1, "India" },
                    { 39, 1, "Lebanon" },
                    { 40, 1, "Malaysia" },
                    { 41, 1, "Nepal" },
                    { 42, 1, "Liechtenstein" },
                    { 43, 1, "Paraguay" },
                    { 44, 1, "Portugal" },
                    { 45, 1, "Saint Vincent & Grenadines" },
                    { 46, 1, "Saudi Arabia" },
                    { 47, 1, "Tanzania" },
                    { 48, 1, "Tonga" },
                    { 49, 4, "Turkey" },
                    { 50, 1, "Uganda" },
                    { 51, 1, "Venezuela" },
                    { 52, 1, "Argentina" },
                    { 53, 1, "Azerbaijan" },
                    { 54, 1, "Bhutan" },
                    { 55, 1, "Burkina Faso" },
                    { 56, 1, "Ivory Coast" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "ImageId", "Title" },
                values: new object[,]
                {
                    { 57, 1, "Cyprus" },
                    { 58, 1, "Guadeloupe" },
                    { 59, 1, "Indonesia" },
                    { 60, 6, "Italy" },
                    { 61, 1, "Moldova" },
                    { 62, 1, "Myanmar" },
                    { 63, 8, "Norway" },
                    { 64, 7, "Sweden" },
                    { 65, 1, "Togo" },
                    { 66, 1, "Vanuatu" },
                    { 67, 1, "Curaçao" },
                    { 68, 1, "Bahrain" },
                    { 69, 1, "Gambia" },
                    { 70, 1, "Kenya" },
                    { 71, 1, "Laos" },
                    { 72, 9, "Spain" },
                    { 73, 1, "Anguilla" },
                    { 74, 1, "Dominica" },
                    { 75, 1, "Mauritius" },
                    { 76, 1, "Nicaragua" },
                    { 77, 1, "Bolivia" },
                    { 78, 1, "Colombia" },
                    { 79, 1, "Croatia" },
                    { 80, 10, "Greece" },
                    { 81, 1, "Martinique" },
                    { 82, 1, "Philippines" },
                    { 83, 1, "United Kingdom" },
                    { 84, 1, "St. Maarten" },
                    { 85, 1, "Belize" },
                    { 86, 1, "Chile" },
                    { 87, 1, "Ethiopia" },
                    { 88, 1, "Greenland" },
                    { 89, 1, "Morocco" },
                    { 90, 1, "Samoa" },
                    { 91, 1, "Cambodia" },
                    { 92, 1, "Cook Islands" },
                    { 93, 1, "Egypt" },
                    { 94, 1, "Ghana" },
                    { 95, 1, "Guyana" },
                    { 96, 1, "Netherlands" },
                    { 97, 1, "Romania" },
                    { 98, 1, "Malta" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "ImageId", "Title" },
                values: new object[,]
                {
                    { 99, 1, "Ireland" },
                    { 100, 1, "Solomon Islands" },
                    { 101, 1, "Taiwan" },
                    { 102, 1, "Saint Martin" },
                    { 103, 1, "Barbados" },
                    { 104, 1, "Belgium" },
                    { 105, 1, "Jamaica" },
                    { 106, 1, "Maldives" },
                    { 107, 1, "Switzerland" },
                    { 108, 1, "Fiji" },
                    { 109, 1, "South Africa" },
                    { 110, 1, "Turks & Caicos Islands" },
                    { 111, 1, "Bermuda" },
                    { 112, 1, "El Salvador" },
                    { 113, 11, "Israel" },
                    { 114, 12, "Germany" },
                    { 115, 1, "Monaco" },
                    { 116, 1, "Qatar" },
                    { 117, 1, "Thailand" },
                    { 118, 1, "Dominican Republic" },
                    { 119, 1, "Cayman Islands" },
                    { 120, 1, "Saint Lucia" },
                    { 121, 1, "Panama" },
                    { 122, 1, "Vietnam" },
                    { 123, 13, "France" },
                    { 124, 1, "South Korea" },
                    { 125, 1, "Iceland" },
                    { 126, 1, "New Zealand" },
                    { 127, 1, "Costa Rica" },
                    { 128, 1, "Faroe Islands" },
                    { 129, 15, "Canada" },
                    { 130, 14, "Australia" },
                    { 131, 1, "Guam" },
                    { 132, 1, "U.S. Virgin Islands" },
                    { 133, 1, "Bahamas" },
                    { 134, 1, "Haiti" },
                    { 135, 16, "Japan" },
                    { 136, 1, "Aruba" },
                    { 137, 1, "Puerto Rico" }
                });

            migrationBuilder.InsertData(
                table: "Facilities",
                columns: new[] { "Id", "FacilityTypeId", "ImageId", "Text" },
                values: new object[,]
                {
                    { 1, 2, 1, "Beachfront" },
                    { 2, 2, 1, "Private beach area" },
                    { 3, 2, 1, "Terrace" }
                });

            migrationBuilder.InsertData(
                table: "Facilities",
                columns: new[] { "Id", "FacilityTypeId", "ImageId", "Text" },
                values: new object[,]
                {
                    { 4, 2, 1, "Garden" },
                    { 5, 3, 1, "Babysitting/Child services" },
                    { 6, 4, 1, "Open all year" },
                    { 7, 4, 1, "All ages welcome" },
                    { 8, 5, 1, "Beach" },
                    { 9, 5, 1, "Kids' club" },
                    { 10, 5, 1, "Snorkeling" },
                    { 11, 5, 1, "Diving" },
                    { 12, 5, 1, "Playground" },
                    { 13, 5, 1, "Game room" },
                    { 14, 6, 1, "Daily housekeeping" },
                    { 15, 6, 1, "Ironing service" },
                    { 16, 6, 1, "Dry cleaning" },
                    { 17, 6, 1, "Laundry" },
                    { 18, 7, 1, "Fitness" },
                    { 19, 7, 1, "Spa facilities" },
                    { 20, 7, 1, "Beach umbrellas" },
                    { 21, 7, 1, "Turkish/Steam Bath" },
                    { 22, 7, 1, "Massage" },
                    { 23, 7, 1, "Spa" },
                    { 24, 7, 1, "Fitness center" },
                    { 25, 7, 1, "Sauna" },
                    { 26, 8, 1, "Fax/Photocopying" },
                    { 27, 8, 1, "Meeting/Banquet facilities" },
                    { 28, 9, 1, "Smoke alarms" },
                    { 29, 9, 1, "24-hour security" },
                    { 30, 9, 1, "Safe" },
                    { 31, 10, 1, "Breakfast in the room" },
                    { 32, 10, 1, "Bar" },
                    { 36, 12, 1, "Free public parking is available on site." },
                    { 37, 12, 1, "Street parking" },
                    { 38, 13, 1, "WiFi is available in all areas and is free of charge." },
                    { 39, 13, 1, "Restaurant" },
                    { 41, 14, 1, "Baggage storage" },
                    { 42, 14, 1, "Tour desk" },
                    { 43, 14, 1, "Currency exchange" },
                    { 44, 14, 1, "24-hour front desk" },
                    { 45, 15, 1, "Designated smoking area" },
                    { 46, 15, 1, "Air conditioning" },
                    { 47, 15, 1, "Heating" },
                    { 48, 15, 1, "Chapel/Shrine" },
                    { 49, 15, 1, "Elevator" }
                });

            migrationBuilder.InsertData(
                table: "Facilities",
                columns: new[] { "Id", "FacilityTypeId", "ImageId", "Text" },
                values: new object[,]
                {
                    { 50, 15, 1, "Family rooms" },
                    { 51, 15, 1, "Facilities for disabled guests" },
                    { 52, 15, 1, "Non-smoking rooms" },
                    { 53, 15, 1, "Room service" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "CityId", "ImageId", "Title" },
                values: new object[,]
                {
                    { 1, null, 2, "Zanzibar" },
                    { 2, null, 2, "Ibiza" },
                    { 3, null, 2, "Bihar" },
                    { 4, null, 2, "Bali" },
                    { 5, null, 2, "Ras Al Khaimah" },
                    { 6, null, 2, "Uttar Pradesh" },
                    { 7, null, 2, "Texel" },
                    { 8, null, 2, "Isle of Wight" },
                    { 9, null, 2, "Jersey" },
                    { 10, null, 2, "Mykonos" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Description", "IsSmokingAllowed", "IsSuite", "PriceInUSD", "PriceInUserCurrency", "RoomSize", "RoomTypeId", "Sleeps", "StayBookingId" },
                values: new object[] { 1, "Test description for room 1", false, true, 54m, 850m, 25, 1, 2, null });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Description", "IsSmokingAllowed", "PriceInUSD", "PriceInUserCurrency", "RoomSize", "RoomTypeId", "Sleeps", "StayBookingId" },
                values: new object[] { 2, "Test description for room 1", true, 85m, 4200m, 30, 2, 3, null });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Description", "IsSmokingAllowed", "IsSuite", "PriceInUSD", "PriceInUserCurrency", "RoomSize", "RoomTypeId", "Sleeps", "StayBookingId" },
                values: new object[] { 3, "Test description for room 1", false, true, 15m, 305m, 35, 3, 5, null });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Description", "IsSmokingAllowed", "PriceInUSD", "PriceInUserCurrency", "RoomSize", "RoomTypeId", "Sleeps", "StayBookingId" },
                values: new object[,]
                {
                    { 4, "Test description for room 1", true, 74m, 1220m, 45, 4, 4, null },
                    { 5, "Test description for room 1", false, 80m, 3890m, 15, 5, 1, null }
                });

            migrationBuilder.InsertData(
                table: "SuggestionRules",
                columns: new[] { "Id", "SuggestionRuleTypeId", "Text", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Cancellation and prepayment policies vary according to accommodations type. Please enter the dates of your stay and check what conditions apply to your preferred room.", null },
                    { 2, 2, "Children of all ages are welcome.", null },
                    { 3, 2, "Children 12 and above are considered adults at this property.", null },
                    { 4, 2, "To see correct prices and occupancy info, add the number and ages of children in your group to your search.", null },
                    { 5, 2, "Additional fees are not calculated automatically in the total cost and will have to be paid for separately during your stay.", null },
                    { 6, 2, "This property doesn't offer extra beds.", null },
                    { 7, 2, "The maximum number of cribs allowed depends on the room you choose. Double-check the maximum capacity for the room you selected.", null },
                    { 8, 2, "All cribs and extra beds are subject to availability.", null },
                    { 9, 3, "The minimum age for check-in is 18.", null },
                    { 10, 4, "Pets are not allowed.", null },
                    { 11, 5, "When booking more than 4 rooms, different policies and additional supplements may apply.", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DisplayName", "Email", "FirstName", "LanguageId", "LastName", "PasswordHash", "PhoneNumber", "RoleId", "SaltHash", "Title" },
                values: new object[,]
                {
                    { 1, "Admin", "apartproject@ukr.net", "Admin", null, "Admin", "yZ31aVQ7LJsxbbLw+k8wr+R4MIjmsarDSHIyORD2E/c=", null, 1, "5kMcP+uiuDBVUtywUz+W/A==", null },
                    { 2, "Admin2", "inko10092001@gmail.com", "Admin2 FirstName", null, "Admin2 LastName", "SPkh8lLW5GvhYfEKMk9eoxG54v7SntfWGqvW4w8WSeo=", null, 1, "4GcMwBXa7wz82VqcGrk8Jw==", null },
                    { 3, "UserTest", "kanyesupreme@ukr.net", "User FirstName", null, "Test", "WOtG1AXmEOExiWIqkjtvaswoKwkK2wzPO4lkeyhMhNc=", null, 2, "SB13sgnTfmqDCzEk+yO8fw==", null },
                    { 4, "UserTest2", "prokter222@gmail.com", "User2 FirstName", null, "User2 LastName", "j3LoMuzqObUVFTDndGzvPz15tEWvyYeo1g5oQvGCnhM=", null, 2, "8yS7U//nE6iw1kvGIgUsQQ==", null }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "ImageId", "Title" },
                values: new object[,]
                {
                    { 1, 1, 41, "Odesa" },
                    { 2, 1, 42, "Kharkiv" },
                    { 3, 1, 43, "Dnipro" },
                    { 4, 1, 44, "Donetsk" },
                    { 5, 1, 45, "Lviv" },
                    { 6, 1, 46, "Zaporizhzhia" },
                    { 7, 1, 47, "Kryvyi Rih" },
                    { 8, 1, 48, "Mykolaiv" },
                    { 9, 1, 49, "Sevastopol" },
                    { 10, 1, 50, "Mariupol" },
                    { 11, 1, 51, "Luhansk" },
                    { 12, 1, 52, "Vinnytsia" },
                    { 13, 1, 53, "Makiivka" },
                    { 14, 1, 54, "Simferopol" },
                    { 15, 1, 55, "Chernihiv" },
                    { 16, 1, 56, "Kherson" },
                    { 17, 1, 57, "Poltava" },
                    { 18, 1, 58, "Khmelnytskyi" },
                    { 19, 1, 59, "Cherkasy" },
                    { 20, 1, 60, "Chernivtsi" },
                    { 21, 1, 61, "Zhytomyr" },
                    { 22, 1, 62, "Sumy" },
                    { 23, 1, 63, "Rivne" },
                    { 24, 1, 64, "Horlivka" },
                    { 25, 1, 65, "Ivano-Frankivsk" },
                    { 26, 1, 66, "Kamianske" },
                    { 27, 1, 67, "Ternopil" },
                    { 28, 1, 68, "Kropyvnytskyi" },
                    { 29, 1, 69, "Kremenchuk" },
                    { 30, 1, 70, "Lutsk" },
                    { 31, 1, 71, "Bila Tserkva" },
                    { 32, 1, 72, "Kerch" },
                    { 33, 1, 73, "Melitopol" },
                    { 34, 1, 74, "Kramatorsk" },
                    { 35, 1, 75, "Uzhhorod" },
                    { 36, 1, 76, "Brovary" },
                    { 37, 1, 77, "Yevpatoria" },
                    { 38, 1, 78, "Berdiansk" },
                    { 39, 1, 79, "Nikopol" },
                    { 40, 1, 80, "Sloviansk" },
                    { 41, 1, 81, "Alchevsk" },
                    { 42, 1, 82, "Pavlohrad" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "ImageId", "Title" },
                values: new object[,]
                {
                    { 43, 1, 83, "Sievierodonetsk" },
                    { 44, 1, 84, "Kamianets-Podilskyi" },
                    { 45, 1, 85, "Lysychansk" },
                    { 46, 1, 86, "Mukachevo" },
                    { 47, 1, 87, "Konotop" },
                    { 48, 1, 88, "Uman" },
                    { 49, 2, 1, "New York" },
                    { 50, 2, 1, "Los Angeles" },
                    { 51, 2, 1, "Chicago" },
                    { 52, 2, 1, "Houston" },
                    { 53, 2, 1, "Phoenix" },
                    { 54, 2, 1, "Philadelphia" },
                    { 55, 2, 1, "San Antonio" },
                    { 56, 2, 1, "San Diego" },
                    { 57, 2, 1, "Dallas" },
                    { 58, 2, 1, "San Jose" },
                    { 59, 2, 1, "Austin" },
                    { 60, 2, 1, "Jacksonville" },
                    { 61, 2, 1, "Fort Worth" },
                    { 62, 2, 1, "Columbus" },
                    { 63, 2, 1, "Indianapolis" },
                    { 64, 2, 1, "Charlotte" },
                    { 65, 2, 1, "San Francisco" },
                    { 66, 2, 1, "Seattle" },
                    { 67, 2, 1, "Denver" },
                    { 68, 2, 1, "Washington" },
                    { 69, 2, 1, "Nashville[" },
                    { 70, 2, 1, "Oklahoma City" },
                    { 71, 2, 1, "El Paso" },
                    { 72, 2, 1, "Boston" },
                    { 73, 2, 1, "Portland" },
                    { 74, 2, 1, "Las Vegas" },
                    { 75, 2, 1, "Detroit" },
                    { 76, 2, 1, "Memphis" },
                    { 77, 2, 1, "Louisville[" },
                    { 78, 2, 1, "Baltimore" },
                    { 79, 2, 1, "Milwaukee" },
                    { 80, 2, 1, "Sacramento" },
                    { 81, 2, 1, "Atlanta" },
                    { 82, 2, 1, "Honolulu" },
                    { 83, 3, 3, "London" },
                    { 84, 3, 3, "Birmingham" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "ImageId", "Title" },
                values: new object[,]
                {
                    { 85, 3, 3, "Leeds" },
                    { 86, 3, 3, "Glasgow" },
                    { 87, 3, 3, "Sheffield" },
                    { 88, 3, 3, "Bradford" },
                    { 89, 3, 3, "Liverpool" },
                    { 90, 3, 3, "Edinburgh" },
                    { 91, 3, 3, "Manchester" },
                    { 92, 3, 3, "Bristol" },
                    { 93, 3, 3, "Kirklees" },
                    { 94, 3, 3, "Fife" },
                    { 95, 3, 3, "Wirral" },
                    { 96, 3, 3, "North Lanarkshire" },
                    { 97, 3, 3, "Wakefield" },
                    { 98, 3, 3, "Cardiff" },
                    { 99, 3, 3, "Dudley" },
                    { 100, 3, 3, "Wigan" },
                    { 101, 49, 4, "Istanbul" },
                    { 102, 49, 4, "Ankara" },
                    { 103, 49, 4, "İzmir" },
                    { 104, 49, 4, "Bursa" },
                    { 105, 49, 4, "Adana" },
                    { 106, 49, 4, "Gaziantep" },
                    { 107, 49, 4, "Konya" },
                    { 108, 49, 4, "Antalya" },
                    { 109, 49, 4, "Kayseri" },
                    { 110, 49, 4, "Mersin" },
                    { 111, 49, 4, "Eskişehir" },
                    { 112, 49, 4, "Diyarbakır" },
                    { 113, 10, 5, "Budapest" },
                    { 114, 10, 5, "Debrecen" },
                    { 115, 10, 5, "Szeged" },
                    { 116, 10, 5, "Miskolc" },
                    { 117, 10, 5, "Pécs" },
                    { 118, 60, 6, "Rome" },
                    { 119, 60, 6, "Milan" },
                    { 120, 60, 6, "Naples" },
                    { 121, 60, 6, "Turin" },
                    { 122, 60, 6, "Palermo" },
                    { 123, 60, 6, "Genoa" },
                    { 124, 64, 7, "Stockholm" },
                    { 125, 64, 7, "Gothenburg" },
                    { 126, 64, 7, "Malmö" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "ImageId", "Title" },
                values: new object[,]
                {
                    { 127, 64, 7, "Uppsala" },
                    { 128, 64, 7, "Västerås" },
                    { 129, 63, 8, "Oslo" },
                    { 130, 63, 8, "Drammen" },
                    { 131, 63, 8, "Trondheim" },
                    { 132, 63, 8, "Kristiansand" },
                    { 133, 63, 8, "Stavanger" },
                    { 134, 72, 9, "Madrid" },
                    { 135, 72, 9, "Barcelona" },
                    { 136, 72, 9, "Valencia" },
                    { 137, 72, 9, "Sevilla" },
                    { 138, 72, 9, "Zaragoza" },
                    { 139, 80, 10, "Athens" },
                    { 140, 80, 10, "Thessaloniki" },
                    { 141, 80, 10, "Patra" },
                    { 142, 80, 10, "Piraeus" },
                    { 143, 80, 10, "Larisa" },
                    { 144, 113, 11, "Jerusalem" },
                    { 145, 113, 11, "Tel Aviv" },
                    { 146, 113, 11, "West Jerusalem" },
                    { 147, 113, 11, "Haifa" },
                    { 148, 113, 11, "Ashdod" },
                    { 149, 114, 12, "Berlin" },
                    { 150, 114, 12, "Hamburg" },
                    { 151, 114, 12, "München" },
                    { 152, 114, 12, "Köln" },
                    { 153, 114, 12, "Frankfurt" },
                    { 154, 123, 13, "Paris" },
                    { 155, 123, 13, "Marseille" },
                    { 156, 123, 13, "Lyon" },
                    { 157, 123, 13, "Toulouse" },
                    { 158, 123, 13, "Nice" },
                    { 159, 130, 14, "Sydney" },
                    { 160, 130, 14, "Melbourne" },
                    { 161, 130, 14, "Brisbane" },
                    { 162, 130, 14, "Perth" },
                    { 163, 130, 14, "Adelaide " },
                    { 164, 129, 15, "Toronto" },
                    { 165, 129, 15, "Montréal" },
                    { 166, 129, 15, "Vancouver" },
                    { 167, 129, 15, "Calgary" },
                    { 168, 129, 15, "Edmonton" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "ImageId", "Title" },
                values: new object[,]
                {
                    { 169, 135, 16, "Tokyo" },
                    { 170, 135, 16, "Yokohama" },
                    { 171, 135, 16, "Osaka" }
                });

            migrationBuilder.InsertData(
                table: "Favorites",
                columns: new[] { "Id", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "Address", "CityId", "CountryId", "DistrictId", "PhoneNumber", "RegionId", "ZipCode" },
                values: new object[,]
                {
                    { 1, "St. Deribasovskaya, 23", 1, 1, null, null, 1, null },
                    { 2, "St. Deribasovskaya, 42", 1, 1, null, null, 2, null },
                    { 3, "St. Khreshchatyk, 72", 2, 1, null, null, 3, null },
                    { 4, "St. Khreshchatyk, 44", 3, 1, null, null, 4, null }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "CityId", "ImageId", "Title" },
                values: new object[,]
                {
                    { 11, 3, 2, "Santorini" },
                    { 12, 4, 2, "Cornwall" },
                    { 13, 1, 2, "England" },
                    { 14, 2, 2, "Tenerife" }
                });

            migrationBuilder.InsertData(
                table: "Suggestions",
                columns: new[] { "Id", "AddressId", "BookingCategoryId", "Description", "IsParkingAvailable", "Name", "Progress", "ServiceCategoryId", "StarsRating", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1, "Test description of test suggestion 1", true, "Arcadia apartment & sea terrace", 100, 1, 3, 1 },
                    { 2, 2, 2, "Test description of test suggestion 2", false, "5 room apartment", 100, 1, 4, 2 },
                    { 3, 3, 3, "Test description of test suggestion 3", true, "Baron ApartHotel", 100, 1, 5, 3 },
                    { 4, 4, 4, "Test description of test suggestion 4", true, "Bon Apart", 100, 1, 5, 1 },
                    { 5, 1, 5, "Test description of test suggestion 5", false, "Plaza Arcadia Apartments", 100, 1, 5, 1 },
                    { 6, 1, 1, "Test description of test suggestion 6", true, "New apartments in Arcadia sea view", 100, 1, 3, 1 },
                    { 7, 2, 2, "Test description of test suggestion 7", false, "Royal Loft Primorskiy Boulevard", 100, 1, 4, 2 },
                    { 8, 3, 3, "Test description of test suggestion 8", true, "Kvartira u samogo moria (GREEN)!", 100, 1, 5, 3 },
                    { 9, 4, 4, "Test description of test suggestion 9", true, "Royal Arcadia Apartment", 100, 1, 5, 1 },
                    { 10, 1, 5, "Test description of test suggestion 10", false, "City Rooms Apartments", 100, 1, 5, 1 },
                    { 11, 1, 1, "Test description of test suggestion 11", true, "RainBow Arkadia Apartment", 100, 1, 3, 1 },
                    { 12, 2, 2, "Test description of test suggestion 12", false, "Rishelyevskiy", 100, 1, 4, 2 },
                    { 13, 3, 3, "Test description of test suggestion 13", true, "Lift Hotel Boutique", 100, 1, 5, 3 },
                    { 14, 4, 4, "Test description of test suggestion 14", true, "Odessa Heart", 100, 1, 5, 1 },
                    { 15, 1, 5, "Test description of test suggestion 15", false, "Elegia ArCadia Apartments", 100, 1, 5, 1 },
                    { 16, 1, 1, "Test description of test suggestion 16", true, "Inn Kyiv", 100, 1, 3, 1 },
                    { 17, 2, 2, "Test description of test suggestion 17", false, "Boutique Apart-Hotel Sherborne", 100, 1, 4, 2 },
                    { 18, 3, 3, "Test description of test suggestion 18", true, "Na Podole Apartment", 100, 1, 5, 3 },
                    { 19, 4, 4, "Test description of test suggestion 19", true, "Centre nigh Mariinsky Park Palace", 100, 1, 5, 1 },
                    { 20, 1, 5, "Test description of test suggestion 20", false, "Apartment On Khreshchatyk 21", 100, 1, 5, 1 },
                    { 21, 1, 1, "Test description of test suggestion 21", true, "Hotel Lime", 100, 1, 3, 1 },
                    { 22, 2, 2, "Test description of test suggestion 22", false, "Apartment in Khreshchatyk Passage", 100, 1, 4, 2 },
                    { 23, 3, 3, "Test description of test suggestion 23", true, "Cosy central ApartmentOpens in new window", 100, 1, 5, 3 },
                    { 24, 4, 4, "Test description of test suggestion 24", true, "Alex Apartments on Lva Tolstogo", 100, 1, 5, 1 },
                    { 25, 1, 5, "Test description of test suggestion 25", false, "Real Home Apartments", 100, 1, 5, 1 },
                    { 26, 1, 1, "Test description of test suggestion 26", true, "Hotel \"Mandarin Clubhouse\"", 100, 1, 3, 1 },
                    { 27, 2, 2, "Test description of test suggestion 27", false, "Bloom Hotel", 100, 1, 4, 2 },
                    { 28, 3, 3, "Test description of test suggestion 28", true, "Apartment on Sumskaya 46 \"Family\"", 100, 1, 5, 3 },
                    { 29, 4, 4, "Test description of test suggestion 29", true, "Ameritania at Times Square", 100, 1, 5, 1 },
                    { 30, 1, 5, "Test description of test suggestion 30", false, "Ace Hotel New York", 100, 1, 5, 1 },
                    { 31, 1, 1, "Test description of test suggestion 31", true, "NH Collection New York Madison Avenue", 100, 1, 3, 1 },
                    { 32, 2, 2, "Test description of test suggestion 32", false, "Freehand New York", 100, 1, 4, 2 },
                    { 33, 3, 3, "Test description of test suggestion 33", true, "Riu Plaza Manhattan Times Square", 100, 1, 5, 3 },
                    { 34, 4, 4, "Test description of test suggestion 34", true, "Royalton New York", 100, 1, 5, 1 },
                    { 35, 1, 5, "Test description of test suggestion 35", false, "Hyatt Place New York City/Times Square", 100, 1, 5, 1 },
                    { 36, 1, 1, "Test description of test suggestion 36", true, "Row NYC at Times Square", 100, 1, 3, 1 },
                    { 37, 2, 2, "Test description of test suggestion 37", false, "The New Yorker, A Wyndham Hotel", 100, 1, 4, 2 },
                    { 38, 3, 3, "Test description of test suggestion 38", true, "Motto by Hilton New York City Chelsea", 100, 1, 5, 3 },
                    { 39, 4, 4, "Test description of test suggestion 39", true, "Element Times Square West", 100, 1, 5, 1 },
                    { 40, 1, 5, "Test description of test suggestion 40", false, "Doubletree By Hilton New York Times Square West", 100, 1, 5, 1 },
                    { 41, 1, 1, "Test description of test suggestion 41", true, "Pod Times Square", 100, 1, 3, 1 },
                    { 42, 2, 2, "Test description of test suggestion 42", false, "DoubleTree by Hilton New York Downtown", 100, 1, 4, 2 }
                });

            migrationBuilder.InsertData(
                table: "Suggestions",
                columns: new[] { "Id", "AddressId", "BookingCategoryId", "Description", "IsParkingAvailable", "Name", "Progress", "ServiceCategoryId", "StarsRating", "UserId" },
                values: new object[,]
                {
                    { 43, 3, 3, "Test description of test suggestion 43", true, "Holiday Inn Express - Times Square South, an IHG Hotel", 100, 1, 5, 3 },
                    { 44, 4, 4, "Test description of test suggestion 44", true, "West Side YMCA", 100, 1, 5, 1 },
                    { 45, 1, 5, "Test description of test suggestion 45", false, "Hotel Edison Times Square", 100, 1, 5, 1 },
                    { 46, 1, 1, "Test description of test suggestion 46", true, "The Carvi Hotel New York, Ascend Hotel Collection", 100, 1, 3, 1 },
                    { 47, 2, 2, "Test description of test suggestion 47", false, "Riu Plaza New York Times Square", 100, 1, 4, 2 },
                    { 48, 3, 3, "Test description of test suggestion 48", true, "Pod 51", 100, 1, 5, 3 },
                    { 49, 4, 4, "Test description of test suggestion 49", true, "Arlo Midtown", 100, 1, 5, 1 },
                    { 50, 1, 5, "Test description of test suggestion 50", false, "Courtyard by Marriott New York Manhattan/Chelsea", 100, 1, 5, 1 },
                    { 51, 1, 1, "Test description of test suggestion 51", true, "SpringHill Suites by Marriott New York Manhattan Chelsea", 100, 1, 3, 1 },
                    { 52, 2, 2, "Test description of test suggestion 52", false, "MADE Hotel", 100, 1, 4, 2 },
                    { 53, 3, 3, "Test description of test suggestion 53", true, "Sheraton Tribeca New York Hotel", 100, 1, 5, 3 },
                    { 54, 4, 4, "Test description of test suggestion 54", true, "Hotel Normandie - Los Angeles", 100, 1, 5, 1 },
                    { 55, 1, 5, "Test description of test suggestion 55", false, "La Quinta by Wyndham LAX", 100, 1, 5, 1 },
                    { 56, 1, 1, "Test description of test suggestion 56", true, "Los Angeles Airport Marriott", 100, 1, 3, 1 },
                    { 57, 2, 2, "Test description of test suggestion 57", false, "Magic Castle Hotel", 100, 1, 4, 2 },
                    { 58, 3, 3, "Test description of test suggestion 58", true, "Holiday Inn Los Angeles - LAX Airport, an IHG Hotel", 100, 1, 5, 3 },
                    { 59, 4, 4, "Test description of test suggestion 59", true, "citizenM Los Angeles Downtown", 100, 1, 5, 1 },
                    { 60, 1, 5, "Test description of test suggestion 60", false, "Four Points by Sheraton Los Angeles Westside", 100, 1, 5, 1 },
                    { 61, 1, 1, "Test description of test suggestion 61", true, "Andaz West Hollywood-a concept by Hyatt", 100, 1, 3, 1 },
                    { 62, 2, 2, "Test description of test suggestion 62", false, "Four Points by Sheraton Los Angeles International Airport", 100, 1, 4, 2 },
                    { 63, 3, 3, "Test description of test suggestion 63", true, "The Hollywood Roosevelt", 100, 1, 5, 3 },
                    { 64, 4, 4, "Test description of test suggestion 64", true, "The Westin Los Angeles Airport", 100, 1, 5, 1 },
                    { 65, 1, 5, "Test description of test suggestion 65", false, "Hilton Los Angeles Airport", 100, 1, 5, 1 },
                    { 66, 1, 1, "Test description of test suggestion 66", true, "Hilton Santa Monica", 100, 1, 3, 1 },
                    { 67, 2, 2, "Test description of test suggestion 67", false, "Sonesta Los Angeles Airport LAX", 100, 1, 4, 2 },
                    { 68, 3, 3, "Test description of test suggestion 68", true, "Beverly Laurel Hotel", 100, 1, 5, 3 },
                    { 69, 4, 4, "Test description of test suggestion 69", true, "Hollywood Inn Express South", 100, 1, 5, 1 },
                    { 70, 1, 5, "Test description of test suggestion 70", false, "Hotel Erwin", 100, 1, 5, 1 },
                    { 71, 1, 1, "Test description of test suggestion 71", true, "Sheraton Universal", 100, 1, 3, 1 },
                    { 72, 2, 2, "Test description of test suggestion 72", false, "AIR Venice on the Beach", 100, 1, 4, 2 },
                    { 73, 3, 3, "Test description of test suggestion 73", true, "Hotel Aventura", 100, 1, 5, 3 },
                    { 74, 4, 4, "Test description of test suggestion 74", true, "Ramada by Wyndham Los Angeles/Wilshire Center", 100, 1, 5, 1 },
                    { 75, 1, 5, "Test description of test suggestion 75", false, "Thompson Hollywood", 100, 1, 5, 1 },
                    { 76, 1, 1, "Test description of test suggestion 76", true, "1 Hotel West Hollywood", 100, 1, 3, 1 },
                    { 77, 2, 2, "Test description of test suggestion 77", false, "Beverly Hills Marriott", 100, 1, 4, 2 },
                    { 78, 3, 3, "Test description of test suggestion 78", true, "Avenue Hotel, Ascend Hotel Collection", 100, 1, 5, 3 },
                    { 79, 4, 4, "Test description of test suggestion 79", true, "Hilton Chicago Magnificent Mile Suites", 100, 1, 5, 1 },
                    { 80, 1, 5, "Test description of test suggestion 80", false, "Kasa Chicago South Loop", 100, 1, 5, 1 },
                    { 81, 1, 1, "Test description of test suggestion 81", true, "Home2 Suites By Hilton Chicago River North", 100, 1, 3, 1 },
                    { 82, 2, 2, "Test description of test suggestion 82", false, "Level Chicago - Old Town", 100, 1, 4, 2 },
                    { 83, 3, 3, "Test description of test suggestion 83", true, "Hotel Felix", 100, 1, 5, 3 },
                    { 84, 4, 4, "Test description of test suggestion 84", true, "Sonder South Wabash", 100, 1, 5, 1 }
                });

            migrationBuilder.InsertData(
                table: "Suggestions",
                columns: new[] { "Id", "AddressId", "BookingCategoryId", "Description", "IsParkingAvailable", "Name", "Progress", "ServiceCategoryId", "StarsRating", "UserId" },
                values: new object[,]
                {
                    { 85, 1, 5, "Test description of test suggestion 85", false, "Club Quarters Hotel Central Loop, Chicago", 100, 1, 5, 1 },
                    { 86, 1, 1, "Test description of test suggestion 86", true, "Pendry Chicago", 100, 1, 3, 1 },
                    { 87, 2, 2, "Test description of test suggestion 87", false, "River Hotel", 100, 1, 4, 2 },
                    { 88, 3, 3, "Test description of test suggestion 88", true, "Hotel Julian Chicago", 100, 1, 5, 3 },
                    { 89, 4, 4, "Test description of test suggestion 89", true, "Homewood Suites by Hilton Chicago-Downtown", 100, 1, 5, 1 },
                    { 90, 1, 5, "Test description of test suggestion 90", false, "Central Loop Hotel", 100, 1, 5, 1 },
                    { 91, 1, 1, "Test description of test suggestion 91", true, "Selina Chicago", 100, 1, 3, 1 },
                    { 92, 2, 2, "Test description of test suggestion 92", false, "Warwick Allerton Chicago", 100, 1, 4, 2 },
                    { 93, 3, 3, "Test description of test suggestion 93", true, "La Quinta by Wyndham Chicago Downtown", 100, 1, 5, 3 },
                    { 94, 4, 4, "Test description of test suggestion 94", true, "Congress Plaza Hotel Chicago", 100, 1, 5, 1 },
                    { 95, 1, 5, "Test description of test suggestion 95", false, "Sonder The Plymouth", 100, 1, 5, 1 },
                    { 96, 1, 1, "Test description of test suggestion 96", true, "Cambria Hotel Chicago Loop/Theatre District", 100, 1, 3, 1 },
                    { 97, 2, 2, "Test description of test suggestion 97", false, "The Talbott Hotel", 100, 1, 4, 2 },
                    { 98, 3, 3, "Test description of test suggestion 98", true, "Sonder at Grant Park", 100, 1, 5, 3 },
                    { 99, 4, 4, "Test description of test suggestion 99", true, "Hotel Blake, an Ascend Hotel Collection Member", 100, 1, 5, 1 },
                    { 100, 1, 5, "Test description of test suggestion 100", false, "The St. Clair Hotel - Magnificent Mile", 100, 1, 5, 1 },
                    { 101, 1, 1, "Test description of test suggestion 101", true, "Aloft Chicago Mag Mile", 100, 1, 3, 1 },
                    { 102, 2, 2, "Test description of test suggestion 102", false, "Paddler's Lane Retreat", 100, 1, 4, 2 },
                    { 103, 3, 3, "Test description of test suggestion 103", true, "The Lodge at Chalk Hill", 100, 1, 5, 3 },
                    { 104, 4, 4, "Test description of test suggestion 104", true, "Tentrr Signature Site - Trout Meadow", 100, 1, 5, 1 },
                    { 105, 1, 5, "Test description of test suggestion 105", false, "Microtel Inn & Suites by Wyndham Hazelton/Bruceton Mills", 100, 1, 5, 1 },
                    { 106, 1, 1, "Test description of test suggestion 106", true, "Cozy Southwind Seven Springs Home, Ski-In and Ski-Out!", 100, 1, 3, 1 },
                    { 107, 2, 2, "Test description of test suggestion 107", false, "Pristine Resort Townhome 2 Mi to Seven Springs Mtn", 100, 1, 4, 2 },
                    { 108, 3, 3, "Test description of test suggestion 108", true, "Swiss Mtn Condo 3 Mi to Seven Springs Resort", 100, 1, 5, 3 },
                    { 109, 4, 4, "Test description of test suggestion 109", true, "The St Gregory Hotel Dupont Circle Georgetown", 100, 1, 5, 1 },
                    { 110, 1, 5, "Test description of test suggestion 110", false, "Hotel Zena, a Viceroy Urban Retreat", 100, 1, 5, 1 },
                    { 111, 1, 1, "Test description of test suggestion 111", true, "Riggs Washington DC", 100, 1, 3, 1 },
                    { 112, 2, 2, "Test description of test suggestion 112", false, "Club Quarters Hotel White House, Washington DC", 100, 1, 4, 2 },
                    { 113, 3, 3, "Test description of test suggestion 113", true, "Hotel Harrington", 100, 1, 5, 3 },
                    { 114, 4, 4, "Test description of test suggestion 114", true, "Beacon Hotel & Corporate Quarters", 100, 1, 5, 1 },
                    { 115, 1, 5, "Test description of test suggestion 115", false, "Hotel Lombardy", 100, 1, 5, 1 },
                    { 116, 1, 1, "Test description of test suggestion 116", true, "Wyndham Garden Washington DC North", 100, 1, 3, 1 },
                    { 117, 2, 2, "Test description of test suggestion 117", false, "Hyatt Place Washington DC/US Capitol", 100, 1, 4, 2 },
                    { 118, 3, 3, "Test description of test suggestion 118", true, "Courtyard by Marriott Washington, DC Dupont Circle", 100, 1, 5, 3 },
                    { 119, 4, 4, "Test description of test suggestion 119", true, "The Architect", 100, 1, 5, 1 },
                    { 120, 1, 5, "Test description of test suggestion 120", false, "The Baron Hotel", 100, 1, 5, 1 }
                });

            migrationBuilder.InsertData(
                table: "UserProfiles",
                columns: new[] { "Id", "AddressId", "BirthDate", "CurrencyId", "GenderId", "ImageId", "LanguageId", "Nationality", "RegisterDate", "UserId" },
                values: new object[,]
                {
                    { 1, 1, null, 1, 1, 1, 1, null, new DateTime(2022, 4, 14, 19, 43, 28, 815, DateTimeKind.Utc).AddTicks(2941), 1 },
                    { 2, 2, null, 2, 1, 2, 2, null, new DateTime(2022, 4, 14, 19, 43, 28, 815, DateTimeKind.Utc).AddTicks(2970), 2 },
                    { 3, 3, null, 2, 1, 1, 1, null, new DateTime(2022, 4, 14, 19, 43, 28, 815, DateTimeKind.Utc).AddTicks(2976), 3 },
                    { 4, 4, null, 2, 1, 2, 2, null, new DateTime(2022, 4, 14, 19, 43, 28, 815, DateTimeKind.Utc).AddTicks(2981), 4 }
                });

            migrationBuilder.InsertData(
                table: "Apartments",
                columns: new[] { "Id", "BathroomsAmount", "Description", "GuestsLimit", "PriceInUSD", "PriceInUserCurrency", "RoomsAmount", "SuggestionId" },
                values: new object[,]
                {
                    { 1, 2, "Test apartment description 1", 3, 25m, 250m, 2, 1 },
                    { 2, 3, "Test apartment description 2", 5, 65m, 650m, 3, 15 },
                    { 3, 4, "Test apartment description 27", 8, 125m, 1250m, 4, 27 },
                    { 4, 5, "Test apartment description 57", 10, 225m, 2250m, 5, 57 },
                    { 5, 3, "Test apartment description 101", 12, 355m, 3550m, 6, 101 },
                    { 6, 2, "Test apartment description 1", 3, 25m, 250m, 2, 1 },
                    { 7, 3, "Test apartment description 2", 5, 65m, 650m, 3, 15 },
                    { 8, 4, "Test apartment description 27", 8, 125m, 1250m, 4, 27 },
                    { 9, 5, "Test apartment description 57", 10, 225m, 2250m, 5, 57 },
                    { 10, 3, "Test apartment description 101", 12, 355m, 3550m, 6, 101 },
                    { 11, 2, "Test apartment description 1", 3, 25m, 250m, 2, 1 },
                    { 12, 3, "Test apartment description 2", 5, 65m, 650m, 3, 15 },
                    { 13, 4, "Test apartment description 27", 8, 125m, 1250m, 4, 27 },
                    { 14, 3, "Test apartment description 28", 10, 228m, 2280m, 8, 28 },
                    { 15, 3, "Test apartment description 101", 12, 355m, 3550m, 6, 101 },
                    { 16, 2, "Test apartment description 1", 3, 25m, 250m, 2, 1 },
                    { 17, 3, "Test apartment description 2", 5, 65m, 650m, 3, 15 },
                    { 18, 4, "Test apartment description 27", 8, 125m, 1250m, 4, 27 },
                    { 19, 5, "Test apartment description 57", 10, 225m, 2250m, 5, 57 },
                    { 20, 3, "Test apartment description 101", 12, 355m, 3550m, 6, 101 },
                    { 21, 2, "Test apartment description 1", 3, 25m, 250m, 2, 1 },
                    { 22, 1, "Test apartment description 19", 4, 55m, 550m, 3, 19 },
                    { 23, 4, "Test apartment description 27", 8, 125m, 1250m, 4, 27 },
                    { 24, 5, "Test apartment description 57", 10, 225m, 2250m, 5, 57 },
                    { 25, 3, "Test apartment description 101", 12, 355m, 3550m, 6, 101 },
                    { 26, 2, "Test apartment description 1", 3, 25m, 250m, 2, 1 },
                    { 27, 3, "Test apartment description 2", 5, 65m, 650m, 3, 15 },
                    { 28, 4, "Test apartment description 27", 8, 125m, 1250m, 4, 27 },
                    { 29, 5, "Test apartment description 57", 10, 225m, 2250m, 5, 57 },
                    { 30, 3, "Test apartment description 101", 12, 355m, 3550m, 6, 101 },
                    { 31, 2, "Test apartment description 1", 3, 25m, 250m, 2, 1 },
                    { 32, 2, "Test apartment description 23", 3, 75m, 750m, 5, 23 },
                    { 33, 4, "Test apartment description 27", 8, 125m, 1250m, 4, 27 },
                    { 34, 5, "Test apartment description 57", 10, 225m, 2250m, 5, 57 },
                    { 35, 3, "Test apartment description 101", 12, 355m, 3550m, 6, 101 },
                    { 36, 2, "Test apartment description 1", 3, 25m, 250m, 2, 1 },
                    { 37, 3, "Test apartment description 2", 5, 65m, 650m, 3, 15 },
                    { 38, 4, "Test apartment description 27", 8, 125m, 1250m, 4, 27 },
                    { 39, 5, "Test apartment description 57", 10, 225m, 2250m, 5, 57 },
                    { 40, 3, "Test apartment description 101", 12, 355m, 3550m, 6, 101 },
                    { 41, 2, "Test apartment description 1", 3, 25m, 250m, 2, 1 },
                    { 42, 3, "Test apartment description 2", 5, 65m, 650m, 3, 15 }
                });

            migrationBuilder.InsertData(
                table: "Apartments",
                columns: new[] { "Id", "BathroomsAmount", "Description", "GuestsLimit", "PriceInUSD", "PriceInUserCurrency", "RoomsAmount", "SuggestionId" },
                values: new object[,]
                {
                    { 43, 4, "Test apartment description 27", 8, 125m, 1250m, 4, 27 },
                    { 44, 5, "Test apartment description 57", 10, 225m, 2250m, 5, 57 },
                    { 45, 3, "Test apartment description 101", 12, 355m, 3550m, 6, 101 },
                    { 46, 2, "Test apartment description 1", 3, 25m, 250m, 2, 1 },
                    { 47, 3, "Test apartment description 2", 5, 65m, 650m, 3, 15 },
                    { 48, 4, "Test apartment description 27", 8, 125m, 1250m, 4, 27 },
                    { 49, 5, "Test apartment description 57", 10, 225m, 2250m, 5, 57 },
                    { 50, 3, "Test apartment description 101", 12, 355m, 3550m, 6, 101 },
                    { 51, 2, "Test apartment description 1", 3, 25m, 250m, 2, 1 },
                    { 52, 8, "Test apartment description 103", 5, 105m, 1050m, 5, 103 },
                    { 53, 4, "Test apartment description 27", 8, 125m, 1250m, 4, 27 },
                    { 54, 5, "Test apartment description 57", 10, 225m, 2250m, 5, 57 },
                    { 55, 3, "Test apartment description 101", 12, 355m, 3550m, 6, 101 },
                    { 56, 2, "Test apartment description 1", 3, 25m, 250m, 2, 1 },
                    { 57, 3, "Test apartment description 2", 5, 65m, 650m, 3, 15 },
                    { 58, 4, "Test apartment description 27", 8, 125m, 1250m, 4, 27 },
                    { 59, 5, "Test apartment description 57", 10, 225m, 2250m, 5, 57 },
                    { 60, 3, "Test apartment description 101", 12, 355m, 3550m, 6, 101 },
                    { 61, 2, "Test apartment description 1", 3, 25m, 250m, 2, 1 },
                    { 62, 3, "Test apartment description 2", 5, 65m, 650m, 3, 15 },
                    { 63, 4, "Test apartment description 27", 8, 125m, 1250m, 4, 27 },
                    { 64, 5, "Test apartment description 57", 10, 225m, 2250m, 5, 57 },
                    { 65, 1, "Test apartment description 97", 2, 40m, 400m, 2, 97 },
                    { 66, 2, "Test apartment description 1", 3, 25m, 250m, 2, 1 },
                    { 67, 3, "Test apartment description 2", 5, 65m, 650m, 3, 15 },
                    { 68, 4, "Test apartment description 27", 8, 125m, 1250m, 4, 27 },
                    { 69, 5, "Test apartment description 57", 10, 225m, 2250m, 5, 57 },
                    { 70, 3, "Test apartment description 101", 12, 355m, 3550m, 6, 101 }
                });

            migrationBuilder.InsertData(
                table: "SuggestionReviewGrades",
                columns: new[] { "Id", "ReviewCategoryId", "SuggestionId", "Value" },
                values: new object[,]
                {
                    { 1, 1, 1, 10.0 },
                    { 2, 2, 2, 7.0 },
                    { 3, 3, 3, 6.0 },
                    { 4, 4, 4, 9.0 },
                    { 5, 5, 5, 6.0 },
                    { 6, 6, 1, 9.0 },
                    { 7, 7, 2, 6.0 },
                    { 8, 8, 3, 3.0 },
                    { 9, 9, 4, 8.0 },
                    { 10, 10, 5, 5.0 },
                    { 11, 11, 1, 10.0 },
                    { 12, 12, 2, 5.0 },
                    { 13, 1, 3, 9.0 },
                    { 14, 2, 4, 7.0 }
                });

            migrationBuilder.InsertData(
                table: "SuggestionReviewGrades",
                columns: new[] { "Id", "ReviewCategoryId", "SuggestionId", "Value" },
                values: new object[,]
                {
                    { 15, 3, 5, 6.0 },
                    { 16, 4, 1, 9.0 },
                    { 17, 5, 2, 6.0 },
                    { 18, 6, 3, 4.0 },
                    { 19, 7, 4, 6.0 },
                    { 20, 8, 5, 3.0 },
                    { 21, 9, 1, 9.0 },
                    { 22, 10, 2, 5.0 },
                    { 23, 11, 3, 7.0 },
                    { 24, 12, 4, 5.0 },
                    { 25, 1, 5, 9.0 },
                    { 26, 2, 1, 10.0 },
                    { 27, 3, 2, 6.0 },
                    { 28, 4, 3, 9.0 },
                    { 29, 5, 4, 6.0 },
                    { 30, 6, 5, 4.0 },
                    { 31, 7, 1, 9.0 },
                    { 32, 8, 2, 3.0 },
                    { 33, 9, 3, 8.0 },
                    { 34, 10, 4, 5.0 },
                    { 35, 11, 5, 7.0 },
                    { 36, 12, 1, 9.0 },
                    { 37, 6, 42, 9.0 },
                    { 38, 7, 42, 9.0 },
                    { 39, 8, 42, 9.0 },
                    { 40, 9, 42, 9.0 },
                    { 41, 10, 42, 9.0 },
                    { 42, 11, 42, 9.0 },
                    { 43, 12, 42, 10.0 },
                    { 44, 6, 43, 10.0 },
                    { 45, 7, 43, 9.0 },
                    { 46, 8, 43, 9.0 },
                    { 47, 9, 43, 10.0 },
                    { 48, 10, 43, 9.0 },
                    { 49, 11, 43, 10.0 },
                    { 50, 12, 43, 10.0 },
                    { 51, 6, 75, 10.0 },
                    { 52, 7, 75, 9.0 },
                    { 53, 8, 75, 9.0 },
                    { 54, 9, 75, 10.0 },
                    { 55, 10, 75, 10.0 },
                    { 56, 11, 75, 10.0 }
                });

            migrationBuilder.InsertData(
                table: "SuggestionReviewGrades",
                columns: new[] { "Id", "ReviewCategoryId", "SuggestionId", "Value" },
                values: new object[,]
                {
                    { 57, 12, 75, 10.0 },
                    { 58, 6, 15, 10.0 },
                    { 59, 7, 15, 9.0 },
                    { 60, 8, 15, 9.0 },
                    { 61, 9, 15, 10.0 },
                    { 62, 10, 15, 9.0 },
                    { 63, 11, 15, 10.0 },
                    { 64, 12, 15, 10.0 },
                    { 65, 6, 33, 10.0 },
                    { 66, 7, 33, 9.0 },
                    { 67, 8, 33, 9.0 },
                    { 68, 9, 33, 10.0 },
                    { 69, 10, 33, 9.0 },
                    { 70, 11, 33, 10.0 },
                    { 71, 12, 33, 10.0 },
                    { 72, 6, 7, 10.0 },
                    { 73, 7, 7, 9.0 },
                    { 74, 8, 7, 9.0 },
                    { 75, 9, 7, 10.0 },
                    { 76, 10, 7, 9.0 },
                    { 77, 11, 7, 9.0 },
                    { 78, 12, 7, 9.0 },
                    { 79, 6, 11, 10.0 },
                    { 80, 7, 11, 9.0 },
                    { 81, 8, 11, 9.0 },
                    { 82, 9, 11, 9.0 },
                    { 83, 10, 11, 9.0 },
                    { 84, 11, 11, 9.0 },
                    { 85, 12, 11, 10.0 }
                });

            migrationBuilder.InsertData(
                table: "SuggestionsFileModels",
                columns: new[] { "ImageId", "SuggestionId", "Id" },
                values: new object[,]
                {
                    { 24, 1, 1 },
                    { 25, 7, 2 },
                    { 26, 11, 3 },
                    { 27, 15, 4 },
                    { 28, 33, 5 }
                });

            migrationBuilder.InsertData(
                table: "ApartmentsBookedPeriods",
                columns: new[] { "ApartmentId", "BookedPeriodId", "Id" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 3, 3 },
                    { 4, 4, 4 },
                    { 5, 5, 5 },
                    { 6, 6, 6 },
                    { 7, 7, 7 },
                    { 8, 8, 8 },
                    { 9, 9, 9 },
                    { 10, 10, 10 },
                    { 11, 11, 11 },
                    { 12, 12, 12 },
                    { 13, 13, 13 },
                    { 14, 14, 14 },
                    { 15, 15, 15 },
                    { 16, 16, 16 },
                    { 17, 17, 17 },
                    { 18, 18, 18 },
                    { 19, 19, 19 },
                    { 20, 20, 20 },
                    { 21, 21, 21 },
                    { 22, 22, 22 },
                    { 23, 23, 23 },
                    { 24, 24, 24 },
                    { 25, 25, 25 },
                    { 26, 26, 26 },
                    { 27, 27, 27 },
                    { 28, 28, 28 },
                    { 29, 29, 29 },
                    { 30, 30, 30 },
                    { 31, 1, 31 },
                    { 32, 2, 32 },
                    { 33, 3, 33 },
                    { 34, 4, 34 },
                    { 35, 5, 35 },
                    { 36, 6, 36 },
                    { 37, 7, 37 },
                    { 38, 8, 38 },
                    { 39, 9, 39 },
                    { 40, 10, 40 },
                    { 41, 11, 41 },
                    { 42, 12, 42 }
                });

            migrationBuilder.InsertData(
                table: "ApartmentsBookedPeriods",
                columns: new[] { "ApartmentId", "BookedPeriodId", "Id" },
                values: new object[,]
                {
                    { 43, 13, 43 },
                    { 44, 14, 44 },
                    { 45, 15, 45 },
                    { 46, 16, 46 },
                    { 47, 17, 47 },
                    { 48, 18, 48 },
                    { 49, 19, 49 },
                    { 50, 20, 50 },
                    { 51, 21, 51 },
                    { 52, 22, 52 },
                    { 53, 23, 53 },
                    { 54, 24, 54 },
                    { 55, 25, 55 },
                    { 56, 26, 56 },
                    { 57, 27, 57 },
                    { 58, 28, 58 },
                    { 59, 29, 59 },
                    { 60, 30, 60 },
                    { 61, 1, 61 },
                    { 62, 2, 62 },
                    { 63, 3, 63 },
                    { 64, 4, 64 },
                    { 65, 5, 65 },
                    { 66, 6, 66 },
                    { 67, 7, 67 },
                    { 68, 8, 68 },
                    { 69, 9, 69 },
                    { 70, 10, 70 }
                });

            migrationBuilder.InsertData(
                table: "ApartmentsRoomTypes",
                columns: new[] { "ApartmentId", "RoomTypeId", "Id" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 3, 3 },
                    { 4, 4, 4 },
                    { 5, 5, 5 },
                    { 6, 6, 6 },
                    { 7, 7, 7 },
                    { 8, 8, 8 },
                    { 9, 9, 9 },
                    { 10, 10, 10 },
                    { 11, 11, 11 },
                    { 12, 12, 12 },
                    { 13, 13, 13 },
                    { 14, 1, 14 }
                });

            migrationBuilder.InsertData(
                table: "ApartmentsRoomTypes",
                columns: new[] { "ApartmentId", "RoomTypeId", "Id" },
                values: new object[,]
                {
                    { 15, 2, 15 },
                    { 16, 3, 16 },
                    { 17, 4, 17 },
                    { 18, 5, 18 },
                    { 19, 6, 19 },
                    { 20, 7, 20 },
                    { 21, 8, 21 },
                    { 22, 9, 22 },
                    { 23, 10, 23 },
                    { 24, 11, 24 },
                    { 25, 12, 25 },
                    { 26, 13, 26 },
                    { 27, 1, 27 },
                    { 28, 2, 28 },
                    { 29, 3, 29 },
                    { 30, 4, 30 },
                    { 31, 5, 31 },
                    { 32, 6, 32 },
                    { 33, 7, 33 },
                    { 34, 8, 34 },
                    { 35, 9, 35 },
                    { 36, 10, 36 },
                    { 37, 11, 37 },
                    { 38, 12, 38 },
                    { 39, 13, 39 },
                    { 40, 1, 40 },
                    { 41, 2, 41 },
                    { 42, 3, 42 },
                    { 43, 4, 43 },
                    { 44, 5, 44 },
                    { 45, 6, 45 },
                    { 46, 7, 46 },
                    { 47, 8, 47 },
                    { 48, 9, 48 },
                    { 49, 10, 49 },
                    { 50, 11, 50 },
                    { 51, 12, 51 },
                    { 52, 13, 52 },
                    { 53, 1, 53 },
                    { 54, 2, 54 },
                    { 55, 3, 55 },
                    { 56, 4, 56 }
                });

            migrationBuilder.InsertData(
                table: "ApartmentsRoomTypes",
                columns: new[] { "ApartmentId", "RoomTypeId", "Id" },
                values: new object[,]
                {
                    { 57, 5, 57 },
                    { 58, 6, 58 },
                    { 59, 7, 59 },
                    { 60, 8, 60 },
                    { 61, 9, 61 },
                    { 62, 10, 62 },
                    { 63, 11, 63 },
                    { 64, 12, 64 },
                    { 65, 13, 65 },
                    { 66, 1, 66 },
                    { 67, 2, 67 },
                    { 68, 3, 68 },
                    { 69, 4, 69 },
                    { 70, 5, 70 }
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
                name: "IX_Addresses_DistrictId",
                table: "Addresses",
                column: "DistrictId",
                unique: true,
                filter: "[DistrictId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_RegionId",
                table: "Addresses",
                column: "RegionId");

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
                name: "IX_AirportTaxiBookings_UserId",
                table: "AirportTaxiBookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_SuggestionId",
                table: "Apartments",
                column: "SuggestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentsBookedPeriods_BookedPeriodId",
                table: "ApartmentsBookedPeriods",
                column: "BookedPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentsRoomTypes_RoomTypeId",
                table: "ApartmentsRoomTypes",
                column: "RoomTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AreaInfos_AreaInfoTypeId",
                table: "AreaInfos",
                column: "AreaInfoTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AttractionBookings_CartId",
                table: "AttractionBookings",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_AttractionBookings_UserId",
                table: "AttractionBookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Beds_BedTypeId",
                table: "Beds",
                column: "BedTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BedSuggestion_SuggestionsId",
                table: "BedSuggestion",
                column: "SuggestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingCategories_ImageId",
                table: "BookingCategories",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_CarRentalBookings_CartId",
                table: "CarRentalBookings",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CarRentalBookings_UserId",
                table: "CarRentalBookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_ImageId",
                table: "Cities",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetails_SuggestionId",
                table: "ContactDetails",
                column: "SuggestionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Countries_ImageId",
                table: "Countries",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_CardTypeId",
                table: "CreditCards",
                column: "CardTypeId");

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
                name: "IX_FacilitySuggestion_SuggestionsId",
                table: "FacilitySuggestion",
                column: "SuggestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityTypes_ImageId",
                table: "FacilityTypes",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserId",
                table: "Favorites",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteSuggestion_SuggestionsId",
                table: "FavoriteSuggestion",
                column: "SuggestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightBookings_CartId",
                table: "FlightBookings",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightBookings_UserId",
                table: "FlightBookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InterestPlaces_SuggestionId",
                table: "InterestPlaces",
                column: "SuggestionId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageSuggestion_SuggestionsId",
                table: "LanguageSuggestion",
                column: "SuggestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_EmitterId",
                table: "Notifications",
                column: "EmitterId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ImageId",
                table: "Notifications",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CreditCardId",
                table: "Payments",
                column: "CreditCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentTypeId",
                table: "Payments",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_CityId",
                table: "Regions",
                column: "CityId",
                unique: true,
                filter: "[CityId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_ImageId",
                table: "Regions",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewCategories_SuggestionId",
                table: "ReviewCategories",
                column: "SuggestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ReviewMessageId",
                table: "Reviews",
                column: "ReviewMessageId",
                unique: true);

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
                unique: true);

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
                name: "IX_StayBookingTempUser_StayBookingsId",
                table: "StayBookingTempUser",
                column: "StayBookingsId");

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionHighlights_FileId",
                table: "SuggestionHighlights",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionHighlights_RoomId",
                table: "SuggestionHighlights",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionHighlights_RoomTypeId",
                table: "SuggestionHighlights",
                column: "RoomTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionHighlights_SuggestionId",
                table: "SuggestionHighlights",
                column: "SuggestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionReviewGrades_ReviewCategoryId",
                table: "SuggestionReviewGrades",
                column: "ReviewCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionReviewGrades_SuggestionId",
                table: "SuggestionReviewGrades",
                column: "SuggestionId");

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
                name: "IX_SuggestionsFileModels_ImageId",
                table: "SuggestionsFileModels",
                column: "ImageId");

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
                column: "AddressId",
                unique: true,
                filter: "[AddressId] IS NOT NULL");

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
                name: "ApartmentsBookedPeriods");

            migrationBuilder.DropTable(
                name: "ApartmentsRoomTypes");

            migrationBuilder.DropTable(
                name: "AreaInfos");

            migrationBuilder.DropTable(
                name: "AttractionBookings");

            migrationBuilder.DropTable(
                name: "BedSuggestion");

            migrationBuilder.DropTable(
                name: "CarRentalBookings");

            migrationBuilder.DropTable(
                name: "ContactDetails");

            migrationBuilder.DropTable(
                name: "FacilitySuggestion");

            migrationBuilder.DropTable(
                name: "FavoriteSuggestion");

            migrationBuilder.DropTable(
                name: "FlightBookings");

            migrationBuilder.DropTable(
                name: "FlightClassTypes");

            migrationBuilder.DropTable(
                name: "InterestPlaces");

            migrationBuilder.DropTable(
                name: "LanguageSuggestion");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "PromoCodes");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "StayBookingTempUser");

            migrationBuilder.DropTable(
                name: "SuggestionHighlights");

            migrationBuilder.DropTable(
                name: "SuggestionReviewGrades");

            migrationBuilder.DropTable(
                name: "SuggestionsFileModels");

            migrationBuilder.DropTable(
                name: "SuggestionSuggestionRule");

            migrationBuilder.DropTable(
                name: "SurroundingObjects");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "BookedDates");

            migrationBuilder.DropTable(
                name: "Apartments");

            migrationBuilder.DropTable(
                name: "AreaInfoTypes");

            migrationBuilder.DropTable(
                name: "Beds");

            migrationBuilder.DropTable(
                name: "Facilities");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "ReviewMessages");

            migrationBuilder.DropTable(
                name: "TempUsers");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "ReviewCategories");

            migrationBuilder.DropTable(
                name: "SuggestionRules");

            migrationBuilder.DropTable(
                name: "SurroundingObjectTypes");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "BedTypes");

            migrationBuilder.DropTable(
                name: "FacilityTypes");

            migrationBuilder.DropTable(
                name: "RoomTypes");

            migrationBuilder.DropTable(
                name: "StayBookings");

            migrationBuilder.DropTable(
                name: "SuggestionRuleTypes");

            migrationBuilder.DropTable(
                name: "BookingPrices");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Suggestions");

            migrationBuilder.DropTable(
                name: "CreditCards");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "BookingCategories");

            migrationBuilder.DropTable(
                name: "ServiceCategories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "CardTypes");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Files");
        }
    }
}
