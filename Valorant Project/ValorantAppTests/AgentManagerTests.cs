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
        AgentManager _agentManager;
        [SetUp]
        public void Setup()
        {
            _agentManager = new AgentManager();
        }

        [Test]
        public void GetAllAgentsTest()
        {
            AgentManager manager = new AgentManager();
            var result = manager.GetAllEntries();
            Assert.AreEqual(result.GetType(), typeof(List<object>));
        }

        [Test]
        public void AddAgentTest()
        {
            //setup
            bool testPassed = false;
            
            AgentArgs args = null;
            int beforeCount = -1;
            AgentType type = new AgentType() { TypeName = "New Type" };
            using (ValorantContext db = new ValorantContext())
            {
                db.AgentType.Add(type);
                db.SaveChanges();

                args = new AgentArgs("Reyna", db.AgentType.ToList().Last(), "sigName", "signatureDisc", "ultName", "ultDisc", "normal1Name", "normal1Disc", "normal2Name", "normal2Disc", "bio");

                beforeCount = db.Agents.ToList().Count;
            }

            //Test method call            
            _agentManager.AddNewEntry(args);

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
            object addedAgent = null;
            using (ValorantContext db = new ValorantContext())
            {
                db.AgentType.Add(new AgentType() { TypeName = "New Type" });
                db.SaveChanges();

                AgentArgs args = new AgentArgs("Reyna", db.AgentType.ToList().Last(), "sigName", "signatureDisc", "ultName", "ultDisc", "normal1Name", "normal1Disc", "normal2Name", "normal2Disc", "bio");

                _agentManager.AddNewEntry(args);
                beforeCount = db.Agents.ToList().Count;
                addedAgent = db.Agents.ToList().Last();
            }

            //Test method call
            _agentManager.RemoveEntry(addedAgent);

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
            object addedAgent = null;
            AgentType type = new AgentType() { TypeName = "New Type" };
            AgentArgs updatedArgs = null;

            using (ValorantContext db = new ValorantContext())
            {
                db.AgentType.Add(type);
                db.SaveChanges();

                AgentArgs args = new AgentArgs("Reyna", db.AgentType.ToList().Last(), "sigName", "signatureDisc", "ultName", "ultDisc", "normal1Name", "normal1Disc", "normal2Name", "normal2Disc", "bio");
                updatedArgs = new AgentArgs("Bob", db.AgentType.ToList().Last(), "sigName", "signatureDisc", "ultName", "ultDisc", "normal1Name", "normal1Disc", "normal2Name", "normal2Disc", "bio");
                _agentManager.AddNewEntry(args);
                addedAgent = db.Agents.ToList().Last();
            }

            //Test
            _agentManager.UpdateEntry(addedAgent, updatedArgs);

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
            object addedAgent = null;

            using (ValorantContext db = new ValorantContext())
            {
                db.AgentType.Add(new AgentType() { TypeName = "New Type" });
                db.SaveChanges();
                AgentArgs args = new AgentArgs("Reyna", db.AgentType.ToList().Last(), "sigName", "signatureDisc", "ultName", "ultDisc", "normal1Name", "normal1Disc", "normal2Name", "normal2Disc", "bio");
                _agentManager.AddNewEntry(args);

                addedAgent = db.Agents.ToList().Last();
            }

            //Test
            string result = _agentManager.GetAgentDataStr(addedAgent, field);

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
            AgentType newAgentType = new AgentType() { TypeName = "New Type" };
            object selectedAgent = null;
            using (ValorantContext db = new ValorantContext())
            {
                db.AgentType.Add(newAgentType);
                db.SaveChanges();

                AgentArgs args = new AgentArgs("name", db.AgentType.ToList().Last(), "sigName", "signatureDisc", "ultName", "UltDisc", "Normal1Name", "Normal1Disc", "Normal2Name", "Normal2Disc", "bio");

                _agentManager.AddNewEntry(args);

                selectedAgent = db.Agents.ToList().Last();
            }

            //Test
            var result = _agentManager.GetAgentTypeObj(selectedAgent);

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
            AgentType newAgentType = new AgentType() { TypeName = "New Type" };
            object selectedAgent = null;
            using (ValorantContext db = new ValorantContext())
            {
                db.AgentType.Add(newAgentType);
                db.SaveChanges();

                AgentArgs args = new AgentArgs("name", db.AgentType.ToList().Last(), "sigName", "signatureDisc", "ultName", "ultDisc", "normal1Name", "normal1Disc", "normal2Name", "normal2Disc", "bio");

                _agentManager.AddNewEntry(args);

                selectedAgent = db.Agents.ToList().Last();
            }

            //Test
            var result = _agentManager.GetAbilityDiscription(selectedAgent, abilityName);

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

                AgentArgs args = new AgentArgs("name", db.AgentType.ToList().Last(), "sigName", "signatureDisc", "ultName", "ultDisc", "normal1Name", "normal1Disc", "normal2Name", "normal2Disc", "bio");

                _agentManager.AddNewEntry(args);

                selectedAgent = db.Agents.ToList().Last();
            }

            //Test
            var result = _agentManager.GetAgentsAbilities(selectedAgent);

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