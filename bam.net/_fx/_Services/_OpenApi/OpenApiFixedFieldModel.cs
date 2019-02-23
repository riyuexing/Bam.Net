using Bam.Net.Razor;
using Bam.Net.ServiceProxy;
using System.Collections.Generic;
using System.Reflection;

namespace Bam.Net.Services.OpenApi
{
    public partial class OpenApiFixedFieldModel
    {
        public string Render()
        {
            List<Assembly> referenceAssemblies = new List<Assembly>
            {
                typeof(OpenApiObjectDatabase).Assembly,
                    typeof(ServiceProxyHelper).Assembly,
                    typeof(Args).Assembly
            };

            RazorParser<RazorTemplate<OpenApiFixedFieldModel>> parser = new RazorParser<RazorTemplate<OpenApiFixedFieldModel>>();
            string result = parser.ExecuteResource("FixedField.tmpl", OpenApiTemplateResources.Path, typeof(OpenApiObjectDatabase).Assembly, new { Model = this }, referenceAssemblies.ToArray());
            return result;
        }
    }
}
