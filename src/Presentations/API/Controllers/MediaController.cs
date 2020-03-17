using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vnit.ApplicationCore.Constants;
using Vnit.ApplicationCore.Entities.MediaAggregate;
using Vnit.ApplicationCore.Entities.News;
using Vnit.ApplicationCore.Entities.Settings;
using Vnit.ApplicationCore.Enums;
using Vnit.ApplicationCore.Helpers;
using Vnit.ApplicationCore.Interfaces;
using Vnit.ApplicationCore.Services.Medias;
using Vnit.Services.Medias;
using Vnit.Services.Security;

namespace Catalog.API.Controllers
{
    public class MediaController : BaseApiController
    {
        #region Members
        private readonly MediaSettings _mediaSettings;
        private readonly GeneralSettings _generalSettings;

        private readonly IMediaService _mediaService;

        private readonly ITagService _tagService;
        private readonly IApplicationConfiguration _applicationConfiguration;

        private readonly IDownloadService _downloadService;
        private readonly INopFileProvider _fileProvider;
        private readonly IPictureService _pictureService;
        #endregion

        #region Ctors

        public MediaController(IMediaService mediaService, ITagService tagService, IApplicationConfiguration applicationConfiguration,
            IDownloadService downloadService,
            INopFileProvider fileProvider,
            IPictureService pictureService
            )
        {
            _downloadService = downloadService;
            _fileProvider = fileProvider;
            _pictureService = pictureService;

            _mediaService = mediaService;
            _tagService = tagService;
            _applicationConfiguration = applicationConfiguration;

            _mediaSettings = new MediaSettings()
            {
                ThumbnailPictureSize = "100x100",
                SmallProfilePictureSize = "64x64",
                MediumProfilePictureSize = "128x128",
                SmallCoverPictureSize = "300x50",
                MediumCoverPictureSize = "800x300",
                PictureSaveLocation = MediaSaveLocation.FileSystem,
                PictureSavePath = _applicationConfiguration.GetSetting(ConstantKey.PictureSavePath),
                VideoSavePath = _applicationConfiguration.GetSetting(ConstantKey.VideoSavePath),

                OtherMediaSaveLocation = MediaSaveLocation.FileSystem,
                OtherMediaSavePath = _applicationConfiguration.GetSetting(ConstantKey.OtherMediaSavePath),
                DefaultUserProfileImageUrl = "/Content/Media/d_male.jpg"
            };
            _generalSettings = new GeneralSettings()
            {
                MediaSaveLocation = _applicationConfiguration.GetSetting(ConstantKey.MediaSaveLocation),
                ImageServerDomain = _applicationConfiguration.GetSetting(ConstantKey.ImageServerUrl),
                VideoServerDomain = _applicationConfiguration.GetSetting(ConstantKey.ImageServerUrl)
            };
        }

