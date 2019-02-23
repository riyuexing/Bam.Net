using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Data.Repositories.Handlebars
{
    public class HandlebarsSchemaTypeModel: SchemaTypeModel
    {
        public string TypeNamePluralized
        {
            get
            {
                return Type.Name.Pluralize();
            }
        }
    }
}
