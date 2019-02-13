using Bam.Net.CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
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

        public static BamSettings Load(string path = null)
        {
            path = path ?? Path.Combine(".", "bam.yaml");
            if (!File.Exists(path))
            {
                BamSettings settings = new BamSettings();
                settings.ToYamlFile(path);
            }
            return path.FromYamlFile<BamSettings>();
        }
    }
}
