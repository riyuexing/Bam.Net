using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;

namespace Bam.Net.Data.Repositories
{
    /// <summary>
    /// A code and assembly generator used to generate schema
    /// specific dao repositories
    /// </summary>
    public partial class SchemaRepositoryGenerator: TypeDaoGenerator
    {
        public SchemaRepositoryGenerator(SchemaRepositoryGeneratorSettings settings, ILogger logger = null)
        {
            if(logger != null)
            {
                Subscribe(logger);
            }

            DaoGenerator = new Schema.DaoGenerator(settings.DaoCodeWriter, settings.DaoTargetStreamResolver);
            WrapperGenerator = settings.WrapperGenerator;
            if(settings.Config != null)
            {
                CheckIdField = settings.Config.CheckForIds;
                BaseRepositoryType = settings.Config.UseInheritanceSchema ? "DatabaseRepository" : "DaoRepository";
            }
        }

        public void AddTypes(Assembly typeAssembly, string sourceNamespace)
        {
            SourceNamespace = sourceNamespace;
            Args.ThrowIfNull(typeAssembly);
            AddTypes(typeAssembly.GetTypes().Where(t => t.Namespace != null && t.Namespace.Equals(sourceNamespace)));
        }

        /// <summary>
        /// Instantiate an instance of SchemaRepositoryGenerator that
        /// is used to generate a schema specific repository for the
        /// specified assembly for types in the specified 
        /// namespace.
        /// </summary>
        /// <param name="typeAssembly"></param>
        /// <param name="sourceNamespace"></param>
        /// <param name="logger"></param>
        public SchemaRepositoryGenerator(Assembly typeAssembly, string sourceNamespace, ILogger logger = null) 
            : base(typeAssembly, sourceNamespace, logger)
        {
            SourceNamespace = sourceNamespace;
            BaseRepositoryType = "DaoRepository";
        }

        /// <summary>
        /// The namespace to analyze for types.
        /// </summary>
        public string SourceNamespace { get; set; }
        public string BaseRepositoryType { get; set; }
        public string SchemaRepositoryNamespace
        {
            get
            {
                return $"{DaoNamespace}.Repository";
            }
        }

        public override void GenerateSource(string writeSourceTo)
        {
            base.GenerateSource(writeSourceTo);
            GenerateRepositorySource(writeSourceTo);
        }

    }
}
