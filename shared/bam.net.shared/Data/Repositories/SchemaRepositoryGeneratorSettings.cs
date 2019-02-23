using Bam.Net.Application;
using Bam.Net.Data.Schema;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Bam.Net.Data.Repositories
{
    public class SchemaRepositoryGeneratorSettings
    {
        public SchemaRepositoryGeneratorSettings(IDaoCodeWriter daoCodeWriter, IDaoTargetStreamResolver daoTargetStreamResolver, IWrapperGenerator wrapperGenerator)
        {
            DaoCodeWriter = daoCodeWriter;
            DaoTargetStreamResolver = daoTargetStreamResolver;
            WrapperGenerator = wrapperGenerator;
        }

        public IDaoCodeWriter DaoCodeWriter { get; set; }
        public IDaoTargetStreamResolver DaoTargetStreamResolver { get; set; }
        public IWrapperGenerator WrapperGenerator { get; set; }
        public GenerationConfig Config { get; set; }
    }
}
