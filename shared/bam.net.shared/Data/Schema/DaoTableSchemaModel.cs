using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bam.Net.Data.Schema
{
    /// <summary>
    /// A model that represents the data necessary to render a Table into a Dao.
    /// </summary>
    public class DaoTableSchemaModel
    {
        public Table Model { get; set; }
        public SchemaDefinition Schema { get; set; }
        public string Namespace { get; set; }
    }
}
