using System;
using System.Linq;
using Vnit.ApplicationCore.Constants;
using Vnit.ApplicationCore.Data;
using Vnit.ApplicationCore.Entities;
using Vnit.ApplicationCore.Entities.MediaAggregate;
using Vnit.ApplicationCore.Entities.Settings;
using Vnit.ApplicationCore.Enums;
using Vnit.ApplicationCore.Helpers;
using Vnit.ApplicationCore.Interfaces;
using Vnit.ApplicationCore.Services;
using Vnit.ApplicationCore.Services.Medias;

namespace Vnit.Services.Medias
{
    public class MediaService : BaseEntityService<Media>, IMediaService
    {
        #region members
        private readonly IDataRepository<EntityMedia> _entityMediaRepository;
        private readonly INopFileProvider _fileProvider;
        private readonly IWebHelper _webHelper;
        private readonly MediaSettings _mediaSettings;

        #endregion

        #region Ctors
        public MediaService(IDataRepository<Media> dataRepository, IDataRepository<EntityMedia> entityMediaRepository, INopFileProvider fileProvider, IWebHelper webHelper, MediaSettings mediaSettings) : base(dataRepository)
        {
            _entityMediaRepository = entityMediaRepository;
            _fileProvider = fileProvider;
            _webHelper = webHelper;
            _mediaSettings = mediaSettings;
        }
        #endregion

        #region Public methods
        public IQueryable<Media> GetEntityMedia<TEntityType>(int entityId, MediaType? mediaType, int page = 1, int count = 10) where TEntityType : BaseEntity
        {
            //first get the media ids for this entity
            var mediaIds = _entityMediaRepository.Get(x => x.EntityId == entityId && x.EntityName == typeof(TEntityType).Name).Select(x => x.MediaId).ToList();
            if (mediaType.HasValue)
                return Get(x => mediaIds.Contains(x.Id) && x.MediaType == mediaType, null, true, page, count);
            return Get(x => mediaIds.Contains(x.Id), null, true, page, count);
        }

        public void AttachMediaToEntity<T>(int entityId, int mediaId) where T : BaseEntity
        {
            if (mediaId == 0)
            {
                throw new Exception("Can't attach entity with media with Id '0'");
            }
            //insert entity picture only if it doesn't exist
            var insertRequired =
                !_entityMediaRepository.Get(x => x.EntityId == entityId && x.MediaId == mediaId).Any();

            if (!insertRequired) return;


            var entityMedia = new EntityMedia()
            {
                EntityId = entityId,
                MediaId = mediaId,
                EntityName = typeof(T).Name,
                DisplayOrder = 0
            };

            _entityMediaRepository.Insert(entityMedia);
        }

        public void AttachMediaToEntity<T>(T entity, Media media) where T : BaseEntity
        {
            if (media.Id == 0)
            {
                Repository.Insert(media);
            }
            AttachMediaToEntity<T>(entity.Id, media.Id);
        }

        public void DetachMediaFromEntity<T>(int entityId, int mediaId) where T : BaseEntity
        {
            _entityMediaRepository.Delete(x => x.EntityId == entityId && x.MediaId == mediaId);
        }

        public void DetachMediaFromEntity<T>(T entity, Media media) where T : BaseEntity
        {
            DetachMediaFromEntity<T>(entity.Id, media.Id);
        }

        public void ClearMediaAttachments(Media media)
        {
            _entityMediaRepository.Delete(x => x.MediaId == media.Id);
        }

        public void ClearEntityMedia<T>(int entityId) where T : BaseEntity
        {
            _entityMediaRepository.Delete(x => x.EntityId == entityId);
        }

        public void ClearEntityMedia<T>(T entity) where T : BaseEntity
        {
            ClearEntityMedia<T>(entity.Id);
        }


        #endregion

        #region upload media
        /// <summary>
        /// Returns the file extension from mime type.
        /// </summary>
        /// <param name="mimeType">Mime type</param>
        /// <returns>File extension</returns>
        protected virtual string GetFileExtensionFromMimeType(string mimeType)
        {
            if (mimeType == null)
                return null;

            //TODO use FileExtensionContentTypeProvider to get file extension

            var parts = mimeType.Split('/');
            var lastPart = parts[parts.Length - 1];
            switch (lastPart)
            {
                case "pjpeg":
                    lastPart = "jpg";
                    break;
                case "x-png":
                    lastPart = "png";
                    break;
                case "x-icon":
                    lastPart = "ico";
                    break;
            }

            return lastPart;
        }


