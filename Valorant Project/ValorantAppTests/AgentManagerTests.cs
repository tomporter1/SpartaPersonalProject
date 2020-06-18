using BussinessLayer;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Linq;
using ValorantDatabase;

namespace ValorantAppTests
{
    public class AgentManagerTests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetAllAgentsTest()
        {
            AgentManager manager = new AgentManager();
            var result = manager.GetAllAgents();
            Assert.AreEqual(result.GetType(), typeof(List<Agents>));
        }

        [Test]
        public void AddAgentTest()
        {
            //setup
            bool testPassed = false;
            AgentManager manager = new AgentManager();
            AgentManagerArgs args = null;
            int beforeCount = -1;
            AgentType type = new AgentType() { TypeName = "New Type" };
            using (ValorantContext db = new ValorantContext())
            {
                db.AgentType.Add(type);
                db.SaveChanges();

                args = new AgentManagerArgs("Reyna", db.AgentType.ToList().Last(), "sigName", "signatureDisc", "ultName", "ultDisc", "normal1Name", "normal1Disc", "normal2Name", "normal2Disc", "bio");

                beforeCount = db.Agents.ToList().Count;
            }

            //Test method call            
            manager.AddNewAgent(args);

            //Assersion 
            using (ValorantContext db = new ValorantContext())
            {
                int afterCount = db.Agents.ToList().Count;

                testPassed = afterCount == beforeCount + 1;
                Assert.IsTrue(testPassed);
            }

            //Undo database changes done by the test
            if (testPassed)
            {
                using (ValorantContext db = new ValorantContext())
                {
                    Agents agentToRemove = db.Agents.ToList().Last();
                    db.Agents.Remove(agentToRemove);                    
                    db.SaveChanges();
                }
            }
            using (ValorantContext db = new ValorantContext())
            {               
                AgentType lastTypeInDB = db.AgentType.ToList().Last();
                db.AgentType.Remove(lastTypeInDB);
                db.SaveChanges();
            }
        }

        [Test]
        public void RemoveAgentTest()
        {
            //setup
            bool testPassed = false;
            int beforeCount = -1;
            AgentManager manager = new AgentManager();
            object addedAgent = null;
            using (ValorantContext db = new ValorantContext())
            {
                db.AgentType.Add(new AgentType() { TypeName = "New Type" });
                db.SaveChanges();

                AgentManagerArgs args = new AgentManagerArgs("Reyna", db.AgentType.ToList().Last(), "sigName", "signatureDisc", "ultName", "ultDisc", "normal1Name", "normal1Disc", "normal2Name", "normal2Disc", "bio");

                manager.AddNewAgent(args);
                beforeCount = db.Agents.ToList().Count;
                addedAgent = db.Agents.ToList().Last();
            }

            //Test method call
            manager.RemoveAgent(addedAgent);

            //Assersion 
            using (ValorantContext db = new ValorantContext())
            {
                int afterCount = db.Agents.ToList().Count;

                testPassed = afterCount == beforeCount - 1;
                Assert.IsTrue(testPassed);
            }

            //Undo database changes done by the test
            if (!testPassed)
            {
                using (ValorantContext db = new ValorantContext())
                {
                    Agents agentToRemove = db.Agents.ToList().Last();
                    db.Agents.Remove(agentToRemove);
                    db.SaveChanges();
                }
            }
            using (ValorantContext db = new ValorantContext())
            {
                AgentType lastTypeInDB = db.AgentType.ToList().Last();
                db.AgentType.Remove(lastTypeInDB);
                db.SaveChanges();
            }
        }

        [Test]
        public void UpdateAgentTest()
        {
            //setup
            AgentManager manager = new AgentManager();
            object addedAgent = null;
            AgentType type = new AgentType() { TypeName = "New Type" };
            AgentManagerArgs updatedArgs = null;

            using (ValorantContext db = new ValorantContext())
            {
                db.AgentType.Add(type);
                db.SaveChanges();

                AgentManagerArgs args = new AgentManagerArgs("Reyna", db.AgentType.ToList().Last(), "sigName", "signatureDisc", "ultName", "ultDisc", "normal1Name", "normal1Disc", "normal2Name", "normal2Disc", "bio");
                updatedArgs = new AgentManagerArgs("Bob", db.AgentType.ToList().Last(), "sigName", "signatureDisc", "ultName", "ultDisc", "normal1Name", "normal1Disc", "normal2Name", "normal2Disc", "bio");
                manager.AddNewAgent(args);
                addedAgent = db.Agents.ToList().Last();
            }

            //Test
            manager.UpdateAgent(addedAgent, updatedArgs);

            //assertion and removing the new entry from the database
            using (ValorantContext db = new ValorantContext())
            {
                Agents lastAgentInDB = db.Agents.ToList().Last();

                Assert.AreEqual(updatedArgs.Name, lastAgentInDB.AgentName);

                db.Agents.Remove(lastAgentInDB);
                AgentType lastTypeInDB = db.AgentType.ToList().Last();
                db.AgentType.Remove(lastTypeInDB);
                db.SaveChanges();
            }
        }

