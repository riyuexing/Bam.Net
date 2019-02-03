﻿using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;
using Bam.Net.Data.Dynamic.Data.Dao.Repository;
using Bam.Net.Data.Dynamic.Data;
using Bam.Net.Testing.Integration;
using System.Threading;
using Bam.Net.CommandLine;
using Bam.Net.Data.SQLite;
using Bam.Net.Test.DataBanana;
using Bam.Net.Test.DataBanana.Dao;
using Bam.Net.Data.Dynamic.Tests.Data;

namespace Bam.Net.Data.Dynamic.Tests
{
    [Serializable]
    public class DynamicRepositoryTests: CommandLineTestInterface
    {
        public class TestDynamicTypeManager: DynamicTypeManager
        {
            DynamicDataManager dataManager;
            public TestDynamicTypeManager(DynamicTypeDataRepository descriptorRepository, DefaultDataDirectoryProvider settings) : base(descriptorRepository, settings)
            {
                dataManager = new DynamicDataManager();
            }

            public void TestSaveTypDescriptor(string typeName, Dictionary<object, object> data)
            {
                SaveTypeDescriptor(typeName, data);
            }
            public void TestSaveData(string sha1, string typeName, Dictionary<object, object> data)
            {
                dataManager.SaveRootData(sha1, typeName, new List<Dictionary<object, object>>(new Dictionary<object, object>[] { data }));
            }

            public void TestGetClrTypeName()
            {
                string value = "arrayOf(The.Big.Monkey)";
                Expect.AreEqual("Monkey[]", GetClrTypeName(value));
                string value2 = "The.Big.Monkey.Banana";
                Expect.AreEqual("Banana", GetClrTypeName(value2));
            }

            public void TestGetClrPropertyName()
            {
                string value = "commit_author";
                Expect.AreEqual("CommitAuthor", GetClrPropertyName(value));
            }
        }

        [UnitTest]
        public void TestDaoRepoSaveAndRetrieve()
        {
            DaoRepository repo = new DaoRepository();
            repo.AddType<TestData>();

            TestData d = new TestData
            {
                Value = "This is a value",
                LongValue = 9223372036854775807,
                ULongValue = 18446744073709551615
            };

            d = repo.Save(d);
            Expect.IsTrue(d.Id > 0);
        }

        [UnitTest]
        public void TestULongSaveAndRetrieve()
        {
            ulong val = 18446744073709551615;
            ConsoleLogger logger = new ConsoleLogger();
            logger.StartLoggingThread();
            SQLiteDatabase db = new SQLiteDatabase(".", "TestDatabase");
            SQLiteRegistrar.Register(db);
            db.TryEnsureSchema<TestClass>();
            TestClass testClass = new TestClass
            {
                Value = val
            };
            testClass.Save(db);
            TestClass retrieved = TestClass.OneWhere(c => c.Id == testClass.Id, db);
            Expect.AreEqual(val, retrieved.Value);
            
            OutLineFormat(db.ConnectionString);
        }

        [ConsoleAction]
        [IntegrationTest]
        public void CanGetClrTypeNameFromArrayOf()
        {
            TestDynamicTypeManager testRepo = new TestDynamicTypeManager(new DynamicTypeDataRepository(), DefaultDataDirectoryProvider.Instance);
            testRepo.TestGetClrTypeName();
        }


        [ConsoleAction]
        [IntegrationTest]
        public void CanGetClrPropertyName()
        {
            TestDynamicTypeManager testRepo = new TestDynamicTypeManager(new DynamicTypeDataRepository(), DefaultDataDirectoryProvider.Instance);
            testRepo.TestGetClrPropertyName();
        }

