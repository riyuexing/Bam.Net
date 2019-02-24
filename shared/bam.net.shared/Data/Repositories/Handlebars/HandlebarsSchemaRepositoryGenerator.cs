using Bam.Net.Application;
using Bam.Net.Data.Schema;
using Bam.Net.Data.Schema.Handlebars;
using Bam.Net.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bam.Net.Data.Repositories.Handlebars
{
    public class HandlebarsSchemaRepositoryGenerator: SchemaRepositoryGenerator
    {
        public HandlebarsSchemaRepositoryGenerator(GenerationConfig config, ILogger logger = null) : base(new SchemaRepositoryGeneratorSettings
        (
            new HandlebarsDaoCodeWriter(new Presentation.Handlebars.HandlebarsDirectory(config.TemplatePath), new Presentation.Handlebars.HandlebarsEmbeddedResources(typeof(SchemaRepositoryGenerator).Assembly)),
            new DaoTargetStreamResolver(),
            new HandlebarsWrapperGenerator()
        ), logger)
        {
            Configure(config);
            Net.Handlebars.HandlebarsDirectory = new Presentation.Handlebars.HandlebarsDirectory(config.TemplatePath);
            Net.Handlebars.HandlebarsEmbeddedResources = new Presentation.Handlebars.HandlebarsEmbeddedResources(typeof(SchemaRepositoryGenerator).Assembly);
        }
    }
}
