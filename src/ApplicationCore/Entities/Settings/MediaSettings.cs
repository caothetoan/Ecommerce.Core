﻿using Vnit.ApplicationCore.Enums;
using Vnit.ApplicationCore.Interfaces;

namespace Vnit.ApplicationCore.Entities.Settings
{
    public class MediaSettings : ISettings
    {
        /// <summary>
        /// Maximum file upload size in bytes for images
        /// </summary>
        public long MaximumFileUploadSizeForImages { get; set; }

        /// <summary>
        /// Maximum file upload size in bytes for videos
        /// </summary>
        public long MaximumFileUploadSizeForVideos { get; set; }

        /// <summary>
        /// Maximum file upload size in bytes for documents
        /// </summary>
        public long MaximumFileUploadSizeForDocuments { get; set; }


        /// <summary>
        /// The path where pictures will be saved on file system
        /// </summary>
        public string PictureSavePath { get; set; }

        /// <summary>
        /// Whether the picture should be saved in database or file system
        /// </summary>
        public MediaSaveLocation PictureSaveLocation { get; set; }

        /// <summary>
        /// The path where videos will be saved on file system
        /// </summary>
        public string VideoSavePath { get; set; }

        /// <summary>
        /// The path where other media will be saved on file system
        /// </summary>
        public string OtherMediaSavePath { get; set; }

        /// <summary>
        /// Whether mediashould be saved in database or file system
        /// </summary>
        public MediaSaveLocation OtherMediaSaveLocation { get; set; }

        /// <summary>
        /// The thumbnail size of a picture in pixels. Value should be written as WidthxHeight e.g. 800x600
        /// </summary>
        public string ThumbnailPictureSize { get; set; }

        /// <summary>
        /// The small profile size of a picture in pixels. Value should be written as WidthxHeight e.g. 100x100
        /// </summary>
        public string SmallProfilePictureSize { get; set; }

        /// <summary>
        /// The medium profile size of a picture in pixels. Value should be written as WidthxHeight e.g. 800x600
        /// </summary>
        public string MediumProfilePictureSize { get; set; }

        /// <summary>
        /// The small cover size of a picture in pixels. Value should be written as WidthxHeight e.g. 800x600
        /// </summary>
        public string SmallCoverPictureSize { get; set; }

        /// <summary>
        /// The medium cover size of a picture in pixels. Value should be written as WidthxHeight e.g. 800x600
        /// </summary>
        public string MediumCoverPictureSize { get; set; }

        /// <summary>
        /// Default image that'll be used for user's profile if no image has been set
        /// </summary>
        public string DefaultUserProfileImageUrl { get; set; }

        /// <summary>
        /// Default image that'll be used for user's cover if no image has been set
        /// </summary>
        public string DefaultUserProfileCoverUrl { get; set; }

        /// <summary>
        /// Default image that'll be used for user's profile if no image has been set
        /// </summary>
        public string DefaultArticleImageUrl { get; set; }

        public string DefaultServicePackImageUrl { get; set; }

        public string DefaultServiceImageUrl { get; set; }

        public string DefaultImageUrl { get; set; }

        /// <summary>
        /// Link tạo ảnh thumbnail
        /// </summary>
        public string GenerateThumbnailUrl { get; set; }

        public bool MultipleThumbDirectories { get; set; }
    }

}
