using System.Collections.Generic;
using Vnit.ApplicationCore.Entities.MediaAggregate;
using Vnit.ApplicationCore.Entities.News;

namespace Vnit.ApplicationCore.Services.Medias
{
    public interface ITagService : IBaseEntityService<Tag>
    {
        void AssignTagToMedia(Tag tag, Media media);


        void AssignTagToMedia(string tagName, Media media);
      

        void UnassignTagToMedia(Tag tag, Media media);


        void UnassignTagToMedia(string tagName, Media media);


        IList<Tag> GetTags(int mediaId);

        IList<Tag> GetTags(Media media);

    }
}