        [TestCase("Reyna", AgentManager.Fields.Name)]
        [TestCase("sigName", AgentManager.Fields.SignatureAbilityName)]
        [TestCase("ultName", AgentManager.Fields.UltamateAbilityName)]
        [TestCase("normal1Name", AgentManager.Fields.AbilityOneName)]
        [TestCase("normal2Name", AgentManager.Fields.AbilityTwoName)]
        public void GetAgentDataTest(string expectedResult, AgentManager.Fields field)
        {
            //setup
            AgentManager manager = new AgentManager();
            object addedAgent = null;

            using (ValorantContext db = new ValorantContext())
            {
                db.AgentType.Add(new AgentType() { TypeName = "New Type" });
                db.SaveChanges();
                AgentManagerArgs args = new AgentManagerArgs("Reyna", db.AgentType.ToList().Last(), "sigName", "signatureDisc", "ultName", "ultDisc", "normal1Name", "normal1Disc", "normal2Name", "normal2Disc", "bio");
                manager.AddNewAgent(args);

                addedAgent = db.Agents.ToList().Last();
            }

            //Test
            string result = manager.GetAgentData(addedAgent, field);

            //Assertion
            Assert.AreEqual(result, expectedResult);

            //Undo database changes done by the test
            using (ValorantContext db = new ValorantContext())
            {
                Agents lastAgentInDB = db.Agents.ToList().Last();
                db.Agents.Remove(lastAgentInDB);
                AgentType lastTypeInDB = db.AgentType.ToList().Last();
                db.AgentType.Remove(lastTypeInDB);
                db.SaveChanges();
            }
        }

        [Test]
        public void GetAgentTypeTest()
        {
            //Setup
            AgentManager agentManager = new AgentManager();
            AgentType newAgentType = new AgentType() { TypeName = "New Type" };
            object selectedAgent = null;
            using (ValorantContext db = new ValorantContext())
            {
                db.AgentType.Add(newAgentType);
                db.SaveChanges();

                AgentManagerArgs args = new AgentManagerArgs("name", db.AgentType.ToList().Last(), "sigName", "signatureDisc", "ultName", "UltDisc", "Normal1Name", "Normal1Disc", "Normal2Name", "Normal2Disc", "bio");

                agentManager.AddNewAgent(args);

                selectedAgent = db.Agents.ToList().Last();
            }

            //Test
            var result = agentManager.GetAgentTypeObj(selectedAgent);

            //Assertion
            Assert.IsTrue(newAgentType.Equals(result));

            //Undo database changes done by the test
            using (ValorantContext db = new ValorantContext())
            {
                Agents lastAgentInDB = db.Agents.ToList().Last();
                db.Agents.Remove(lastAgentInDB);
                AgentType lastTypeInDB = db.AgentType.ToList().Last();
                db.AgentType.Remove(lastTypeInDB);
                db.SaveChanges();
            }
        }

        [TestCase("signatureDisc", "sigName")]
        [TestCase("ultDisc", "ultName")]
        [TestCase("normal1Disc", "normal1Name")]
        [TestCase("normal2Disc", "normal2Name")]
        public void GetAgentAbilityDiscriptionTest(string expectedResult, object abilityName)
        {
            //Setup
            AgentManager agentManager = new AgentManager();
            AgentType newAgentType = new AgentType() { TypeName = "New Type" };
            object selectedAgent = null;
            using (ValorantContext db = new ValorantContext())
            {
                db.AgentType.Add(newAgentType);
                db.SaveChanges();

                AgentManagerArgs args = new AgentManagerArgs("name", db.AgentType.ToList().Last(), "sigName", "signatureDisc", "ultName", "ultDisc", "normal1Name", "normal1Disc", "normal2Name", "normal2Disc", "bio");

                agentManager.AddNewAgent(args);

                selectedAgent = db.Agents.ToList().Last();
            }

            //Test
            var result = agentManager.GetAbilityDiscription(selectedAgent, abilityName);

            //Assertion
            Assert.AreEqual(expectedResult, result);

            //Undo database changes done by the test
            using (ValorantContext db = new ValorantContext())
            {
                Agents lastAgentInDB = db.Agents.ToList().Last();
                db.Agents.Remove(lastAgentInDB);
                AgentType lastTypeInDB = db.AgentType.ToList().Last();
                db.AgentType.Remove(lastTypeInDB);
                db.SaveChanges();
            }
        }

        [Test]
        public void GetAgentAbilitiesTest()
        {
            //Setup
            AgentManager agentManager = new AgentManager();
            AgentType newAgentType = new AgentType() { TypeName = "New Type" };
            List<string> expectedResult = new List<string>()
            {
                "sigName",
                "ultName",
                "normal1Name",
                "normal2Name"
            };
            object selectedAgent = null;
            using (ValorantContext db = new ValorantContext())
            {
                db.AgentType.Add(newAgentType);
                db.SaveChanges();

                AgentManagerArgs args = new AgentManagerArgs("name", db.AgentType.ToList().Last(), "sigName", "signatureDisc", "ultName", "ultDisc", "normal1Name", "normal1Disc", "normal2Name", "normal2Disc", "bio");

                agentManager.AddNewAgent(args);

                selectedAgent = db.Agents.ToList().Last();
            }

            //Test
            var result = agentManager.GetAgentsAbilities(selectedAgent);

            //Assertion
            Assert.AreEqual(expectedResult, result);

            //Undo database changes done by the test
            using (ValorantContext db = new ValorantContext())
            {
                Agents lastAgentInDB = db.Agents.ToList().Last();
                db.Agents.Remove(lastAgentInDB);
                AgentType lastTypeInDB = db.AgentType.ToList().Last();
                db.AgentType.Remove(lastTypeInDB);
                db.SaveChanges();
            }
        }
    }
}