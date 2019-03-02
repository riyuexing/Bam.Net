using Bam.Net.Configuration;
using Bam.Net.CoreServices;
using Bam.Net.Logging;
using Bam.Net.Presentation;
using Bam.Net.Services.Clients;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Bam.Net.Services
{
    /// <summary>
    /// A service registry (or dependency injection container) for the currently running application process.  The application name is
    /// determined by the default configuration file (app.config or web.config).
    /// </summary>
    public class ApplicationServiceRegistry: ServiceRegistry
    {
        protected CoreClient CoreClient { get; set; }

        static ApplicationServiceRegistry _appRegistry;
        static object _appRegistryLock = new object();
        public static ApplicationServiceRegistry Current
        {
            get
            {
                return _appRegistryLock.DoubleCheckLock(ref _appRegistry, () => Configure(Configurer ?? ((reg) => { })));
            }
            private set
            {
                _appRegistry = value;
            }
        }

        public static Action<ApplicationServiceRegistry> Configurer { get; set; }

        public static ApplicationServiceRegistry Discover(string directoryPath)
        {
            return Discover(new DirectoryInfo(directoryPath));
        }

        public static ApplicationServiceRegistry Discover(DirectoryInfo directoryInfo)
        {
            try
            {
                return Configure((appServiceRegistry) =>
                {
                    foreach (FileInfo file in directoryInfo.GetFiles())
                    {
                        if ((file.Extension?.Equals(".dll", StringComparison.InvariantCultureIgnoreCase)).Value || (file.Extension?.Equals(".exe", StringComparison.InvariantCultureIgnoreCase)).Value)
                        {
                            try
                            {
                                Assembly assembly = Assembly.LoadFile(file.FullName);
                                foreach(Type type in assembly.GetTypes().Where(t => t.HasCustomAttributeOfType<AppModuleAttribute>()))
                                {
                                    appServiceRegistry.Set(type, type);
                                }
                            }
                            catch (Exception ex)
                            {
                                Log.Warn("Exception loading file for service discovery {0}: {1}", ex, file.FullName, ex.Message);
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Log.Warn("Exception discovering services: {0}", ex, ex.Message);
            }
            return Configure((a) => { });
        }

        [ServiceRegistryLoader]
        public static ApplicationServiceRegistry Configure(Action<ApplicationServiceRegistry> configure)
        {
            Configurer = configure;
            ApplicationServiceRegistry appRegistry = new ApplicationServiceRegistry();
            appRegistry.CombineWith(CoreClientServiceRegistryContainer.Current);
            appRegistry
                .For<IApplicationNameProvider>().Use<DefaultConfigurationApplicationNameProvider>()
                .For<ProxyAssemblyGeneratorService>().Use<ProxyAssemblyGeneratorServiceProxy>()
                .For<ApplicationServiceRegistry>().Use(appRegistry)
                .For<ApplicationModel>().Use<ApplicationModel>();

            configure(appRegistry);
            appRegistry.CoreClient = appRegistry.Get<CoreClient>();
            Current = appRegistry;
            return appRegistry;
        }
    }
}
