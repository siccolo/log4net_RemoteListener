using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log4netListener
{
    public interface ILogger
    {
        void LogError(Exception errorInfo);

        void LogError(string message
                    , [System.Runtime.CompilerServices.CallerMemberName] string callingMethodName = ""
                    , [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = ""
                    , [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0);

        void LogInfo(string message
                    , [System.Runtime.CompilerServices.CallerMemberName] string callingMethodName = ""
                    , [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = ""
                    , [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0);
    }
}
