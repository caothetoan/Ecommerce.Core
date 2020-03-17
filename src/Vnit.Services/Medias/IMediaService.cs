using System.Linq;
using Vnit.ApplicationCore.Entities;
using Vnit.ApplicationCore.Entities.MediaAggregate;
using Vnit.ApplicationCore.Entities.Settings;

namespace Vnit.ApplicationCore.Services.Medias
{
    public interface IMediaService : IBaseEntityService<Media>
    {
        #region media entity
        IQueryable<Media> GetEntityMedia<TEntityType>(int entityId, MediaType? mediaType, int page = 1, int count = 15) where TEntityType : BaseEntity;

        void AttachMediaToEntity<T>(int entityId, int mediaId) where T : BaseEntity;

        void AttachMediaToEntity<T>(T entity, Media media) where T : BaseEntity;

        void DetachMediaFromEntity<T>(int entityId, int mediaId) where T : BaseEntity;

        void DetachMediaFromEntity<T>(T entity, Media media) where T : BaseEntity;

        void ClearEntityMedia<T>(int entityId) where T : BaseEntity;

        void ClearEntityMedia<T>(T entity) where T : BaseEntity;

        void ClearMediaAttachments(Media media);

        #endregion

        #region Upload media
        void WritePictureBytes(Media picture, GeneralSettings generalSettings, MediaSettings mediaSettings);

        #endregion

    }
}
