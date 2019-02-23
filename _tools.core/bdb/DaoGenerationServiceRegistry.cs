using Bam.Net.CommandLine;
using Bam.Net.CoreServices;
using Bam.Net.Data.Repositories;
using Bam.Net.Data.Repositories.Handlebars;
using Bam.Net.Data.Schema;
using Bam.Net.Data.Schema.Handlebars;
using Bam.Net.Services;
using Bam.Net.Testing;
using System;

namespace Bam.Net.Tests
{
    [Serializable]
    public class DaoGenerationServiceRegistry : ApplicationServiceRegistry
    {
        [ServiceRegistryLoader]
        public static DaoGenerationServiceRegistry GetHandlebarsInstance()
        {
            DaoGenerationServiceRegistry daoRegistry = new DaoGenerationServiceRegistry();
            daoRegistry.CombineWith(Configure(appRegistry =>
            {
                appRegistry
                    .For<IDaoCodeWriter>().Use<HandlebarsDaoCodeWriter>()
                    .For<IDaoTargetStreamResolver>().Use<DaoTargetStreamResolver>()
                    .For<IWrapperGenerator>().Use<HandlebarsWrapperGenerator>()
                    .For<SchemaRepositoryGeneratorSettings>().Use<SchemaRepositoryGeneratorSettings>();               
            }));

            return daoRegistry;
        }
    }
}
