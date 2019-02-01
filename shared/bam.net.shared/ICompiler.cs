using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Bam.Net
{
    public interface ICompiler
    {
        Assembly[] ReferenceAssemblies { get; set; }

        Assembly Compile(DirectoryInfo directoryInfo, string assemblyFileName);
        Assembly Compile(FileInfo[] files, string assemblyFileName);
        Assembly Compile(string sourceCode, string assemblyFileName);        
    }
}
