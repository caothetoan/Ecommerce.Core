using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vnit.ApplicationCore.Entities.Catalog;
using Vnit.ApplicationCore.Entities.Common;
using Vnit.ApplicationCore.Entities.Courses;
using Vnit.ApplicationCore.Entities.Emails;
using Vnit.ApplicationCore.Entities.EntityProperties;
using Vnit.ApplicationCore.Entities.Faqs;
using Vnit.ApplicationCore.Entities.Localization;
using Vnit.ApplicationCore.Entities.MediaAggregate;
using Vnit.ApplicationCore.Entities.Menus;
using Vnit.ApplicationCore.Entities.News;
using Vnit.ApplicationCore.Entities.Notifications;
using Vnit.ApplicationCore.Entities.OrderAggregate;
using Vnit.ApplicationCore.Entities.Pages;
using Vnit.ApplicationCore.Entities.Polls;
using Vnit.ApplicationCore.Entities.Security;
using Vnit.ApplicationCore.Entities.Seo;
using Vnit.ApplicationCore.Entities.Settings;
using Vnit.ApplicationCore.Entities.Shipping;
using Vnit.ApplicationCore.Entities.Skills;
using Vnit.ApplicationCore.Entities.Users;

namespace Vnit.Infrastructure.Data
{

    public class AddressMap : BaseEntityConfiguration<Address> { }

    public class CategoryConfiguration : BaseEntityConfiguration<Category> { }

    public class CategoryTemplateConfiguration : BaseEntityConfiguration<CategoryTemplate> { }

    public class ProductConfiguration : BaseEntityConfiguration<Product> { }

    public class ProductTemplateConfiguration : BaseEntityConfiguration<ProductTemplate> { }

    public class ProductCategoryConfiguration : BaseEntityConfiguration<ProductCategory> { }

    public class ProductAttributeConfiguration : BaseEntityConfiguration<ProductAttribute> { }


    public class ProductMediaConfiguration : BaseEntityConfiguration<ProductMedia> { }

    public class ProductReviewConfiguration : BaseEntityConfiguration<ProductReview> { }


    public class CountryConfiguration : BaseEntityConfiguration<Country> { }

    public class EntityMediaMap : BaseEntityConfiguration<EntityMedia>
    {
      
    }
    public class EmailAccountMap : BaseEntityConfiguration<EmailAccount> { }

    public class QueuedEmailMap : BaseEntityConfiguration<QueuedEmail> { }

    public class EmailTemplateMap : BaseEntityConfiguration<EmailTemplate> { }


    public class EntityPropertyMap : BaseEntityConfiguration<EntityProperty> { }

    public class FAQMap : BaseEntityConfiguration<FAQ> { }

    public class FaqCategoryMap : BaseEntityConfiguration<FaqCategory> { }

    public class LanguageMap : BaseEntityConfiguration<Language> { }

    public class LocalizedPropertyMap : BaseEntityConfiguration<LocalizedProperty> { }

    public class LocaleStringResourceMap : BaseEntityConfiguration<LocaleStringResource> { }

    public class MediaMap : BaseEntityConfiguration<Media> { }

    public class MenuMap : BaseEntityConfiguration<Menu> { }

    public class NewsCategoryMap : BaseEntityConfiguration<NewsCategory> { }

    public class NewsItemMap : BaseEntityConfiguration<NewsItem> { }

    public class NewsItemTagMap : BaseEntityConfiguration<NewsItemTag> { }

    public class NewsCommentMap: BaseEntityConfiguration<NewsComment> { }

    public class NewsLetterSubscriptionMap : BaseEntityConfiguration<NewsLetterSubscription> { }


    public class NotificationEventTypeMap : BaseEntityConfiguration<NotificationEvent> { }

    public class NotificationMap : BaseEntityConfiguration<Notification> { }

    public class PageMap : BaseEntityConfiguration<Page> { }

    public class PermissionRecordMap : BaseEntityConfiguration<PermissionRecord> { }

    public class PermissionRoleMap : BaseEntityConfiguration<PermissionRole> { }

    public class PollMap : BaseEntityConfiguration<Poll> { }

    public class PollAnswerMap : BaseEntityConfiguration<PollAnswer> { }

    public class PollVotingRecordMap : BaseEntityConfiguration<PollVotingRecord> { }

    public class RoleMap : BaseEntityConfiguration<Role> { }

    public class SettingMap : BaseEntityConfiguration<Setting> { }


    public class TagMap : BaseEntityConfiguration<Tag> { }


    public class StateProvinceMap : BaseEntityConfiguration<StateProvince> { }

    #region elearning freelancer
    public class CourseConfiguration : BaseEntityConfiguration<Course> { }

    public class LessonConfiguration : BaseEntityConfiguration<Lesson> { }

    public class AssessmentConfiguration : BaseEntityConfiguration<Assessment> { }

    public class SkillConfiguration : BaseEntityConfiguration<Skill> { }

    public class UserConfiguration : BaseEntityConfiguration<User> { }

    public class UserSkillConfiguration : BaseEntityConfiguration<UserSkill> { } 
    #endregion

    public class UrlRecordMap : BaseEntityConfiguration<UrlRecord> { }

    public class OrderMap : BaseEntityConfiguration<Order> { }

    public class OrderItemMap : BaseEntityConfiguration<OrderItem> { }

    public class OrderNoteMap : BaseEntityConfiguration<OrderNote> { }

    public class ShipmentMap : BaseEntityConfiguration<Shipment> { }

    public class ShipmentItemMap : BaseEntityConfiguration<ShipmentItem> { }
}
