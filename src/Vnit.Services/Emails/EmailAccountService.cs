using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Entities.Emails;

namespace Vnit.ApplicationCore.Services.Emails
{
    public partial class EmailAccountService : BaseEntityService<EmailAccount>, IEmailAccountService
    {
        public EmailAccountService(IDataRepository<EmailAccount> dataRepository) : base(dataRepository)
        {
        }
    }
}