        // save descriptor
        [ConsoleAction]
        [IntegrationTest]
        public void SaveDescriptorDoesntDuplicte()
        {
            TestDynamicTypeManager testRepo = new TestDynamicTypeManager(new DynamicTypeDataRepository(), DefaultDataDirectoryProvider.Instance);            
            JObject jobj = (JObject)JsonConvert.DeserializeObject(new { Name = "some name", Arr = new object[] { new { Fromage = "gooey" } } }.ToJson());
            Dictionary<object, object> data = jobj.ToObject<Dictionary<object, object>>();
            string testTypeName = "test_typeName";
            testRepo.DynamicTypeDataRepository.DeleteWhere<DynamicTypeDescriptor>(new { TypeName = testTypeName });
            DynamicTypeDescriptor descriptor = testRepo.DynamicTypeDataRepository.DynamicTypeDescriptorsWhere(d => d.TypeName == testTypeName).FirstOrDefault();
            Expect.IsNull(descriptor);

            testRepo.TestSaveTypDescriptor(testTypeName, data);
            int count = testRepo.DynamicTypeDataRepository.DynamicTypeDescriptorsWhere(d => d.TypeName == testTypeName).Count();

            Expect.IsTrue(count == 1);
            testRepo.TestSaveTypDescriptor(testTypeName, data);
            count = testRepo.DynamicTypeDataRepository.DynamicTypeDescriptorsWhere(d => d.TypeName == testTypeName).Count();
            Expect.IsTrue(count == 1);
        }
        // save child descriptors
        [ConsoleAction]
        [IntegrationTest]
        public void SaveDataSavesTypes()
        {
            string testTypeName = nameof(SaveDataSavesTypes).RandomLetters(5);
            SetupTestData(out TestDynamicTypeManager testRepo, out Dictionary<object, object> data);

            // Test
            testRepo.TestSaveData("sha1NotUsedForThisTest", testTypeName, data);
            List<DynamicTypeDescriptor> descriptors = testRepo.DynamicTypeDataRepository.DynamicTypeDescriptorsWhere(d => d.Id > 0).ToList();
            Expect.IsTrue(descriptors.Count == 3);
            descriptors.Each(d => OutLineFormat("{0}: {1}", d.Id, d.TypeName, ConsoleColor.Cyan));
        }

        // save data
        [ConsoleAction]
        [IntegrationTest]
        public void SaveDataSavesInstance()
        {
            string testTypeName = nameof(SaveDataSavesInstance).RandomLetters(5);
            SetupTestData(out TestDynamicTypeManager testRepo, out Dictionary<object, object> data);

            testRepo.TestSaveData("sha1NotUsedForThisTest", testTypeName, data);
            List<DataInstance> datas = testRepo.DynamicTypeDataRepository.DataInstancesWhere(d => d.TypeName == testTypeName).ToList();
            Expect.IsTrue(datas.Count == 1);
            Expect.IsTrue(datas[0].TypeName.Equals(testTypeName));
            datas[0].Properties.Each(p => OutLine($"{p.PropertyName} = {p.Value}"));
        }
        
        [ConsoleAction]
        [IntegrationTest]
        public void AssociationsAreMade()
        {
            DynamicTypeManager mgr = new DynamicTypeManager(new DynamicTypeDataRepository(), DefaultDataDirectoryProvider.Instance);
            DynamicNamespaceDescriptor ns = new DynamicNamespaceDescriptor { Namespace = $"Test.Name.Space.{nameof(AssociationsAreMade)}" };
            ns = mgr.DynamicTypeDataRepository.Save(ns);

            DynamicTypeDescriptor typeDescriptor = new DynamicTypeDescriptor { DynamicNamespaceDescriptorId = ns.Id };
            Expect.IsNull(typeDescriptor.DynamicNamespaceDescriptor);

            typeDescriptor = mgr.DynamicTypeDataRepository.Save(typeDescriptor);
            Expect.IsNotNull(typeDescriptor.DynamicNamespaceDescriptor);
            Expect.AreEqual(ns, typeDescriptor.DynamicNamespaceDescriptor);
        }

        [ConsoleAction]
        [IntegrationTest]
        public void CanAddType()
        {
            DynamicTypeManager mgr = new DynamicTypeManager(new DynamicTypeDataRepository(), DefaultDataDirectoryProvider.Instance);
            string testType = "CanAddTypeTest";
            DynamicTypeDescriptor typeDescriptor = mgr.GetTypeDescriptor(testType);
            Expect.IsNull(typeDescriptor, "typeDescriptor should have been null");
            typeDescriptor = mgr.AddType(testType);
            Expect.IsNotNull(typeDescriptor, "typeDescriptor should NOT have been null");
            typeDescriptor = mgr.GetTypeDescriptor(testType);
            Expect.IsNotNull(typeDescriptor, "typeDescriptor should NOT have been null");
            OutLineFormat("{0}", typeDescriptor.PropertiesToString());
        }

