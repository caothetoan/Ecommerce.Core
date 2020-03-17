using System;
using System.Collections.Generic;
using Vnit.ApplicationCore.Entities.Courses;
using Vnit.ApplicationCore.Entities.Emails;
using Vnit.ApplicationCore.Entities.OrderAggregate;
using Vnit.ApplicationCore.Entities.Tokens;
using Vnit.ApplicationCore.Entities.Users;
using Vnit.ApplicationCore.Interfaces;

namespace Vnit.ApplicationCore.Services.Emails
{
    public class EmailService : IEmailService
    {
        #region fields
        private readonly IEmailMessageService _emailService;
        private readonly ITokenProcessor _tokenProcessor;
        private readonly IEmailTemplateService _emailTemplateService;
        #endregion

        public EmailService(IEmailMessageService emailService, ITokenProcessor tokenProcessor, IEmailTemplateService emailTemplateService)
        {
            _emailService = emailService;
            _tokenProcessor = tokenProcessor;
            _emailTemplateService = emailTemplateService;
        }
        /// <summary>
        /// Loads a named email template from database and replaces tokens with passed entities, and returns a new email message object with template values
        /// </summary>
        /// <param name="templateName"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        private EmailMessage LoadAndProcessTemplate(string templateName, params object[] entities)
        {
            //first load the template from database
            var template = _emailTemplateService.FirstOrDefault(x => x.TemplateSystemName.Equals(templateName),
                x => x.EmailAccount, x => x.ParentEmailTemplate);
            if (template == null)
                return null;
            //we'll check if there are parent templates and get all parent template 
            var processedContentTemplate = _emailTemplateService.GetProcessedContentTemplate(template);

            var processedTemplateString = _tokenProcessor.ProcessAllTokens(processedContentTemplate, entities);

            //template.Subject = _tokenProcessor.ProcessAllTokens(template.Subject, entities);

            var emailAccount = template.EmailAccount;
            //create a new email message
            var emailMessage = new EmailMessage()
            {
                IsEmailBodyHtml = true,
                EmailBody = processedTemplateString,
                EmailAccountId = emailAccount.Id,
                Subject = template.Subject,
                OriginalEmailTemplate = template,
                Tos = new List<EmailMessage.UserInfo>(),
                CreatedDate = DateTime.Now
            };

            return emailMessage;
        }


