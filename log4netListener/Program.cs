using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Unity;
using Unity.Injection;
using Unity.Microsoft.DependencyInjection;

using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

using log4net.Core;
using System.Runtime.Remoting;

namespace log4netListener
{
    class Program
    {
        static void Main(string[] args)
        {
            Run(args);
            Console.WriteLine("log4netListener started");
            Console.WriteLine("Press key to exit");
            Console.WriteLine();
            Console.ReadLine();
        }

        private static void Run(string[] args)
        {
            //  --  using Microsoft.Extensions.DependencyInjection;
            string environment = Settings.Environment();
            string notificationRecipientList = Settings.NotificationRecipientList();
            string sendEmailGateway = Settings.SendEmailGateway();

            var serviceProvider = new ServiceCollection()
                                .AddSingleton<IConfig, AppConfig>(sp => new AppConfig(environment, notificationRecipientList, sendEmailGateway) )
                                .AddSingleton<ILogger, Logger>()
                                
                                //httpclientfactory
                                .AddHttpClient("Sender", (c) =>
                                {
                                    c.BaseAddress = new Uri(sendEmailGateway);
                                }).Services
                                //httpclientfactory

                                //sender
                                .AddSingleton<ISender, Sender>()

                                //
                                .AddTransient(typeof(RemoteAppenderSink))

                                .BuildServiceProvider();

            //var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
            var sink = serviceProvider.GetService<RemoteAppenderSink>();
            RemotingConfiguration.Configure("log4netListener.exe.config", false);
            RemotingServices.Marshal(sink, "RemoteAppenderLoggingSink");

            //  --  using Microsoft.Extensions.DependencyInjection;
        }

        private static void Run_UsingUnityDI(string[] args)
        {
            using (var container = new Unity.UnityContainer().EnableDiagnostic())
            {
                
                string environment = Settings.Environment();
                string notificationRecipientList = Settings.NotificationRecipientList();
                string sendEmailGateway = Settings.SendEmailGateway();

                //  --  httpclientfactory
                Microsoft.Extensions.DependencyInjection.ServiceCollection serviceCollection = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
                serviceCollection.AddHttpClient("Sender", (c) =>
                {
                    c.BaseAddress = new Uri(sendEmailGateway);
                });
                serviceCollection.BuildServiceProvider(container);
                //var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
                IHttpClientFactory clientFactory = container.Resolve<IHttpClientFactory>();
                HttpClient httpClient = clientFactory.CreateClient("Sender");

                container.RegisterSingleton<IConfig, AppConfig>(
                                                        new InjectionProperty("Environment", environment)
                                                        , new InjectionProperty("NotificationRecipientList", notificationRecipientList)
                                                        , new InjectionProperty("SendEmailGateway", sendEmailGateway)
                                                        );

                container.RegisterSingleton<ILogger, Logger>();


                //Sender(IConfig config, IHttpClientFactory httpClientFactory, ILogger logger)
                /*
                container.RegisterSingleton<ISender, Sender>(
                    new InjectionConstructor(
                               new ResolvedParameter(typeof(IConfig))
                               , new ResolvedParameter(typeof(IHttpClientFactory))
                               , new ResolvedParameter(typeof(ILogger))
                           )
                    );*/
                container.RegisterSingleton<ISender, Sender>();

                container.RegisterType(typeof(RemoteAppenderSink));

                var sink = container.Resolve<RemoteAppenderSink>();

                //sink.EventsReached += (s, a) => AddLog(a.LoggingEvents);
                RemotingConfiguration.Configure("log4netListener.exe.config", false);
                RemotingServices.Marshal(sink, "RemoteAppenderLoggingSink");
            }
        }


    }
}
