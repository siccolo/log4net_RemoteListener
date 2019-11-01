using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log4netListener
{
    public interface IConfig 
    {
        string Environment { get;  }
        string NotificationRecipientList { get; }
        string SendEmailGateway { get; }
    }

    public class AppConfig:IConfig
    {
        public string Environment { get; private set; }
        public string NotificationRecipientList { get; private set; }
        public string SendEmailGateway { get; private set; }

        public AppConfig(string env, string recipientlist, string sendergateway)
        {
            Environment = env;
            NotificationRecipientList = recipientlist;
            SendEmailGateway = sendergateway;
        }
    }
}
