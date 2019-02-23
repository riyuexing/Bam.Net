using Bam.Net.Presentation.Handlebars;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Data.Repositories.Handlebars
{
    public class HandlebarsWrapperGeneratorConfig
    {
        public HandlebarsWrapperGeneratorConfig(string wrapperNamespace, string daoNamespace, TypeSchema typeSchema = null)
        {
            WrapperNamespace = wrapperNamespace;
            DaoNamespace = daoNamespace;
            TypeSchema = typeSchema;
        }

        public string WrapperNamespace { get; set; }
        public string DaoNamespace { get; set; }
        public TypeSchema TypeSchema { get; set; }

        public HandlebarsDirectory HandlebarsDirectory { get; set; }
        public HandlebarsEmbeddedResources HandlebarsEmbeddedResources { get; set; }
    }
}
