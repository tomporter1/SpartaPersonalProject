using BussinessLayer;
using NUnit.Framework;
using System;
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
            var result = manager.GetAllTypes();
            Assert.AreEqual(result.GetType(), typeof(List<AgentType>));
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
            manager.AddNewAgentType("new type");

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
                manager.AddNewAgentType("New Type");
                beforeCount = db.AgentType.ToList().Count;
                addedAgent = db.AgentType.ToList().Last();
            }

            //Test method call
            manager.RemoveAgentType(addedAgent);

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
            int addedTypeId = -1;
            using (ValorantContext db = new ValorantContext())
            {
                //setup
                manager.AddNewAgentType("new type");
                addedTypeId = db.AgentType.ToList().Last().TypeId;
            }

            string newTypeName = "better type";

            manager.UpdateAgentType(addedTypeId, newTypeName);

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

            manager.AddNewAgentType(expectedResult);

            using (ValorantContext db = new ValorantContext())
            {
                addedType = db.AgentType.ToList().Last();
            }

            //Test
            string result = manager.GetAgentData(addedType, field);

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
