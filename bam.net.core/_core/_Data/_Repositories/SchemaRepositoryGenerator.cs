using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    public partial class SchemaRepositoryGenerator
    {

        public void GenerateRepositorySource(string writeSourceTo, string schemaName = null)
        {
            schemaName = schemaName ?? SchemaName;
            SchemaName = schemaName;
            base.GenerateSource(writeSourceTo);
            SchemaRepositoryModel schemaModel = new SchemaRepositoryModel
            {
                BaseRepositoryType = BaseRepositoryType,
                BaseNamespace = SourceNamespace,
                SchemaRepositoryNamespace = SchemaRepositoryNamespace,
                SchemaName = schemaName,
                Types = Types.Select(t => SchemaTypeModel.FromType(t, DaoNamespace)).ToArray()
            };
            string code = Bam.Net.Handlebars.Render("SchemaRepositoryTemplate", schemaModel);
            code.SafeWriteToFile(Path.Combine(writeSourceTo, $"{schemaName}Repository.cs"));
        }
    }
}