        /// <summary>
        /// Get picture (thumb) local path
        /// </summary>
        /// <param name="thumbFileName">Filename</param>
        /// <returns>Local picture thumb path</returns>
        protected virtual string GetThumbLocalPath(string thumbFileName)
        {
            var thumbsDirectoryPath = _fileProvider.GetAbsolutePath(NopMediaDefaults.ImageThumbsPath);

            if (_mediaSettings.MultipleThumbDirectories)
            {
                //get the first two letters of the file name
                var fileNameWithoutExtension = _fileProvider.GetFileNameWithoutExtension(thumbFileName);
                if (fileNameWithoutExtension != null && fileNameWithoutExtension.Length > NopMediaDefaults.MultipleThumbDirectoriesLength)
                {
                    var subDirectoryName = fileNameWithoutExtension.Substring(0, NopMediaDefaults.MultipleThumbDirectoriesLength);
                    thumbsDirectoryPath = _fileProvider.GetAbsolutePath(NopMediaDefaults.ImageThumbsPath, subDirectoryName);
                    _fileProvider.CreateDirectory(thumbsDirectoryPath);
                }
            }

            var thumbFilePath = _fileProvider.Combine(thumbsDirectoryPath, thumbFileName);
            return thumbFilePath;
        }

        /// <summary>
        /// Get picture (thumb) URL 
        /// </summary>
        /// <param name="thumbFileName">Filename</param>
        /// <param name="storeLocation">Store location URL; null to use determine the current store location automatically</param>
        /// <returns>Local picture thumb path</returns>
        protected virtual string GetThumbUrl(string thumbFileName, string storeLocation = null)
        {
            storeLocation = !string.IsNullOrEmpty(storeLocation)
                                    ? storeLocation
                                    : _webHelper.GetStoreLocation();
            var url = storeLocation + "images/thumbs/";

            if (_mediaSettings.MultipleThumbDirectories)
            {
                //get the first two letters of the file name
                var fileNameWithoutExtension = _fileProvider.GetFileNameWithoutExtension(thumbFileName);
                if (fileNameWithoutExtension != null && fileNameWithoutExtension.Length > NopMediaDefaults.MultipleThumbDirectoriesLength)
                {
                    var subDirectoryName = fileNameWithoutExtension.Substring(0, NopMediaDefaults.MultipleThumbDirectoriesLength);
                    url = url + subDirectoryName + "/";
                }
            }

            url = url + thumbFileName;
            return url;
        }


        /// <summary>
        /// Get picture local path. Used when images stored on file system (not in the database)
        /// </summary>
        /// <param name="fileName">Filename</param>
        /// <returns>Local picture path</returns>
        protected virtual string GetPictureLocalPath(string fileName)
        {
            return _fileProvider.GetAbsolutePath("images", fileName);
        }

        /// <summary>
        /// Gets the loaded picture binary depending on picture storage settings
        /// </summary>
        /// <param name="picture">Picture</param>
        /// <param name="fromDb">Load from database; otherwise, from file system</param>
        /// <returns>Picture binary</returns>
        protected virtual byte[] LoadPictureBinary(Picture picture, bool fromDb)
        {
            if (picture == null)
                throw new ArgumentNullException(nameof(picture));

            var result = fromDb
                ? picture.PictureBinary?.BinaryData ?? new byte[0]
                : LoadPictureFromFile(picture.Id, picture.MimeType);

            return result;
        }

