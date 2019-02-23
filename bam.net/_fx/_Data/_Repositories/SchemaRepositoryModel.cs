using Bam.Net.Data.Schema;
using Bam.Net.Razor;
using Bam.Net.ServiceProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    public partial class SchemaRepositoryModel
    {
        public string Render()
        {
            List<Assembly> referenceAssemblies = new List<Assembly>{
                    typeof(DaoGenerator).Assembly,
                    typeof(ServiceProxySystem).Assembly,
                    typeof(Args).Assembly,
                    typeof(DaoRepository).Assembly};
            RazorParser<SchemaRepositoryTemplate> parser = new RazorParser<SchemaRepositoryTemplate>(RazorBaseTemplate.DefaultInspector);
            string output = parser.ExecuteResource("SchemaRepository.tmpl", RepositoryTemplateResources.Path, typeof(SchemaRepositoryModel).Assembly, new { Model = this }, referenceAssemblies.ToArray());
            return output;
        }
    }
}
