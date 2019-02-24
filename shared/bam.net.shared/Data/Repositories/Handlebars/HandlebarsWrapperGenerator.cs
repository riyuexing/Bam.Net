using Bam.Net.Presentation.Handlebars;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Bam.Net.Data.Repositories.Handlebars
{
    public class HandlebarsWrapperGenerator : WrapperGenerator
    {
        public HandlebarsDirectory HandlebarsDirectory { get; set; }
        public HandlebarsEmbeddedResources HandlebarsEmbeddedResources { get; set; }

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
            WriteSourceTo = writeSourceDir;

        }
    }
}