        /// <summary>
        /// Get a value indicating whether some file (thumb) already exists
        /// </summary>
        /// <param name="thumbFilePath">Thumb file path</param>
        /// <param name="thumbFileName">Thumb file name</param>
        /// <returns>Result</returns>
        protected virtual bool GeneratedThumbExists(string thumbFilePath, string thumbFileName)
        {
            return _fileProvider.FileExists(thumbFilePath);
        }

        
        /// <summary>
        /// Save a value indicating whether some file (thumb) already exists
        /// </summary>
        /// <param name="thumbFilePath">Thumb file path</param>
        /// <param name="thumbFileName">Thumb file name</param>
        /// <param name="mimeType">MIME type</param>
        /// <param name="binary">Picture binary</param>
        protected virtual void SaveThumb(string thumbFilePath, string thumbFileName, string mimeType, byte[] binary)
        {
            //ensure \thumb directory exists
            var thumbsDirectoryPath = _fileProvider.GetAbsolutePath(NopMediaDefaults.ImageThumbsPath);
            _fileProvider.CreateDirectory(thumbsDirectoryPath);

            //save
            _fileProvider.WriteAllBytes(thumbFilePath, binary);
        }
        /// <summary>
        /// Loads a picture from file
        /// </summary>
        /// <param name="pictureId">Picture identifier</param>
        /// <param name="mimeType">MIME type</param>
        /// <returns>Picture binary</returns>
        protected virtual byte[] LoadPictureFromFile(int pictureId, string mimeType)
        {
            var lastPart = GetFileExtensionFromMimeType(mimeType);
            var fileName = $"{pictureId:0000000}_0.{lastPart}";
            var filePath = GetPictureLocalPath(fileName);

            return _fileProvider.ReadAllBytes(filePath);
        }

        /// <summary>
        /// Save picture on file system
        /// </summary>
        /// <param name="pictureId">Picture identifier</param>
        /// <param name="pictureBinary">Picture binary</param>
        /// <param name="mimeType">MIME type</param>
        protected virtual void SavePictureInFile(int pictureId, byte[] pictureBinary, string mimeType)
        {
            var fileName = GetPictureSystemName(pictureId, mimeType);
            _fileProvider.WriteAllBytes(GetPictureLocalPath(fileName), pictureBinary);
        }

        public string GetPictureSystemName(int pictureId, string mimeType)
        {
            var lastPart = GetFileExtensionFromMimeType(mimeType);
            var fileName = $"{pictureId:0000000}_0.{lastPart}";
            return fileName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="picture"></param>
        /// <param name="generalSettings"></param>
        /// <param name="mediaSettings"></param>
        public void WritePictureBytes(Media picture, GeneralSettings generalSettings, MediaSettings mediaSettings)
        {
            //we need to save the file on file system
            if (picture.Binary == null || !picture.Binary.Any())
            {
                var errorMessage = "Can't write empty bytes for picture";
                throw new Exception(errorMessage);
            }

            if (mediaSettings.PictureSaveLocation == MediaSaveLocation.FileSystem)
            {
                var fileName = GetPictureSystemName(picture.Id, picture.MimeType);
                SavePictureInFile(picture.Id, picture.Binary, picture.MimeType);

                //string fileExtension = PathUtility.GetFileExtensionFromContentType(picture.MimeType);

                //if (string.IsNullOrEmpty(picture.SystemName))
                //    picture.SystemName = $"{Guid.NewGuid().ToString("n")}";

                //string proposedFileName = $"{picture.SystemName}{fileExtension}";

                //// get directory Path
                //string directoryPath = GetDirectoryPath(mediaSettings);

                //string filePath = PathUtility.GetFileSavePath(directoryPath, proposedFileName);


                //ImageFormat imageFormat = PictureUtility.GetImageFormatFromContentType(picture.MimeType);

                //// lưu ảnh lên ổ cứng
                //WriteBytesToImage(picture.Binary, filePath, imageFormat);

                //clear bytes t/h ghi file system
                //picture.Binary = null;

                // upload to local server
                //string expectedFile = ServerHelper.GetRelativePathFromLocalPath(filePath);

                //picture.ThumbnailPath = GetThumbUrl(fileName);

                // upload to remote server
                //string expectedFile = $"{filePath}";

                //picture.ThumbnailPath = expectedFile.Replace(generalSettings.MediaSaveLocation, generalSettings.ImageServerDomain).Replace("\\", "/");

                // update đường dẫn local trên server lưu trữ
                picture.LocalPath = GetPictureLocalPath(fileName);

            }
        }

        ///// <summary>
        ///// Lưu ảnh lên ổ cứng
        ///// </summary>
        ///// <param name="imageBytes"></param>
        ///// <param name="filePath"></param>
        ///// <param name="imageFormat"></param>
        //public void WriteBytesToImage(byte[] imageBytes, string filePath, ImageFormat imageFormat)
        //{
        //    using (var mStream = new MemoryStream(imageBytes))
        //    {
        //        var image = MediaTypeNames.Image.FromStream(mStream);
        //        image.Save(filePath, imageFormat);
        //    }
        //}


        #endregion

    }
}
