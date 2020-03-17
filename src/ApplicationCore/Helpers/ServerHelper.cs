using System;
using System.Collections.ObjectModel;

namespace Vnit.ApplicationCore.Helpers
{
    public class ServerHelper
    {
        //private HttpContext _httpContext;

        //public ServerHelper(IHttpContextAccessor httpContextAccessor)
        //{
        //    _httpContext = httpContextAccessor.HttpContext;
        //}

        ///// <summary>
        ///// Maps a relative path to local path on file system
        ///// </summary>
        ///// <param name="relativePath"></param>
        ///// <returns></returns>
        //public string GetLocalPathFromRelativePath(string relativePath)
        //{
        //    return _httpContext.Connection..Server.MapPath(relativePath);
        //}

        ///// <summary>
        ///// Maps a file system path to relative path
        ///// </summary>
        ///// <param name="localPath"></param>
        ///// <returns></returns>
        //public static string GetRelativePathFromLocalPath(string localPath)
        //{
        //    var appPath = _httpContext.Current.Server.MapPath("~");
        //    var res = $"~{localPath.Replace(appPath, "").Replace("\\", "/")}";
        //    return res;
        //}
        /// <summary>
        /// Gets available timezones on the server
        /// </summary>
        public static ReadOnlyCollection<TimeZoneInfo> GetAvailableTimezones()
        {
            var availableTimezones = TimeZoneInfo.GetSystemTimeZones();
            return availableTimezones;
        }
    }
}
