using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Application
{
    /// <summary>
    /// Configuration for generating a schema repository.
    /// </summary>
    public class GenerationConfig
    {
        public string TypeAssembly { get; set; }
        public string SchemaName { get; set; }
        public string FromNameSpace { get; set; }
        public string ToNameSpace { get; set; }
        public string WriteSourceTo { get; set; }

        /// <summary>
        /// Check the specified data classes for Id properties
        /// </summary>
        public bool CheckForIds { get; set; }

        /// <summary>
        /// If yes the generated Repository will inherit from DatabaseRepository otherwise DaoRepository
        /// </summary>
        public bool UseInheritanceSchema { get; set; }
    }
}
