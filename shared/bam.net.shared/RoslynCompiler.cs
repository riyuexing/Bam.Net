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
            return Compile(sourceCode.ToString(), assemblyFileName);
        }

        public Assembly Compile(string sourceCode, string assemblyFileName)
        {
            SyntaxTree tree = SyntaxFactory.ParseSyntaxTree(sourceCode);
            CSharpCompilation compilation = CSharpCompilation.Create(assemblyFileName, new SyntaxTree[] { tree }, GetMetadataReferences(), new CSharpCompilationOptions(this.OutputKind));
            Assembly compiledAssembly;
            // TODO: handle diagnostics (errors); compilation.GetDiagnostics();
            using(MemoryStream stream = new MemoryStream())
            {
                EmitResult compileResult = compilation.Emit(stream);
                compiledAssembly = Assembly.Load(stream.GetBuffer());
            }
            return compiledAssembly;
        }

        private MetadataReference[] GetMetadataReferences()
        {
            return ReferenceAssemblies.Select(ass => MetadataReference.CreateFromFile(ass.Location)).ToArray();
        }
    }
}
