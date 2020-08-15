using BussinessLayer;
using BussinessLayer.Args;
using BussinessLayer.Managers;
using Microsoft.EntityFrameworkCore;
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
        ValorantContext _context;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<ValorantContext> options = new DbContextOptionsBuilder<ValorantContext>()
               .UseInMemoryDatabase(databaseName: "AgentTestDb")
               .Options;
            _context = new ValorantContext(options);
            _context.AgentType.AddRange(new List<AgentType>()
            {
                new AgentType() { TypeName = "Duelist", ImagePath = "/image1.png" },
                new AgentType() { TypeName = "Controller", ImagePath = "/image2.png" },
                new AgentType() { TypeName = "Initiator", ImagePath = "/image3.png" }
            });
            _context.SaveChanges();
            _context.Agents.AddRange(new List<Agents>()
            {
                new Agents() { AgentName = "Reyna", AgentType= _context.AgentType.ToList()[0], SignatureAbilityName = "sigName", SignatureAbilityDiscription = "signatureDisc", UltamateAbilityName = "ultName", UltamateAbilityDiscription = "ultDisc", AbilityOneName = "normal1Name", AbilityOneDiscription = "normal1Disc", AbilityTwoName = "normal2Name", AbilityTwoDiscription = "normal2Disc", Bio = "bio" },
                new Agents() { AgentName = "Jett", AgentType= _context.AgentType.ToList()[1], SignatureAbilityName = "sigName", SignatureAbilityDiscription = "signatureDisc", UltamateAbilityName = "ultName", UltamateAbilityDiscription = "ultDisc", AbilityOneName = "normal1Name", AbilityOneDiscription = "normal1Disc", AbilityTwoName = "normal2Name", AbilityTwoDiscription = "normal2Disc", Bio = "bio" },
                new Agents() { AgentName = "Cypher", AgentType= _context.AgentType.ToList()[2], SignatureAbilityName = "sigName", SignatureAbilityDiscription = "signatureDisc", UltamateAbilityName = "ultName", UltamateAbilityDiscription = "ultDisc", AbilityOneName = "normal1Name", AbilityOneDiscription = "normal1Disc", AbilityTwoName = "normal2Name", AbilityTwoDiscription = "normal2Disc", Bio = "bio" }
            });
            _context.SaveChanges();

            _agentManager = new AgentManager(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Agents.RemoveRange(_context.Agents);
            _context.AgentType.RemoveRange(_context.AgentType);
            _context.SaveChanges();
        }

        [Test]
        public void GetAllAgentsTest()
        {
            var result = _agentManager.GetAllEntries();
            Assert.That(result.GetType(), Is.EqualTo(typeof(List<object>)));
            Assert.That(result.Count, Is.EqualTo(3));
        }

        [Test]
        public void AddAgentTest()
        {
            //Test method call            
            _agentManager.AddNewEntry(new AgentArgs("New Boi", _context.AgentType.ToList().Last(), "sigName", "signatureDisc", "ultName", "ultDisc", "normal1Name", "normal1Disc", "normal2Name", "normal2Disc", "bio"));
            object addedAgent = _context.Agents.ToList()[3];

            //Assersion  
            Assert.That(_context.Agents.ToList(), Contains.Item(addedAgent));
        }

        [Test]
        public void RemoveAgentTest()
        {
            object AgentToRemove = _context.Agents.ToList().Last();
            //Test method call
            _agentManager.RemoveEntry(AgentToRemove);

            //Assersion             
            Assert.That(_context.Agents.ToList(), !Contains.Item(AgentToRemove));
        }

        [Test]
        public void UpdateAgentTest()
        {
            AgentArgs updatedArgs = new AgentArgs("Bob", _context.AgentType.ToList().First(), "sigName", "signatureDisc", "ultName", "ultDisc", "normal1Name", "normal1Disc", "normal2Name", "normal2Disc", "bio");
            object agentToUpdate = _context.Agents.ToList().Last();

            //Test
            _agentManager.UpdateEntry(agentToUpdate, updatedArgs);

            //assertion and removing the new entry from the database            
            Assert.That(updatedArgs.Name, Is.EqualTo("Bob"));
        }

        [TestCase("Cypher", AgentManager.Fields.Name)]
        [TestCase("sigName", AgentManager.Fields.SignatureAbilityName)]
        [TestCase("ultName", AgentManager.Fields.UltamateAbilityName)]
        [TestCase("normal1Name", AgentManager.Fields.AbilityOneName)]
        [TestCase("normal2Name", AgentManager.Fields.AbilityTwoName)]
        public void GetAgentDataTest(string expectedResult, AgentManager.Fields field)
        {
            //setup
            object addedAgent = _context.Agents.ToList().Last();

            //Test
            string result = _agentManager.GetAgentDataStr(addedAgent, field);

            //Assertion
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetAgentTypeTest()
        {
            //Setup
            object selectedAgent = _context.Agents.ToList().Last();

            //Test
            var result = _agentManager.GetAgentTypeObj(selectedAgent);
            AgentType type = _context.AgentType.Where(t => t.TypeId == ((Agents)selectedAgent).AgentTypeId).FirstOrDefault();
            //Assertion
            Assert.That(type, Is.EqualTo(result));
        }

        [TestCase("signatureDisc", "sigName")]
        [TestCase("ultDisc", "ultName")]
        [TestCase("normal1Disc", "normal1Name")]
        [TestCase("normal2Disc", "normal2Name")]
        public void GetAgentAbilityDiscriptionTest(string expectedResult, object abilityName)
        {
            //Setup
            object selectedAgent = _context.Agents.ToList().Last();

            //Test
            var result = _agentManager.GetAbilityDiscription(selectedAgent, abilityName);

            //Assertion
            Assert.That(expectedResult, Is.EqualTo(result));
        }

        [Test]
        public void GetAgentAbilitiesTest()
        {
            //Setup
            List<string> expectedResult = new List<string>()
            {
                "sigName",
                "ultName",
                "normal1Name",
                "normal2Name"
            };
            object selectedAgent = _context.Agents.ToList().Last();

            //Test
            var result = _agentManager.GetAgentsAbilities(selectedAgent);

            //Assertion
            Assert.AreEqual(expectedResult, result);
        }
    }
}