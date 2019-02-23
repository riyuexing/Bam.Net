using Bam.Net.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices
{
    public partial class ProxyMethodModel
    {
        public string Render()
        {
            return Render(ReferenceAssemblies);
        }

        public string Render(params Assembly[] assembliesToReference)
        {
            return RazorRenderer.RenderResource<ProxyMethodModel>(this, ProxyTemplateResources.Path, "ProxyMethod.tmpl", assembliesToReference);
        }
    }
}
