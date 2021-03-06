USE [master]
GO
/****** Object:  Database [Vnit.Ecommerce]    Script Date: 8/9/2018 1:43:04 PM ******/
CREATE DATABASE [Vnit.Ecommerce]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Vnit.Ecommerce', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Vnit.Ecommerce.mdf' , SIZE = 4288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Vnit.Ecommerce_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Vnit.Ecommerce_log.ldf' , SIZE = 2112KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Vnit.Ecommerce] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Vnit.Ecommerce].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Vnit.Ecommerce] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Vnit.Ecommerce] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Vnit.Ecommerce] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Vnit.Ecommerce] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Vnit.Ecommerce] SET ARITHABORT OFF 
GO
ALTER DATABASE [Vnit.Ecommerce] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Vnit.Ecommerce] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Vnit.Ecommerce] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Vnit.Ecommerce] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Vnit.Ecommerce] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Vnit.Ecommerce] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Vnit.Ecommerce] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Vnit.Ecommerce] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Vnit.Ecommerce] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Vnit.Ecommerce] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Vnit.Ecommerce] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Vnit.Ecommerce] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Vnit.Ecommerce] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Vnit.Ecommerce] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Vnit.Ecommerce] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Vnit.Ecommerce] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [Vnit.Ecommerce] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Vnit.Ecommerce] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Vnit.Ecommerce] SET  MULTI_USER 
GO
ALTER DATABASE [Vnit.Ecommerce] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Vnit.Ecommerce] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Vnit.Ecommerce] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Vnit.Ecommerce] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Vnit.Ecommerce] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Vnit.Ecommerce]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Company] [nvarchar](max) NULL,
	[CountryId] [int] NULL,
	[StateProvinceId] [int] NULL,
	[County] [nvarchar](max) NULL,
	[City] [nvarchar](max) NULL,
	[Address1] [nvarchar](max) NULL,
	[Address2] [nvarchar](max) NULL,
	[ZipPostalCode] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[FaxNumber] [nvarchar](max) NULL,
	[CustomAttributes] [nvarchar](max) NULL,
	[CreatedOnUtc] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Assessment]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assessment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[Point] [decimal](18, 2) NOT NULL,
	[StartOnUtc] [datetime2](7) NOT NULL,
	[EndOnUtc] [datetime2](7) NULL,
	[Duration] [int] NOT NULL,
	[AllowDuration] [int] NOT NULL,
	[Finished] [bit] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Body] [nvarchar](max) NULL,
 CONSTRAINT [PK_Assessment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BasketItem]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BasketItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
	[CatalogItemId] [int] NOT NULL,
	[BasketId] [int] NULL,
 CONSTRAINT [PK_BasketItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Baskets]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Baskets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BuyerId] [nvarchar](max) NULL,
 CONSTRAINT [PK_Baskets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Catalog]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Catalog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[PictureUri] [nvarchar](max) NULL,
	[CatalogTypeId] [int] NOT NULL,
	[CatalogBrandId] [int] NOT NULL,
 CONSTRAINT [PK_Catalog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CatalogBrand]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CatalogBrand](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Brand] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_CatalogBrand] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CatalogType]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CatalogType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_CatalogType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[CategoryTemplateId] [int] NOT NULL,
	[MetaKeywords] [nvarchar](max) NULL,
	[MetaDescription] [nvarchar](max) NULL,
	[MetaTitle] [nvarchar](max) NULL,
	[ParentCategoryId] [int] NOT NULL,
	[PictureId] [int] NOT NULL,
	[PageSize] [int] NOT NULL,
	[AllowCustomersToSelectPageSize] [bit] NOT NULL,
	[PageSizeOptions] [nvarchar](max) NULL,
	[PriceRanges] [nvarchar](max) NULL,
	[ShowOnHomePage] [bit] NOT NULL,
	[IncludeInTopMenu] [bit] NOT NULL,
	[SubjectToAcl] [bit] NOT NULL,
	[LimitedToStores] [bit] NOT NULL,
	[Published] [bit] NOT NULL,
	[Deleted] [bit] NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[CreatedOnUtc] [datetime2](7) NOT NULL,
	[UpdatedOnUtc] [datetime2](7) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CategoryTemplate]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoryTemplate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[ViewPath] [nvarchar](max) NULL,
	[DisplayOrder] [int] NOT NULL,
 CONSTRAINT [PK_CategoryTemplate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[AllowsBilling] [bit] NOT NULL,
	[AllowsShipping] [bit] NOT NULL,
	[TwoLetterIsoCode] [nvarchar](max) NULL,
	[ThreeLetterIsoCode] [nvarchar](max) NULL,
	[NumericIsoCode] [int] NOT NULL,
	[SubjectToVat] [bit] NOT NULL,
	[Published] [bit] NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[LimitedToStores] [bit] NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Course]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Body] [nvarchar](max) NULL,
	[Thumbnail] [nvarchar](max) NULL,
	[DisplayOrder] [int] NOT NULL,
	[Credits] [int] NOT NULL,
	[Published] [bit] NOT NULL,
	[Deleted] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifyDate] [datetime2](7) NULL,
	[ModifyBy] [int] NULL,
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerGuid] [uniqueidentifier] NOT NULL,
	[Username] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[EmailToRevalidate] [nvarchar](max) NULL,
	[AdminComment] [nvarchar](max) NULL,
	[IsTaxExempt] [bit] NOT NULL,
	[AffiliateId] [int] NOT NULL,
	[VendorId] [int] NOT NULL,
	[HasShoppingCartItems] [bit] NOT NULL,
	[RequireReLogin] [bit] NOT NULL,
	[FailedLoginAttempts] [int] NOT NULL,
	[CannotLoginUntilDateUtc] [datetime2](7) NULL,
	[Active] [bit] NOT NULL,
	[Deleted] [bit] NOT NULL,
	[IsSystemAccount] [bit] NOT NULL,
	[SystemName] [nvarchar](max) NULL,
	[LastIpAddress] [nvarchar](max) NULL,
	[CreatedOnUtc] [datetime2](7) NOT NULL,
	[LastLoginDateUtc] [datetime2](7) NULL,
	[LastActivityDateUtc] [datetime2](7) NOT NULL,
	[RegisteredInStoreId] [int] NOT NULL,
	[BillingAddressId] [int] NULL,
	[ShippingAddressId] [int] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmailAccount]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailAccount](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](max) NULL,
	[DisplayName] [nvarchar](max) NULL,
	[Host] [nvarchar](max) NULL,
	[Port] [int] NOT NULL,
	[Username] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[EnableSsl] [bit] NOT NULL,
	[UseDefaultCredentials] [bit] NOT NULL,
	[IsDefault] [bit] NULL,
 CONSTRAINT [PK_EmailAccount] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmailTemplate]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailTemplate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[TemplateSystemName] [nvarchar](max) NULL,
	[Template] [nvarchar](max) NULL,
	[IsMaster] [bit] NOT NULL,
	[ParentEmailTemplateId] [int] NULL,
	[BccEmailAddresses] [nvarchar](max) NULL,
	[Subject] [nvarchar](max) NULL,
	[Body] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[DelayBeforeSend] [int] NULL,
	[DelayPeriodId] [int] NOT NULL,
	[AttachedDownloadId] [int] NOT NULL,
	[EmailAccountId] [int] NOT NULL,
	[LimitedToStores] [bit] NOT NULL,
	[DelayPeriod] [int] NOT NULL,
	[AdministrationEmail] [nvarchar](max) NULL,
	[IsSystem] [bit] NOT NULL,
 CONSTRAINT [PK_EmailTemplate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Enrollment]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Enrollment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CourseId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Grade] [int] NULL,
 CONSTRAINT [PK_Enrollment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EntityMedia]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EntityMedia](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MediaId] [int] NOT NULL,
	[EntityId] [int] NOT NULL,
	[EntityName] [nvarchar](max) NULL,
	[DisplayOrder] [int] NOT NULL,
 CONSTRAINT [PK_EntityMedia] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EntityProperty]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EntityProperty](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EntityId] [int] NOT NULL,
	[EntityName] [nvarchar](max) NULL,
	[PropertyName] [nvarchar](max) NULL,
	[Value] [nvarchar](max) NULL,
	[CreatedDate] [datetime2](7) NULL,
	[ExpiredDate] [datetime2](7) NULL,
 CONSTRAINT [PK_EntityProperty] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FAQ]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FAQ](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Short] [nvarchar](max) NULL,
	[answer] [nvarchar](max) NULL,
	[faq_status] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[FaqCategoryId] [int] NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifyDate] [datetime2](7) NULL,
	[ModifyBy] [int] NULL,
	[Pageview] [int] NOT NULL,
	[MetaTitle] [nvarchar](max) NULL,
	[MetaDescription] [nvarchar](max) NULL,
	[MetaKeywords] [nvarchar](max) NULL,
	[Sequence] [int] NULL,
	[Version] [int] NULL,
 CONSTRAINT [PK_FAQ] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FaqCategory]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FaqCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Sequence] [int] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifyDate] [datetime2](7) NULL,
	[ModifyBy] [int] NULL,
	[ParentId] [int] NULL,
 CONSTRAINT [PK_FaqCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Language]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Language](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[LanguageCulture] [nvarchar](max) NULL,
	[UniqueSeoCode] [nvarchar](max) NULL,
	[FlagImageFileName] [nvarchar](max) NULL,
	[Rtl] [bit] NOT NULL,
	[LimitedToStores] [bit] NOT NULL,
	[DefaultCurrencyId] [int] NOT NULL,
	[Published] [bit] NOT NULL,
	[DisplayOrder] [int] NOT NULL,
 CONSTRAINT [PK_Language] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lesson]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lesson](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Body] [nvarchar](max) NULL,
	[Thumbnail] [nvarchar](max) NULL,
	[DisplayOrder] [int] NOT NULL,
	[Duration] [int] NULL,
	[Pageview] [int] NOT NULL,
	[IsAutoplay] [bit] NOT NULL,
	[Published] [bit] NOT NULL,
	[SourcecodeUrl] [nvarchar](max) NULL,
	[DownloadUrl] [nvarchar](max) NULL,
	[CourseId] [int] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifyDate] [datetime2](7) NULL,
	[ModifyBy] [int] NULL,
 CONSTRAINT [PK_Lesson] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LocaleStringResource]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LocaleStringResource](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LanguageId] [int] NOT NULL,
	[ResourceName] [nvarchar](max) NULL,
	[ResourceValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_LocaleStringResource] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LocalizedProperty]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LocalizedProperty](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EntityId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[LocaleKeyGroup] [nvarchar](max) NULL,
	[LocaleKey] [nvarchar](max) NULL,
	[LocaleValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_LocalizedProperty] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Manufacturer]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manufacturer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Body] [nvarchar](max) NULL,
	[MetaKeywords] [nvarchar](max) NULL,
	[MetaDescription] [nvarchar](max) NULL,
	[MetaTitle] [nvarchar](max) NULL,
	[ParentManufacturerId] [int] NULL,
	[Published] [bit] NOT NULL,
	[Deleted] [bit] NULL,
	[DisplayOrder] [int] NOT NULL,
	[CreatedOnUtc] [datetime2](7) NOT NULL,
	[UpdatedOnUtc] [datetime2](7) NULL,
	[Icon] [nvarchar](max) NULL,
 CONSTRAINT [PK_Manufacturer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Media]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Media](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[SystemName] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[AlternativeText] [nvarchar](max) NULL,
	[LocalPath] [nvarchar](max) NULL,
	[ThumbnailPath] [nvarchar](max) NULL,
	[MimeType] [nvarchar](max) NULL,
	[Binary] [varbinary](max) NULL,
	[MediaType] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[IsFeatured] [bit] NULL,
 CONSTRAINT [PK_Media] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Url] [nvarchar](max) NULL,
	[Sequence] [int] NOT NULL,
	[ParentId] [int] NULL,
	[PositionId] [int] NOT NULL,
	[NewWindow] [bit] NOT NULL,
	[Icon] [nvarchar](max) NULL,
	[Active] [bit] NULL,
	[IsSystem] [bit] NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifyDate] [datetime2](7) NULL,
	[ModifyBy] [int] NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NewsCategory]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NewsCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Short] [nvarchar](max) NULL,
	[DisplayOrder] [int] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifyDate] [datetime2](7) NULL,
	[ModifyBy] [int] NULL,
	[ParentId] [int] NULL,
 CONSTRAINT [PK_NewsCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NewsComment]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NewsComment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CommentTitle] [nvarchar](max) NULL,
	[CommentText] [nvarchar](max) NULL,
	[NewsItemId] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[IsApproved] [bit] NOT NULL,
	[StoreId] [int] NOT NULL,
	[CreatedOnUtc] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_NewsComment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NewsItem]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NewsItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Short] [nvarchar](max) NULL,
	[Full] [nvarchar](max) NULL,
	[Published] [bit] NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[StartDateUtc] [datetime2](7) NULL,
	[EndDateUtc] [datetime2](7) NULL,
	[AllowComments] [bit] NOT NULL,
	[LimitedToStores] [bit] NOT NULL,
	[MetaKeywords] [nvarchar](max) NULL,
	[MetaDescription] [nvarchar](max) NULL,
	[MetaTitle] [nvarchar](max) NULL,
	[CreatedOnUtc] [datetime2](7) NOT NULL,
	[UpdatedOnUtc] [datetime2](7) NULL,
	[Pageview] [int] NOT NULL,
	[Thumbnail] [nvarchar](max) NULL,
	[Version] [int] NOT NULL,
 CONSTRAINT [PK_NewsItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NewsItemCategory]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NewsItemCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NewsItemId] [int] NOT NULL,
	[NewsCategoryId] [int] NOT NULL,
	[DisplayOrder] [int] NOT NULL,
 CONSTRAINT [PK_NewsItemCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NewsItemTag]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NewsItemTag](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NewsItemId] [int] NOT NULL,
	[TagId] [int] NOT NULL,
	[DisplayOrder] [int] NOT NULL,
 CONSTRAINT [PK_NewsItemTag] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NewsLetterSubscription]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NewsLetterSubscription](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NewsLetterSubscriptionGuid] [uniqueidentifier] NOT NULL,
	[Email] [nvarchar](max) NULL,
	[Mobile] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[Active] [bit] NOT NULL,
	[StatusId] [int] NOT NULL,
	[Content] [nvarchar](max) NULL,
	[Subject] [nvarchar](max) NULL,
	[CustomerId] [int] NULL,
	[CreatedOnUtc] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_NewsLetterSubscription] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[IsRead] [bit] NOT NULL,
	[PublishDateTime] [datetime2](7) NOT NULL,
	[ReadDateTime] [datetime2](7) NULL,
	[EntityId] [int] NOT NULL,
	[EntityName] [nvarchar](max) NULL,
	[NotificationEventId] [int] NULL,
	[InitiatorId] [int] NOT NULL,
	[InitiatorName] [nvarchar](max) NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NotificationEvent]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotificationEvent](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EventName] [nvarchar](max) NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_NotificationEvent] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItems]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ItemOrdered_CatalogItemId] [int] NOT NULL,
	[ItemOrdered_ProductName] [nvarchar](max) NULL,
	[ItemOrdered_PictureUri] [nvarchar](max) NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
	[Units] [int] NOT NULL,
	[OrderId] [int] NULL,
 CONSTRAINT [PK_OrderItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BuyerId] [nvarchar](max) NULL,
	[OrderDate] [datetimeoffset](7) NOT NULL,
	[ShipToAddress_Street] [nvarchar](max) NULL,
	[ShipToAddress_City] [nvarchar](max) NULL,
	[ShipToAddress_State] [nvarchar](max) NULL,
	[ShipToAddress_Country] [nvarchar](max) NULL,
	[ShipToAddress_ZipCode] [nvarchar](max) NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Page]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Page](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Body] [nvarchar](max) NULL,
	[ShowInMenu] [bit] NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[Icon] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
	[Url] [nvarchar](max) NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateBy] [int] NULL,
	[ModifyDate] [datetime2](7) NULL,
	[ModifyBy] [int] NULL,
 CONSTRAINT [PK_Page] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PermissionRecord]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermissionRecord](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[SystemName] [nvarchar](max) NULL,
	[Category] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_PermissionRecord] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PermissionRole]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermissionRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PermissionRecordId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_PermissionRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Poll]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Poll](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[SystemKeyword] [nvarchar](max) NULL,
	[Published] [bit] NOT NULL,
	[ShowOnHomePage] [bit] NOT NULL,
	[AllowGuestsToVote] [bit] NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[LimitedToStores] [bit] NOT NULL,
	[StartDateUtc] [datetime2](7) NULL,
	[EndDateUtc] [datetime2](7) NULL,
 CONSTRAINT [PK_Poll] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PollAnswer]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PollAnswer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PollId] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[NumberOfVotes] [int] NOT NULL,
	[DisplayOrder] [int] NOT NULL,
 CONSTRAINT [PK_PollAnswer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PollVotingRecord]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PollVotingRecord](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PollAnswerId] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[CreatedOnUtc] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_PollVotingRecord] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductTypeId] [int] NOT NULL,
	[ParentGroupedProductId] [int] NOT NULL,
	[VisibleIndividually] [bit] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Short] [nvarchar](max) NULL,
	[Full] [nvarchar](max) NULL,
	[AdminComment] [nvarchar](max) NULL,
	[ProductTemplateId] [int] NOT NULL,
	[VendorId] [int] NOT NULL,
	[ShowOnHomePage] [bit] NOT NULL,
	[MetaKeywords] [nvarchar](max) NULL,
	[MetaDescription] [nvarchar](max) NULL,
	[MetaTitle] [nvarchar](max) NULL,
	[AllowCustomerReviews] [bit] NOT NULL,
	[ApprovedRatingSum] [int] NOT NULL,
	[NotApprovedRatingSum] [int] NOT NULL,
	[ApprovedTotalReviews] [int] NOT NULL,
	[NotApprovedTotalReviews] [int] NOT NULL,
	[SubjectToAcl] [bit] NOT NULL,
	[LimitedToStores] [bit] NOT NULL,
	[Sku] [nvarchar](max) NULL,
	[ManufacturerPartNumber] [nvarchar](max) NULL,
	[Gtin] [nvarchar](max) NULL,
	[IsGiftCard] [bit] NOT NULL,
	[GiftCardTypeId] [int] NOT NULL,
	[OverriddenGiftCardAmount] [decimal](18, 2) NULL,
	[RequireOtherProducts] [bit] NOT NULL,
	[RequiredProductIds] [nvarchar](max) NULL,
	[AutomaticallyAddRequiredProducts] [bit] NOT NULL,
	[IsDownload] [bit] NOT NULL,
	[DownloadId] [int] NOT NULL,
	[UnlimitedDownloads] [bit] NOT NULL,
	[MaxNumberOfDownloads] [int] NOT NULL,
	[DownloadExpirationDays] [int] NULL,
	[DownloadActivationTypeId] [int] NOT NULL,
	[HasSampleDownload] [bit] NOT NULL,
	[SampleDownloadId] [int] NOT NULL,
	[HasUserAgreement] [bit] NOT NULL,
	[UserAgreementText] [nvarchar](max) NULL,
	[IsRecurring] [bit] NOT NULL,
	[RecurringCycleLength] [int] NOT NULL,
	[RecurringCyclePeriodId] [int] NOT NULL,
	[RecurringTotalCycles] [int] NOT NULL,
	[IsRental] [bit] NOT NULL,
	[RentalPriceLength] [int] NOT NULL,
	[RentalPricePeriodId] [int] NOT NULL,
	[IsShipEnabled] [bit] NOT NULL,
	[IsFreeShipping] [bit] NOT NULL,
	[ShipSeparately] [bit] NOT NULL,
	[AdditionalShippingCharge] [decimal](18, 2) NOT NULL,
	[DeliveryDateId] [int] NOT NULL,
	[IsTaxExempt] [bit] NOT NULL,
	[TaxCategoryId] [int] NOT NULL,
	[IsTelecommunicationsOrBroadcastingOrElectronicServices] [bit] NOT NULL,
	[ManageInventoryMethodId] [int] NOT NULL,
	[ProductAvailabilityRangeId] [int] NOT NULL,
	[UseMultipleWarehouses] [bit] NOT NULL,
	[WarehouseId] [int] NOT NULL,
	[StockQuantity] [int] NOT NULL,
	[DisplayStockAvailability] [bit] NOT NULL,
	[DisplayStockQuantity] [bit] NOT NULL,
	[MinStockQuantity] [int] NOT NULL,
	[LowStockActivityId] [int] NOT NULL,
	[NotifyAdminForQuantityBelow] [int] NOT NULL,
	[BackorderModeId] [int] NOT NULL,
	[AllowBackInStockSubscriptions] [bit] NOT NULL,
	[OrderMinimumQuantity] [int] NOT NULL,
	[OrderMaximumQuantity] [int] NOT NULL,
	[AllowedQuantities] [nvarchar](max) NULL,
	[AllowAddingOnlyExistingAttributeCombinations] [bit] NOT NULL,
	[NotReturnable] [bit] NOT NULL,
	[DisableBuyButton] [bit] NOT NULL,
	[DisableWishlistButton] [bit] NOT NULL,
	[AvailableForPreOrder] [bit] NOT NULL,
	[PreOrderAvailabilityStartDateTimeUtc] [datetime2](7) NULL,
	[CallForPrice] [bit] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[OldPrice] [decimal](18, 2) NOT NULL,
	[ProductCost] [decimal](18, 2) NOT NULL,
	[CustomerEntersPrice] [bit] NOT NULL,
	[MinimumCustomerEnteredPrice] [decimal](18, 2) NOT NULL,
	[MaximumCustomerEnteredPrice] [decimal](18, 2) NOT NULL,
	[BasepriceEnabled] [bit] NOT NULL,
	[BasepriceAmount] [decimal](18, 2) NOT NULL,
	[BasepriceUnitId] [int] NOT NULL,
	[BasepriceBaseAmount] [decimal](18, 2) NOT NULL,
	[BasepriceBaseUnitId] [int] NOT NULL,
	[MarkAsNew] [bit] NOT NULL,
	[MarkAsNewStartDateTimeUtc] [datetime2](7) NULL,
	[MarkAsNewEndDateTimeUtc] [datetime2](7) NULL,
	[HasTierPrices] [bit] NOT NULL,
	[HasDiscountsApplied] [bit] NOT NULL,
	[Weight] [decimal](18, 2) NOT NULL,
	[Length] [decimal](18, 2) NOT NULL,
	[Width] [decimal](18, 2) NOT NULL,
	[Height] [decimal](18, 2) NOT NULL,
	[AvailableStartDateTimeUtc] [datetime2](7) NULL,
	[AvailableEndDateTimeUtc] [datetime2](7) NULL,
	[DisplayOrder] [int] NOT NULL,
	[Published] [bit] NOT NULL,
	[Deleted] [bit] NOT NULL,
	[CreatedOnUtc] [datetime2](7) NOT NULL,
	[UpdatedOnUtc] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductAttribute]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductAttribute](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_ProductAttribute] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductCategory]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[IsFeaturedProduct] [bit] NOT NULL,
	[DisplayOrder] [int] NOT NULL,
 CONSTRAINT [PK_ProductCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductManufacturer]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductManufacturer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[ManufacturerId] [int] NOT NULL,
	[IsFeatured] [bit] NOT NULL,
	[DisplayOrder] [int] NOT NULL,
 CONSTRAINT [PK_ProductManufacturer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductMedia]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductMedia](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[MediaId] [int] NOT NULL,
	[DisplayOrder] [int] NOT NULL,
 CONSTRAINT [PK_ProductMedia] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductReview]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductReview](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[StoreId] [int] NOT NULL,
	[IsApproved] [bit] NOT NULL,
	[Title] [nvarchar](max) NULL,
	[ReviewText] [nvarchar](max) NULL,
	[ReplyText] [nvarchar](max) NULL,
	[CustomerNotifiedOfReply] [bit] NOT NULL,
	[Rating] [int] NOT NULL,
	[HelpfulYesTotal] [int] NOT NULL,
	[HelpfulNoTotal] [int] NOT NULL,
	[CreatedOnUtc] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ProductReview] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductTemplate]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductTemplate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[ViewPath] [nvarchar](max) NULL,
	[DisplayOrder] [int] NOT NULL,
	[IgnoredProductTypes] [nvarchar](max) NULL,
 CONSTRAINT [PK_ProductTemplate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QueuedEmail]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QueuedEmail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PriorityId] [int] NOT NULL,
	[From] [nvarchar](max) NULL,
	[FromName] [nvarchar](max) NULL,
	[To] [nvarchar](max) NULL,
	[ToName] [nvarchar](max) NULL,
	[ReplyTo] [nvarchar](max) NULL,
	[ReplyToName] [nvarchar](max) NULL,
	[CC] [nvarchar](max) NULL,
	[Bcc] [nvarchar](max) NULL,
	[Subject] [nvarchar](max) NULL,
	[Body] [nvarchar](max) NULL,
	[AttachmentFilePath] [nvarchar](max) NULL,
	[AttachmentFileName] [nvarchar](max) NULL,
	[AttachedDownloadId] [int] NOT NULL,
	[CreatedOnUtc] [datetime2](7) NOT NULL,
	[DontSendBeforeDateUtc] [datetime2](7) NULL,
	[SentTries] [int] NOT NULL,
	[SentOnUtc] [datetime2](7) NULL,
	[EmailAccountId] [int] NOT NULL,
	[Priority] [int] NOT NULL,
 CONSTRAINT [PK_QueuedEmail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](max) NULL,
	[IsSystemRole] [bit] NOT NULL,
	[SystemName] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Setting]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Setting](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Value] [nvarchar](max) NULL,
	[GroupName] [nvarchar](max) NULL,
	[StoreId] [int] NOT NULL,
 CONSTRAINT [PK_Setting] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Skill]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Skill](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[FeaturedImageId] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Skill] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StateProvince]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StateProvince](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CountryId] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Abbreviation] [nvarchar](max) NULL,
	[Published] [bit] NOT NULL,
	[DisplayOrder] [int] NOT NULL,
 CONSTRAINT [PK_StateProvince] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Store]    Script Date: 8/9/2018 1:43:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Store](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Url] [nvarchar](max) NULL,
	[SslEnabled] [bit] NOT NULL,
	[Hosts] [nvarchar](max) NULL,
	[DefaultLanguageId] [int] NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[CompanyName] [nvarchar](max) NULL,
	[CompanyAddress] [nvarchar](max) NULL,
	[CompanyPhoneNumber] [nvarchar](max) NULL,
	[CompanyVat] [nvarchar](max) NULL,
 CONSTRAINT [PK_Store] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tag]    Script Date: 8/9/2018 1:43:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tag](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[DisplayOrder] [int] NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Tag] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UrlRecord]    Script Date: 8/9/2018 1:43:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UrlRecord](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EntityId] [int] NOT NULL,
	[EntityName] [nvarchar](max) NULL,
	[Slug] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[LanguageId] [int] NOT NULL,
 CONSTRAINT [PK_UrlRecord] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 8/9/2018 1:43:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Guid] [uniqueidentifier] NOT NULL,
	[FullName] [nvarchar](max) NULL,
	[UserName] [nvarchar](max) NULL,
	[BirthDay] [datetime2](7) NULL,
	[Email] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[PasswordSalt] [nvarchar](max) NULL,
	[PasswordFormat] [int] NOT NULL,
	[Address] [nvarchar](max) NULL,
	[Remarks] [nvarchar](max) NULL,
	[LastIpAddress] [nvarchar](max) NULL,
	[CreatedOnUtc] [datetime2](7) NOT NULL,
	[LastLoginDateUtc] [datetime2](7) NULL,
	[FailedLoginAttempts] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[LastActivityDateUtc] [datetime2](7) NULL,
	[IsSystemAccount] [bit] NOT NULL,
	[IsAdmin] [bit] NOT NULL,
	[Deleted] [bit] NOT NULL,
	[Gender] [int] NULL,
	[IdentityCard] [nvarchar](max) NULL,
	[CustomerType] [int] NULL,
	[ProvinceId] [int] NULL,
	[StudentYear] [int] NULL,
	[StudentCategory] [nvarchar](max) NULL,
	[StudentUniversity] [nvarchar](max) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 8/9/2018 1:43:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserSkill]    Script Date: 8/9/2018 1:43:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserSkill](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SkillId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[ExternalUrl] [nvarchar](max) NULL,
	[DisplayOrder] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserSkill] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20180808084354_Initial', N'2.1.1-rtm-30846')
