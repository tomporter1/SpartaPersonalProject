using BussinessLayer;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using ValorantDatabase;

namespace ValorantAppTests
{
    public class AgentTypeManagerTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void GetAllAgentsTest()
        {
            AgentTypeManager manager = new AgentTypeManager();
            var result = manager.GetAllEntries();
            Assert.AreEqual(result.GetType(), typeof(List<object>));
        }

        [Test]
        public void AddAgentTypeTest()
        {
            //setup
            bool testPassed = false;
            AgentTypeManager manager = new AgentTypeManager();
            int beforeCount = -1;
            using (ValorantContext db = new ValorantContext())
            {
                beforeCount = db.AgentType.ToList().Count;
            }

            //Test method call
            manager.AddNewEntry(new AgentTypeArgs("new type"));

            //Assersion 
            using (ValorantContext db = new ValorantContext())
            {
                int afterCount = db.AgentType.ToList().Count;

                testPassed = afterCount == beforeCount + 1;
                Assert.IsTrue(testPassed);
            }

            //Undo database changes done by the test
            if (testPassed)
            {
                using (ValorantContext db = new ValorantContext())
                {
                    AgentType typeToremove = db.AgentType.ToList().Last();

                    db.AgentType.Remove(typeToremove);
                    db.SaveChanges();
                }
            }
        }

        [Test]
        public void RemoveAgentTypeTest()
        {
            //setup
            AgentTypeManager manager = new AgentTypeManager();
            bool testPassed = false;
            int beforeCount = -1;
            object addedAgent = null;
            using (ValorantContext db = new ValorantContext())
            {
                manager.AddNewEntry(new AgentTypeArgs("New Type"));
                beforeCount = db.AgentType.ToList().Count;
                addedAgent = db.AgentType.ToList().Last();
            }

            //Test method call
            manager.RemoveEntry(addedAgent);

            //Assersion 
            using (ValorantContext db = new ValorantContext())
            {
                int afterCount = db.AgentType.ToList().Count;

                testPassed = afterCount == beforeCount - 1;
                Assert.IsTrue(testPassed);
            }

            if (!testPassed)
            {
                //Undo database changes done by the test
                using (ValorantContext db = new ValorantContext())
                {
                    AgentType agentTypeToRemove = db.AgentType.ToList().Last();

                    db.AgentType.Remove(agentTypeToRemove);
                    db.SaveChanges();
                }
            }
        }

        [Test]
        public void UpdateAgentTypeTest()
        {
            AgentTypeManager manager = new AgentTypeManager();
            object addedType = null;
            using (ValorantContext db = new ValorantContext())
            {
                //setup
                manager.AddNewEntry(new AgentTypeArgs("new type"));
                addedType = db.AgentType.ToList().Last();
            }

            string newTypeName = "better type";

            manager.UpdateEntry(addedType, new AgentTypeArgs(newTypeName));

            using (ValorantContext db = new ValorantContext())
            {
                AgentType lastTypeInDB = db.AgentType.ToList().Last();
                Assert.AreEqual(newTypeName, lastTypeInDB.TypeName);

                db.AgentType.Remove(lastTypeInDB);
                db.SaveChanges();
            }
        }

        [TestCase("new type", AgentTypeManager.Fields.Name)]
        public void GetMapDataTest(string expectedResult, AgentTypeManager.Fields field)
        {
            //setup
            AgentTypeManager manager = new AgentTypeManager();
            object addedType = null;

            manager.AddNewEntry(new AgentTypeArgs(expectedResult));

            using (ValorantContext db = new ValorantContext())
            {
                addedType = db.AgentType.ToList().Last();
            }

            //Test
            string result = manager.GetTypeData(addedType, field);

            //Assertion
            Assert.AreEqual(result, expectedResult);

            //Undo database changes done by the test
            using (ValorantContext db = new ValorantContext())
            {
                AgentType lastTypeInDB = db.AgentType.ToList().Last();
                db.AgentType.Remove(lastTypeInDB);
                db.SaveChanges();
            }
        }
    }
}
