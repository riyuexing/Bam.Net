using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Bam.Net.Data.Schema
{
    public interface IDaoCodeWriter
    {
        string Namespace { get; set; }
        void WriteContextClass(SchemaDefinition schema, Func<string, Stream> targetResolver, string root);
        void WriteDaoClass(SchemaDefinition schema, Func<string, Stream> target, string root, Table table);
        void WriteQueryClass(SchemaDefinition schema, Func<string, Stream> target, string root, Table table);
        void WritePagedQueryClass(SchemaDefinition schema, Func<string, Stream> target, string root, Table table);
        void WriteQiClass(SchemaDefinition schema, Func<string, Stream> target, string root, Table table);
        void WriteCollectionClass(SchemaDefinition schema, Func<string, Stream> target, string root, Table table);
        void WriteColumnsClass(SchemaDefinition schema, Func<string, Stream> target, string root, Table table);
    }
}
