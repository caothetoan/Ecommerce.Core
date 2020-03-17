using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Vnit.Api.ViewModels
{
    public class RootResponseModel
    {
        public bool Success { get; set; }

        public string ErrorMessages { get; set; }

        public string Messages { get; set; }

        public dynamic ResponseData { get; set; }

        public int Total { get; set; }

        public HttpStatusCode Code { get; set; }

        public string RedirectUrl { get; set; }
    }
}
