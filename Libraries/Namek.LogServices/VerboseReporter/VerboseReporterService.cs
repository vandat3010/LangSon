using System.Linq;
using System.Runtime.CompilerServices;

namespace Namek.LogServices.VerboseReporter
{
    public class VerboseReporterService : IVerboseReporterService
    {
        private string _verboseErrorMessages;
        private string _verboseSuccessMessages; //Dictionary<string, List<string>>

        public void ReportError(string error, string errorContextName = "", [CallerMemberName] string callerName = null)
        {
            //todo: use callername to keep track and log the error sources
            //if (!_verboseErrorMessages.ContainsKey(errorContextName))
            //    _verboseErrorMessages.Add(errorContextName, new List<string>());

            //_verboseErrorMessages[errorContextName].Add(error);
            _verboseErrorMessages = error;
        }

        public void ReportSuccess(string success, string successContextName = "")
        {
            //if (!_verboseSuccessMessages.ContainsKey(successContextName))
            //    _verboseSuccessMessages.Add(successContextName, new List<string>());

            //_verboseSuccessMessages[successContextName].Add(success);

            _verboseSuccessMessages = success;
        }

        public string GetErrorsList()
        {
            return _verboseErrorMessages;
        }

        public string GetSuccessList()
        {
            return _verboseSuccessMessages;
        }

        public bool HasErrors()
        {
            return _verboseErrorMessages.Any();
        }

        public bool HasErrors(string errorContextName)
        {
            return
                !string.IsNullOrEmpty(
                    _verboseErrorMessages); // _verboseErrorMessages.Any(x => x.Key == errorContextName && x.Value.Any());
        }
    }
}