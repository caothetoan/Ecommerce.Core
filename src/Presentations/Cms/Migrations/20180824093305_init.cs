using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vnit.Cms.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Baskets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BuyerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baskets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogBrands",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Brand = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogBrands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CategoryTemplateId = table.Column<int>(nullable: true),
                    MetaKeywords = table.Column<string>(nullable: true),
                    MetaDescription = table.Column<string>(nullable: true),
                    MetaTitle = table.Column<string>(nullable: true),
                    ParentCategoryId = table.Column<int>(nullable: true),
                    PictureId = table.Column<int>(nullable: false),
                    PageSize = table.Column<int>(nullable: false),
                    AllowCustomersToSelectPageSize = table.Column<bool>(nullable: false),
                    PageSizeOptions = table.Column<string>(nullable: true),
                    PriceRanges = table.Column<string>(nullable: true),
                    ShowOnHomePage = table.Column<bool>(nullable: false),
                    IncludeInTopMenu = table.Column<bool>(nullable: false),
                    SubjectToAcl = table.Column<bool>(nullable: false),
                    LimitedToStores = table.Column<bool>(nullable: false),
                    Published = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(nullable: false),
                    UpdatedOnUtc = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ViewPath = table.Column<string>(nullable: true),
                    DisplayOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    AllowsBilling = table.Column<bool>(nullable: false),
                    AllowsShipping = table.Column<bool>(nullable: false),
                    TwoLetterIsoCode = table.Column<string>(nullable: true),
                    ThreeLetterIsoCode = table.Column<string>(nullable: true),
                    NumericIsoCode = table.Column<int>(nullable: false),
                    SubjectToVat = table.Column<bool>(nullable: false),
                    Published = table.Column<bool>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    LimitedToStores = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    Thumbnail = table.Column<string>(nullable: true),
                    DisplayOrder = table.Column<int>(nullable: false),
                    Credits = table.Column<int>(nullable: false),
                    Published = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifyDate = table.Column<DateTime>(nullable: true),
                    ModifyBy = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerGuid = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    EmailToRevalidate = table.Column<string>(nullable: true),
                    AdminComment = table.Column<string>(nullable: true),
                    IsTaxExempt = table.Column<bool>(nullable: false),
                    AffiliateId = table.Column<int>(nullable: false),
                    VendorId = table.Column<int>(nullable: false),
                    HasShoppingCartItems = table.Column<bool>(nullable: false),
                    RequireReLogin = table.Column<bool>(nullable: false),
                    FailedLoginAttempts = table.Column<int>(nullable: false),
                    CannotLoginUntilDateUtc = table.Column<DateTime>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    IsSystemAccount = table.Column<bool>(nullable: false),
                    SystemName = table.Column<string>(nullable: true),
                    LastIpAddress = table.Column<string>(nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(nullable: false),
                    LastLoginDateUtc = table.Column<DateTime>(nullable: true),
                    LastActivityDateUtc = table.Column<DateTime>(nullable: false),
                    RegisteredInStoreId = table.Column<int>(nullable: false),
                    BillingAddressId = table.Column<int>(nullable: true),
                    ShippingAddressId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailAccount",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    Host = table.Column<string>(nullable: true),
                    Port = table.Column<int>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    EnableSsl = table.Column<bool>(nullable: false),
                    UseDefaultCredentials = table.Column<bool>(nullable: false),
                    IsDefault = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailAccount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntityProperty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EntityId = table.Column<int>(nullable: false),
                    EntityName = table.Column<string>(nullable: true),
                    PropertyName = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ExpiredDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityProperty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FaqCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Sequence = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifyDate = table.Column<DateTime>(nullable: true),
                    ModifyBy = table.Column<int>(nullable: true),
                    ParentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaqCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    LanguageCulture = table.Column<string>(nullable: true),
                    UniqueSeoCode = table.Column<string>(nullable: true),
                    FlagImageFileName = table.Column<string>(nullable: true),
                    Rtl = table.Column<bool>(nullable: false),
                    LimitedToStores = table.Column<bool>(nullable: false),
                    DefaultCurrencyId = table.Column<int>(nullable: false),
                    Published = table.Column<bool>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    MetaKeywords = table.Column<string>(nullable: true),
                    MetaDescription = table.Column<string>(nullable: true),
                    MetaTitle = table.Column<string>(nullable: true),
                    ParentManufacturerId = table.Column<int>(nullable: true),
                    Published = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: true),
                    DisplayOrder = table.Column<int>(nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(nullable: false),
                    UpdatedOnUtc = table.Column<DateTime>(nullable: true),
                    Icon = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    SystemName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AlternativeText = table.Column<string>(nullable: true),
                    LocalPath = table.Column<string>(nullable: true),
                    ThumbnailPath = table.Column<string>(nullable: true),
                    MimeType = table.Column<string>(nullable: true),
                    Binary = table.Column<byte[]>(nullable: true),
                    MediaType = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    IsFeatured = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Sequence = table.Column<int>(nullable: false),
                    ParentId = table.Column<int>(nullable: true),
                    PositionId = table.Column<int>(nullable: false),
                    NewWindow = table.Column<bool>(nullable: false),
                    Icon = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: true),
                    IsSystem = table.Column<bool>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifyDate = table.Column<DateTime>(nullable: true),
                    ModifyBy = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Short = table.Column<string>(nullable: true),
                    DisplayOrder = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifyDate = table.Column<DateTime>(nullable: true),
                    ModifyBy = table.Column<int>(nullable: true),
                    ParentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsLetterSubscription",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NewsLetterSubscriptionGuid = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    CustomerId = table.Column<int>(nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsLetterSubscription", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationEvent",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EventName = table.Column<string>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationEvent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Page",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    ShowInMenu = table.Column<bool>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    Icon = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    CreateBy = table.Column<int>(nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: true),
                    ModifyBy = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Page", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermissionRecord",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    SystemName = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionRecord", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductTypeId = table.Column<int>(nullable: false),
                    ParentGroupedProductId = table.Column<int>(nullable: false),
                    VisibleIndividually = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Short = table.Column<string>(nullable: true),
                    Full = table.Column<string>(nullable: true),
                    AdminComment = table.Column<string>(nullable: true),
                    ProductTemplateId = table.Column<int>(nullable: false),
                    VendorId = table.Column<int>(nullable: false),
                    ShowOnHomePage = table.Column<bool>(nullable: false),
                    MetaKeywords = table.Column<string>(nullable: true),
                    MetaDescription = table.Column<string>(nullable: true),
                    MetaTitle = table.Column<string>(nullable: true),
                    AllowCustomerReviews = table.Column<bool>(nullable: false),
                    ApprovedRatingSum = table.Column<int>(nullable: false),
                    NotApprovedRatingSum = table.Column<int>(nullable: false),
                    ApprovedTotalReviews = table.Column<int>(nullable: false),
                    NotApprovedTotalReviews = table.Column<int>(nullable: false),
                    SubjectToAcl = table.Column<bool>(nullable: false),
                    LimitedToStores = table.Column<bool>(nullable: false),
                    Sku = table.Column<string>(nullable: true),
                    ManufacturerPartNumber = table.Column<string>(nullable: true),
                    Gtin = table.Column<string>(nullable: true),
                    IsGiftCard = table.Column<bool>(nullable: false),
                    GiftCardTypeId = table.Column<int>(nullable: false),
                    OverriddenGiftCardAmount = table.Column<decimal>(nullable: true),
                    RequireOtherProducts = table.Column<bool>(nullable: false),
                    RequiredProductIds = table.Column<string>(nullable: true),
                    AutomaticallyAddRequiredProducts = table.Column<bool>(nullable: false),
                    IsDownload = table.Column<bool>(nullable: false),
                    DownloadId = table.Column<int>(nullable: false),
                    UnlimitedDownloads = table.Column<bool>(nullable: false),
                    MaxNumberOfDownloads = table.Column<int>(nullable: false),
                    DownloadExpirationDays = table.Column<int>(nullable: true),
                    DownloadActivationTypeId = table.Column<int>(nullable: false),
                    HasSampleDownload = table.Column<bool>(nullable: false),
                    SampleDownloadId = table.Column<int>(nullable: false),
                    HasUserAgreement = table.Column<bool>(nullable: false),
                    UserAgreementText = table.Column<string>(nullable: true),
                    IsRecurring = table.Column<bool>(nullable: false),
                    RecurringCycleLength = table.Column<int>(nullable: false),
                    RecurringCyclePeriodId = table.Column<int>(nullable: false),
                    RecurringTotalCycles = table.Column<int>(nullable: false),
                    IsRental = table.Column<bool>(nullable: false),
                    RentalPriceLength = table.Column<int>(nullable: false),
                    RentalPricePeriodId = table.Column<int>(nullable: false),
                    IsShipEnabled = table.Column<bool>(nullable: false),
                    IsFreeShipping = table.Column<bool>(nullable: false),
                    ShipSeparately = table.Column<bool>(nullable: false),
                    AdditionalShippingCharge = table.Column<decimal>(nullable: false),
                    DeliveryDateId = table.Column<int>(nullable: false),
                    IsTaxExempt = table.Column<bool>(nullable: false),
                    TaxCategoryId = table.Column<int>(nullable: false),
                    IsTelecommunicationsOrBroadcastingOrElectronicServices = table.Column<bool>(nullable: false),
                    ManageInventoryMethodId = table.Column<int>(nullable: false),
                    ProductAvailabilityRangeId = table.Column<int>(nullable: false),
                    UseMultipleWarehouses = table.Column<bool>(nullable: false),
                    WarehouseId = table.Column<int>(nullable: false),
                    StockQuantity = table.Column<int>(nullable: false),
                    DisplayStockAvailability = table.Column<bool>(nullable: false),
                    DisplayStockQuantity = table.Column<bool>(nullable: false),
                    MinStockQuantity = table.Column<int>(nullable: false),
                    LowStockActivityId = table.Column<int>(nullable: false),
                    NotifyAdminForQuantityBelow = table.Column<int>(nullable: false),
                    BackorderModeId = table.Column<int>(nullable: false),
                    AllowBackInStockSubscriptions = table.Column<bool>(nullable: false),
                    OrderMinimumQuantity = table.Column<int>(nullable: false),
                    OrderMaximumQuantity = table.Column<int>(nullable: false),
                    AllowedQuantities = table.Column<string>(nullable: true),
                    AllowAddingOnlyExistingAttributeCombinations = table.Column<bool>(nullable: false),
                    NotReturnable = table.Column<bool>(nullable: false),
                    DisableBuyButton = table.Column<bool>(nullable: false),
                    DisableWishlistButton = table.Column<bool>(nullable: false),
                    AvailableForPreOrder = table.Column<bool>(nullable: false),
                    PreOrderAvailabilityStartDateTimeUtc = table.Column<DateTime>(nullable: true),
                    CallForPrice = table.Column<bool>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    OldPrice = table.Column<decimal>(nullable: false),
                    ProductCost = table.Column<decimal>(nullable: false),
                    CustomerEntersPrice = table.Column<bool>(nullable: false),
                    MinimumCustomerEnteredPrice = table.Column<decimal>(nullable: false),
                    MaximumCustomerEnteredPrice = table.Column<decimal>(nullable: false),
                    BasepriceEnabled = table.Column<bool>(nullable: false),
                    BasepriceAmount = table.Column<decimal>(nullable: false),
                    BasepriceUnitId = table.Column<int>(nullable: false),
                    BasepriceBaseAmount = table.Column<decimal>(nullable: false),
                    BasepriceBaseUnitId = table.Column<int>(nullable: false),
                    MarkAsNew = table.Column<bool>(nullable: false),
                    MarkAsNewStartDateTimeUtc = table.Column<DateTime>(nullable: true),
                    MarkAsNewEndDateTimeUtc = table.Column<DateTime>(nullable: true),
                    HasTierPrices = table.Column<bool>(nullable: false),
                    HasDiscountsApplied = table.Column<bool>(nullable: false),
                    Weight = table.Column<decimal>(nullable: false),
                    Length = table.Column<decimal>(nullable: false),
                    Width = table.Column<decimal>(nullable: false),
                    Height = table.Column<decimal>(nullable: false),
                    AvailableStartDateTimeUtc = table.Column<DateTime>(nullable: true),
                    AvailableEndDateTimeUtc = table.Column<DateTime>(nullable: true),
                    DisplayOrder = table.Column<int>(nullable: false),
                    Published = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(nullable: false),
                    UpdatedOnUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttribute",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttribute", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ViewPath = table.Column<string>(nullable: true),
                    DisplayOrder = table.Column<int>(nullable: false),
                    IgnoredProductTypes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleName = table.Column<string>(nullable: true),
                    IsSystemRole = table.Column<bool>(nullable: false),
                    SystemName = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    GroupName = table.Column<string>(nullable: true),
                    StoreId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DisplayOrder = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    FeaturedImageId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Store",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    SslEnabled = table.Column<bool>(nullable: false),
                    Hosts = table.Column<string>(nullable: true),
                    DefaultLanguageId = table.Column<int>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    CompanyName = table.Column<string>(nullable: true),
                    CompanyAddress = table.Column<string>(nullable: true),
                    CompanyPhoneNumber = table.Column<string>(nullable: true),
                    CompanyVat = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    DisplayOrder = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UrlRecord",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EntityId = table.Column<int>(nullable: false),
                    EntityName = table.Column<string>(nullable: true),
                    Slug = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlRecord", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Guid = table.Column<Guid>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    BirthDay = table.Column<DateTime>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    PasswordSalt = table.Column<string>(nullable: true),
                    PasswordFormat = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    LastIpAddress = table.Column<string>(nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(nullable: false),
                    LastLoginDateUtc = table.Column<DateTime>(nullable: true),
                    FailedLoginAttempts = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    LastActivityDateUtc = table.Column<DateTime>(nullable: true),
                    IsSystemAccount = table.Column<bool>(nullable: false),
                    IsAdmin = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Gender = table.Column<int>(nullable: true),
                    IdentityCard = table.Column<string>(nullable: true),
                    CustomerType = table.Column<int>(nullable: true),
                    ProvinceId = table.Column<int>(nullable: true),
                    StudentYear = table.Column<int>(nullable: true),
                    StudentCategory = table.Column<string>(nullable: true),
                    StudentUniversity = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BasketItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UnitPrice = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    CatalogItemId = table.Column<int>(nullable: false),
                    BasketId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasketItem_Baskets_BasketId",
                        column: x => x.BasketId,
                        principalTable: "Baskets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CatalogItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    PictureUri = table.Column<string>(nullable: true),
                    CatalogTypeId = table.Column<int>(nullable: false),
                    CatalogBrandId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatalogItems_CatalogBrands_CatalogBrandId",
                        column: x => x.CatalogBrandId,
                        principalTable: "CatalogBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatalogItems_CatalogTypes_CatalogTypeId",
                        column: x => x.CatalogTypeId,
                        principalTable: "CatalogTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StateProvince",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CountryId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Abbreviation = table.Column<string>(nullable: true),
                    Published = table.Column<bool>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateProvince", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StateProvince_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lesson",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    Thumbnail = table.Column<string>(nullable: true),
                    DisplayOrder = table.Column<int>(nullable: false),
                    Duration = table.Column<int>(nullable: true),
                    Pageview = table.Column<int>(nullable: false),
                    IsAutoplay = table.Column<bool>(nullable: false),
                    Published = table.Column<bool>(nullable: false),
                    SourcecodeUrl = table.Column<string>(nullable: true),
                    DownloadUrl = table.Column<string>(nullable: true),
                    CourseId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifyDate = table.Column<DateTime>(nullable: true),
                    ModifyBy = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lesson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lesson_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    TemplateSystemName = table.Column<string>(nullable: true),
                    Template = table.Column<string>(nullable: true),
                    IsMaster = table.Column<bool>(nullable: false),
                    ParentEmailTemplateId = table.Column<int>(nullable: true),
                    BccEmailAddresses = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    DelayBeforeSend = table.Column<int>(nullable: true),
                    DelayPeriodId = table.Column<int>(nullable: false),
                    AttachedDownloadId = table.Column<int>(nullable: false),
                    EmailAccountId = table.Column<int>(nullable: false),
                    LimitedToStores = table.Column<bool>(nullable: false),
                    DelayPeriod = table.Column<int>(nullable: false),
                    AdministrationEmail = table.Column<string>(nullable: true),
                    IsSystem = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailTemplate_EmailAccount_EmailAccountId",
                        column: x => x.EmailAccountId,
                        principalTable: "EmailAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmailTemplate_EmailTemplate_ParentEmailTemplateId",
                        column: x => x.ParentEmailTemplateId,
                        principalTable: "EmailTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QueuedEmail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PriorityId = table.Column<int>(nullable: false),
                    From = table.Column<string>(nullable: true),
                    FromName = table.Column<string>(nullable: true),
                    To = table.Column<string>(nullable: true),
                    ToName = table.Column<string>(nullable: true),
                    ReplyTo = table.Column<string>(nullable: true),
                    ReplyToName = table.Column<string>(nullable: true),
                    CC = table.Column<string>(nullable: true),
                    Bcc = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    AttachmentFilePath = table.Column<string>(nullable: true),
                    AttachmentFileName = table.Column<string>(nullable: true),
                    AttachedDownloadId = table.Column<int>(nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(nullable: false),
                    DontSendBeforeDateUtc = table.Column<DateTime>(nullable: true),
                    SentTries = table.Column<int>(nullable: false),
                    SentOnUtc = table.Column<DateTime>(nullable: true),
                    EmailAccountId = table.Column<int>(nullable: false),
                    Priority = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueuedEmail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QueuedEmail_EmailAccount_EmailAccountId",
                        column: x => x.EmailAccountId,
                        principalTable: "EmailAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FAQ",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Short = table.Column<string>(nullable: true),
                    answer = table.Column<string>(nullable: true),
                    faq_status = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    FaqCategoryId = table.Column<int>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    ModifyDate = table.Column<DateTime>(nullable: true),
                    ModifyBy = table.Column<int>(nullable: true),
                    Pageview = table.Column<int>(nullable: false),
                    MetaTitle = table.Column<string>(nullable: true),
                    MetaDescription = table.Column<string>(nullable: true),
                    MetaKeywords = table.Column<string>(nullable: true),
                    Sequence = table.Column<int>(nullable: true),
                    Version = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQ", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FAQ_FaqCategory_FaqCategoryId",
                        column: x => x.FaqCategoryId,
                        principalTable: "FaqCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocaleStringResource",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LanguageId = table.Column<int>(nullable: false),
                    ResourceName = table.Column<string>(nullable: true),
                    ResourceValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocaleStringResource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocaleStringResource_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocalizedProperty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EntityId = table.Column<int>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false),
                    LocaleKeyGroup = table.Column<string>(nullable: true),
                    LocaleKey = table.Column<string>(nullable: true),
                    LocaleValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalizedProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocalizedProperty_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewsItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LanguageId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Short = table.Column<string>(nullable: true),
                    Full = table.Column<string>(nullable: true),
                    Published = table.Column<bool>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    StartDateUtc = table.Column<DateTime>(nullable: true),
                    EndDateUtc = table.Column<DateTime>(nullable: true),
                    AllowComments = table.Column<bool>(nullable: false),
                    LimitedToStores = table.Column<bool>(nullable: false),
                    MetaKeywords = table.Column<string>(nullable: true),
                    MetaDescription = table.Column<string>(nullable: true),
                    MetaTitle = table.Column<string>(nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(nullable: false),
                    UpdatedOnUtc = table.Column<DateTime>(nullable: true),
                    Pageview = table.Column<int>(nullable: false),
                    Thumbnail = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsItem_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Poll",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LanguageId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SystemKeyword = table.Column<string>(nullable: true),
                    Published = table.Column<bool>(nullable: false),
                    ShowOnHomePage = table.Column<bool>(nullable: false),
                    AllowGuestsToVote = table.Column<bool>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    LimitedToStores = table.Column<bool>(nullable: false),
                    StartDateUtc = table.Column<DateTime>(nullable: true),
                    EndDateUtc = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poll", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Poll_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntityMedia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MediaId = table.Column<int>(nullable: false),
                    EntityId = table.Column<int>(nullable: false),
                    EntityName = table.Column<string>(nullable: true),
                    DisplayOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityMedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityMedia_Media_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<int>(nullable: false),
                    IsRead = table.Column<bool>(nullable: false),
                    PublishDateTime = table.Column<DateTime>(nullable: false),
                    ReadDateTime = table.Column<DateTime>(nullable: true),
                    EntityId = table.Column<int>(nullable: false),
                    EntityName = table.Column<string>(nullable: true),
                    NotificationEventId = table.Column<int>(nullable: true),
                    InitiatorId = table.Column<int>(nullable: false),
                    InitiatorName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notification_NotificationEvent_NotificationEventId",
                        column: x => x.NotificationEventId,
                        principalTable: "NotificationEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    IsFeaturedProduct = table.Column<bool>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCategory_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductManufacturer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(nullable: false),
                    ManufacturerId = table.Column<int>(nullable: false),
                    IsFeatured = table.Column<bool>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductManufacturer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductManufacturer_Manufacturer_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductManufacturer_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductMedia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(nullable: false),
                    MediaId = table.Column<int>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductMedia_Media_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductMedia_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PermissionRole",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PermissionRecordId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermissionRole_PermissionRecord_PermissionRecordId",
                        column: x => x.PermissionRecordId,
                        principalTable: "PermissionRecord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductReview",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    StoreId = table.Column<int>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    ReviewText = table.Column<string>(nullable: true),
                    ReplyText = table.Column<string>(nullable: true),
                    CustomerNotifiedOfReply = table.Column<bool>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    HelpfulYesTotal = table.Column<int>(nullable: false),
                    HelpfulNoTotal = table.Column<int>(nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductReview_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductReview_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductReview_Store_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assessment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    CourseId = table.Column<int>(nullable: false),
                    Point = table.Column<decimal>(nullable: false),
                    StartOnUtc = table.Column<DateTime>(nullable: false),
                    EndOnUtc = table.Column<DateTime>(nullable: true),
                    Duration = table.Column<int>(nullable: false),
                    AllowDuration = table.Column<int>(nullable: false),
                    Finished = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assessment_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assessment_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enrollment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CourseId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Grade = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrollment_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollment_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSkill",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SkillId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    ExternalUrl = table.Column<string>(nullable: true),
                    DisplayOrder = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSkill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSkill_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSkill_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Company = table.Column<string>(nullable: true),
                    CountryId = table.Column<int>(nullable: true),
                    StateProvinceId = table.Column<int>(nullable: true),
                    County = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    ZipPostalCode = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    FaxNumber = table.Column<string>(nullable: true),
                    CustomAttributes = table.Column<string>(nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Address_StateProvince_StateProvinceId",
                        column: x => x.StateProvinceId,
                        principalTable: "StateProvince",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NewsComment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CommentTitle = table.Column<string>(nullable: true),
                    CommentText = table.Column<string>(nullable: true),
                    NewsItemId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false),
                    StoreId = table.Column<int>(nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsComment_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsComment_NewsItem_NewsItemId",
                        column: x => x.NewsItemId,
                        principalTable: "NewsItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsComment_Store_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewsItemCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NewsItemId = table.Column<int>(nullable: false),
                    NewsCategoryId = table.Column<int>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsItemCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsItemCategory_NewsCategory_NewsCategoryId",
                        column: x => x.NewsCategoryId,
                        principalTable: "NewsCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsItemCategory_NewsItem_NewsItemId",
                        column: x => x.NewsItemId,
                        principalTable: "NewsItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewsItemTag",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NewsItemId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsItemTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsItemTag_NewsItem_NewsItemId",
                        column: x => x.NewsItemId,
                        principalTable: "NewsItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsItemTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PollAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PollId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NumberOfVotes = table.Column<int>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PollAnswer_Poll_PollId",
                        column: x => x.PollId,
                        principalTable: "Poll",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderGuid = table.Column<Guid>(nullable: false),
                    StoreId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    BillingAddressId = table.Column<int>(nullable: false),
                    ShippingAddressId = table.Column<int>(nullable: true),
                    PickupAddressId = table.Column<int>(nullable: true),
                    PickUpInStore = table.Column<bool>(nullable: false),
                    OrderStatusId = table.Column<int>(nullable: false),
                    ShippingStatusId = table.Column<int>(nullable: false),
                    PaymentStatusId = table.Column<int>(nullable: false),
                    PaymentMethodSystemName = table.Column<string>(nullable: true),
                    CustomerCurrencyCode = table.Column<string>(nullable: true),
                    CurrencyRate = table.Column<decimal>(nullable: false),
                    CustomerTaxDisplayTypeId = table.Column<int>(nullable: false),
                    VatNumber = table.Column<string>(nullable: true),
                    OrderSubtotalInclTax = table.Column<decimal>(nullable: false),
                    OrderSubtotalExclTax = table.Column<decimal>(nullable: false),
                    OrderSubTotalDiscountInclTax = table.Column<decimal>(nullable: false),
                    OrderSubTotalDiscountExclTax = table.Column<decimal>(nullable: false),
                    OrderShippingInclTax = table.Column<decimal>(nullable: false),
                    OrderShippingExclTax = table.Column<decimal>(nullable: false),
                    PaymentMethodAdditionalFeeInclTax = table.Column<decimal>(nullable: false),
                    PaymentMethodAdditionalFeeExclTax = table.Column<decimal>(nullable: false),
                    TaxRates = table.Column<string>(nullable: true),
                    OrderTax = table.Column<decimal>(nullable: false),
                    OrderDiscount = table.Column<decimal>(nullable: false),
                    OrderTotal = table.Column<decimal>(nullable: false),
                    RefundedAmount = table.Column<decimal>(nullable: false),
                    RewardPointsHistoryEntryId = table.Column<int>(nullable: true),
                    CheckoutAttributeDescription = table.Column<string>(nullable: true),
                    CheckoutAttributesXml = table.Column<string>(nullable: true),
                    CustomerLanguageId = table.Column<int>(nullable: false),
                    AffiliateId = table.Column<int>(nullable: false),
                    CustomerIp = table.Column<string>(nullable: true),
                    AllowStoringCreditCardNumber = table.Column<bool>(nullable: false),
                    CardType = table.Column<string>(nullable: true),
                    CardName = table.Column<string>(nullable: true),
                    CardNumber = table.Column<string>(nullable: true),
                    MaskedCreditCardNumber = table.Column<string>(nullable: true),
                    CardCvv2 = table.Column<string>(nullable: true),
                    CardExpirationMonth = table.Column<string>(nullable: true),
                    CardExpirationYear = table.Column<string>(nullable: true),
                    AuthorizationTransactionId = table.Column<string>(nullable: true),
                    AuthorizationTransactionCode = table.Column<string>(nullable: true),
                    AuthorizationTransactionResult = table.Column<string>(nullable: true),
                    CaptureTransactionId = table.Column<string>(nullable: true),
                    CaptureTransactionResult = table.Column<string>(nullable: true),
                    SubscriptionTransactionId = table.Column<string>(nullable: true),
                    PaidDateUtc = table.Column<DateTime>(nullable: true),
                    ShippingMethod = table.Column<string>(nullable: true),
                    ShippingRateComputationMethodSystemName = table.Column<string>(nullable: true),
                    CustomValuesXml = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(nullable: false),
                    CustomOrderNumber = table.Column<string>(nullable: true),
                    OrderStatus = table.Column<int>(nullable: false),
                    PaymentStatus = table.Column<int>(nullable: false),
                    BuyerId = table.Column<string>(nullable: true),
                    OrderDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Address_BillingAddressId",
                        column: x => x.BillingAddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_Address_PickupAddressId",
                        column: x => x.PickupAddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_Address_ShippingAddressId",
                        column: x => x.ShippingAddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PollVotingRecord",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PollAnswerId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollVotingRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PollVotingRecord_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PollVotingRecord_PollAnswer_PollAnswerId",
                        column: x => x.PollAnswerId,
                        principalTable: "PollAnswer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderItemGuid = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    UnitPriceInclTax = table.Column<decimal>(nullable: false),
                    UnitPriceExclTax = table.Column<decimal>(nullable: false),
                    PriceInclTax = table.Column<decimal>(nullable: false),
                    PriceExclTax = table.Column<decimal>(nullable: false),
                    DiscountAmountInclTax = table.Column<decimal>(nullable: false),
                    DiscountAmountExclTax = table.Column<decimal>(nullable: false),
                    OriginalProductCost = table.Column<decimal>(nullable: false),
                    AttributeDescription = table.Column<string>(nullable: true),
                    AttributesXml = table.Column<string>(nullable: true),
                    DownloadCount = table.Column<int>(nullable: false),
                    IsDownloadActivated = table.Column<bool>(nullable: false),
                    LicenseDownloadId = table.Column<int>(nullable: true),
                    ItemWeight = table.Column<decimal>(nullable: true),
                    RentalStartDateUtc = table.Column<DateTime>(nullable: true),
                    RentalEndDateUtc = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderNote",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderId = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    DownloadId = table.Column<int>(nullable: false),
                    DisplayToCustomer = table.Column<bool>(nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderNote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderNote_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shipment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderId = table.Column<int>(nullable: false),
                    TrackingNumber = table.Column<string>(nullable: true),
                    TotalWeight = table.Column<decimal>(nullable: true),
                    ShippedDateUtc = table.Column<DateTime>(nullable: true),
                    DeliveryDateUtc = table.Column<DateTime>(nullable: true),
                    AdminComment = table.Column<string>(nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shipment_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShipmentItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ShipmentId = table.Column<int>(nullable: false),
                    OrderItemId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    WarehouseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipmentItem_Shipment_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "Shipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_CountryId",
                table: "Address",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_StateProvinceId",
                table: "Address",
                column: "StateProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_CourseId",
                table: "Assessment",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_UserId",
                table: "Assessment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItem_BasketId",
                table: "BasketItem",
                column: "BasketId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItems_CatalogBrandId",
                table: "CatalogItems",
                column: "CatalogBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItems_CatalogTypeId",
                table: "CatalogItems",
                column: "CatalogTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplate_EmailAccountId",
                table: "EmailTemplate",
                column: "EmailAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplate_ParentEmailTemplateId",
                table: "EmailTemplate",
                column: "ParentEmailTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_CourseId",
                table: "Enrollment",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_UserId",
                table: "Enrollment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityMedia_MediaId",
                table: "EntityMedia",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_FAQ_FaqCategoryId",
                table: "FAQ",
                column: "FaqCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_CourseId",
                table: "Lesson",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_LocaleStringResource_LanguageId",
                table: "LocaleStringResource",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalizedProperty_LanguageId",
                table: "LocalizedProperty",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsComment_CustomerId",
                table: "NewsComment",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsComment_NewsItemId",
                table: "NewsComment",
                column: "NewsItemId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsComment_StoreId",
                table: "NewsComment",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsItem_LanguageId",
                table: "NewsItem",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsItemCategory_NewsCategoryId",
                table: "NewsItemCategory",
                column: "NewsCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsItemCategory_NewsItemId",
                table: "NewsItemCategory",
                column: "NewsItemId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsItemTag_NewsItemId",
                table: "NewsItemTag",
                column: "NewsItemId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsItemTag_TagId",
                table: "NewsItemTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_CustomerId",
                table: "Notification",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_NotificationEventId",
                table: "Notification",
                column: "NotificationEventId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_BillingAddressId",
                table: "Order",
                column: "BillingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_PickupAddressId",
                table: "Order",
                column: "PickupAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ShippingAddressId",
                table: "Order",
                column: "ShippingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductId",
                table: "OrderItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderNote_OrderId",
                table: "OrderNote",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionRole_PermissionRecordId",
                table: "PermissionRole",
                column: "PermissionRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionRole_RoleId",
                table: "PermissionRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Poll_LanguageId",
                table: "Poll",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_PollAnswer_PollId",
                table: "PollAnswer",
                column: "PollId");

            migrationBuilder.CreateIndex(
                name: "IX_PollVotingRecord_CustomerId",
                table: "PollVotingRecord",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PollVotingRecord_PollAnswerId",
                table: "PollVotingRecord",
                column: "PollAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_CategoryId",
                table: "ProductCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_ProductId",
                table: "ProductCategory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductManufacturer_ManufacturerId",
                table: "ProductManufacturer",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductManufacturer_ProductId",
                table: "ProductManufacturer",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMedia_MediaId",
                table: "ProductMedia",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMedia_ProductId",
                table: "ProductMedia",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_CustomerId",
                table: "ProductReview",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_ProductId",
                table: "ProductReview",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReview_StoreId",
                table: "ProductReview",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_QueuedEmail_EmailAccountId",
                table: "QueuedEmail",
                column: "EmailAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipment_OrderId",
                table: "Shipment",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentItem_ShipmentId",
                table: "ShipmentItem",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_StateProvince_CountryId",
                table: "StateProvince",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSkill_SkillId",
                table: "UserSkill",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSkill_UserId",
                table: "UserSkill",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assessment");

            migrationBuilder.DropTable(
                name: "BasketItem");

            migrationBuilder.DropTable(
                name: "CatalogItems");

            migrationBuilder.DropTable(
                name: "CategoryTemplate");

            migrationBuilder.DropTable(
                name: "EmailTemplate");

            migrationBuilder.DropTable(
                name: "Enrollment");

            migrationBuilder.DropTable(
                name: "EntityMedia");

            migrationBuilder.DropTable(
                name: "EntityProperty");

            migrationBuilder.DropTable(
                name: "FAQ");

            migrationBuilder.DropTable(
                name: "Lesson");

            migrationBuilder.DropTable(
                name: "LocaleStringResource");

            migrationBuilder.DropTable(
                name: "LocalizedProperty");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "NewsComment");

            migrationBuilder.DropTable(
                name: "NewsItemCategory");

            migrationBuilder.DropTable(
                name: "NewsItemTag");

            migrationBuilder.DropTable(
                name: "NewsLetterSubscription");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "OrderNote");

            migrationBuilder.DropTable(
                name: "Page");

            migrationBuilder.DropTable(
                name: "PermissionRole");

            migrationBuilder.DropTable(
                name: "PollVotingRecord");

            migrationBuilder.DropTable(
                name: "ProductAttribute");

            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropTable(
                name: "ProductManufacturer");

            migrationBuilder.DropTable(
                name: "ProductMedia");

            migrationBuilder.DropTable(
                name: "ProductReview");

            migrationBuilder.DropTable(
                name: "ProductTemplate");

            migrationBuilder.DropTable(
                name: "QueuedEmail");

            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DropTable(
                name: "ShipmentItem");

            migrationBuilder.DropTable(
                name: "UrlRecord");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "UserSkill");

            migrationBuilder.DropTable(
                name: "Baskets");

            migrationBuilder.DropTable(
                name: "CatalogBrands");

            migrationBuilder.DropTable(
                name: "CatalogTypes");

            migrationBuilder.DropTable(
                name: "FaqCategory");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "NewsCategory");

            migrationBuilder.DropTable(
                name: "NewsItem");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "NotificationEvent");

            migrationBuilder.DropTable(
                name: "PermissionRecord");

            migrationBuilder.DropTable(
                name: "PollAnswer");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Manufacturer");

            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Store");

            migrationBuilder.DropTable(
                name: "EmailAccount");

            migrationBuilder.DropTable(
                name: "Shipment");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Poll");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "StateProvince");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
