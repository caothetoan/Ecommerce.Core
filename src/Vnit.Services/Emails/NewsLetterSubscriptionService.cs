using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Entities.Emails;

namespace Vnit.ApplicationCore.Services.Emails
{
    public class NewsLetterSubscriptionService : BaseEntityService<NewsLetterSubscription>, INewsLetterSubscriptionService
    {
        public NewsLetterSubscriptionService(IDataRepository<NewsLetterSubscription> dataRepository) : base(dataRepository)
        {
        }
    }
}
