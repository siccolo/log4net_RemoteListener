using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Xml;

using System.Net.Http;

namespace log4netListener
{
    public interface ISender {
        Task<bool> SendNotificationAsync(string subject, string message);
    }

    public class Sender:ISender
    {
        private readonly ILogger _Logger;
        private readonly IConfig _AppConfig;

        
        private readonly IHttpClientFactory _HttpClientFactory;

        private const string _SendEmailParametersTemplate = ""
                        + "<?xml version=\"1.0\" encoding=\"utf-8\"?>"
                        + "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://www.w3.org/2003/05/soap-envelope\">"
                        + "<soap:Body>"
                        + "<SendMailMessage  xmlns=\"https://www.eranit.co.il\">"
                        + "<recipients><string>{0}</string><string>{0}</string></recipients>"
                        + "<subject>{1}</subject>"
                        + "<body><![CDATA[{2}]]></body>"
                        + "</SendMailMessage>"
                        + "</soap:Body>"
                        + "</soap:Envelope>";

        public Sender(IConfig config, IHttpClientFactory httpClientFactory, ILogger logger)
        {
            _AppConfig = config ?? throw new System.ArgumentNullException("config is missing");
            if (String.IsNullOrEmpty(_AppConfig.SendEmailGateway))
            {
                throw new System.ArgumentNullException("sendemailgateway is missing");
            }
            if (String.IsNullOrEmpty(_AppConfig.NotificationRecipientList))
            {
                throw new System.ArgumentNullException("notificationrecipientlist is missing");
            }
            //

            _Logger = logger ?? throw new System.ArgumentNullException("logger is missing");

            _HttpClientFactory = httpClientFactory ?? throw new System.ArgumentNullException("httpClientFactory is missing"); ;
        }

        public async Task<bool> SendNotificationAsync(string subject, string message)
        {
            bool sent = false;
            try
            {
                string url = _AppConfig.SendEmailGateway;// "http://172.10.47.30:7970/MailGateway.asmx";
                string[] recipients = _AppConfig.NotificationRecipientList.Split(';');
                //
                var httpClient = _HttpClientFactory.CreateClient();
                if (!httpClient.DefaultRequestHeaders.Contains("SOAPAction"))
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("SOAPAction", "https://www.eranit.co.il/SendMailMessage");
                }
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "text/xml; charset=utf-8");
                //
                foreach (var recipient in recipients)
                {
                    var parametersXml = String.Format(_SendEmailParametersTemplate
                                                    , recipient
                                                    , subject
                                                    , message);

                    string sendresult = String.Empty;
                    using (var formdata = new StringContent(parametersXml, System.Text.Encoding.UTF8, "text/xml"))
                    {
                        //Task<HttpResponseMessage> taskresponse = httpClient.PostAsync(url, formdata);
                        using(HttpResponseMessage response = await httpClient.PostAsync(url, formdata).ConfigureAwait(false))
                        {
                            //response.EnsureSuccessStatusCode();
                            if (response.IsSuccessStatusCode)
                            {
                                sendresult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                                var doc = new XmlDocument();
                                doc.LoadXml(sendresult);
                                if (doc.ChildNodes.Count > 1)
                                {
                                    sendresult = doc.ChildNodes[1].ChildNodes[0].ChildNodes[0].InnerText;
                                }
                                else
                                {
                                    sendresult = doc.ChildNodes[0].InnerText;
                                }
                                sent = sendresult.ToLower()=="true";
                            }
                            else
                            {
                                string errorresult = await response.Content.ReadAsStringAsync();
                            }                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex);
            }
            return sent;
        }
    }
}