GO
SET IDENTITY_INSERT [dbo].[Catalog] ON 
GO
INSERT [dbo].[Catalog] ([Id], [Name], [Description], [Price], [PictureUri], [CatalogTypeId], [CatalogBrandId]) VALUES (1, N'.NET Bot Black Sweatshirt', N'.NET Bot Black Sweatshirt', CAST(19.50 AS Decimal(18, 2)), N'http://catalogbaseurltobereplaced/images/products/1.png', 2, 2)
GO
INSERT [dbo].[Catalog] ([Id], [Name], [Description], [Price], [PictureUri], [CatalogTypeId], [CatalogBrandId]) VALUES (2, N'.NET Black & White Mug', N'.NET Black & White Mug', CAST(8.50 AS Decimal(18, 2)), N'http://catalogbaseurltobereplaced/images/products/2.png', 1, 2)
GO
INSERT [dbo].[Catalog] ([Id], [Name], [Description], [Price], [PictureUri], [CatalogTypeId], [CatalogBrandId]) VALUES (3, N'Prism White T-Shirt', N'Prism White T-Shirt', CAST(12.00 AS Decimal(18, 2)), N'http://catalogbaseurltobereplaced/images/products/3.png', 2, 5)
GO
INSERT [dbo].[Catalog] ([Id], [Name], [Description], [Price], [PictureUri], [CatalogTypeId], [CatalogBrandId]) VALUES (4, N'.NET Foundation Sweatshirt', N'.NET Foundation Sweatshirt', CAST(12.00 AS Decimal(18, 2)), N'http://catalogbaseurltobereplaced/images/products/4.png', 2, 2)
GO
INSERT [dbo].[Catalog] ([Id], [Name], [Description], [Price], [PictureUri], [CatalogTypeId], [CatalogBrandId]) VALUES (5, N'Roslyn Red Sheet', N'Roslyn Red Sheet', CAST(8.50 AS Decimal(18, 2)), N'http://catalogbaseurltobereplaced/images/products/5.png', 3, 5)
GO
INSERT [dbo].[Catalog] ([Id], [Name], [Description], [Price], [PictureUri], [CatalogTypeId], [CatalogBrandId]) VALUES (6, N'.NET Blue Sweatshirt', N'.NET Blue Sweatshirt', CAST(12.00 AS Decimal(18, 2)), N'http://catalogbaseurltobereplaced/images/products/6.png', 2, 2)
GO
INSERT [dbo].[Catalog] ([Id], [Name], [Description], [Price], [PictureUri], [CatalogTypeId], [CatalogBrandId]) VALUES (7, N'Roslyn Red T-Shirt', N'Roslyn Red T-Shirt', CAST(12.00 AS Decimal(18, 2)), N'http://catalogbaseurltobereplaced/images/products/7.png', 2, 5)
GO
INSERT [dbo].[Catalog] ([Id], [Name], [Description], [Price], [PictureUri], [CatalogTypeId], [CatalogBrandId]) VALUES (8, N'Kudu Purple Sweatshirt', N'Kudu Purple Sweatshirt', CAST(8.50 AS Decimal(18, 2)), N'http://catalogbaseurltobereplaced/images/products/8.png', 2, 5)
GO
INSERT [dbo].[Catalog] ([Id], [Name], [Description], [Price], [PictureUri], [CatalogTypeId], [CatalogBrandId]) VALUES (9, N'Cup<T> White Mug', N'Cup<T> White Mug', CAST(12.00 AS Decimal(18, 2)), N'http://catalogbaseurltobereplaced/images/products/9.png', 1, 5)
GO
INSERT [dbo].[Catalog] ([Id], [Name], [Description], [Price], [PictureUri], [CatalogTypeId], [CatalogBrandId]) VALUES (10, N'.NET Foundation Sheet', N'.NET Foundation Sheet', CAST(12.00 AS Decimal(18, 2)), N'http://catalogbaseurltobereplaced/images/products/10.png', 3, 2)
GO
INSERT [dbo].[Catalog] ([Id], [Name], [Description], [Price], [PictureUri], [CatalogTypeId], [CatalogBrandId]) VALUES (11, N'Cup<T> Sheet', N'Cup<T> Sheet', CAST(8.50 AS Decimal(18, 2)), N'http://catalogbaseurltobereplaced/images/products/11.png', 3, 2)
GO
INSERT [dbo].[Catalog] ([Id], [Name], [Description], [Price], [PictureUri], [CatalogTypeId], [CatalogBrandId]) VALUES (12, N'Prism White TShirt', N'Prism White TShirt', CAST(12.00 AS Decimal(18, 2)), N'http://catalogbaseurltobereplaced/images/products/12.png', 2, 5)
GO
SET IDENTITY_INSERT [dbo].[Catalog] OFF
GO
SET IDENTITY_INSERT [dbo].[CatalogBrand] ON 
GO
INSERT [dbo].[CatalogBrand] ([Id], [Brand]) VALUES (1, N'Azure')
GO
INSERT [dbo].[CatalogBrand] ([Id], [Brand]) VALUES (2, N'.NET')
GO
INSERT [dbo].[CatalogBrand] ([Id], [Brand]) VALUES (3, N'Visual Studio')
GO
INSERT [dbo].[CatalogBrand] ([Id], [Brand]) VALUES (4, N'SQL Server')
GO
INSERT [dbo].[CatalogBrand] ([Id], [Brand]) VALUES (5, N'Other')
GO
SET IDENTITY_INSERT [dbo].[CatalogBrand] OFF
GO
SET IDENTITY_INSERT [dbo].[CatalogType] ON 
GO
INSERT [dbo].[CatalogType] ([Id], [Type]) VALUES (1, N'Mug')
GO
INSERT [dbo].[CatalogType] ([Id], [Type]) VALUES (2, N'T-Shirt')
GO
INSERT [dbo].[CatalogType] ([Id], [Type]) VALUES (3, N'Sheet')
GO
INSERT [dbo].[CatalogType] ([Id], [Type]) VALUES (4, N'USB Memory Stick')
GO
SET IDENTITY_INSERT [dbo].[CatalogType] OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON 
GO
INSERT [dbo].[Category] ([Id], [Name], [Description], [CategoryTemplateId], [MetaKeywords], [MetaDescription], [MetaTitle], [ParentCategoryId], [PictureId], [PageSize], [AllowCustomersToSelectPageSize], [PageSizeOptions], [PriceRanges], [ShowOnHomePage], [IncludeInTopMenu], [SubjectToAcl], [LimitedToStores], [Published], [Deleted], [DisplayOrder], [CreatedOnUtc], [UpdatedOnUtc]) VALUES (1, N'Laptop', NULL, 0, NULL, NULL, NULL, 0, 0, 0, 0, NULL, NULL, 0, 0, 0, 0, 1, 0, 1, CAST(N'2018-08-08T17:12:41.7446133' AS DateTime2), NULL)
GO
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[EmailAccount] ON 
GO
INSERT [dbo].[EmailAccount] ([Id], [Email], [DisplayName], [Host], [Port], [Username], [Password], [EnableSsl], [UseDefaultCredentials], [IsDefault]) VALUES (1, N'mailer@ecommerce.vnit.top', N'Ecommerce', N'smtp.ecommerce.vnit.top', 485, N'mailer@ecommerce.vnit.top', N'password', 1, 0, 1)
GO
INSERT [dbo].[EmailAccount] ([Id], [Email], [DisplayName], [Host], [Port], [Username], [Password], [EnableSsl], [UseDefaultCredentials], [IsDefault]) VALUES (2, N'support@ecommerce.vnit.top', N'Ecommerce', N'smtp.gmail.com', 587, N'haylamvietnam@gmail.com', N'123456@A', 1, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[EmailAccount] OFF
GO
SET IDENTITY_INSERT [dbo].[EmailTemplate] ON 
GO
INSERT [dbo].[EmailTemplate] ([Id], [Name], [TemplateSystemName], [Template], [IsMaster], [ParentEmailTemplateId], [BccEmailAddresses], [Subject], [Body], [IsActive], [DelayBeforeSend], [DelayPeriodId], [AttachedDownloadId], [EmailAccountId], [LimitedToStores], [DelayPeriod], [AdministrationEmail], [IsSystem]) VALUES (1, N'Master', N'Master', N'', 1, NULL, NULL, N'Master Template', NULL, 0, NULL, 0, 0, 2, 0, 0, N'caothetoan@gmail.com', 1)
GO
INSERT [dbo].[EmailTemplate] ([Id], [Name], [TemplateSystemName], [Template], [IsMaster], [ParentEmailTemplateId], [BccEmailAddresses], [Subject], [Body], [IsActive], [DelayBeforeSend], [DelayPeriodId], [AttachedDownloadId], [EmailAccountId], [LimitedToStores], [DelayPeriod], [AdministrationEmail], [IsSystem]) VALUES (2, N'User Registered', N'User.Registered', N'', 0, 1, NULL, N'Your account has been created', NULL, 0, NULL, 0, 0, 2, 0, 0, N'caothetoan@gmail.com', 1)
GO
INSERT [dbo].[EmailTemplate] ([Id], [Name], [TemplateSystemName], [Template], [IsMaster], [ParentEmailTemplateId], [BccEmailAddresses], [Subject], [Body], [IsActive], [DelayBeforeSend], [DelayPeriodId], [AttachedDownloadId], [EmailAccountId], [LimitedToStores], [DelayPeriod], [AdministrationEmail], [IsSystem]) VALUES (3, N'User Registered Administrator', N'User.Registered.Admin', N'', 0, 1, NULL, N'A new user has registered', NULL, 0, NULL, 0, 0, 2, 0, 0, N'caothetoan@gmail.com', 1)
GO
INSERT [dbo].[EmailTemplate] ([Id], [Name], [TemplateSystemName], [Template], [IsMaster], [ParentEmailTemplateId], [BccEmailAddresses], [Subject], [Body], [IsActive], [DelayBeforeSend], [DelayPeriodId], [AttachedDownloadId], [EmailAccountId], [LimitedToStores], [DelayPeriod], [AdministrationEmail], [IsSystem]) VALUES (4, N'User Activated', N'User.Activated', N'', 0, 1, NULL, N'Your account has been activated', NULL, 0, NULL, 0, 0, 2, 0, 0, N'caothetoan@gmail.com', 1)
GO
INSERT [dbo].[EmailTemplate] ([Id], [Name], [TemplateSystemName], [Template], [IsMaster], [ParentEmailTemplateId], [BccEmailAddresses], [Subject], [Body], [IsActive], [DelayBeforeSend], [DelayPeriodId], [AttachedDownloadId], [EmailAccountId], [LimitedToStores], [DelayPeriod], [AdministrationEmail], [IsSystem]) VALUES (5, N'User Activation Link', N'User.ActivationLink', N'', 0, 1, NULL, N'Activate your account', NULL, 0, NULL, 0, 0, 2, 0, 0, N'caothetoan@gmail.com', 1)
GO
INSERT [dbo].[EmailTemplate] ([Id], [Name], [TemplateSystemName], [Template], [IsMaster], [ParentEmailTemplateId], [BccEmailAddresses], [Subject], [Body], [IsActive], [DelayBeforeSend], [DelayPeriodId], [AttachedDownloadId], [EmailAccountId], [LimitedToStores], [DelayPeriod], [AdministrationEmail], [IsSystem]) VALUES (6, N'Password Recovery Link', N'Common.PasswordRecovery', N'', 0, 1, NULL, N'We have received a password reset request', NULL, 0, NULL, 0, 0, 2, 0, 0, N'caothetoan@gmail.com', 1)
GO
INSERT [dbo].[EmailTemplate] ([Id], [Name], [TemplateSystemName], [Template], [IsMaster], [ParentEmailTemplateId], [BccEmailAddresses], [Subject], [Body], [IsActive], [DelayBeforeSend], [DelayPeriodId], [AttachedDownloadId], [EmailAccountId], [LimitedToStores], [DelayPeriod], [AdministrationEmail], [IsSystem]) VALUES (7, N'Password Changed', N'Common.PasswordChanged', N'', 0, 1, NULL, N'Your password has been changed', NULL, 0, NULL, 0, 0, 2, 0, 0, N'caothetoan@gmail.com', 1)
GO
INSERT [dbo].[EmailTemplate] ([Id], [Name], [TemplateSystemName], [Template], [IsMaster], [ParentEmailTemplateId], [BccEmailAddresses], [Subject], [Body], [IsActive], [DelayBeforeSend], [DelayPeriodId], [AttachedDownloadId], [EmailAccountId], [LimitedToStores], [DelayPeriod], [AdministrationEmail], [IsSystem]) VALUES (8, N'User Account Deactivated', N'User.Deactivated', N'', 0, 1, NULL, N'Your account has been deactivated', NULL, 0, NULL, 0, 0, 2, 0, 0, N'caothetoan@gmail.com', 1)
GO
INSERT [dbo].[EmailTemplate] ([Id], [Name], [TemplateSystemName], [Template], [IsMaster], [ParentEmailTemplateId], [BccEmailAddresses], [Subject], [Body], [IsActive], [DelayBeforeSend], [DelayPeriodId], [AttachedDownloadId], [EmailAccountId], [LimitedToStores], [DelayPeriod], [AdministrationEmail], [IsSystem]) VALUES (9, N'User Account Deactivated Administrator', N'User.Deactivated.Admin', N'', 0, 1, NULL, N'Your account has been deactivated', NULL, 0, NULL, 0, 0, 2, 0, 0, N'caothetoan@gmail.com', 1)
GO
INSERT [dbo].[EmailTemplate] ([Id], [Name], [TemplateSystemName], [Template], [IsMaster], [ParentEmailTemplateId], [BccEmailAddresses], [Subject], [Body], [IsActive], [DelayBeforeSend], [DelayPeriodId], [AttachedDownloadId], [EmailAccountId], [LimitedToStores], [DelayPeriod], [AdministrationEmail], [IsSystem]) VALUES (10, N'User Account Deleted', N'User.AccountDeleted', N'', 0, 1, NULL, N'Your account has been deleted', NULL, 0, NULL, 0, 0, 2, 0, 0, N'caothetoan@gmail.com', 1)
GO
INSERT [dbo].[EmailTemplate] ([Id], [Name], [TemplateSystemName], [Template], [IsMaster], [ParentEmailTemplateId], [BccEmailAddresses], [Subject], [Body], [IsActive], [DelayBeforeSend], [DelayPeriodId], [AttachedDownloadId], [EmailAccountId], [LimitedToStores], [DelayPeriod], [AdministrationEmail], [IsSystem]) VALUES (11, N'User Account Deleted Administrator', N'User.AccountDeleted.Admin', N'', 0, 1, NULL, N'A user account has been deleted', NULL, 0, NULL, 0, 0, 2, 0, 0, N'caothetoan@gmail.com', 1)
GO
SET IDENTITY_INSERT [dbo].[EmailTemplate] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 
GO
INSERT [dbo].[Product] ([Id], [ProductTypeId], [ParentGroupedProductId], [VisibleIndividually], [Name], [Short], [Full], [AdminComment], [ProductTemplateId], [VendorId], [ShowOnHomePage], [MetaKeywords], [MetaDescription], [MetaTitle], [AllowCustomerReviews], [ApprovedRatingSum], [NotApprovedRatingSum], [ApprovedTotalReviews], [NotApprovedTotalReviews], [SubjectToAcl], [LimitedToStores], [Sku], [ManufacturerPartNumber], [Gtin], [IsGiftCard], [GiftCardTypeId], [OverriddenGiftCardAmount], [RequireOtherProducts], [RequiredProductIds], [AutomaticallyAddRequiredProducts], [IsDownload], [DownloadId], [UnlimitedDownloads], [MaxNumberOfDownloads], [DownloadExpirationDays], [DownloadActivationTypeId], [HasSampleDownload], [SampleDownloadId], [HasUserAgreement], [UserAgreementText], [IsRecurring], [RecurringCycleLength], [RecurringCyclePeriodId], [RecurringTotalCycles], [IsRental], [RentalPriceLength], [RentalPricePeriodId], [IsShipEnabled], [IsFreeShipping], [ShipSeparately], [AdditionalShippingCharge], [DeliveryDateId], [IsTaxExempt], [TaxCategoryId], [IsTelecommunicationsOrBroadcastingOrElectronicServices], [ManageInventoryMethodId], [ProductAvailabilityRangeId], [UseMultipleWarehouses], [WarehouseId], [StockQuantity], [DisplayStockAvailability], [DisplayStockQuantity], [MinStockQuantity], [LowStockActivityId], [NotifyAdminForQuantityBelow], [BackorderModeId], [AllowBackInStockSubscriptions], [OrderMinimumQuantity], [OrderMaximumQuantity], [AllowedQuantities], [AllowAddingOnlyExistingAttributeCombinations], [NotReturnable], [DisableBuyButton], [DisableWishlistButton], [AvailableForPreOrder], [PreOrderAvailabilityStartDateTimeUtc], [CallForPrice], [Price], [OldPrice], [ProductCost], [CustomerEntersPrice], [MinimumCustomerEnteredPrice], [MaximumCustomerEnteredPrice], [BasepriceEnabled], [BasepriceAmount], [BasepriceUnitId], [BasepriceBaseAmount], [BasepriceBaseUnitId], [MarkAsNew], [MarkAsNewStartDateTimeUtc], [MarkAsNewEndDateTimeUtc], [HasTierPrices], [HasDiscountsApplied], [Weight], [Length], [Width], [Height], [AvailableStartDateTimeUtc], [AvailableEndDateTimeUtc], [DisplayOrder], [Published], [Deleted], [CreatedOnUtc], [UpdatedOnUtc]) VALUES (1, 0, 0, 0, N'Macbook Pro', NULL, NULL, NULL, 0, 0, 0, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, NULL, NULL, NULL, 0, 0, NULL, 0, NULL, 0, 0, 0, 0, 0, NULL, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, CAST(0.00 AS Decimal(18, 2)), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, NULL, 0, 0, 0, 0, 0, NULL, 0, CAST(30000000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, CAST(0.00 AS Decimal(18, 2)), 0, CAST(0.00 AS Decimal(18, 2)), 0, 0, NULL, NULL, 0, 0, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), NULL, NULL, 0, 1, 0, CAST(N'2018-08-08T17:12:41.7446133' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductCategory] ON 
GO
INSERT [dbo].[ProductCategory] ([Id], [ProductId], [CategoryId], [IsFeaturedProduct], [DisplayOrder]) VALUES (1, 1, 1, 0, 0)
GO
SET IDENTITY_INSERT [dbo].[ProductCategory] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 
GO
INSERT [dbo].[Role] ([Id], [RoleName], [IsSystemRole], [SystemName], [IsActive]) VALUES (1, N'Administrator', 1, N'Administrator', 1)
GO
INSERT [dbo].[Role] ([Id], [RoleName], [IsSystemRole], [SystemName], [IsActive]) VALUES (2, N'Registered', 1, N'Registered', 1)
GO
INSERT [dbo].[Role] ([Id], [RoleName], [IsSystemRole], [SystemName], [IsActive]) VALUES (3, N'Visitor', 1, N'Visitor', 1)
GO
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Setting] ON 
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (1, N'Domain', N'ecommerce.vnit.top', N'GeneralSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (2, N'MediaSaveLocation', N'', N'GeneralSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (3, N'WebsiteApiUrl', N'ecommerce.vnit.top/api', N'GeneralSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (4, N'AutomationApiUrl', N'ecommerce.vnit.top/api', N'GeneralSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (5, N'ApplicationCookieDomain', N'ecommerce.vnit.top', N'GeneralSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (6, N'ImageServerDomain', N'//ecommerce.vnit.top/api', N'GeneralSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (7, N'VideoServerDomain', N'//ecommerce.vnit.top/api', N'GeneralSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (8, N'DefaultTimeZoneId', N'', N'GeneralSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (9, N'SettingVat', N'0', N'GeneralSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (10, N'IsInstalled', N'True', N'GeneralSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (11, N'MaximumFileUploadSizeForImages', N'0', N'MediaSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (12, N'MaximumFileUploadSizeForVideos', N'0', N'MediaSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (13, N'MaximumFileUploadSizeForDocuments', N'0', N'MediaSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (14, N'PictureSavePath', N'~/Content/Media/Uploads/Images', N'MediaSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (15, N'PictureSaveLocation', N'FileSystem', N'MediaSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (16, N'VideoSavePath', N'~/Content/Media/Uploads/Videos', N'MediaSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (17, N'OtherMediaSavePath', N'~/Content/Media/Uploads/Others', N'MediaSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (18, N'OtherMediaSaveLocation', N'FileSystem', N'MediaSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (19, N'ThumbnailPictureSize', N'100x100', N'MediaSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (20, N'SmallProfilePictureSize', N'64x64', N'MediaSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (21, N'MediumProfilePictureSize', N'128x128', N'MediaSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (22, N'SmallCoverPictureSize', N'300x50', N'MediaSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (23, N'MediumCoverPictureSize', N'800x300', N'MediaSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (24, N'DefaultUserProfileImageUrl', N'~/Content/Media/d_male.jpg', N'MediaSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (25, N'DefaultUserProfileCoverUrl', N'~/Content/Media/d_cover.jpg', N'MediaSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (26, N'DefaultArticleImageUrl', N'', N'MediaSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (27, N'DefaultServicePackImageUrl', N'', N'MediaSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (28, N'DefaultServiceImageUrl', N'', N'MediaSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (29, N'DefaultImageUrl', N'', N'MediaSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (30, N'GenerateThumbnailUrl', N'', N'MediaSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (31, N'UserRegistrationDefaultMode', N'WithActivationEmail', N'UserSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (32, N'PasswordFormat', N'0', N'UserSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (33, N'SaltLength', N'0', N'UserSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (34, N'AreUserNamesEnabled', N'False', N'UserSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (35, N'RequireEmailForUserActivation', N'False', N'UserSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (36, N'UserLinkTemplate', N'<a href='''' data-uid=''{0}''>{1}</a>', N'UserSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (37, N'PeopleSearchTermMinimumLength', N'0', N'UserSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (38, N'ActivationPageUrl', N'ecommerce.vnit.top/activate', N'UserSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (39, N'PasswordResetPageUrl', N'', N'UserSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (40, N'MaxFailedLoginAttempts', N'0', N'UserSettings', 0)
GO
INSERT [dbo].[Setting] ([Id], [Name], [Value], [GroupName], [StoreId]) VALUES (41, N'DefaultPasswordStorageFormat', N'Sha1Hashed', N'UserSettings', 0)
GO
SET IDENTITY_INSERT [dbo].[Setting] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([Id], [Guid], [FullName], [UserName], [BirthDay], [Email], [Phone], [Password], [PasswordSalt], [PasswordFormat], [Address], [Remarks], [LastIpAddress], [CreatedOnUtc], [LastLoginDateUtc], [FailedLoginAttempts], [Active], [LastActivityDateUtc], [IsSystemAccount], [IsAdmin], [Deleted], [Gender], [IdentityCard], [CustomerType], [ProvinceId], [StudentYear], [StudentCategory], [StudentUniversity]) VALUES (1, N'1331c2bb-52ee-4f64-a718-bd99a67054fd', NULL, N'caothetoan@gmail.com', NULL, N'caothetoan@gmail.com', NULL, N'fBvAxKgz8iPjRoncVapZraxfxfg=', N'90yWFUcJJoM=', 0, NULL, NULL, N'', CAST(N'2018-08-08T10:12:38.9286133' AS DateTime2), CAST(N'2018-08-08T10:12:38.9286133' AS DateTime2), 0, 1, CAST(N'2018-08-08T10:12:38.9286133' AS DateTime2), 0, 0, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRole] ON 
GO
INSERT [dbo].[UserRole] ([Id], [UserId], [RoleId]) VALUES (1, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[UserRole] OFF
GO
/****** Object:  Index [IX_Address_CountryId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_Address_CountryId] ON [dbo].[Address]
(
	[CountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Address_StateProvinceId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_Address_StateProvinceId] ON [dbo].[Address]
(
	[StateProvinceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Assessment_CourseId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_Assessment_CourseId] ON [dbo].[Assessment]
(
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Assessment_UserId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_Assessment_UserId] ON [dbo].[Assessment]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_BasketItem_BasketId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_BasketItem_BasketId] ON [dbo].[BasketItem]
(
	[BasketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Catalog_CatalogBrandId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_Catalog_CatalogBrandId] ON [dbo].[Catalog]
(
	[CatalogBrandId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Catalog_CatalogTypeId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_Catalog_CatalogTypeId] ON [dbo].[Catalog]
(
	[CatalogTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmailTemplate_EmailAccountId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmailTemplate_EmailAccountId] ON [dbo].[EmailTemplate]
(
	[EmailAccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmailTemplate_ParentEmailTemplateId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmailTemplate_ParentEmailTemplateId] ON [dbo].[EmailTemplate]
(
	[ParentEmailTemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Enrollment_CourseId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_Enrollment_CourseId] ON [dbo].[Enrollment]
(
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Enrollment_UserId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_Enrollment_UserId] ON [dbo].[Enrollment]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EntityMedia_MediaId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_EntityMedia_MediaId] ON [dbo].[EntityMedia]
(
	[MediaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FAQ_FaqCategoryId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_FAQ_FaqCategoryId] ON [dbo].[FAQ]
(
	[FaqCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Lesson_CourseId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_Lesson_CourseId] ON [dbo].[Lesson]
(
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LocaleStringResource_LanguageId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_LocaleStringResource_LanguageId] ON [dbo].[LocaleStringResource]
(
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LocalizedProperty_LanguageId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_LocalizedProperty_LanguageId] ON [dbo].[LocalizedProperty]
(
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_NewsComment_CustomerId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_NewsComment_CustomerId] ON [dbo].[NewsComment]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_NewsComment_NewsItemId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_NewsComment_NewsItemId] ON [dbo].[NewsComment]
(
	[NewsItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_NewsComment_StoreId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_NewsComment_StoreId] ON [dbo].[NewsComment]
(
	[StoreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_NewsItem_LanguageId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_NewsItem_LanguageId] ON [dbo].[NewsItem]
(
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_NewsItemCategory_NewsCategoryId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_NewsItemCategory_NewsCategoryId] ON [dbo].[NewsItemCategory]
(
	[NewsCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_NewsItemCategory_NewsItemId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_NewsItemCategory_NewsItemId] ON [dbo].[NewsItemCategory]
(
	[NewsItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_NewsItemTag_NewsItemId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_NewsItemTag_NewsItemId] ON [dbo].[NewsItemTag]
(
	[NewsItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_NewsItemTag_TagId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_NewsItemTag_TagId] ON [dbo].[NewsItemTag]
(
	[TagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Notification_CustomerId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_Notification_CustomerId] ON [dbo].[Notification]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Notification_NotificationEventId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_Notification_NotificationEventId] ON [dbo].[Notification]
(
	[NotificationEventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderItems_OrderId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_OrderItems_OrderId] ON [dbo].[OrderItems]
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PermissionRole_PermissionRecordId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_PermissionRole_PermissionRecordId] ON [dbo].[PermissionRole]
(
	[PermissionRecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PermissionRole_RoleId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_PermissionRole_RoleId] ON [dbo].[PermissionRole]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Poll_LanguageId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_Poll_LanguageId] ON [dbo].[Poll]
(
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PollAnswer_PollId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_PollAnswer_PollId] ON [dbo].[PollAnswer]
(
	[PollId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PollVotingRecord_CustomerId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_PollVotingRecord_CustomerId] ON [dbo].[PollVotingRecord]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PollVotingRecord_PollAnswerId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_PollVotingRecord_PollAnswerId] ON [dbo].[PollVotingRecord]
(
	[PollAnswerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductCategory_CategoryId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_ProductCategory_CategoryId] ON [dbo].[ProductCategory]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductCategory_ProductId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_ProductCategory_ProductId] ON [dbo].[ProductCategory]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductManufacturer_ManufacturerId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_ProductManufacturer_ManufacturerId] ON [dbo].[ProductManufacturer]
(
	[ManufacturerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductManufacturer_ProductId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_ProductManufacturer_ProductId] ON [dbo].[ProductManufacturer]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductMedia_MediaId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_ProductMedia_MediaId] ON [dbo].[ProductMedia]
(
	[MediaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductMedia_ProductId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_ProductMedia_ProductId] ON [dbo].[ProductMedia]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductReview_CustomerId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_ProductReview_CustomerId] ON [dbo].[ProductReview]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductReview_ProductId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_ProductReview_ProductId] ON [dbo].[ProductReview]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductReview_StoreId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_ProductReview_StoreId] ON [dbo].[ProductReview]
(
	[StoreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_QueuedEmail_EmailAccountId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_QueuedEmail_EmailAccountId] ON [dbo].[QueuedEmail]
(
	[EmailAccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_StateProvince_CountryId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_StateProvince_CountryId] ON [dbo].[StateProvince]
(
	[CountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserRole_RoleId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserRole_RoleId] ON [dbo].[UserRole]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserRole_UserId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserRole_UserId] ON [dbo].[UserRole]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserSkill_SkillId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserSkill_SkillId] ON [dbo].[UserSkill]
(
	[SkillId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserSkill_UserId]    Script Date: 8/9/2018 1:43:05 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserSkill_UserId] ON [dbo].[UserSkill]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_Country_CountryId] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([Id])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_Country_CountryId]
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_StateProvince_StateProvinceId] FOREIGN KEY([StateProvinceId])
REFERENCES [dbo].[StateProvince] ([Id])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_StateProvince_StateProvinceId]
GO
ALTER TABLE [dbo].[Assessment]  WITH CHECK ADD  CONSTRAINT [FK_Assessment_Course_CourseId] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Assessment] CHECK CONSTRAINT [FK_Assessment_Course_CourseId]
GO
ALTER TABLE [dbo].[Assessment]  WITH CHECK ADD  CONSTRAINT [FK_Assessment_User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Assessment] CHECK CONSTRAINT [FK_Assessment_User_UserId]
GO
ALTER TABLE [dbo].[BasketItem]  WITH CHECK ADD  CONSTRAINT [FK_BasketItem_Baskets_BasketId] FOREIGN KEY([BasketId])
REFERENCES [dbo].[Baskets] ([Id])
GO
ALTER TABLE [dbo].[BasketItem] CHECK CONSTRAINT [FK_BasketItem_Baskets_BasketId]
GO
ALTER TABLE [dbo].[Catalog]  WITH CHECK ADD  CONSTRAINT [FK_Catalog_CatalogBrand_CatalogBrandId] FOREIGN KEY([CatalogBrandId])
REFERENCES [dbo].[CatalogBrand] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Catalog] CHECK CONSTRAINT [FK_Catalog_CatalogBrand_CatalogBrandId]
GO
ALTER TABLE [dbo].[Catalog]  WITH CHECK ADD  CONSTRAINT [FK_Catalog_CatalogType_CatalogTypeId] FOREIGN KEY([CatalogTypeId])
REFERENCES [dbo].[CatalogType] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Catalog] CHECK CONSTRAINT [FK_Catalog_CatalogType_CatalogTypeId]
GO
ALTER TABLE [dbo].[EmailTemplate]  WITH CHECK ADD  CONSTRAINT [FK_EmailTemplate_EmailAccount_EmailAccountId] FOREIGN KEY([EmailAccountId])
REFERENCES [dbo].[EmailAccount] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EmailTemplate] CHECK CONSTRAINT [FK_EmailTemplate_EmailAccount_EmailAccountId]
GO
ALTER TABLE [dbo].[EmailTemplate]  WITH CHECK ADD  CONSTRAINT [FK_EmailTemplate_EmailTemplate_ParentEmailTemplateId] FOREIGN KEY([ParentEmailTemplateId])
REFERENCES [dbo].[EmailTemplate] ([Id])
GO
ALTER TABLE [dbo].[EmailTemplate] CHECK CONSTRAINT [FK_EmailTemplate_EmailTemplate_ParentEmailTemplateId]
GO
ALTER TABLE [dbo].[Enrollment]  WITH CHECK ADD  CONSTRAINT [FK_Enrollment_Course_CourseId] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Enrollment] CHECK CONSTRAINT [FK_Enrollment_Course_CourseId]
GO
ALTER TABLE [dbo].[Enrollment]  WITH CHECK ADD  CONSTRAINT [FK_Enrollment_User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Enrollment] CHECK CONSTRAINT [FK_Enrollment_User_UserId]
GO
ALTER TABLE [dbo].[EntityMedia]  WITH CHECK ADD  CONSTRAINT [FK_EntityMedia_Media_MediaId] FOREIGN KEY([MediaId])
REFERENCES [dbo].[Media] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EntityMedia] CHECK CONSTRAINT [FK_EntityMedia_Media_MediaId]
GO
ALTER TABLE [dbo].[FAQ]  WITH CHECK ADD  CONSTRAINT [FK_FAQ_FaqCategory_FaqCategoryId] FOREIGN KEY([FaqCategoryId])
REFERENCES [dbo].[FaqCategory] ([Id])
GO
ALTER TABLE [dbo].[FAQ] CHECK CONSTRAINT [FK_FAQ_FaqCategory_FaqCategoryId]
GO
ALTER TABLE [dbo].[Lesson]  WITH CHECK ADD  CONSTRAINT [FK_Lesson_Course_CourseId] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Lesson] CHECK CONSTRAINT [FK_Lesson_Course_CourseId]
GO
ALTER TABLE [dbo].[LocaleStringResource]  WITH CHECK ADD  CONSTRAINT [FK_LocaleStringResource_Language_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LocaleStringResource] CHECK CONSTRAINT [FK_LocaleStringResource_Language_LanguageId]
GO
ALTER TABLE [dbo].[LocalizedProperty]  WITH CHECK ADD  CONSTRAINT [FK_LocalizedProperty_Language_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LocalizedProperty] CHECK CONSTRAINT [FK_LocalizedProperty_Language_LanguageId]
GO
ALTER TABLE [dbo].[NewsComment]  WITH CHECK ADD  CONSTRAINT [FK_NewsComment_Customer_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NewsComment] CHECK CONSTRAINT [FK_NewsComment_Customer_CustomerId]
GO
ALTER TABLE [dbo].[NewsComment]  WITH CHECK ADD  CONSTRAINT [FK_NewsComment_NewsItem_NewsItemId] FOREIGN KEY([NewsItemId])
REFERENCES [dbo].[NewsItem] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NewsComment] CHECK CONSTRAINT [FK_NewsComment_NewsItem_NewsItemId]
GO
ALTER TABLE [dbo].[NewsComment]  WITH CHECK ADD  CONSTRAINT [FK_NewsComment_Store_StoreId] FOREIGN KEY([StoreId])
REFERENCES [dbo].[Store] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NewsComment] CHECK CONSTRAINT [FK_NewsComment_Store_StoreId]
GO
ALTER TABLE [dbo].[NewsItem]  WITH CHECK ADD  CONSTRAINT [FK_NewsItem_Language_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NewsItem] CHECK CONSTRAINT [FK_NewsItem_Language_LanguageId]
GO
ALTER TABLE [dbo].[NewsItemCategory]  WITH CHECK ADD  CONSTRAINT [FK_NewsItemCategory_NewsCategory_NewsCategoryId] FOREIGN KEY([NewsCategoryId])
REFERENCES [dbo].[NewsCategory] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NewsItemCategory] CHECK CONSTRAINT [FK_NewsItemCategory_NewsCategory_NewsCategoryId]
GO
ALTER TABLE [dbo].[NewsItemCategory]  WITH CHECK ADD  CONSTRAINT [FK_NewsItemCategory_NewsItem_NewsItemId] FOREIGN KEY([NewsItemId])
REFERENCES [dbo].[NewsItem] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NewsItemCategory] CHECK CONSTRAINT [FK_NewsItemCategory_NewsItem_NewsItemId]
GO
ALTER TABLE [dbo].[NewsItemTag]  WITH CHECK ADD  CONSTRAINT [FK_NewsItemTag_NewsItem_NewsItemId] FOREIGN KEY([NewsItemId])
REFERENCES [dbo].[NewsItem] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NewsItemTag] CHECK CONSTRAINT [FK_NewsItemTag_NewsItem_NewsItemId]
GO
ALTER TABLE [dbo].[NewsItemTag]  WITH CHECK ADD  CONSTRAINT [FK_NewsItemTag_Tag_TagId] FOREIGN KEY([TagId])
REFERENCES [dbo].[Tag] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NewsItemTag] CHECK CONSTRAINT [FK_NewsItemTag_Tag_TagId]
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_Customer_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_Customer_CustomerId]
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_NotificationEvent_NotificationEventId] FOREIGN KEY([NotificationEventId])
REFERENCES [dbo].[NotificationEvent] ([Id])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_NotificationEvent_NotificationEventId]
GO
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD  CONSTRAINT [FK_OrderItems_Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
GO
ALTER TABLE [dbo].[OrderItems] CHECK CONSTRAINT [FK_OrderItems_Orders_OrderId]
GO
ALTER TABLE [dbo].[PermissionRole]  WITH CHECK ADD  CONSTRAINT [FK_PermissionRole_PermissionRecord_PermissionRecordId] FOREIGN KEY([PermissionRecordId])
REFERENCES [dbo].[PermissionRecord] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PermissionRole] CHECK CONSTRAINT [FK_PermissionRole_PermissionRecord_PermissionRecordId]
GO
ALTER TABLE [dbo].[PermissionRole]  WITH CHECK ADD  CONSTRAINT [FK_PermissionRole_Role_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PermissionRole] CHECK CONSTRAINT [FK_PermissionRole_Role_RoleId]
GO
ALTER TABLE [dbo].[Poll]  WITH CHECK ADD  CONSTRAINT [FK_Poll_Language_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Poll] CHECK CONSTRAINT [FK_Poll_Language_LanguageId]
GO
ALTER TABLE [dbo].[PollAnswer]  WITH CHECK ADD  CONSTRAINT [FK_PollAnswer_Poll_PollId] FOREIGN KEY([PollId])
REFERENCES [dbo].[Poll] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PollAnswer] CHECK CONSTRAINT [FK_PollAnswer_Poll_PollId]
GO
ALTER TABLE [dbo].[PollVotingRecord]  WITH CHECK ADD  CONSTRAINT [FK_PollVotingRecord_Customer_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PollVotingRecord] CHECK CONSTRAINT [FK_PollVotingRecord_Customer_CustomerId]
GO
ALTER TABLE [dbo].[PollVotingRecord]  WITH CHECK ADD  CONSTRAINT [FK_PollVotingRecord_PollAnswer_PollAnswerId] FOREIGN KEY([PollAnswerId])
REFERENCES [dbo].[PollAnswer] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PollVotingRecord] CHECK CONSTRAINT [FK_PollVotingRecord_PollAnswer_PollAnswerId]
GO
ALTER TABLE [dbo].[ProductCategory]  WITH CHECK ADD  CONSTRAINT [FK_ProductCategory_Category_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductCategory] CHECK CONSTRAINT [FK_ProductCategory_Category_CategoryId]
GO
ALTER TABLE [dbo].[ProductCategory]  WITH CHECK ADD  CONSTRAINT [FK_ProductCategory_Product_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductCategory] CHECK CONSTRAINT [FK_ProductCategory_Product_ProductId]
GO
ALTER TABLE [dbo].[ProductManufacturer]  WITH CHECK ADD  CONSTRAINT [FK_ProductManufacturer_Manufacturer_ManufacturerId] FOREIGN KEY([ManufacturerId])
REFERENCES [dbo].[Manufacturer] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductManufacturer] CHECK CONSTRAINT [FK_ProductManufacturer_Manufacturer_ManufacturerId]
GO
ALTER TABLE [dbo].[ProductManufacturer]  WITH CHECK ADD  CONSTRAINT [FK_ProductManufacturer_Product_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductManufacturer] CHECK CONSTRAINT [FK_ProductManufacturer_Product_ProductId]
GO
ALTER TABLE [dbo].[ProductMedia]  WITH CHECK ADD  CONSTRAINT [FK_ProductMedia_Media_MediaId] FOREIGN KEY([MediaId])
REFERENCES [dbo].[Media] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductMedia] CHECK CONSTRAINT [FK_ProductMedia_Media_MediaId]
GO
ALTER TABLE [dbo].[ProductMedia]  WITH CHECK ADD  CONSTRAINT [FK_ProductMedia_Product_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductMedia] CHECK CONSTRAINT [FK_ProductMedia_Product_ProductId]
GO
ALTER TABLE [dbo].[ProductReview]  WITH CHECK ADD  CONSTRAINT [FK_ProductReview_Customer_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductReview] CHECK CONSTRAINT [FK_ProductReview_Customer_CustomerId]
GO
ALTER TABLE [dbo].[ProductReview]  WITH CHECK ADD  CONSTRAINT [FK_ProductReview_Product_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductReview] CHECK CONSTRAINT [FK_ProductReview_Product_ProductId]
GO
ALTER TABLE [dbo].[ProductReview]  WITH CHECK ADD  CONSTRAINT [FK_ProductReview_Store_StoreId] FOREIGN KEY([StoreId])
REFERENCES [dbo].[Store] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductReview] CHECK CONSTRAINT [FK_ProductReview_Store_StoreId]
GO
ALTER TABLE [dbo].[QueuedEmail]  WITH CHECK ADD  CONSTRAINT [FK_QueuedEmail_EmailAccount_EmailAccountId] FOREIGN KEY([EmailAccountId])
REFERENCES [dbo].[EmailAccount] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[QueuedEmail] CHECK CONSTRAINT [FK_QueuedEmail_EmailAccount_EmailAccountId]
GO
ALTER TABLE [dbo].[StateProvince]  WITH CHECK ADD  CONSTRAINT [FK_StateProvince_Country_CountryId] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[StateProvince] CHECK CONSTRAINT [FK_StateProvince_Country_CountryId]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_Role_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_Role_RoleId]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_User_UserId]
GO
ALTER TABLE [dbo].[UserSkill]  WITH CHECK ADD  CONSTRAINT [FK_UserSkill_Skill_SkillId] FOREIGN KEY([SkillId])
REFERENCES [dbo].[Skill] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserSkill] CHECK CONSTRAINT [FK_UserSkill_Skill_SkillId]
GO
ALTER TABLE [dbo].[UserSkill]  WITH CHECK ADD  CONSTRAINT [FK_UserSkill_User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserSkill] CHECK CONSTRAINT [FK_UserSkill_User_UserId]
GO
USE [master]
GO
ALTER DATABASE [Vnit.Ecommerce] SET  READ_WRITE 
GO
