using Bam.Net.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices
{
    public partial class ProxyModel
    {
        public string Render()
        {
            return RazorRenderer.RenderResource(this, ProxyTemplateResources.Path, "Proxy.tmpl", ReferenceAssemblies);
        }
    }
}
