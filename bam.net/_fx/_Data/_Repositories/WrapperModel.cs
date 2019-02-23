using Bam.Net.CommandLine;
using Bam.Net.Data.Schema;
using Bam.Net.Razor;
using Bam.Net.ServiceProxy;
using Bam.Net.Testing;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Bam.Net.Data.Repositories
{
    public partial class WrapperModel
    {

        public string Render()
        {
            List<Assembly> references = new List<Assembly>(GetDefaultAssembliesToReference())
            {
                BaseType.Assembly
            };
            RazorParser<WrapperTemplate> parser = new RazorParser<WrapperTemplate>(RazorBaseTemplate.DefaultInspector);
            string output = parser.ExecuteResource("Wrapper.tmpl", RepositoryTemplateResources.Path, typeof(WrapperGenerator).Assembly,
                new { Model = this }, references.ToArray());

            return output;
        }

        private static Assembly[] GetDefaultAssembliesToReference()
        {
            Assembly[] assembliesToReference = new Assembly[]{typeof(WrapperTemplate).Assembly,
                    typeof(DaoGenerator).Assembly,
                    typeof(ServiceProxySystem).Assembly,
                    typeof(Resolver).Assembly};
            return assembliesToReference;
        }
    }
}
