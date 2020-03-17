using System;
using System.Collections.Generic;
using System.Linq;
using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Entities.MediaAggregate;
using Vnit.ApplicationCore.Entities.News;
using Vnit.ApplicationCore.Services;
using Vnit.ApplicationCore.Services.Medias;

namespace Vnit.Services.Medias
{
    public class TagService : BaseEntityService<Tag>, ITagService
    {
        private readonly IDataRepository<NewsItemTag> _mediaTagDataRepository;

        public TagService(IDataRepository<Tag> dataRepository,
            IDataRepository<NewsItemTag> mediaTagDataRepository) : base(dataRepository)
        {
            _mediaTagDataRepository = mediaTagDataRepository;

        }

        public void AssignTagToMedia(Tag tag, Media media)
        {
            var isAlreadyAssigned = GetTags(media).Any(x => x.Id == tag.Id);
            if (isAlreadyAssigned)
                return;

            _mediaTagDataRepository.Insert(new NewsItemTag()
            {
                TagId = tag.Id,
                NewsItemId = media.Id
            });
        }

        public void AssignTagToMedia(string tagName, Media media)
        {
            var tag =
                Repository.Get(
                        x => string.Compare(x.Name, tagName, StringComparison.InvariantCultureIgnoreCase) == 0)
                    .FirstOrDefault();

            if (tag == null)
                throw new Exception(string.Format("The tag with name '{0}' can't be found", tagName));

            AssignTagToMedia(tag, media);
        }

        public void UnassignTagToMedia(Tag tag, Media media)
        {
            var tags = GetTags(media).FirstOrDefault(x => x.Id == tag.Id);
            if (tags == null)
                return;

            _mediaTagDataRepository.Delete(x => x.NewsItemId == media.Id && x.TagId == tag.Id);

        }

        public void UnassignTagToMedia(string tagName, Media media)
        {
            var userRole = GetTags(media).FirstOrDefault(x => x.Name == tagName);
            if (userRole == null)
                return;

            _mediaTagDataRepository.Delete(x => x.NewsItemId == media.Id && x.Tag.Name == tagName);
        }

        public IList<Tag> GetTags(int mediaId)
        {
            var userRoles = _mediaTagDataRepository.Get(x => x.NewsItemId == mediaId).Select(x => x.Tag).ToList();
            return userRoles;
        }

        public IList<Tag> GetTags(Media media)
        {
            return GetTags(media.Id);
        }
    }
}
