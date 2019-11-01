using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net.Core;
using log4net.Appender;

namespace log4netListener
{
    public class RemoteAppenderSink : MarshalByRefObject, RemotingAppender.IRemoteLoggingSink
    {
        private readonly ILogger _Logger;
        private readonly ISender _EmailSender;
        
        public RemoteAppenderSink(ISender sender, ILogger logger)
        {
            _Logger = logger ?? throw new System.ArgumentNullException("logger is missing");
            _EmailSender = sender ?? throw new System.ArgumentNullException("sender is missing");
        }
        /*
        public class LoggingArgs : EventArgs
        {
            public IEnumerable<LoggingEvent> LoggingEvents;
        }
        public EventHandler<LoggingArgs> EventsReached;
        */
        void RemotingAppender.IRemoteLoggingSink.LogEvents(LoggingEvent[] events)
        {
            foreach (var loggingEvent in events)
            {
                //process log4net event
                //Console.WriteLine($"    Received event{loggingEvent.Level}");
                string subject = $"{loggingEvent.Level.DisplayName} Event occured in {loggingEvent.Domain}";
                string message = $"{loggingEvent.Level.DisplayName} event occured in {loggingEvent.Domain} - {loggingEvent.RenderedMessage}";
                //var sent = _EmailSender.SendNotificationAsync(subject, message);
                var sent = Task.Run(() => _EmailSender.SendNotificationAsync(subject, message)).GetAwaiter().GetResult();
                if (sent)
                {
                    Console.WriteLine($"    {subject} sent ");
                }
            }
        }
    }
}
