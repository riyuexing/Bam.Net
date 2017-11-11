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
using System.Reflection;
using ThreeHeadz.Tvg;
using Bam.Net.Services;
using Bam.Net.CoreServices;

namespace ThreeHeadz.Tvg.CoreHeart
{
    [Serializable]
    public class ConsoleActions : CommandLineTestInterface
    {
        const string Organization = "Three Headz";
        const string ApplicationName = "CoreHeartServer";
        
        static ServiceProxyServer server;

        [ConsoleAction("killHeartServer", "Kill the Heart server")]
        public void StopServer()
        {
            if (server != null)
            {
                server.Stop();
                Pause("Heart stopped");
            }
            else
            {
                OutLine("Heart server not running");
            }
        }

        [ConsoleAction("startHeartServer", "Start the Heart server")]
        public void StartCoreHeartServer()
        {
            HostPrefix[] prefixes = GetConfiguredHostPrefixes();
            ILogger logger = GetLogger();
            Log.Default = logger;
            ServiceRegistry serviceRegistry = CoreServiceRegistryContainer.Create();            
            server = serviceRegistry.Serve(prefixes, logger);
            Pause($"Heart server is serving service registry {serviceRegistry.Name}");
        }

        public static HostPrefix[] GetConfiguredHostPrefixes()
        {
            int port = int.Parse(DefaultConfiguration.GetAppSetting("Port", "80"));
            bool ssl = DefaultConfiguration.GetAppSetting("Ssl").IsAffirmative();
            List<HostPrefix> results = new List<HostPrefix>();
            foreach(string hostName in DefaultConfiguration.GetAppSetting("HostNames").Or("localhost").DelimitSplit(",", true))
            {
                HostPrefix hostPrefix = new HostPrefix()
                {
                    HostName = hostName,
                    Port = port,
                    Ssl = ssl
                };
                results.Add(hostPrefix);
            }
            return results.ToArray();
        }

        private static ConsoleLogger GetLogger()
        {
            ConsoleLogger logger = new ConsoleLogger()
            {
                AddDetails = false,
                UseColors = true
            };
            logger.StartLoggingThread();
            return logger;
        }

        private Type GetServiceType(string className)
        {
            Assembly assembly = GetAssembly(className, out Type result);
            return result;
        }

        private Assembly GetAssembly(string className, out Type type)
        {
            type = null;
            Assembly result = null;
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                type = GetType(assembly, className);
                if(type != null)
                {
                    result = assembly;
                    break;
                }
            }
            if(result == null)
            {
                string assemblyPath = GetArgument("assemblyPath", true);
                result = Assembly.LoadFrom(assemblyPath);
                type = GetType(result, className);
                if(type == null)
                {
                    type = result.GetType(className);
                }
            }

            return result;
        }

        private Type GetType(Assembly assembly, string className)
        {
            return assembly.GetTypes().Where(t => t.Name.Equals(className) || $"{t.Namespace}.{t.Name}".Equals(className)).FirstOrDefault();
        }
    }
}