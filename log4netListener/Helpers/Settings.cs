using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log4netListener
{
    public static class Settings
    {
         
        public static string Environment()
        {
            return System.Configuration.ConfigurationManager.AppSettings["Environment"].ToString();
        }

        public static string NotificationRecipientList()
        {
            return System.Configuration.ConfigurationManager.AppSettings["NotificationRecipientList"].ToString();
        }

        public static string SendEmailGateway()
        {
            return System.Configuration.ConfigurationManager.AppSettings["SendEmailGateway"].ToString();
        }
    }
}
