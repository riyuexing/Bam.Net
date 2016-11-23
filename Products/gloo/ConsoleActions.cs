using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bam.Net.CommandLine;
using Bam.Net;
using Bam.Net.Testing;
using Bam.Net.Encryption;
using Bam.Net.Server;
using Bam.Net.Configuration;
using Bam.Net.ServiceProxy;
using Bam.Net.Logging;
using Bam.Net.Server.Tvg;
using System.Reflection;

namespace gloo
{
    [Serializable]
    public class ConsoleActions : CommandLineTestInterface
    {
        static string contentRootConfigKey = "ContentRoot";
        static string defaultRoot = "C:\\tvg";
        static GlooServer glooServer;
        
        [ConsoleAction("startGlooServer", "Start the gloo server")]
        public void StartGlooServer()
        {
            ConsoleLogger logger = GetLogger();
            StartGlooServer(logger);
            Pause("Gloo is running");
        }

        [ConsoleAction("killGlooServer", "Kill the gloo server")]
        public void StopGlooServer()
        {
            if (glooServer != null)
            {
                glooServer.Stop();
                Pause("Gloo stopped");
            }
            else
            {
                OutLine("Gloo server not running");
            }
        }

        public static void StartGlooServer(ConsoleLogger logger)
        {
            BamConf conf = BamConf.Load(DefaultConfiguration.GetAppSetting(contentRootConfigKey).Or(defaultRoot));
            glooServer = new GlooServer(conf, logger);
            glooServer.HostPrefixes = new HostPrefix[] { GetConfiguredHostPrefix() };
            glooServer.MonitorDirectories = DefaultConfiguration.GetAppSetting("MonitorDirectories").DelimitSplit(",", ";");
            glooServer.Start();
        }
        public static HostPrefix GetConfiguredHostPrefix()
        {
            HostPrefix hostPrefix = new HostPrefix();
            hostPrefix.HostName = DefaultConfiguration.GetAppSetting("HostName").Or("localhost");
            hostPrefix.Port = int.Parse(DefaultConfiguration.GetAppSetting("Port"));
            hostPrefix.Ssl = bool.Parse(DefaultConfiguration.GetAppSetting("Ssl"));
            return hostPrefix;
        }
        private static ConsoleLogger GetLogger()
        {
            ConsoleLogger logger = new ConsoleLogger();
            logger.AddDetails = false;
            logger.UseColors = true;
            logger.StartLoggingThread();
            return logger;
        }
    }
}