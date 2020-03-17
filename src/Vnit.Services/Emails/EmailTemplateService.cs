using System.Linq;
using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Entities.Emails;

namespace Vnit.ApplicationCore.Services.Emails
{
    public class EmailTemplateService : BaseEntityService<EmailTemplate>, IEmailTemplateService
    {
        public EmailTemplateService(IDataRepository<EmailTemplate> dataRepository) : base(dataRepository)
        {

        }

        public string GetProcessedContentTemplate(EmailTemplate emailTemplate)
        {
            if (emailTemplate == null) return "";

            if (emailTemplate.ParentEmailTemplate == null)
                return emailTemplate.Template;
            return
                GetProcessedContentTemplate(emailTemplate.ParentEmailTemplate)
                    .Replace(EmailTokenNames.MessageContent, emailTemplate.Template);
        }

        public EmailTemplate GetOrInsert(string templateSystemName)
        {
            var temp = Get(t => t.TemplateSystemName.ToLower() == templateSystemName.ToLower()).FirstOrDefault();
            var masterTemplate =Get(x => x.TemplateSystemName.Equals("Master") ).FirstOrDefault();

            if (temp != null) return temp;

            temp = new EmailTemplate()
            {
               
                TemplateSystemName= templateSystemName,
                Name = templateSystemName,
                IsMaster=false,
                EmailAccountId= 1,
                ParentEmailTemplateId= masterTemplate!=null? masterTemplate.Id: 0
            };

            Insert(temp);

            return temp;
        }
    }
}
