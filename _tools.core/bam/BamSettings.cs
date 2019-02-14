using Bam.Net.CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Bam.Net.Application
{
    public class BamSettings
    {
        public BamSettings()
        {
            ArgumentPrefix = ParsedArguments.DefaultArgPrefix;
            Environment = StandardEnvironments.Development;
        }

        public string ArgumentPrefix { get; set; }

        public StandardEnvironments Environment { get; set; }
        public string GitPath { get; set; }
        public string DotNetPath { get; set; }
        public string NpxPath { get; set; }

        public bool IsValid(Action<string> notValid)
        {
            if (string.IsNullOrEmpty(GitPath.Trim()))
            {
                GitPath = GetPath("git");
            }
            if (string.IsNullOrEmpty(DotNetPath.Trim()))
            {
                DotNetPath = GetPath("dotnet");
            }
            if (string.IsNullOrEmpty(NpxPath.Trim()))
            {
                NpxPath = GetPath("npx");
            }

            StringBuilder message = new StringBuilder();
            if (string.IsNullOrEmpty(GitPath.Trim()))
            {
                message.Append("git was not found, please specify the path to the git executable");
            }
            if (string.IsNullOrEmpty(DotNetPath.Trim()))
            {
                message.Append("dotnet was not found, please specify the path to the dotnet executable");
            }
            if (string.IsNullOrEmpty(NpxPath.Trim()))
            {
                message.AppendLine("npx was not found, please specify the path to the npx executable");
            }
            if (!string.IsNullOrEmpty(message.ToString()))
            {
                notValid(message.ToString());
                return false;
            }
            return true;
        }

        public static BamSettings Load(string path = null)
        {
            path = path ?? Path.Combine(".", $"bam-{OSInfo.Current.ToString()}.yaml");
            if (!File.Exists(path))
            {
                BamSettings settings = new BamSettings
                {
                    GitPath = GetPath("git"),
                    DotNetPath = GetPath("dotnet"),
                    NpxPath = GetPath("npx")
                };
                settings.ToYamlFile(path);
            }
            return path.FromYamlFile<BamSettings>();
        }
        
        private static string GetPath(string fileName)
        {
            if(OSInfo.Current == OSNames.Windows)
            {
                ProcessOutput whereOutput = $"where {fileName}".Run();
                return whereOutput.StandardOutput.DelimitSplit("\r", "\n")[0].Trim();
            }
            ProcessOutput whichOutput = $"which {fileName}".Run();
            return whichOutput.StandardOutput.DelimitSplit("\r", "\n")[0].Trim();
        }
    }
}
