using BussinessLayer;
using BussinessLayer.Args;
using BussinessLayer.Managers;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using ValorantDatabase;

namespace ValorantAppTests
{
    public class MapManagerTests
    {
        MapManager _manager;
        ValorantContext _context;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<ValorantContext> options = new DbContextOptionsBuilder<ValorantContext>()
                .UseInMemoryDatabase(databaseName: "MapTestDb")
                .Options;
            _context = new ValorantContext(options);
            _context.Maps.AddRange(new List<Maps>()
            {
                new Maps() { MapName = "Spit", ImagePath = "/image1.png", LayoutImagePath="/image1.png" },
                new Maps() { MapName = "Bind", ImagePath = "/image2.png", LayoutImagePath="/image2.png" },
                new Maps() { MapName = "Haven", ImagePath = "/image3.png", LayoutImagePath="/image3.png" }
            });
            _context.SaveChanges();
            _manager = new MapManager(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Maps.RemoveRange(_context.Maps);
            _context.SaveChanges();
        }

        [Test]
        public void GetAllMapsTest()
        {
            var result = _manager.GetAllEntries();
            Assert.That(result.GetType(), Is.EqualTo(typeof(List<object>)));
            Assert.That(result.Count, Is.EqualTo(3));
        }

        [Test]
        public void GetMapDataTest()
        {
            //setup
            object mapObj = _context.Maps.ToList().First();
            string name = ((Maps)mapObj).MapName;
            //Assertion
            Assert.That(_manager.GetMapsDataStr(mapObj, MapManager.Fields.Name), Is.EqualTo(name));
        }

        [Test]
        public void AddMapTest()
        {
            //Test method call
            _manager.AddNewEntry(new MapArgs("new map"));
            object addedMap = _context.Maps.ToList().Last();

            //Assersion 
            Assert.That(_context.Maps.ToList(), Contains.Item(addedMap));
        }

        [Test]
        public void RemoveMapTest()
        {
            object lastMap = _context.Maps.ToList().Last();

            //Test method call
            _manager.RemoveEntry(lastMap);

            //Assersion 
            Assert.That(_context.Maps.ToList(), !Contains.Item(lastMap));
        }

        [Test]
        public void UpdateMapTest()
        {
            //setup
            object map = _context.Maps.ToList().First();

            string newMapName = "better map";

            _manager.UpdateEntry(map, new MapArgs(newMapName));

            Assert.AreEqual(newMapName, _context.Maps.ToList().First().MapName);
        }
    }
}
