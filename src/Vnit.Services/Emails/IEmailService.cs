using Vnit.ApplicationCore.Entities.Courses;
using Vnit.ApplicationCore.Entities.Emails;
using Vnit.ApplicationCore.Entities.OrderAggregate;
using Vnit.ApplicationCore.Entities.Users;

namespace Vnit.ApplicationCore.Services.Emails
{
    public interface IEmailService
    {
        bool SendTestEmail(string email, EmailAccount emailAccount);

        void SendEmailBase(string templateName,
            string toName,
            string toEmail,
            object entities);
        #region register active
        /// <summary>
        /// Gửi email đăng ký tài khoản
        /// </summary>
        /// <param name="user"></param>
        /// <param name="withAdmin"></param>
        void SendUserRegisteredMessage(User user, bool withAdmin = true);

        /// <summary>
        /// Gửi email đăng ký tài khoản tạo mật khẩu ngẫu nhiên
        /// </summary>
        /// <param name="user"></param>
        /// <param name="withAdmin"></param>
        /// <param name="randomPassword"></param>
        void SendUserRegisteredMessage(User user, bool withAdmin, string randomPassword);

        /// <summary>
        /// Gửi email kích hoạt tài khoản
        /// </summary>
        /// <param name="user"></param>
        /// <param name="activationUrl"></param>
        void SendUserActivationLinkMessage(User user, string activationUrl, string code = "");

        void SendUserActivatedMessage(User user);
        
        #endregion

        #region password recovery
        void SendPasswordRecoveryLinkMessage(User user, string recoveryUrl);

        void SendPasswordChangedMessage(User user);
        #endregion

        #region order send mail
        void SendUserAssessment(Assessment assessment, User user, bool withAdmin = true);

        void SendUserOrder(Order order,User user, bool withAdmin = true);

        //void SendUserBooked(Booking booking, bool withAdmin = true);

        void SendUserNewsLetterSubscription(NewsLetterSubscription newsLetterSubscription, bool withAdmin = true);

        #endregion
    }
}
