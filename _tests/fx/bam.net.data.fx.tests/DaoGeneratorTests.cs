using Bam.Net.Data.Schema;
using Bam.Net.Logging.Http;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using System;
using System.IO;
using System.Reflection;

namespace Bam.Net.Tests
{
    [Serializable]
    public class DaoGeneratorTests : CommandLineTestInterface
    {
        [UnitTest]
        public static void GenerateShouldUseSpecifiedTargetResolver()
        {
            string filePath = MethodBase.GetCurrentMethod().Name;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            DaoGenerator generator = new DaoGenerator("Test");

            SchemaDefinition schema = GetTestSchema();
            generator.DisposeOnComplete = false;
            generator.Generate(schema, (s) => {
                return new FileStream(filePath, FileMode.Create);
            });

            string output = File.ReadAllText(filePath);

            Expect.IsGreaterThan(output.Length, 0);

            OutLine(output, ConsoleColor.Cyan);
            DeleteSchema(schema);
        }

        private static SchemaDefinition GetTestSchema()
        {
            SchemaManager mgr = new SchemaManager();
            SchemaDefinition schema = mgr.SetSchema("test");
            mgr.AddTable("monkey");
            return schema;
        }

        internal static void DeleteSchema(SchemaDefinition schema)
        {
            try
            {
                File.Delete(schema.File);
            }
            catch (Exception ex)
            {
                OutLineFormat("An error occurred deleting schema file: {0}", ConsoleColor.Red, ex.Message);
            }
        }
    }
}
