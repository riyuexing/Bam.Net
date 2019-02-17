﻿using Bam.Net.Logging;
using Bam.Net.Presentation.Handlebars;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bam.Net.Data.Schema.Handlebars
{
    public class HandlebarsDaoCodeWriter : Loggable, IDaoCodeWriter
    {
        public HandlebarsDaoCodeWriter(HandlebarsDirectory handlebarsDirectory, HandlebarsEmbeddedResources handlebarsEmbeddedResources, IDaoTargetStreamResolver daoTargetStreamResolver = null)
        {
            DaoTargetStreamResolver = daoTargetStreamResolver ?? new DaoTargetStreamResolver();
            HandlebarsDirectory = handlebarsDirectory;
            HandlebarsEmbeddedResources = handlebarsEmbeddedResources;
        }
        protected bool Loaded { get; set; }

        public void Load()
        {
            if (!Loaded)
            {
                Reload();
            }
        }

        public void Reload()
        {
            HandlebarsDirectory.Reload();
            HandlebarsEmbeddedResources.Reload();
            Loaded = true;
        }

        public string Namespace { get; set; }
        public IDaoTargetStreamResolver DaoTargetStreamResolver { get; set; }
        public HandlebarsDirectory HandlebarsDirectory { get; set; }
        public HandlebarsEmbeddedResources HandlebarsEmbeddedResources { get; set; }

        public void WriteCollectionClass(SchemaDefinition schema, Func<string, Stream> targetResolver, string rootDirectory, Table table)
        {
            Load();            
            DaoTableSchemaModel renderModel = GetModel(schema, table);
            Render("Collection", renderModel, DaoTargetStreamResolver.GetTargetCollectionStream(targetResolver, rootDirectory, table));
        }

        public void WriteColumnsClass(SchemaDefinition schema, Func<string, Stream> targetResolver, string rootDirectory, Table table)
        {
            Load();
            DaoTableSchemaModel renderModel = GetModel(schema, table);
            Render("ColumnsClass", renderModel, DaoTargetStreamResolver.GetTargetColumnsClassStream(targetResolver, rootDirectory, table));
        }

        public void WriteContextClass(SchemaDefinition schema, Func<string, Stream> targetResolver, string rootDirectory)
        {
            Load();
            DaoTableSchemaModel renderModel = GetModel(schema, null);
            Render("Context", renderModel, DaoTargetStreamResolver.GetTargetContextStream(targetResolver, rootDirectory, schema));
        }

        public void WriteDaoClass(SchemaDefinition schema, Func<string, Stream> targetResolver, string rootDirectory, Table table)
        {
            Load();
            DaoTableSchemaModel renderModel = GetModel(schema, table);
            Render("Class", renderModel, DaoTargetStreamResolver.GetTargetClassStream(targetResolver, rootDirectory, table));
        }

        public void WritePagedQueryClass(SchemaDefinition schema, Func<string, Stream> targetResolver, string rootDirectory, Table table)
        {
            Load();
            DaoTableSchemaModel renderModel = GetModel(schema, table);
            Render("PagedQueryClass", renderModel, DaoTargetStreamResolver.GetTargetPagedQueryClassStream(targetResolver, rootDirectory, table));
        }

        public void WriteQiClass(SchemaDefinition schema, Func<string, Stream> targetResolver, string rootDirectory, Table table)
        {
            Load();
            DaoTableSchemaModel renderModel = GetModel(schema, table);
            Render("QiClass", renderModel, DaoTargetStreamResolver.GetTargetQiClassStream(targetResolver, rootDirectory, table));
        }

        public void WriteQueryClass(SchemaDefinition schema, Func<string, Stream> targetResolver, string rootDirectory, Table table)
        {
            Load();
            DaoTableSchemaModel renderModel = GetModel(schema, table);
            Render("QueryClass", renderModel, DaoTargetStreamResolver.GetTargetQueryClassStream(targetResolver, rootDirectory, table));
        }

        private DaoTableSchemaModel GetModel(SchemaDefinition schema, Table table)
        {
            return new DaoTableSchemaModel { Model = table, Schema = schema, Namespace = Namespace };
        }

        private void Render(string templateName, DaoTableSchemaModel renderModel, Stream output)
        {
            if ((HandlebarsDirectory?.Templates?.ContainsKey(templateName)).Value)
            {
                string code = HandlebarsDirectory.Render(templateName, renderModel);

                code.WriteToStream(output);
            }
            else if ((HandlebarsEmbeddedResources?.Templates?.ContainsKey(templateName)).Value)
            {
                string code = HandlebarsEmbeddedResources.Render(templateName, renderModel);
                code.WriteToStream(output);
            }
        }
    }
}
