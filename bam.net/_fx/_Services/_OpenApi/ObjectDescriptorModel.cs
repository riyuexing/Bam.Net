using Bam.Net.Razor;
using Bam.Net.ServiceProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.OpenApi
{
    public partial class ObjectDescriptorModel
    {
        public string Render()
        {
            List<Assembly> referenceAssemblies = new List<Assembly>
            {
                typeof(OpenApiObjectDatabase).Assembly,
                    typeof(ServiceProxyHelper).Assembly,
                    typeof(Args).Assembly
            };

            RazorParser<OpenApiObjectDescriptorModelTemplate> parser = new RazorParser<OpenApiObjectDescriptorModelTemplate>();
            string result = parser.ExecuteResource("ObjectDescriptor.tmpl", OpenApiTemplateResources.Path, typeof(OpenApiObjectDatabase).Assembly, new { Model = this }, referenceAssemblies.ToArray());
            return result;
        }
    }
}
