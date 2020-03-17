using System.Net;
using Microsoft.AspNetCore.Mvc;
using Vnit.ApplicationCore.Entities.Users;
using Vnit.Api.ViewModels;
using Vnit.ApplicationCore.Management;
using Vnit.Services.VerboseReporter;

namespace Vnit.Api.Controllers.Api
{
    /// <summary>
    /// Base Api Controller
    /// </summary>
    [Route("api/[controller]s")]
    [ApiController]
    public class BaseApiController : Controller
    {
        protected IVerboseReporterService VerboseReporter;

        public User CurrentUser
        {
            get
            {
                return new User();
            }
        }
        protected BaseApiController()
        {
            VerboseReporter = EngineContext.Current.Resolve<IVerboseReporterService>();
        }

        public IActionResult ResponseActionResult(dynamic obj)
        {
            return Ok(obj);
        }

        public IActionResult RespondSuccess()
        {
            return RespondSuccess(null);
        }
        public IActionResult RespondSuccess(dynamic additionalData)
        {
            return RespondSuccess(additionalData, 0);
        }
        public IActionResult RespondSuccess(dynamic additionalData, int total)
        {
            return ResponseActionResult(new RootResponseModel()
            {
                Success = true,
                Code = HttpStatusCode.OK,
                ErrorMessages = VerboseReporter.GetErrorsList(),
                Messages = VerboseReporter.GetSuccessList(),
                ResponseData = additionalData,
                Total = total
            });
        }
        public IActionResult RespondSuccess(string successMessage, string contextName, dynamic additionalData = null)
        {
            VerboseReporter.ReportSuccess(successMessage, contextName);
            return RespondSuccess(additionalData);
        }

        public IActionResult RespondFailure()
        {
            return RespondFailure(null);
        }

        public IActionResult RespondFailure(string errorMessage, string contextName, dynamic additionalData = null)
        {
            VerboseReporter.ReportError(errorMessage, contextName);
            return RespondFailure(additionalData);
        }

        public IActionResult RespondFailure(dynamic additionalData)
        {
            return ResponseActionResult(new RootResponseModel()
            {
                Success = false,
                Code = HttpStatusCode.ExpectationFailed,
                ErrorMessages = VerboseReporter.GetErrorsList(),
                Messages = VerboseReporter.GetSuccessList(),
                ResponseData = additionalData
            });
        }
    }
}