        [ConsoleAction]
        [IntegrationTest]
        public void CanAddPropertyToType()
        {
            DynamicTypeManager mgr = new DynamicTypeManager(new DynamicTypeDataRepository(), DefaultDataDirectoryProvider.Instance);
            mgr.DynamicTypeDataRepository.Query<DynamicTypeDescriptor>(d => d.Id > 0).Each(d => mgr.DynamicTypeDataRepository.Delete(d));
            mgr.DynamicTypeDataRepository.Query<DynamicTypePropertyDescriptor>(p => p.Id > 0).Each(p => mgr.DynamicTypeDataRepository.Delete(p));
            string testType = nameof(CanAddPropertyToType);
            string testProperty = "SomeProperty";
            string testProperty2 = "BooleanProperty";
            mgr.AddType(testType);
            DynamicTypePropertyDescriptor prop = mgr.AddProperty(testType, testProperty, "String");
            DynamicTypePropertyDescriptor prop2 = mgr.AddProperty(testType, testProperty2, "Boolean");
            Expect.IsNotNull(prop);
            DynamicTypeDescriptor typeDescriptor = mgr.GetTypeDescriptor(testType);
            Expect.IsNotNull(typeDescriptor);
            Expect.IsTrue(typeDescriptor.Properties.Count == 2);
        }

        [ConsoleAction]
        [IntegrationTest]
        public void CanSpecifyNamespace()
        {
            DynamicTypeManager mgr = new DynamicTypeManager(new DynamicTypeDataRepository(), DefaultDataDirectoryProvider.Instance);
            mgr.DynamicTypeDataRepository.Query<DynamicTypeDescriptor>(d => d.Id > 0).Each(d => mgr.DynamicTypeDataRepository.Delete(d));
            mgr.DynamicTypeDataRepository.Query<DynamicTypePropertyDescriptor>(p => p.Id > 0).Each(p => mgr.DynamicTypeDataRepository.Delete(p));
            string testType = nameof(CanSpecifyNamespace);
            string testType2 = nameof(CanSpecifyNamespace) + "2";
            string testNamespace = "My.Test.Namespace";
            mgr.AddType(testType, testNamespace);
            mgr.AddType(testType2, testNamespace);
            DynamicNamespaceDescriptor ns = mgr.GetNamespaceDescriptor(testNamespace);
            Expect.IsNotNull(ns, "namspace was null");
            Expect.IsTrue(ns.Types.Count == 2);
        }

        [ConsoleAction]
        [IntegrationTest]
        public void CanGetAssembly()
        {
            DynamicTypeManager mgr = new DynamicTypeManager(new DynamicTypeDataRepository(), DefaultDataDirectoryProvider.Instance);
            mgr.DynamicTypeDataRepository.Query<DynamicTypeDescriptor>(d => d.Id > 0).Each(d => mgr.DynamicTypeDataRepository.Delete(d));
            mgr.DynamicTypeDataRepository.Query<DynamicTypePropertyDescriptor>(p => p.Id > 0).Each(p => mgr.DynamicTypeDataRepository.Delete(p));
            string testType = nameof(CanAddPropertyToType);
            mgr.AddType(testType);
            string testProperty = "SomeProperty";
            string testProperty2 = "BooleanProperty";
            DynamicTypePropertyDescriptor prop = mgr.AddProperty(testType, testProperty, "String");
            DynamicTypePropertyDescriptor prop2 = mgr.AddProperty(testType, testProperty2, "Boolean");

            Assembly ass = mgr.GenerateAssembly();
            Expect.IsNotNull(ass);
        }

        private static void SetupTestData(out TestDynamicTypeManager repo, out Dictionary<object, object> data)
        {
            // setup test data
            JObject jobj = (JObject)JsonConvert.DeserializeObject(new
            {
                Name = "some name",
                ChildObjectProp = new
                {
                    Name = "child name",
                    ChildProp = "only child has this"
                },
                ChildArrProp = new object[]
                {
                    new
                    {
                        Fromage = "gooey"
                    }
                }
            }.ToJson());

            data = jobj.ToObject<Dictionary<object, object>>();
            repo = GetTestDynamicTypeManager();
        }

        private static TestDynamicTypeManager GetTestDynamicTypeManager()
        {
            TestDynamicTypeManager repo;
            // clear existing entries if any
            TestDynamicTypeManager testRepo = new TestDynamicTypeManager(new DynamicTypeDataRepository(), DefaultDataDirectoryProvider.Instance);
            testRepo.DynamicTypeDataRepository.DynamicTypeDescriptorsWhere(d => d.Id > 0).Each(d => testRepo.DynamicTypeDataRepository.Delete(d));
            testRepo.DynamicTypeDataRepository.DataInstancesWhere(d => d.Id > 0).Each(d => testRepo.DynamicTypeDataRepository.Delete(d));

            // make sure the type isn't in the repo 
            DynamicTypeDescriptor descriptor = testRepo.DynamicTypeDataRepository.DynamicTypeDescriptorsWhere(d => d.Id > 0).FirstOrDefault();
            Expect.IsNull(descriptor);

            repo = testRepo;
            return repo;
        }
    }
}