        #endregion
        [HttpPost]
        public virtual IActionResult AsyncUpload()
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.UploadPictures))
            //    return Json(new { success = false, error = "You do not have required permissions" }, "text/plain");

            var httpPostedFile = Request.Form.Files.FirstOrDefault();
            if (httpPostedFile == null)
            {
                return Json(new
                {
                    success = false,
                    message = "No file uploaded",
                    downloadGuid = Guid.Empty
                });
            }

            var fileBinary = _downloadService.GetDownloadBits(httpPostedFile);

            const string qqFileNameParameter = "qqfilename";
            var fileName = httpPostedFile.FileName;
            if (string.IsNullOrEmpty(fileName) && Request.Form.ContainsKey(qqFileNameParameter))
                fileName = Request.Form[qqFileNameParameter].ToString();
            //remove path (passed in IE)
            fileName = _fileProvider.GetFileName(fileName);

            var contentType = httpPostedFile.ContentType;

            var fileExtension = _fileProvider.GetFileExtension(fileName);
            if (!string.IsNullOrEmpty(fileExtension))
                fileExtension = fileExtension.ToLowerInvariant();

            //contentType is not always available 
            //that's why we manually update it here
            //http://www.sfsu.edu/training/mimetype.htm
            if (string.IsNullOrEmpty(contentType))
            {
                switch (fileExtension)
                {
                    case ".bmp":
                        contentType = MimeTypes.ImageBmp;
                        break;
                    case ".gif":
                        contentType = MimeTypes.ImageGif;
                        break;
                    case ".jpeg":
                    case ".jpg":
                    case ".jpe":
                    case ".jfif":
                    case ".pjpeg":
                    case ".pjp":
                        contentType = MimeTypes.ImageJpeg;
                        break;
                    case ".png":
                        contentType = MimeTypes.ImagePng;
                        break;
                    case ".tiff":
                    case ".tif":
                        contentType = MimeTypes.ImageTiff;
                        break;
                    default:
                        break;
                }
            }

            var picture = _pictureService.InsertPicture(fileBinary, contentType, null);

            //when returning JSON the mime-type must be set to text/plain
            //otherwise some browsers will pop-up a "Save As" dialog.
            return Json(new { success = true, pictureId = picture.Id, imageUrl = _pictureService.GetPictureUrl(picture, 100) });
        }

        #region Upload
        [Route("uploadpictures")]
        [HttpPost]
        public IActionResult UploadPictures()
        {
            var httpPostedFiles = Request.Form.Files;

            if (httpPostedFiles.Count == 0)
            {
                VerboseReporter.ReportError("No file uploaded", "upload_pictures");
                return RespondFailure();
            }
            
           
            var newImages = new List<object>();
            for (var index = 0; index < httpPostedFiles.Count; index++)
            {

                //the file
                var file = httpPostedFiles[index];

                //and it's name
                //var fileName = file.FileName;

                var httpPostedFile = httpPostedFiles.FirstOrDefault();

                if (httpPostedFile == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "No file uploaded",
                        downloadGuid = Guid.Empty
                    });
                }

                var fileBinary = _downloadService.GetDownloadBits(httpPostedFile);

                const string qqFileNameParameter = "qqfilename";
                var fileName = httpPostedFile.FileName;
                if (string.IsNullOrEmpty(fileName) && Request.Form.ContainsKey(qqFileNameParameter))
                    fileName = Request.Form[qqFileNameParameter].ToString();
                //remove path (passed in IE)
                fileName = _fileProvider.GetFileName(fileName);

                var contentType = httpPostedFile.ContentType;

                var fileExtension = _fileProvider.GetFileExtension(fileName);
                if (!string.IsNullOrEmpty(fileExtension))
                    fileExtension = fileExtension.ToLowerInvariant();

                //stream to read the bytes
                //var stream = file.InputStream;
                //var pictureBytes = new byte[stream.Length];
                //stream.Read(pictureBytes, 0, pictureBytes.Length);

               

                if (string.IsNullOrEmpty(contentType))
                {
                    contentType = PictureUtility.GetContentType(fileExtension);
                }

                var media = new Media()
                {
                    Binary = fileBinary,
                    MimeType = contentType,
                    Name = fileName,
                    UserId = CurrentUser.Id,
                    DateCreated = DateTime.UtcNow,
                    MediaType = MediaType.Image
                };

                _mediaService.WritePictureBytes(media, _generalSettings, _mediaSettings);
                //save it
                _mediaService.Insert(media);
                //newImages.Add(picture.ToModel(_mediaService, _mediaSettings));

                newImages.Add(media);
            }

            return RespondSuccess(new { Images = newImages });
        }

        //#endregion

        //#region Upload Video
        //[Route("uploadVideos")]
        //[HttpPost]
        //public JsonResult UploadVideos(MediaModel model)
        //{
        //    if (model == null)
        //    {
        //        return RespondFailure();
        //    }

        //    Uri urlCheck = new Uri(model.LocalPath);
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlCheck);
        //    request.Timeout = 15000;
        //    try
        //    {
        //        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //    }
        //    catch (Exception)
        //    {
        //        VerboseReporter.ReportError("Đường dẫn không hợp lệ");
        //        return RespondFailure();
        //    }

        //    var checkYoutubeUrl = YoutubeHelper.IsYouTubeUrl(model.LocalPath);
        //    var youTubeId = "";
        //    var youtubeThumbnail = "";
        //    var youtubeEmbed = "";

        //    if (checkYoutubeUrl)
        //    {
        //        youTubeId = YoutubeHelper.GetId(model.LocalPath);
        //        youtubeThumbnail = YoutubeHelper.GetThumbFromId(youTubeId);
        //        youtubeEmbed = YoutubeHelper.GetEmbedUrlFromId(youTubeId);
        //    }

        //    var media = new Media()
        //    {
        //        SystemName = youTubeId,
        //        LocalPath = youtubeEmbed,
        //        ThumbnailPath = youtubeThumbnail,
        //        UserId = CurrentUser.Id,
        //        DateCreated = DateTime.UtcNow,
        //        MediaType = MediaType.Video
        //    };

        //    _mediaService.Insert(media);

        //    return RespondSuccess(media);
        //}

        #endregion

       
        //#region Public methods
        //[HttpGet]
        //[Route("get")]
        //public JsonResult Get(MediaRequestModel requestModel)
        //{
        //    #region predicate
        //    Expression<Func<Media, bool>> where = x => true;
        //    DateTime dateFrom, dateTo;

        //    if (requestModel.MediaType.HasValue)
        //        where = a => a.MediaType == (MediaType)requestModel.MediaType.Value;

        //    if (!string.IsNullOrWhiteSpace(requestModel.Name))
        //        where = ExpressionHelpers.CombineAnd<Media>(where, a => a.Description.Contains(requestModel.Name));

        //    if ((requestModel.DateFrom.IsNotNullOrEmpty()))
        //    {
        //        dateFrom = requestModel.DateFrom.ToDateTime();
        //        where = ExpressionHelpers.CombineAnd<Media>(where, a => a.DateCreated >= dateFrom);
        //    }
        //    if ((requestModel.DateTo.IsNotNullOrEmpty()))
        //    {
        //        dateTo = requestModel.DateTo.ToDateTime();
        //        where = ExpressionHelpers.CombineAnd<Media>(where, a => a.DateCreated <= dateTo);
        //    }
        //    #endregion

        //    var allMedia = _mediaService.GetPagedList(
        //        where,
        //        x => x.DateCreated,
        //        false,
        //        requestModel.Page - 1,
        //        requestModel.Count, e => e.MediaTags.Select(x => x.Tag));
        //    if (allMedia == null)
        //        return RespondFailure();
        //    var model = allMedia;

        //    return RespondSuccess(model, model.TotalCount);
        //}

        //[HttpGet]
        //[Route("getTags")]
        //public JsonResult GetTags(MediaRequestModel requestModel)
        //{
        //    Expression<Func<Tag, bool>> where = x => true;

        //    if (!string.IsNullOrWhiteSpace(requestModel.Name))
        //        where = ExpressionHelpers.CombineAnd<Tag>(where, a => a.Name.Contains(requestModel.Name));

        //    var allMedia = _tagService.GetPagedList(
        //        where,
        //        null,
        //        false,
        //        requestModel.Page - 1,
        //        requestModel.Count);
        //    if (allMedia == null)
        //        return RespondFailure();
        //    var model = allMedia;

        //    return RespondSuccess(model, model.TotalCount);
        //}

        //[HttpGet]
        //[Route("get/{id:int}")]
        //public JsonResult GetById(int id)
        //{
        //    var media = _mediaService.Get(id, e => e.MediaTags.Select(x => x.Tag));
        //    if (media == null)
        //        return RespondFailure();
        //    var model = media.ToModel();

        //    return RespondSuccess(model);
        //}

        //[HttpPost]
        //public JsonResult Post(MediaModel model)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest();

        //    var media = new Media
        //    {
        //        DateCreated = DateTime.Now,
        //        Name = model.Name,
        //        SystemName = model.SystemName,
        //        Description = model.Description,
        //        AlternativeText = model.AlternativeText,
        //        LocalPath = model.LocalPath,
        //        ThumbnailPath = model.ThumbnailPath,
        //        MimeType = model.MimeType,
        //        Binary = model.Binary,
        //        MediaType = model.MediaType,
        //        UserId = CurrentUser.Id
        //    };
        //    //save it and respond

        //    _mediaService.Insert(media);
        //    //we should have at least one role
        //    //if (model.TagIds.Length == 0)
        //    //{
        //    //    VerboseReporter.ReportError("Ban chưa gán album cho media", "post_user");
        //    //    return RespondFailure();
        //    //}

        //    if (model.TagIds != null && model.TagIds.Length > 0)
        //    {
        //        media.MediaTags = new List<MediaTag>();

        //        foreach (int tagId in model.TagIds)
        //        {
        //            MediaTag newsPubs = new MediaTag()
        //            {
        //                MediaId = media.Id,
        //                TagId = tagId,
        //                //IsFeatured = model.IsFeatured
        //            };
        //            media.MediaTags.Add(newsPubs);
        //        }
        //        _mediaService.Update(media);
        //    }

        //    VerboseReporter.ReportSuccess("Tạo thư viện ảnh thành công", "post_setting");
        //    return RespondSuccess(media);
        //}

        //[HttpPut]
        //public JsonResult Put(MediaModel entityModel)
        //{
        //    var media = _mediaService.FirstOrDefault(x => x.Id == entityModel.Id, x => x.MediaTags);
        //    //save it and respond

        //    media.MediaType = entityModel.MediaType;
        //    media.Description = entityModel.Description;
        //    media.IsFeatured = entityModel.IsFeatured;

        //    _mediaService.Update(media);

        //    VerboseReporter.ReportSuccess("Tạo thư viện ảnh thành công", "post");

        //    //assign the tags now

        //    List<int> currentTagIds = new List<int>();
        //    if (media.MediaTags != null)
        //    {
        //        currentTagIds = media.MediaTags.Select(x => x.TagId).ToList();
        //        //tag to unassign
        //        if (entityModel.TagIds != null)
        //        {
        //            var tagsToUnassign = currentTagIds.Except(entityModel.TagIds);
        //            foreach (var tagId in tagsToUnassign)
        //            {
        //                var tag = _tagService.FirstOrDefault(x => x.Id == tagId);
        //                if (tag == null)
        //                    continue;

        //                _tagService.UnassignTagToMedia(tag, media);
        //            }

        //        }

        //    }
        //    if (entityModel.TagIds != null)
        //    {
        //        //roles to assign
        //        var tagsToAssign = entityModel.TagIds.Except(currentTagIds);
        //        foreach (var tagId in tagsToAssign)
        //        {
        //            var tag = _tagService.FirstOrDefault(x => x.Id == tagId);
        //            if (tag == null)
        //                continue;

        //            _tagService.AssignTagToMedia(tag, media);
        //        }
        //    }
        //    VerboseReporter.ReportSuccess("Gán từ khóa thành công", "post");
        //    return RespondSuccess(media);
        //}

        //[Route("delete/{id:int}")]
        //[HttpDelete]
        //public JsonResult Delete(int id)
        //{
        //    if (id <= 0)
        //        return BadRequest();

        //    var media = _mediaService.FirstOrDefault(x => x.Id == id);

        //    _mediaService.ClearMediaAttachments(media);

        //    _mediaService.Delete(media);

        //    VerboseReporter.ReportSuccess("Xóa ảnh thành công", "delete");
        //    return RespondSuccess();
        //}

        //[HttpGet]
        //public JsonResult GetMediaTypes()
        //{
        //    return RespondSuccess(SelectListItemExtension.GetEnums<MediaType>());
        //}
        //#endregion

    }
}