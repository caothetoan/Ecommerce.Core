using Vnit.ApplicationCore.Entities.Emails;

namespace Vnit.ApplicationCore.Services.Emails
{
    public interface IEmailTemplateService : IBaseEntityService<EmailTemplate>
    {
        string GetProcessedContentTemplate(EmailTemplate emailTemplate);

        EmailTemplate GetOrInsert(string emailTemplateName);
    }
}
