using BussinessLayer.Args;
using BussinessLayer.Interfaces;
using BussinessLayer.Managers;
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

        [SetUp]
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

        [TearDown]
        public void TearDown()
        {
            _context.AgentType.RemoveRange(_context.AgentType);
            _context.SaveChanges();
        }

        [Test]
        public void GetAllAgentsTest()
        {
            var result = _manager.GetAllEntries();
            Assert.That(result.GetType(), Is.EqualTo(typeof(List<object>)));
            Assert.That(result.Count, Is.EqualTo(3));
        }

        [Test]
        public void GetMapDataTest()
        {
            //setup
            object typeObj = _context.AgentType.ToList().First();
            string name = ((AgentType)typeObj).TypeName;
            //Assertion
            Assert.That(_manager.GetTypeDataStr(typeObj, IAgentTypesManager.Fields.Name), Is.EqualTo(name));
        }

        [Test]
        public void AddAgentTypeTest()
        {
            //Test method call
            _manager.AddNewEntry(new AgentTypeArgs("new type"));
            object addedType = _context.AgentType.ToList()[3];

            //Assersion 
            Assert.That(_context.AgentType.ToList(), Contains.Item(addedType));
        }

        [Test]
        public void RemoveAgentTypeTest()
        {
            //setup
            object addedType = _context.AgentType.ToList().Last();

            //Test method call
            _manager.RemoveEntry(addedType);

            //Assersion 
            Assert.That(_context.AgentType.ToList(), !Contains.Item(addedType));
        }

        [Test]
        public void UpdateAgentTypeTest()
        {
            //setup
            object type = _context.AgentType.ToList().Last();

            string newTypeName = "better type";

            _manager.UpdateEntry(type, new AgentTypeArgs(newTypeName));

            Assert.That(newTypeName, Is.EqualTo(_context.AgentType.ToList().Last().TypeName));
        }
    }
}
