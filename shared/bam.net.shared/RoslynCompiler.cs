using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Bam.Net
{
    public class RoslynCompiler : ICompiler
    {
        public RoslynCompiler()
        {
            OutputKind = OutputKind.DynamicallyLinkedLibrary;
            ReferenceAssemblies = DefaultReferenceAssemblies;
        }

        public Assembly[] ReferenceAssemblies { get; set; }
        public OutputKind OutputKind { get; set; }
        public Assembly Compile(DirectoryInfo directoryInfo, string assemblyFileName)
        {
            return Compile(directoryInfo.GetFiles("*.cs").ToArray(), assemblyFileName);
        }

        public Assembly Compile(FileInfo[] sourceFiles, string assemblyFileName)
        {
            StringBuilder sourceCode = new StringBuilder();
            foreach(FileInfo file in sourceFiles)
            {
                sourceCode.AppendLine(file.ReadAllText());
            }
            return Assembly.Load(Compile(sourceCode.ToString(), assemblyFileName));
        }

        public byte[] Compile(string sourceCode, string assemblyName)
        {
            SyntaxTree tree = SyntaxFactory.ParseSyntaxTree(sourceCode);
            CSharpCompilation compilation = CSharpCompilation.Create(assemblyName, new SyntaxTree[] { tree }, GetMetadataReferences(), new CSharpCompilationOptions(this.OutputKind));
            using(MemoryStream stream = new MemoryStream())
            {
                EmitResult compileResult = compilation.Emit(stream);
                if (!compileResult.Success)
                {
                    throw new RoslynCompilationException(compilation.GetDiagnostics());
                }
                return stream.GetBuffer();
            }
        }

        static Assembly[] _defaultReferenceAssemblies = new Assembly[] { };
        public static Assembly[] DefaultReferenceAssemblies
        {
            get
            {
                if (_defaultReferenceAssemblies.Length == 0)
                {
                    List<Assembly> defaultAssemblies = new List<Assembly>
                    {
                        typeof(object).Assembly,
                        typeof(System.Dynamic.DynamicObject).Assembly,
                        typeof(System.Xml.XmlDocument).Assembly,
                        typeof(System.Data.DataTable).Assembly,
                        Assembly.GetExecutingAssembly()
                    };
                    _defaultReferenceAssemblies = defaultAssemblies.ToArray();
                }

                return _defaultReferenceAssemblies;
            }
        }

        private MetadataReference[] GetMetadataReferences()
        {
            MetadataReference[] metaDataReferences = ReferenceAssemblies.Select(ass => MetadataReference.CreateFromFile(ass.Location)).ToArray();
            return metaDataReferences;
        }
    }
}
