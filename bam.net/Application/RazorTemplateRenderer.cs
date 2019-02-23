using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Bam.Net.Razor
{
    public class RazorTemplateRenderer<TModel> : ITemplateResourceRenderer
    {
        public const string DefaultTemplateName = "DefaultRazorTemplate";

        public RazorTemplateRenderer()
        {
            TemplateName = DefaultTemplateName;
        }

        public TModel Model { get; set; }
        public string TemplateName { get; set; }

        public virtual RazorParser<RazorTemplate<TModel>> GetParser()
        {
            return new RazorParser<RazorTemplate<TModel>>();
        }

        public string ResourcePath { get; set; }
        public Assembly ResourceContainer { get; set; }
        /// <summary>
        /// Gets or sets the reference assemblies necessary for a given template.
        /// </summary>
        /// <value>
        /// The reference assemblies.
        /// </value>
        public Assembly[] ReferenceAssemblies { get; set; }

        public void Render(object toRender, Stream output)
        {
            Render(TemplateName, toRender, output);
        }

        public void Render(string templateName, object toRender, Stream output)
        {
            RenderResource(templateName, toRender, output);
        }

        public void RenderResource(string templateName, object toRender, Stream output)
        {
            List<Assembly> referenceAssemblies = new List<Assembly>{
                    GetType().Assembly,
                    typeof(TModel).Assembly};

            object modelOptions = new { Model = toRender };
            RazorParser<RazorTemplate<TModel>> parser = GetParser();
            string result = parser.ExecuteResource(templateName, ResourcePath, ResourceContainer, modelOptions, referenceAssemblies.ToArray()).Trim();
            WriteToStream(result, output);
        }

        protected void WriteToStream(string text, Stream s)
        {
            using(StreamWriter sw = new StreamWriter(s))
            {
                sw.Write(text);
                sw.Flush();
            }
        }
    }
}
