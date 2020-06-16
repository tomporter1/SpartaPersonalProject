using BussinessLayer;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ValorantDatabase;

namespace ValorantAppTests
{
    public class AgentManagerTests
    {
        static AgentManagerArgs _newAgentArgs, _newAgentArgsUpdatedName;

        [SetUp]
        public void Setup()
        {
            AgentType type = new AgentType();

            using (ValorantContext db = new ValorantContext())
            {
                type = db.AgentType.First();
            }

            _newAgentArgs = new AgentManagerArgs(
                "Reyna",
                type.TypeId,
                "Leer",
                "Equip an ethereal, destructible eye. Activate to cast the eye a short distance forward. The eye will nearsight all enemies who look at it.",
                "Empress",
                "Instantly enter a frenzy, increasing firing, equip and reload speed dramatically. Gain infinite charges of Soul Harvest abilities. Scoring a kill renews the duration.", "Devour",
                "Enemies killed by Reyna leave behind Soul Orbs that last 3 seconds. Instantly consume a nearby soul orb, rapidly healing for a short duration. Health gained through this skill exceeding 100 will decay over time. If Empress is active, this skill will automatically cast and not consume the Soul Orb.",
                "Dismiss",
                "Instantly consume a nearby Soul Orb, becoming instangible for a short duration. If Empress is active, also become invisible.",
                "Reyna is a VALORANT agent with an aggressive, duel-centric playstyle. Reyna's unique mechanic are the so-called Soul Orbs, which drop when she kills an opponent and which, upon consumption, give her various bonuses — from massive healing to invisibility. Reyna's ultimate ability is Empress, which turns her into a rapid-fire death machine, dramatically increasing all her combat stats. Every kill renews Empress' duration, and a skilled Reyna player can wipe away the entire team in one swift offense.");
            _newAgentArgsUpdatedName = new AgentManagerArgs(
                "Bob",
                type.TypeId,
                "Leer",
                "Equip an ethereal, destructible eye. Activate to cast the eye a short distance forward. The eye will nearsight all enemies who look at it.",
                "Empress",
                "Instantly enter a frenzy, increasing firing, equip and reload speed dramatically. Gain infinite charges of Soul Harvest abilities. Scoring a kill renews the duration.", "Devour",
                "Enemies killed by Reyna leave behind Soul Orbs that last 3 seconds. Instantly consume a nearby soul orb, rapidly healing for a short duration. Health gained through this skill exceeding 100 will decay over time. If Empress is active, this skill will automatically cast and not consume the Soul Orb.",
                "Dismiss",
                "Instantly consume a nearby Soul Orb, becoming instangible for a short duration. If Empress is active, also become invisible.",
                "Reyna is a VALORANT agent with an aggressive, duel-centric playstyle. Reyna's unique mechanic are the so-called Soul Orbs, which drop when she kills an opponent and which, upon consumption, give her various bonuses — from massive healing to invisibility. Reyna's ultimate ability is Empress, which turns her into a rapid-fire death machine, dramatically increasing all her combat stats. Every kill renews Empress' duration, and a skilled Reyna player can wipe away the entire team in one swift offense.");
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
            int beforeCount = -1;
            using (ValorantContext db = new ValorantContext())
            {
                beforeCount = db.Agents.ToList().Count;
            }

            //Test method call
            manager.AddNewAgent(_newAgentArgs);

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
        }

        [Test]
        public void RemoveAgentTest()
        {
            //setup
            bool testPassed = false;
            int beforeCount = -1;
            AgentManager manager = new AgentManager();
            object addedAgent = null;

            manager.AddNewAgent(_newAgentArgs);
            using (ValorantContext db = new ValorantContext())
            {
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
        }

        [Test]
        public void UpdateAgentTest()
        {
            //setup
            AgentManager manager = new AgentManager();
            int addedAgentId = -1;
            using (ValorantContext db = new ValorantContext())
            {
                manager.AddNewAgent(_newAgentArgs);
                addedAgentId = db.Agents.ToList().Last().AgentId;
            }

            //Test
            manager.UpdateAgent(addedAgentId, _newAgentArgsUpdatedName);

            //assertion and removing the new entry from the database
            using (ValorantContext db = new ValorantContext())
            {
                Agents lastAgentInDB = db.Agents.ToList().Last();
                Assert.AreEqual(_newAgentArgsUpdatedName.Name, lastAgentInDB.AgentName);

                db.Agents.Remove(lastAgentInDB);
                db.SaveChanges();
            }
        }

        [TestCase("Reyna", AgentManager.Fields.Name)]
        [TestCase("Leer", AgentManager.Fields.SignatureAbilityName)]
        [TestCase("Empress", AgentManager.Fields.UltamateAbilityName)]
        [TestCase("Devour", AgentManager.Fields.AbilityOneName)]
        [TestCase("Dismiss", AgentManager.Fields.AbilityTwoName)]
        public void GetAgentDataTest(string expectedResult, AgentManager.Fields field)
        {
            //setup
            AgentManager manager = new AgentManager();
            object addedAgent = null;

            manager.AddNewAgent(_newAgentArgs);

            using (ValorantContext db = new ValorantContext())
            {
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
                db.SaveChanges();
            }
        }
    }
}