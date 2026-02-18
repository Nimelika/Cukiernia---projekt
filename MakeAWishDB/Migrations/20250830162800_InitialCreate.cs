using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MakeAWishDB.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CakeFillings",
                columns: table => new
                {
                    CakeFillingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CakeFill__71C3570A252C7797", x => x.CakeFillingID);
                });

            migrationBuilder.CreateTable(
                name: "CelebrationCakeSizes",
                columns: table => new
                {
                    CelebrationCakeSizeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Celebrat__72BBF00B609C94D9", x => x.CelebrationCakeSizeId);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryAbbreviation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Countrie__3214EC07512BC5E0", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MainPageArticles",
                columns: table => new
                {
                    ArticleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageAlt = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublishDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsPublished = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MainPage__9C6270C8D011781F", x => x.ArticleID);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    ModuleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Modules__2B7477879B416450", x => x.ModuleID);
                });

            migrationBuilder.CreateTable(
                name: "OrderSteps",
                columns: table => new
                {
                    OrderStepID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StepNo = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderSte__64CE5BB1327EC3B7", x => x.OrderStepID);
                });

            migrationBuilder.CreateTable(
                name: "PageHeaders",
                columns: table => new
                {
                    PageHeaderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternalName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DisplayedHeader = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsVisible = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PageHead__B6F5E8091F76D526", x => x.PageHeaderID);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    PaymentMethodID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PaymentM__DC31C1F34F89F601", x => x.PaymentMethodID);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    ProductCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageAlt = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProductC__3224ECEE66C212BC", x => x.ProductCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    ShopID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternalName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageAlt = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StreetAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Mail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OpeningHoursMdFr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OpeningHoursSt = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GoogleMapsURL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Shops__67C55629D721EE63", x => x.ShopID);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Statuses__C8EE206375D7C1E5", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "TeamMembers",
                columns: table => new
                {
                    TeamMemberID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageAlt = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TeamMemb__C7C09285EC34D6D1", x => x.TeamMemberID);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    UnitID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Units__44F5EC956539BDD0", x => x.UnitID);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserRole__8AFACE3AD1017CAB", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "CelebrationCakes",
                columns: table => new
                {
                    CelebrationCakeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageAlt = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CakeFilling = table.Column<int>(type: "int", nullable: false),
                    PriceSmall = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    PriceMedium = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    PriceLarge = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    WithNuts = table.Column<bool>(type: "bit", nullable: true),
                    WithFruits = table.Column<bool>(type: "bit", nullable: true),
                    WithAlcohol = table.Column<bool>(type: "bit", nullable: true),
                    Vegan = table.Column<bool>(type: "bit", nullable: true),
                    LowSugar = table.Column<bool>(type: "bit", nullable: true),
                    NoSugar = table.Column<bool>(type: "bit", nullable: true),
                    WeddingOffer = table.Column<bool>(type: "bit", nullable: true),
                    ChildrenOffer = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Celebrat__545A5A522EB28E60", x => x.CelebrationCakeID);
                    table.ForeignKey(
                        name: "FK__Celebrati__CakeF__60A75C0F",
                        column: x => x.CakeFilling,
                        principalTable: "CakeFillings",
                        principalColumn: "CakeFillingID");
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    RegionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CountryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Regions__ACD84443E2426DD2", x => x.RegionID);
                    table.ForeignKey(
                        name: "FK__Regions__Country__4D94879B",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LongArticles",
                columns: table => new
                {
                    ArticleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageHeaderID = table.Column<int>(type: "int", nullable: false),
                    Section1Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Section1Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Section2Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Section2Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Section3Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Section3Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Section4Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Section4Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Section5Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Section5Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Section6Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Section6Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Section7Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Section7Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPublished = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LongArti__9C6270C8D1CD4465", x => x.ArticleID);
                    table.ForeignKey(
                        name: "FK__LongArtic__PageH__10566F31",
                        column: x => x.PageHeaderID,
                        principalTable: "PageHeaders",
                        principalColumn: "PageHeaderID");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ProductCategory = table.Column<int>(type: "int", nullable: false),
                    CelebrationCakeID = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Products__B40CC6ED76A06646", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK__Products__Produc__5BE2A6F2",
                        column: x => x.ProductCategory,
                        principalTable: "ProductCategories",
                        principalColumn: "ProductCategoryID");
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    IngredientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UnitID = table.Column<int>(type: "int", nullable: false),
                    IsAllergenic = table.Column<bool>(type: "bit", nullable: false),
                    IsVegan = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ingredie__BEAEB27ABD481B7C", x => x.IngredientID);
                    table.ForeignKey(
                        name: "FK__Ingredien__UnitI__25518C17",
                        column: x => x.UnitID,
                        principalTable: "Units",
                        principalColumn: "UnitID");
                });

            migrationBuilder.CreateTable(
                name: "ModuleAccess",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    ModuleID = table.Column<int>(type: "int", nullable: false),
                    HasAccess = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ModuleAc__E84D8942AE4D833D", x => new { x.RoleID, x.ModuleID });
                    table.ForeignKey(
                        name: "FK__ModuleAcc__Modul__43D61337",
                        column: x => x.ModuleID,
                        principalTable: "Modules",
                        principalColumn: "ModuleID");
                    table.ForeignKey(
                        name: "FK__ModuleAcc__RoleI__42E1EEFE",
                        column: x => x.RoleID,
                        principalTable: "UserRoles",
                        principalColumn: "RoleID");
                });

            migrationBuilder.CreateTable(
                name: "UserAccounts",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    UserRole = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    LastLogin = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserAcco__1788CCAC56D1F1EE", x => x.UserID);
                    table.ForeignKey(
                        name: "FK__UserAccou__UserR__498EEC8D",
                        column: x => x.UserRole,
                        principalTable: "UserRoles",
                        principalColumn: "RoleID");
                });

            migrationBuilder.CreateTable(
                name: "Recomendations",
                columns: table => new
                {
                    RecomendationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CelebrationCake = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Recomend__0EF51DC0B009ED34", x => x.RecomendationID);
                    table.ForeignKey(
                        name: "FK__Recomenda__Celeb__6477ECF3",
                        column: x => x.CelebrationCake,
                        principalTable: "CelebrationCakes",
                        principalColumn: "CelebrationCakeID");
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Website = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StreetNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Region = table.Column<int>(type: "int", nullable: true),
                    Country = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    DeliveryStreet = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DeliveryStreetNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DeliveryPostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DeliveryCity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DeliveryRegion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DeliveryCountry = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsCompany = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    CompanyName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    NIP = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    REGON = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SearchTerm = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    RegisteredAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__A4AE64B89F80E383", x => x.CustomerID);
                    table.ForeignKey(
                        name: "FK__Customers__Count__5441852A",
                        column: x => x.Country,
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Customers__Regio__5535A963",
                        column: x => x.Region,
                        principalTable: "Regions",
                        principalColumn: "RegionID");
                });

            migrationBuilder.CreateTable(
                name: "CakeAllergens",
                columns: table => new
                {
                    CakeAllergenID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CelebrationCakeID = table.Column<int>(type: "int", nullable: false),
                    IngredientID = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CakeAlle__76ADC3D9FC6080B0", x => x.CakeAllergenID);
                    table.ForeignKey(
                        name: "FK__CakeAller__Celeb__02C769E9",
                        column: x => x.CelebrationCakeID,
                        principalTable: "CelebrationCakes",
                        principalColumn: "CelebrationCakeID");
                    table.ForeignKey(
                        name: "FK__CakeAller__Ingre__03BB8E22",
                        column: x => x.IngredientID,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientID");
                });

            migrationBuilder.CreateTable(
                name: "CakeRecipes",
                columns: table => new
                {
                    RecipeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CelebrationCakeID = table.Column<int>(type: "int", nullable: false),
                    CelebrationCakeSizeID = table.Column<int>(type: "int", nullable: false),
                    IngredientID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CakeReci__FDD988D01E35817D", x => x.RecipeID);
                    table.ForeignKey(
                        name: "FK__CakeRecip__Celeb__2BFE89A6",
                        column: x => x.CelebrationCakeID,
                        principalTable: "CelebrationCakes",
                        principalColumn: "CelebrationCakeID");
                    table.ForeignKey(
                        name: "FK__CakeRecip__Celeb__2CF2ADDF",
                        column: x => x.CelebrationCakeSizeID,
                        principalTable: "CelebrationCakeSizes",
                        principalColumn: "CelebrationCakeSizeId");
                    table.ForeignKey(
                        name: "FK__CakeRecip__Ingre__2DE6D218",
                        column: x => x.IngredientID,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientID");
                });

            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    StockID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngredientID = table.Column<int>(type: "int", nullable: false),
                    QuantityAvailable = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Stock__2C83A9E28969CB2B", x => x.StockID);
                    table.ForeignKey(
                        name: "FK__Stock__Ingredien__29221CFB",
                        column: x => x.IngredientID,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientID");
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartItems",
                columns: table => new
                {
                    CartItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    SessionToken = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CelebrationCakeID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    AddedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Shopping__488B0B2AB87E3588", x => x.CartItemID);
                    table.ForeignKey(
                        name: "FK__ShoppingC__Celeb__4F47C5E3",
                        column: x => x.CelebrationCakeID,
                        principalTable: "CelebrationCakes",
                        principalColumn: "CelebrationCakeID");
                    table.ForeignKey(
                        name: "FK__ShoppingC__UserI__4E53A1AA",
                        column: x => x.UserID,
                        principalTable: "UserAccounts",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "ContactMessages",
                columns: table => new
                {
                    MessageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Customer = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    MessageText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IsReplied = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ContactM__C87C037C2C85FD68", x => x.MessageID);
                    table.ForeignKey(
                        name: "FK__ContactMe__IsRep__09A971A2",
                        column: x => x.Customer,
                        principalTable: "Customers",
                        principalColumn: "CustomerID");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Customer = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    GuestName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GuestEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GuestPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Shop = table.Column<int>(type: "int", nullable: false),
                    CollectionDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orders__C3905BAF44AD5B3E", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK__Orders__Customer__74AE54BC",
                        column: x => x.Customer,
                        principalTable: "Customers",
                        principalColumn: "CustomerID");
                    table.ForeignKey(
                        name: "FK__Orders__Shop__75A278F5",
                        column: x => x.Shop,
                        principalTable: "Shops",
                        principalColumn: "ShopID");
                    table.ForeignKey(
                        name: "FK__Orders__Status__76969D2E",
                        column: x => x.Status,
                        principalTable: "Statuses",
                        principalColumn: "StatusId");
                });

            migrationBuilder.CreateTable(
                name: "QuoteRequests",
                columns: table => new
                {
                    QuoteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Customer = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    GuestName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GuestEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DesiredDeliveryDate = table.Column<DateOnly>(type: "date", nullable: true),
                    UploadedImagePath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    IsConvertedToOrder = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__QuoteReq__AF9688E17EDA8951", x => x.QuoteID);
                    table.ForeignKey(
                        name: "FK__QuoteRequ__Custo__03F0984C",
                        column: x => x.Customer,
                        principalTable: "Customers",
                        principalColumn: "CustomerID");
                    table.ForeignKey(
                        name: "FK__QuoteRequ__Statu__02FC7413",
                        column: x => x.Status,
                        principalTable: "Statuses",
                        principalColumn: "StatusId");
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(67)", maxLength: 67, nullable: true, computedColumnSql: "((((('FV/'+CONVERT([nvarchar],datepart(year,[InvoiceDate])))+'/')+right('0'+CONVERT([nvarchar],datepart(month,[InvoiceDate])),(2)))+'/')+CONVERT([nvarchar],[InvoiceID]))", stored: true),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    InvoiceDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TotalNet = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    VATRate = table.Column<decimal>(type: "decimal(4,2)", nullable: true, defaultValue: 23.00m),
                    TotalVAT = table.Column<decimal>(type: "decimal(19,8)", nullable: true, computedColumnSql: "(([TotalNet]*[VATRate])/(100))", stored: true),
                    TotalGross = table.Column<decimal>(type: "decimal(20,8)", nullable: true, computedColumnSql: "([TotalNet]+([TotalNet]*[VATRate])/(100))", stored: true),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    DueDate = table.Column<DateTime>(type: "datetime", nullable: true, computedColumnSql: "(case when [PaymentMethod]=(3) then dateadd(day,(14),[CreatedAt])  end)", stored: true),
                    PaidAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Invoices__D796AAD50FB24066", x => x.InvoiceID);
                    table.ForeignKey(
                        name: "FK__Invoices__OrderI__17F790F9",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID");
                    table.ForeignKey(
                        name: "FK__Invoices__Paymen__18EBB532",
                        column: x => x.PaymentMethod,
                        principalTable: "PaymentMethods",
                        principalColumn: "PaymentMethodID");
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    CelebrationCake = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    UnitPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    CelebrationCakeSize = table.Column<int>(type: "int", nullable: true),
                    PersonalizedText = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderIte__57ED06A183952EF7", x => x.OrderItemID);
                    table.ForeignKey(
                        name: "FK__OrderItem__Celeb__7B5B524B",
                        column: x => x.CelebrationCakeSize,
                        principalTable: "CelebrationCakeSizes",
                        principalColumn: "CelebrationCakeSizeId");
                    table.ForeignKey(
                        name: "FK__OrderItem__Celeb__7C4F7684",
                        column: x => x.CelebrationCake,
                        principalTable: "CelebrationCakes",
                        principalColumn: "CelebrationCakeID");
                    table.ForeignKey(
                        name: "FK__OrderItem__Order__7A672E12",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderID");
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItems",
                columns: table => new
                {
                    InvoiceItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    UnitPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    LineTotal = table.Column<decimal>(type: "decimal(21,2)", nullable: true, computedColumnSql: "([Quantity]*[UnitPrice])", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__InvoiceI__478FE0FCD1F6EC69", x => x.InvoiceItemID);
                    table.ForeignKey(
                        name: "FK__InvoiceIt__Invoi__1CBC4616",
                        column: x => x.InvoiceID,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceID");
                    table.ForeignKey(
                        name: "FK__InvoiceIt__Produ__1DB06A4F",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateTable(
                name: "IngredientReservations",
                columns: table => new
                {
                    ReservationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderItemID = table.Column<int>(type: "int", nullable: false),
                    CelebrationCakeSizeID = table.Column<int>(type: "int", nullable: false),
                    IngredientID = table.Column<int>(type: "int", nullable: false),
                    QuantityReserved = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    IsCancelled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ingredie__B7EE5F04A724586C", x => x.ReservationID);
                    table.ForeignKey(
                        name: "FK__Ingredien__Celeb__339FAB6E",
                        column: x => x.CelebrationCakeSizeID,
                        principalTable: "CelebrationCakeSizes",
                        principalColumn: "CelebrationCakeSizeId");
                    table.ForeignKey(
                        name: "FK__Ingredien__Ingre__3493CFA7",
                        column: x => x.IngredientID,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientID");
                    table.ForeignKey(
                        name: "FK__Ingredien__IsCan__32AB8735",
                        column: x => x.OrderItemID,
                        principalTable: "OrderItems",
                        principalColumn: "OrderItemID");
                });

            migrationBuilder.CreateTable(
                name: "StockShortages",
                columns: table => new
                {
                    ShortageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngredientID = table.Column<int>(type: "int", nullable: false),
                    OrderItemID = table.Column<int>(type: "int", nullable: false),
                    QuantityMissing = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    RecordedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IsValid = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__StockSho__6423218244215EAD", x => x.ShortageID);
                    table.ForeignKey(
                        name: "FK__StockShor__Ingre__395884C4",
                        column: x => x.IngredientID,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientID");
                    table.ForeignKey(
                        name: "FK__StockShor__Order__3A4CA8FD",
                        column: x => x.OrderItemID,
                        principalTable: "OrderItems",
                        principalColumn: "OrderItemID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CakeAllergens_CelebrationCakeID",
                table: "CakeAllergens",
                column: "CelebrationCakeID");

            migrationBuilder.CreateIndex(
                name: "IX_CakeAllergens_IngredientID",
                table: "CakeAllergens",
                column: "IngredientID");

            migrationBuilder.CreateIndex(
                name: "index_recipes_cake_size",
                table: "CakeRecipes",
                columns: new[] { "CelebrationCakeID", "CelebrationCakeSizeID" });

            migrationBuilder.CreateIndex(
                name: "IX_CakeRecipes_CelebrationCakeSizeID",
                table: "CakeRecipes",
                column: "CelebrationCakeSizeID");

            migrationBuilder.CreateIndex(
                name: "IX_CakeRecipes_IngredientID",
                table: "CakeRecipes",
                column: "IngredientID");

            migrationBuilder.CreateIndex(
                name: "IX_CelebrationCakes_CakeFilling",
                table: "CelebrationCakes",
                column: "CakeFilling");

            migrationBuilder.CreateIndex(
                name: "IX_ContactMessages_Customer",
                table: "ContactMessages",
                column: "Customer");

            migrationBuilder.CreateIndex(
                name: "UQ__Countrie__0F71A1ED6680DA07",
                table: "Countries",
                column: "CountryAbbreviation",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Countrie__E056F201E44B3207",
                table: "Countries",
                column: "CountryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Country",
                table: "Customers",
                column: "Country");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Region",
                table: "Customers",
                column: "Region");

            migrationBuilder.CreateIndex(
                name: "index_reservations_ingredient",
                table: "IngredientReservations",
                column: "IngredientID");

            migrationBuilder.CreateIndex(
                name: "index_reservations_orderitem",
                table: "IngredientReservations",
                column: "OrderItemID");

            migrationBuilder.CreateIndex(
                name: "index_reservations_status",
                table: "IngredientReservations",
                column: "IsCancelled");

            migrationBuilder.CreateIndex(
                name: "index_reservations_usage",
                table: "IngredientReservations",
                column: "IsUsed");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientReservations_CelebrationCakeSizeID",
                table: "IngredientReservations",
                column: "CelebrationCakeSizeID");

            migrationBuilder.CreateIndex(
                name: "index_ingredients_isavailable",
                table: "Ingredients",
                column: "IsAvailable");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_UnitID",
                table: "Ingredients",
                column: "UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_InvoiceID",
                table: "InvoiceItems",
                column: "InvoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_ProductID",
                table: "InvoiceItems",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "index_invoices_order",
                table: "Invoices",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PaymentMethod",
                table: "Invoices",
                column: "PaymentMethod");

            migrationBuilder.CreateIndex(
                name: "IX_LongArticles_PageHeaderID",
                table: "LongArticles",
                column: "PageHeaderID");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleAccess_ModuleID",
                table: "ModuleAccess",
                column: "ModuleID");

            migrationBuilder.CreateIndex(
                name: "UQ__Modules__EAC9AEC3AFBB1771",
                table: "Modules",
                column: "ModuleName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "index_orderitems_cake",
                table: "OrderItems",
                column: "CelebrationCake");

            migrationBuilder.CreateIndex(
                name: "index_orderitems_order",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_CelebrationCakeSize",
                table: "OrderItems",
                column: "CelebrationCakeSize");

            migrationBuilder.CreateIndex(
                name: "index_orders_orderdate",
                table: "Orders",
                column: "OrderDate");

            migrationBuilder.CreateIndex(
                name: "index_orders_status",
                table: "Orders",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Customer",
                table: "Orders",
                column: "Customer");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Shop",
                table: "Orders",
                column: "Shop");

            migrationBuilder.CreateIndex(
                name: "UQ__OrderSte__2434686271605109",
                table: "OrderSteps",
                column: "StepNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__PageHead__CAD7CE323DE0811F",
                table: "PageHeaders",
                column: "InternalName",
                unique: true,
                filter: "[InternalName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCategory",
                table: "Products",
                column: "ProductCategory");

            migrationBuilder.CreateIndex(
                name: "index_quotes_created",
                table: "QuoteRequests",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "index_quotes_status",
                table: "QuoteRequests",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequests_Customer",
                table: "QuoteRequests",
                column: "Customer");

            migrationBuilder.CreateIndex(
                name: "UQ__Recomend__46385E408BDB0A0C",
                table: "Recomendations",
                column: "CelebrationCake",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Regions_CountryID",
                table: "Regions",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_CelebrationCakeID",
                table: "ShoppingCartItems",
                column: "CelebrationCakeID");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_UserID",
                table: "ShoppingCartItems",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "index_stock_ingredient",
                table: "Stock",
                column: "IngredientID");

            migrationBuilder.CreateIndex(
                name: "index_shortages_ingredient_valid",
                table: "StockShortages",
                columns: new[] { "IngredientID", "IsValid" });

            migrationBuilder.CreateIndex(
                name: "index_shortages_orderitem",
                table: "StockShortages",
                column: "OrderItemID");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccounts_UserRole",
                table: "UserAccounts",
                column: "UserRole");

            migrationBuilder.CreateIndex(
                name: "UQ__UserAcco__536C85E48A608285",
                table: "UserAccounts",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CakeAllergens");

            migrationBuilder.DropTable(
                name: "CakeRecipes");

            migrationBuilder.DropTable(
                name: "ContactMessages");

            migrationBuilder.DropTable(
                name: "IngredientReservations");

            migrationBuilder.DropTable(
                name: "InvoiceItems");

            migrationBuilder.DropTable(
                name: "LongArticles");

            migrationBuilder.DropTable(
                name: "MainPageArticles");

            migrationBuilder.DropTable(
                name: "ModuleAccess");

            migrationBuilder.DropTable(
                name: "OrderSteps");

            migrationBuilder.DropTable(
                name: "QuoteRequests");

            migrationBuilder.DropTable(
                name: "Recomendations");

            migrationBuilder.DropTable(
                name: "ShoppingCartItems");

            migrationBuilder.DropTable(
                name: "Stock");

            migrationBuilder.DropTable(
                name: "StockShortages");

            migrationBuilder.DropTable(
                name: "TeamMembers");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "PageHeaders");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "UserAccounts");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "CelebrationCakeSizes");

            migrationBuilder.DropTable(
                name: "CelebrationCakes");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "CakeFillings");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Shops");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
