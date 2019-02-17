/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using Bam.Net.ServiceProxy;
using Bam.Net.Razor;

namespace Bam.Net.Data.Schema
{
    /// <summary>
    /// A code generator that writes Dao code for a SchemaDefinition
    /// </summary>
    public class DaoGenerator
    {
        List<Stream> _resultStreams = new List<Stream>();
        public DaoGenerator(IDaoCodeWriter codeWriter = null, ITargetStreamResolver targetStreamResolver = null)
        {
            DisposeOnComplete = true;
            SubscribeToEvents();
			
            Namespace = "DaoGenerated";
            TargetStreamResolver = targetStreamResolver ?? new DaoTargetStreamResolver();
            DaoCodeWriter = codeWriter ?? new RazorParserDaoCodeWriter
            {
                TargetStreamResolver = TargetStreamResolver
            };
        }

        public static List<string> DefaultReferenceAssemblies
        {
            get
            {
				return new List<string>(AdHocCSharpCompiler.DefaultReferenceAssemblies);
            }
        }

        private void SubscribeToEvents()
        {
            this.GenerateComplete += (g, schema) =>
            {
                if (DisposeOnComplete)
                {
                    foreach (Stream s in this._resultStreams)
                    {
                        s.Dispose();
                    }
                }

                this._resultStreams.Clear();
            };
        }
        
        public DaoGenerator(string nameSpace)
            : this()
        {
            this.Namespace = nameSpace;
        }

        public DaoGenerator(string ns, Action<string> resultInspector)
            : this(ns)
        {
            this.RazorResultInspector = resultInspector;
        }

        public IDaoCodeWriter DaoCodeWriter
        {
            get;
            set;
        }

        public ITargetStreamResolver TargetStreamResolver
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether dispose will
        /// be called on the output streams after code generation.
        /// </summary>
        public bool DisposeOnComplete { get; set; }

        public Action<string> RazorResultInspector { get; set; }
        #region events
        
        /// <summary>
        /// The event that fires prior to code generation
        /// </summary>
        public event GeneratorEventDelegate GenerateStarted;

        /// <summary>
        /// The event that fires when code generation is complete
        /// </summary>
        public event GeneratorEventDelegate GenerateComplete;

        protected void OnGenerateStarted(SchemaDefinition schema)
        {
            if (GenerateStarted != null)
            {
                GenerateStarted(this, schema);
            }
        }

        protected void OnGenerateComplete(SchemaDefinition schema)
        {
            if (GenerateComplete != null)
            {
                GenerateComplete(this, schema);
            }
        }
        
        #endregion events

		/// <summary>
		/// If the generator compiled generated files, this will be the FileInfo 
		/// representing the compiled assembly
		/// </summary>
		public FileInfo DaoAssemblyFile { get; set; }

        public string Namespace { get; set; }
                 
        public void Generate(SchemaDefinition schema)
        {
            Generate(schema, ".\\");
        }

        /// <summary>
        /// Generate code for the specified schema
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="root"></param>
        public void Generate(SchemaDefinition schema, string root)
        {
            Generate(schema, null, root, null);
        }

        public void Generate(SchemaDefinition schema, string root, string partialsDir)
        {
            Generate(schema, null, root, partialsDir);
        }

        /// <summary>
        /// Generate code for the specified schema
        /// </summary>
        /// <param name="schema">The schema to generate code for</param>
        /// <param name="targetResolver">If specified, generated code will be 
        /// written to the stream returned by this function</param>
        /// <param name="root">The root file path to use if no target resolver is specified</param>
        public void Generate(SchemaDefinition schema, Func<string, Stream> targetResolver = null, string root = ".\\", string partialsDir = null)
        {
            if (string.IsNullOrEmpty(Namespace))
            {
                throw new NamespaceNotSpecifiedException();
            }
            DaoCodeWriter.Namespace = Namespace;

            OnGenerateStarted(schema);            
            
            DaoCodeWriter.WriteContextClass(schema, targetResolver, root);
            
            bool writePartial = !string.IsNullOrEmpty(partialsDir);
            if (writePartial)
            {
                EnsurePartialsDir(partialsDir);
            }

            foreach (Table table in schema.Tables)
            {
                if (writePartial)
                {
                    WritePartial(schema, partialsDir, table);
                }
                DaoCodeWriter.WriteDaoClass(schema, targetResolver, root, table);
                DaoCodeWriter.WriteQueryClass(schema, targetResolver, root, table);
                DaoCodeWriter.WritePagedQueryClass(schema, targetResolver, root, table);
                DaoCodeWriter.WriteQiClass(schema, targetResolver, root, table);
                DaoCodeWriter.WriteCollectionClass(schema, targetResolver, root, table);
                DaoCodeWriter.WriteColumnsClass(schema, targetResolver, root, table);
            }

            OnGenerateComplete(schema);
        }

        private static void EnsurePartialsDir(string partialsDir)
        {
            DirectoryInfo partials = new DirectoryInfo(partialsDir);
            if (!partials.Exists)
            {
                partials.Create();
            }
        }
        
        public static Assembly[] GetReferenceAssemblies()
        {
            Assembly[] assembliesToReference = new Assembly[]{typeof(SchemaTemplate).Assembly, 
					typeof(DaoGenerator).Assembly,
					typeof(ServiceProxySystem).Assembly, 
                    typeof(DataTypes).Assembly,
					typeof(Resolver).Assembly};
            return assembliesToReference;
        }

        private void WritePartial(SchemaDefinition schema, string partialsDir, Table table)
        {
            RazorParser<TableTemplate> parser = new RazorParser<TableTemplate>(RazorResultInspector)
            {
                GetDefaultAssembliesToReference = GetReferenceAssemblies
            };
            Stream s = null;

            Type type = this.GetType();
            string result = parser.ExecuteResource("Partial.tmpl", SchemaTemplateResources.Path, type.Assembly, new { Model = table, Schema = schema, Namespace = Namespace });


            FileInfo partial = new FileInfo(Path.Combine(partialsDir, "{0}.cs"._Format(table.Name)));
            if (!partial.Exists)
            {
                s = partial.OpenWrite();
                WritePartialToStream(result, s);
            }
        }
        
        protected virtual void WritePartialToStream(string code, Stream s)
        {
            WriteToStream(code, s);
        }
        
        private static void WriteToStream(string text, Stream s)
        {
            using (StreamWriter sw = new StreamWriter(s))
            {
                sw.Write(text);
                sw.Flush();
            }
        }
    }
}
