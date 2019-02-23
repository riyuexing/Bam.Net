using System;
using System.Collections.Generic;
using System.Reflection;
using Bam.Net.Data.Schema;
using Bam.Net.Razor;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Data.Repositories
{
    public partial class DtoModel
    {
        public DtoModel(Dao dao, string nameSpace)
        : this(dao.BuildDynamicType<ColumnAttribute>(), nameSpace)
        { }

        public string Render()
        {
            List<Assembly> references = new List<Assembly>(GetDefaultAssembliesToReference())
            {
                typeof(PropertyInfo).Assembly
            };
            RazorParser<DtoTemplate> parser = new RazorParser<DtoTemplate>(RazorBaseTemplate.DefaultInspector);
            string output = parser.ExecuteResource("Dto.tmpl", RepositoryTemplateResources.Path, typeof(DtoTemplate).Assembly,
                new { Model = this }, references.ToArray());

            return output;
        }

        private static Assembly[] GetDefaultAssembliesToReference()
        {
            Assembly[] assembliesToReference = new Assembly[]{typeof(DtoTemplate).Assembly,
                    typeof(DaoGenerator).Assembly,
                    typeof(ServiceProxySystem).Assembly,
                    typeof(Resolver).Assembly};
            return assembliesToReference;
        }
    }
}
