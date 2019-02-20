using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Bam.Net.Data.Repositories.Handlebars
{
    public class HandlebarsWrapperGenerator : WrapperGenerator
    {
        public HandlebarsWrapperGenerator(string wrapperNamespace, string daoNamespace, TypeSchema typeSchema = null) : base(wrapperNamespace, daoNamespace, typeSchema)
        { }

        object _generateLock = new object();
        public override GeneratedAssemblyInfo GenerateAssembly()
        {
            lock (_generateLock)
            {
                RoslynCompiler compiler = new RoslynCompiler();
                Assembly assembly = compiler.Compile(new System.IO.DirectoryInfo(WriteSourceTo), $"{WrapperNamespace}.Wrapper.dll");
                GeneratedAssemblyInfo result = new GeneratedAssemblyInfo($"{WrapperNamespace}.Wrapper.dll", assembly);
                result.Save();
                return result;
            }
        }

        public override void WriteSource(string writeSourceDir)
        {
            throw new NotImplementedException();
        }
    }
}
