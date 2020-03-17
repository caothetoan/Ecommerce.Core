using System;
using System.Collections.Generic;
using System.Text;

namespace Vnit.ApplicationCore.Entities.Emails
{
    public class EmailTemplateNames
    {
        public const string Master = "Master";

        #region customer register
        public const string UserRegisteredMessage = "User.Registered";

        public const string UserRegisteredRandomPassword = "User.Registered.RandomPassword";

        public const string UserRegisteredMessageToAdmin = "User.Registered.Admin";

        public const string UserActivatedMessage = "User.Activated";

        public const string UserActivatedSendDiscountMessage = "User.ActivatedSendDiscount";

        public const string UserActivationLinkMessage = "User.ActivationLink";


        public const string PasswordRecoveryLinkMessage = "Common.PasswordRecovery";

        public const string PasswordChangedMessage = "Common.PasswordChanged";

        public const string UserDeactivatedMessage = "User.Deactivated";

        public const string UserDeactivatedMessageToAdmin = "User.Deactivated.Admin";

        public const string UserAccountDeletedMessage = "User.AccountDeleted";

        public const string UserAccountDeletedMessageToAdmin = "User.AccountDeleted.Admin";

        #endregion
        // order
        public const string UserOrdered = "User.Ordered";

        public const string UserOrderedToAdmin = "User.Ordered.Admin";

        // booking
        public const string UserBooked = "User.Booked";

        public const string UserBookedToMerchant = "User.Booked.ToMerchant";

        public const string UserBookedToAdmin = "User.Booked.Admin";


        public const string UserBookedConfirmLink = "User.Booked.ConfirmLink";

        public const string UserBookedMerchantConfirm = "User.Booked.MerchantConfirmed";

        public const string UserBookedMerchantDenied = "User.Booked.MerchantDenied";

        public const string UserBookedMerchantRecommend = "User.Booked.MerchantRecommend";

        public const string UserBookedCompleted = "User.Booked.Completed";

        public const string UserBookedCanceled = "User.Booked.Canceled";

        //NewsLetterSubscription
        public const string UserNewsLetterSubscription = "User.NewsLetterSubscription";

        public const string UserNewsLetterSubscriptionToAdmin = "User.NewsLetterSubscription.Admin";

        public static string UserAssessment = "User.UserAssessment";
        public static string UserAssessmentToAdmin = "User.UserAssessment.Admin";
    }
}