        public bool SendTestEmail(string email, EmailAccount emailAccount)
        {
            var subject = " Test Email";
            var message = "Thank you for using email sender. This is a sample email to test if emails are functioning.";
            //create a new email message
            var emailMessage = new EmailMessage()
            {
                IsEmailBodyHtml = true,
                EmailBody = message,
                EmailAccount = emailAccount,
                Subject = subject,
                To = email,
                CreatedDate = DateTime.Now,
                SendingDate = DateTime.Now,
                Tos = new List<EmailMessage.UserInfo>()
                {
                    new EmailMessage.UserInfo("WebAdmin", email)
                }
            };
            return _emailService.SendEmail(emailMessage, true);
        }
        /// <summary>
        /// Gửi email cơ bản
        /// </summary>
        /// <param name="templateName"></param>
        /// <param name="toName"></param>
        /// <param name="toEmail"></param>
        /// <param name="entities"></param>
        public void SendEmailBase(string templateName,
            string toName,
            string toEmail,
            object entities)
        {
            var message = LoadAndProcessTemplate(templateName, entities);
            if (message != null)
            {
                message.Tos.Add(new EmailMessage.UserInfo(toName, toEmail));
                message.To = toEmail;
                _emailService.Queue(message);
            }
        }
        #region Register active
        /// <summary>
        /// Gửi email đăng ký tài khoản tạo mật khẩu ngẫu nhiên
        /// </summary>
        /// <param name="user"></param>
        /// <param name="withAdmin"></param>
        public void SendUserRegisteredMessage(User user, bool withAdmin)
        {
            var message = LoadAndProcessTemplate(EmailTemplateNames.UserRegisteredMessage, user);

            message.To = user.Email;
            message.Tos.Add(new EmailMessage.UserInfo(user.FullName, user.Email));
            _emailService.Queue(message);
            if (withAdmin) //send to admin if needed
            {
                message = LoadAndProcessTemplate(EmailTemplateNames.UserRegisteredMessageToAdmin, user);

                if (message != null)
                {
                    message.To = message.OriginalEmailTemplate.AdministrationEmail;
                    message.Tos.Add(new EmailMessage.UserInfo("Administrator", message.OriginalEmailTemplate.AdministrationEmail));
                    _emailService.Queue(message);
                }

            }
        }
        /// <summary>
        /// Gửi email đăng ký tài khoản tạo mật khẩu ngẫu nhiên
        /// </summary>
        /// <param name="user"></param>
        /// <param name="withAdmin"></param>
        /// <param name="randomPassword"></param>
        public void SendUserRegisteredMessage(User user, bool withAdmin, string randomPassword)
        {
            var message = LoadAndProcessTemplate(EmailTemplateNames.UserRegisteredRandomPassword, user);
            message.EmailBody = _tokenProcessor.ProcessProvidedTokens(message.EmailBody, new List<Token>
            {
                new Token(EmailTokenNames.RandomPassword, randomPassword)
            });
            message.To = user.Email;
            message.Tos.Add(new EmailMessage.UserInfo(user.FullName, user.Email));
            _emailService.Queue(message);
            if (withAdmin) //send to admin if needed
            {
                message = LoadAndProcessTemplate(EmailTemplateNames.UserRegisteredMessageToAdmin, user);

                if (message != null)
                {
                    message.To = message.OriginalEmailTemplate.AdministrationEmail;
                    message.Tos.Add(new EmailMessage.UserInfo("Administrator", message.OriginalEmailTemplate.AdministrationEmail));
                    _emailService.Queue(message);
                }

            }
        }
        /// <summary>
        /// Gửi email kích hoạt tài khoản
        /// </summary>
        /// <param name="user"></param>
        /// <param name="activationUrl"></param>
        public void SendUserActivationLinkMessage(User user, string activationUrl, string code = "")
        {
            var message = LoadAndProcessTemplate(EmailTemplateNames.UserActivationLinkMessage, user);
            message.Subject = _tokenProcessor.ProcessProvidedTokens(message.Subject, new List<Token>
            {
                new Token("{{User.UserName}}", user.UserName)
            });
            //additional tokens 
            message.EmailBody = _tokenProcessor.ProcessProvidedTokens(message.EmailBody, new List<Token>
            {
                new Token(EmailTokenNames.ActivationUrl, activationUrl),
                new Token("{{SBD}}", user.UserName)
            });
            message.To = user.Email;
            message.Tos.Add(new EmailMessage.UserInfo(user.FullName, user.Email));
            _emailService.Queue(message);

            //message.To = message.OriginalEmailTemplate.AdministrationEmail;
            //message.Tos.Add(new EmailMessage.UserInfo("Administrator", message.OriginalEmailTemplate.AdministrationEmail));
            //_emailService.Queue(message);
        }
        /// <summary>
        /// Gửi emai thông báo tài khoản đã được kích hoạt
        /// </summary>
        /// <param name="user"></param>
        public void SendUserActivatedMessage(User user)
        {
            var message = LoadAndProcessTemplate(EmailTemplateNames.UserActivatedMessage, user);
            message.To = user.Email;
            message.Tos.Add(new EmailMessage.UserInfo(user.FullName, user.Email));
            _emailService.Queue(message);
        }
        #endregion

        #region password recovery
        public void SendPasswordRecoveryLinkMessage(User user, string activationUrl)
        {
            var message = LoadAndProcessTemplate(EmailTemplateNames.PasswordRecoveryLinkMessage, user);
            //additional tokens 
            message.EmailBody = _tokenProcessor.ProcessProvidedTokens(message.EmailBody, new List<Token>
            {
                new Token(EmailTokenNames.RecoveryUrl, activationUrl)
            });
            message.To = user.Email;
            message.Tos.Add(new EmailMessage.UserInfo(user.UserName, user.Email));
            _emailService.Queue(message);
        }

        public void SendPasswordChangedMessage(User user)
        {
            var message = LoadAndProcessTemplate(EmailTemplateNames.PasswordChangedMessage, user);
            message.To = user.Email;
            message.Tos.Add(new EmailMessage.UserInfo(user.UserName, user.Email));
            _emailService.Queue(message);
        }
        #endregion


        #region order send email

