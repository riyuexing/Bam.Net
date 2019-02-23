using Bam.Net.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Schema
{
    public partial class XrefInfo
    {
        protected string Render(string templateName)
        {
            Type type = this.GetType();
            RazorParser<DaoRazorTemplate<XrefInfo>> razorParser = new RazorParser<DaoRazorTemplate<XrefInfo>>
            {
                GetDefaultAssembliesToReference = DaoGenerator.GetReferenceAssemblies
            };
            return razorParser.ExecuteResource(templateName, SchemaTemplateResources.Path, type.Assembly, new { Model = this });
        }
    }
}
