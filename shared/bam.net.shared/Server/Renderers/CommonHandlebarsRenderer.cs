using Bam.Net.Presentation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bam.Net.Server.Renderers
{
    public class CommonHandlebarsRenderer : WebRenderer, ITemplateManager
    {
        public CommonHandlebarsRenderer(ContentResponder content)
            : base("text/html", ".htm", ".html")
        {
            ContentResponder = content;
        }

        public string CombinedCompiledLayoutTemplates => throw new NotImplementedException();

        public string CombinedCompiledTemplates => throw new NotImplementedException();

        public IEnumerable<ICompiledTemplate> CompiledTemplates => throw new NotImplementedException();

        public string ContentRoot => throw new NotImplementedException();

        public override void Render(object toRender, Stream output)
        {
            throw new NotImplementedException();
        }

        public void RenderLayout(LayoutModel toRender, Stream output)
        {
            throw new NotImplementedException();
        }
    }
}
