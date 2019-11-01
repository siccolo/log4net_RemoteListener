using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log4netListener
{
    class Logger : ILogger
    {
        public void LogError(Exception errorInfo)
        {
            //[assembly: log4net.Config.XmlConfigurator(Watch=true)]
            //log4net.Config.XmlConfigurator.Configure();
            try
            {
                log4net.ILog log =
                    log4net.LogManager.GetLogger
                           (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                //4.5 --> CallerMemberNameAttribute!
                //string caller = new System.Diagnostics.StackFrame(1, true).GetMethod().Name;
                log.Error(errorInfo);
            }
            catch {; }
        }


        public void LogError(string message
                    , [System.Runtime.CompilerServices.CallerMemberName] string callingMethodName = ""
                    , [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = ""
                    , [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0
            )
        {
            //[assembly: log4net.Config.XmlConfigurator(Watch=true)]
            //log4net.Config.XmlConfigurator.Configure();
            try
            {
                log4net.ILog log =
                    log4net.LogManager.GetLogger
                           (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                //4.5 --> CallerMemberNameAttribute!
                //[System.Runtime.CompilerServices.CallerMemberName] string callingMethodName= "" , 
                if (String.IsNullOrEmpty(callingMethodName))
                {
                    callingMethodName = new System.Diagnostics.StackFrame(1, true).GetMethod().Name;
                }
                //log.Info(String.Format("{0}", message));
                log.Error(String.Format("{0} {1}", callingMethodName, message));
            }
            catch {; }
        }

        public void LogInfo(string message
                    , [System.Runtime.CompilerServices.CallerMemberName] string callingMethodName = ""
                    , [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = ""
                    , [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0
            )
        {
            //[assembly: log4net.Config.XmlConfigurator(Watch=true)]
            //log4net.Config.XmlConfigurator.Configure();
            try
            {
                log4net.ILog log =
                    log4net.LogManager.GetLogger
                           (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                //4.5 --> CallerMemberNameAttribute!
                //[System.Runtime.CompilerServices.CallerMemberName] string callingMethodName= "" , 
                if (String.IsNullOrEmpty(callingMethodName))
                {
                    callingMethodName = new System.Diagnostics.StackFrame(1, true).GetMethod().Name;
                }
                //log.Info(String.Format("{0}", message));
                log.Info(String.Format("{0} {1}", callingMethodName, message));
            }
            catch {; }
        }

    }
}
