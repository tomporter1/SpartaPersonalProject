using BussinessLayer;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using ValorantDatabase;

namespace ValorantAppTests
{
    public class AgentTypeManagerTests
    {
        AgentTypeManager _manager;
        ValorantContext _context;

        [OneTimeSetUp]
        public void Setup()
        {
            DbContextOptions<ValorantContext> options = new DbContextOptionsBuilder<ValorantContext>()
                .UseInMemoryDatabase(databaseName: "AgentTypeTestDb")
                .Options;
            _context = new ValorantContext(options);
            _context.AgentType.AddRange(new List<AgentType>()
            {
                new AgentType() { TypeName = "Duelist", ImagePath = "/image1.png" },
                new AgentType() { TypeName = "Controller", ImagePath = "/image2.png" },
                new AgentType() { TypeName = "Initiator", ImagePath = "/image3.png" }
            });
            _context.SaveChanges();
            _manager = new AgentTypeManager(_context);
        }

        [Test]
        public void GetAllAgentsTest()
        {
            Assert.That(_manager.GetAllEntries().GetType(), Is.EqualTo(typeof(List<object>)));
            Assert.That(_manager.GetAllEntries().Count, Is.EqualTo(3));
        }

        [TestCase("Duelist", AgentTypeManager.Fields.Name)]
        public void GetMapDataTest(string expectedResult, AgentTypeManager.Fields field)
        {
            _manager.AddNewEntry(new AgentTypeArgs(expectedResult));

            //setup
            object typeObj = _context.AgentType.ToList().First();

            //Assertion
            Assert.That(_manager.GetTypeDataStr(typeObj, field), Is.EqualTo(expectedResult));
        }

        [Test]
        public void AddAgentTypeTest()
        {
            //Test method call
            _manager.AddNewEntry(new AgentTypeArgs("new type"));
            object addedType = _context.AgentType.ToList().Last();

            //Assersion 
            Assert.That(_context.AgentType.ToList(), Contains.Item(addedType));

            //Undo database changes done by the test
            if (_context.AgentType.ToList().Contains(addedType))
            {
                AgentType typeToremove = _context.AgentType.ToList().Last();
                _context.AgentType.Remove(typeToremove);
                _context.SaveChanges();
            }
        }

        [Test]
        public void RemoveAgentTypeTest()
        {
            //setup
            _manager.AddNewEntry(new AgentTypeArgs("New Type"));
            object addedType = _context.AgentType.ToList().Last();

            //Test method call
            _manager.RemoveEntry(addedType);

            //Assersion 
            Assert.That(_context.AgentType.ToList(), !Contains.Item(addedType));

            //Undo database changes done by the test
            if (_context.AgentType.ToList().Contains(addedType))
            {
                AgentType agentTypeToRemove = _context.AgentType.ToList().Last();
                _context.AgentType.Remove(agentTypeToRemove);
                _context.SaveChanges();
            }
        }

        [Test]
        public void UpdateAgentTypeTest()
        {
            //setup
            object type = _context.AgentType.ToList().Last();

            string newTypeName = "better type";

            _manager.UpdateEntry(type, new AgentTypeArgs(newTypeName));


            AgentType lastTypeInDB = _context.AgentType.ToList().Last();
            Assert.AreEqual(newTypeName, lastTypeInDB.TypeName);

            lastTypeInDB.TypeName = "Duelist";
            _context.SaveChanges();
        }
    }
}
