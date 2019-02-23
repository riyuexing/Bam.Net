using Bam.Net.Logging;
using Bam.Net.Razor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Razor.Parser;

namespace Bam.Net.Data.Schema
{
    public class RazorParserDaoCodeWriter : Loggable, IDaoCodeWriter
    {
        public RazorParserDaoCodeWriter()
        {
            ResultInspector = (s) => Console.WriteLine(s);
        }

        public Action<string> ResultInspector { get; set; }
        public string Namespace { get; set; }

        public IDaoTargetStreamResolver DaoTargetStreamResolver { get; set; }

        public event EventHandler BeforeClassParse;
        public event EventHandler AfterClassParse;
        public void WriteDaoClass(SchemaDefinition schema, Func<string, Stream> targetResolver, string root, Table table)
        {
            Razor.RazorParser<TableTemplate> parser = GetParser<TableTemplate>();
            Type type = GetType();

            FireEvent(BeforeClassParse, new DaoGenerationEventArgs { SchemaDefinition = schema, Table = table });
            string code = parser.ExecuteResource("Class.tmpl", SchemaTemplateResources.Path, type.Assembly, new { Model = table, Schema = schema, Namespace });
            FireEvent(AfterClassParse, new DaoGenerationEventArgs { SchemaDefinition = schema, Table = table });

            Stream stream = DaoTargetStreamResolver.GetTargetClassStream(targetResolver, root, table);
            WriteToStream(code, stream);
        }

        public event EventHandler BeforeCollectionParse;
        public event EventHandler AfterCollectionParse;
        public void WriteCollectionClass(SchemaDefinition schema, Func<string, Stream> targetResolver, string root, Table table)
        {
            RazorParser<TableTemplate> parser = GetParser<TableTemplate>();
            Type type = GetType();
            FireEvent(BeforeCollectionParse, new DaoGenerationEventArgs { SchemaDefinition = schema, Table = table });
            string code = parser.ExecuteResource("Collection.tmpl", SchemaTemplateResources.Path, type.Assembly, new { Model = table, Schema = schema, Namespace });
            FireEvent(AfterCollectionParse, new DaoGenerationEventArgs { SchemaDefinition = schema, Table = table });

            Stream stream = DaoTargetStreamResolver.GetTargetCollectionStream(targetResolver, root, table);
            WriteToStream(code, stream);
        }

        public event EventHandler BeforeColmnsClassParse;
        public event EventHandler AfterColumnsClassParse;
        public void WriteColumnsClass(SchemaDefinition schema, Func<string, Stream> target, string root, Table table)
        {
            RazorParser<TableTemplate> parser = GetParser<TableTemplate>();
            Type type = GetType();
            FireEvent(BeforeColmnsClassParse, new DaoGenerationEventArgs { SchemaDefinition = schema, Table = table });
            string code = parser.ExecuteResource("ColumnsClass.tmpl", SchemaTemplateResources.Path, type.Assembly, new { Model = table, Schema = schema, Namespace });
            FireEvent(AfterColumnsClassParse, new DaoGenerationEventArgs { SchemaDefinition = schema, Table = table });

            Stream stream = DaoTargetStreamResolver.GetTargetColumnsClassStream(target, root, table);
            WriteToStream(code, stream);
        }

        public event EventHandler BeforeContextClassParse;
        public event EventHandler AfterContextClassParse;
        public void WriteContextClass(SchemaDefinition schema, Func<string, Stream> targetResolver, string root)
        {
            Razor.RazorParser<SchemaTemplate> parser = GetParser<SchemaTemplate>();
            Type type = GetType();

            FireEvent(BeforeContextClassParse, new DaoGenerationEventArgs { SchemaDefinition = schema });
            string result = parser.ExecuteResource("Context.tmpl", SchemaTemplateResources.Path, type.Assembly, new { Model = schema, Namespace });
            FireEvent(AfterContextClassParse, new DaoGenerationEventArgs { SchemaDefinition = schema });

            Stream s = DaoTargetStreamResolver.GetTargetContextStream(targetResolver, root, schema);

            WriteToStream(result, s);
        }

