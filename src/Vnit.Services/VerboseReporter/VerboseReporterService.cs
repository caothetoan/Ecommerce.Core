using System.Runtime.CompilerServices;
using System.Text;

namespace Vnit.Services.VerboseReporter
{
    public class VerboseReporterService : IVerboseReporterService
    {
        private readonly StringBuilder _verboseErrorMessages;
        private readonly StringBuilder _verboseSuccessMessages;

        public VerboseReporterService()
        {
            _verboseErrorMessages = new StringBuilder();
            _verboseSuccessMessages = new StringBuilder();
        }

        public void ReportError(string error, string errorContextName = "", [CallerMemberName] string callerName = null)
        {
            //todo: use callername to keep track and log the error sources
            //if (!_verboseErrorMessages.ContainsKey(errorContextName))
            //    _verboseErrorMessages.Add(errorContextName, new List<string>());

            //_verboseErrorMessages[errorContextName].Add(error);
            _verboseErrorMessages.AppendLine(error);
        }

        public void ReportSuccess(string success, string successContextName = "")
        {
            //if (!_verboseSuccessMessages.ContainsKey(successContextName))
            //    _verboseSuccessMessages.Add(successContextName, new List<string>());

            //_verboseSuccessMessages[successContextName].Add(success);

            _verboseSuccessMessages.AppendLine(success);
        }

        public string GetErrorsList()
        {
            return _verboseErrorMessages.ToString();
        }

        public string GetSuccessList()
        {
            return _verboseSuccessMessages.ToString();
        }

        public bool HasErrors()
        {
            return _verboseErrorMessages.Length > 0;
        }

        public bool HasErrors(string errorContextName)
        {
            return _verboseErrorMessages.Length > 0;
            //return !string.IsNullOrEmpty(_verboseErrorMessages);
            // _verboseErrorMessages.Any(x => x.Key == errorContextName && x.Value.Any());
        }

    }
}