        public void SendUserAssessment(Assessment assessment, User user, bool withAdmin = true)
        {
            try
            {
                var message = LoadAndProcessTemplate(EmailTemplateNames.UserAssessment, assessment);

                message.EmailBody = _tokenProcessor.ProcessAllTokens(message.EmailBody);

                message.Tos.Add(new EmailMessage.UserInfo(user.FullName, user.Email));
                message.To = user.Email;

                _emailService.Queue(message);

                if (withAdmin) //send to admin if needed
                {
                    var messageAdmin = LoadAndProcessTemplate(EmailTemplateNames.UserAssessmentToAdmin, assessment);
                    if (messageAdmin != null)
                    {
                        messageAdmin.To = messageAdmin.OriginalEmailTemplate.AdministrationEmail;

                        messageAdmin.Tos.Add(new EmailMessage.UserInfo("Administrator", messageAdmin.OriginalEmailTemplate.AdministrationEmail));
                        _emailService.Queue(messageAdmin);
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <param name="user"></param>
        /// <param name="withAdmin"></param>
        public void SendUserOrder(Order order, User user, bool withAdmin = true)
        {
            try
            {
                var message = LoadAndProcessTemplate(EmailTemplateNames.UserOrdered, order);

                //message.EmailBody = _tokenProcessor.ProcessAllTokens(message.EmailBody, user);
                message.Subject = _tokenProcessor.ProcessProvidedTokens(message.Subject, new List<Token>
                {
                    new Token(EmailTokenNames.OrderCustomOrderNumber, "order.CustomOrderNumber") //order.CustomOrderNumber
                });
                message.EmailBody = _tokenProcessor.ProcessProvidedTokens(message.EmailBody, 
                    new List<Token>
                    {
                        new Token("{{User.FullName}}", user.FullName),
                        new Token("{{User.Email}}", user.Email),
                        new Token("{{User.Phone}}", user.Phone)
                    }
                );
                message.Tos.Add(new EmailMessage.UserInfo(user.FullName, user.Email));
                message.To = user.Email;

                _emailService.Queue(message);

                if (withAdmin) //send to admin if needed
                {
                    var messageAdmin = LoadAndProcessTemplate(EmailTemplateNames.UserOrderedToAdmin, order);
                    if (messageAdmin != null)
                    {
                        messageAdmin.To = messageAdmin.OriginalEmailTemplate.AdministrationEmail;
                        messageAdmin.Subject = _tokenProcessor.ProcessProvidedTokens(messageAdmin.Subject,
                            new List<Token>
                            {
                                new Token(EmailTokenNames.OrderCustomOrderNumber, "order.CustomOrderNumber")
                            });
                        messageAdmin.Tos.Add(new EmailMessage.UserInfo("Administrator",
                            messageAdmin.OriginalEmailTemplate.AdministrationEmail));
                        _emailService.Queue(messageAdmin);
                    }

                }
            }
            catch (Exception)
            {
                //_logService.InsertLog(LogLevel.Error, e.Message);
            }
        }
        //public void SendUserBooked(Booking booking, bool withAdmin = true)
        //{
        //    var message = LoadAndProcessTemplate(EmailTemplateNames.UserBooked, booking);

        //    message.EmailBody = _tokenProcessor.ProcessAllTokens(message.EmailBody);
        //    message.Subject = _tokenProcessor.ProcessProvidedTokens(message.Subject, new List<Token>
        //    {
        //        new Token(EmailTokenNames.BookingId, booking.BookingId)
        //    });
        //    message.Tos.Add(new EmailMessage.UserInfo(booking.CustomerName, booking.CustomerEmail));
        //    message.To = booking.CustomerEmail;

        //    _emailService.Queue(message);

        //    if (withAdmin) //send to admin if needed
        //    {
        //        var messageAdmin = LoadAndProcessTemplate(EmailTemplateNames.UserBookedToAdmin, booking);
        //        if (messageAdmin != null)
        //        {
        //            messageAdmin.To = messageAdmin.OriginalEmailTemplate.AdministrationEmail;
        //            messageAdmin.Subject = _tokenProcessor.ProcessProvidedTokens(messageAdmin.Subject, new List<Token>
        //            {
        //                new Token(EmailTokenNames.BookingId, booking.BookingId)
        //            });
        //            messageAdmin.Tos.Add(new EmailMessage.UserInfo("Administrator", messageAdmin.OriginalEmailTemplate.AdministrationEmail));
        //            _emailService.Queue(messageAdmin);
        //        }

        //    }
        //}

        public void SendUserNewsLetterSubscription(NewsLetterSubscription newsLetterSubscription, bool withAdmin = true)
        {
            var message = LoadAndProcessTemplate(EmailTemplateNames.UserNewsLetterSubscription, newsLetterSubscription);

            message.EmailBody = _tokenProcessor.ProcessAllTokens(message.EmailBody, newsLetterSubscription);
          
            message.Tos.Add(new EmailMessage.UserInfo(newsLetterSubscription.Name, newsLetterSubscription.Email));
            message.To = newsLetterSubscription.Email;

            _emailService.Queue(message);

            if (withAdmin) //send to admin if needed
            {
                var messageAdmin = LoadAndProcessTemplate(EmailTemplateNames.UserNewsLetterSubscriptionToAdmin, newsLetterSubscription);
                if (messageAdmin != null)
                {
                    messageAdmin.To = messageAdmin.OriginalEmailTemplate.AdministrationEmail;
                   
                    messageAdmin.Tos.Add(new EmailMessage.UserInfo("Administrator", messageAdmin.OriginalEmailTemplate.AdministrationEmail));
                    _emailService.Queue(messageAdmin);
                }

            }
        }

        #endregion
    }
}
