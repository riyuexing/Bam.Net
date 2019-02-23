using Bam.Net.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Schema
{
    public partial class Column
    {
        protected internal virtual string RenderClassProperty()
        {
            if (this.Key)
            {
                return Render<Column>("KeyProperty.tmpl");
            }
            else
            {
                return Render<Column>("Property.tmpl");
            }
        }

        protected internal virtual string RenderColumnsClassProperty()
        {
            return Render<Column>("ColumnsProperty.tmpl");
        }

        protected string Render<T>(string templateName, string ns = "")
        {
            Type type = this.GetType();
            RazorParser<DaoRazorTemplate<T>> razorParser = new RazorParser<DaoRazorTemplate<T>>
            {
                GetDefaultAssembliesToReference = DaoGenerator.GetReferenceAssemblies
            };
            return razorParser.ExecuteResource(templateName, SchemaTemplateResources.Path, type.Assembly, new { Model = this, Namespace = ns });
        }
    }
}
