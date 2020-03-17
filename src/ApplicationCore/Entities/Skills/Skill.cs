
using Vnit.ApplicationCore.Interfaces;

namespace Vnit.ApplicationCore.Entities.Skills
{
    public class Skill : BaseEntity, IPermalinkSupported
    {
        public int DisplayOrder { get; set; }

        public int UserId { get; set; }

        public int FeaturedImageId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