        public event EventHandler BeforePagedQueryClassParse;
        public event EventHandler AfterPagedQueryClassParse;
        public void WritePagedQueryClass(SchemaDefinition schema, Func<string, Stream> targetResolver, string root, Table table)
        {
            Razor.RazorParser<TableTemplate> parser = GetParser<TableTemplate>();
            Type type = GetType();

            FireEvent(BeforePagedQueryClassParse, new DaoGenerationEventArgs { SchemaDefinition = schema });
            string result = parser.ExecuteResource("PagedQueryClass.tmpl", SchemaTemplateResources.Path, type.Assembly, new { Model = table, Schema = schema, Namespace });
            FireEvent(AfterPagedQueryClassParse, new DaoGenerationEventArgs { SchemaDefinition = schema });

            Stream stream = DaoTargetStreamResolver.GetTargetPagedQueryClassStream(targetResolver, root, table);
            WriteToStream(result, stream);
        }

        public event EventHandler BeforeQiClassParse;
        public event EventHandler AfterQiClassParse;
        public void WriteQiClass(SchemaDefinition schema, Func<string, Stream> targetResolver, string root, Table table)
        {
            Razor.RazorParser<TableTemplate> parser = GetParser<TableTemplate>();
            Type type = GetType();

            FireEvent(BeforeQiClassParse, new DaoGenerationEventArgs { SchemaDefinition = schema });
            string result = parser.ExecuteResource("QiClass.tmpl", SchemaTemplateResources.Path, type.Assembly, new { Model = table, Schema = schema, Namespace });
            FireEvent(AfterQiClassParse, new DaoGenerationEventArgs { SchemaDefinition = schema });

            Stream stream = DaoTargetStreamResolver.GetTargetQiClassStream(targetResolver, root, table);
            WriteToStream(result, stream);
        }

        public event EventHandler BeforeQueryClassParse;
        public event EventHandler AfterQueryClassParse;
        public void WriteQueryClass(SchemaDefinition schema, Func<string, Stream> targetResolver, string root, Table table)
        {
            Razor.RazorParser<TableTemplate> parser = GetParser<TableTemplate>();
            Type type = GetType();

            FireEvent(BeforeQueryClassParse, new DaoGenerationEventArgs { SchemaDefinition = schema });
            string result = parser.ExecuteResource("QueryClass.tmpl", SchemaTemplateResources.Path, type.Assembly, new { Model = table, Schema = schema, Namespace });
            FireEvent(AfterQueryClassParse, new DaoGenerationEventArgs { SchemaDefinition = schema });

            Stream stream = DaoTargetStreamResolver.GetTargetQueryClassStream(targetResolver, root, table);
            WriteToStream(result, stream);
        }

        public event EventHandler BeforeWritePartial;
        public event EventHandler AfterWriterPartial;
        public void WritePartial(SchemaDefinition schema, Func<string, Stream> targetResolver, string root, Table table)
        {
            RazorParser<TableTemplate> parser = GetParser<TableTemplate>();
            Type type = GetType();

            FireEvent(BeforeWritePartial, new DaoGenerationEventArgs { SchemaDefinition = schema });
            string result = parser.ExecuteResource("Partial.tmpl", SchemaTemplateResources.Path, type.Assembly, new { Model = table, Schema = schema, Namespace });
            FireEvent(AfterWriterPartial, new DaoGenerationEventArgs { SchemaDefinition = schema });

            Stream stream = DaoTargetStreamResolver.GetTargetPartialClassStream(targetResolver, root, table);
            WriteToStream(result, stream);
        }

        private static void WriteToStream(string text, Stream s)
        {
            using (StreamWriter sw = new StreamWriter(s))
            {
                sw.Write(text);
                sw.Flush();
            }
        }

        private Razor.RazorParser<T> GetParser<T>() where T : RazorBaseTemplate
        {
            return new Razor.RazorParser<T>(ResultInspector)
            {
                GetDefaultAssembliesToReference = DaoGenerator.GetReferenceAssemblies
            };
        }
    }
}
