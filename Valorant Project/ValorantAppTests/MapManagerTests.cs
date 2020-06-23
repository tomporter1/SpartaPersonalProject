using BussinessLayer;
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

        [OneTimeSetUp]
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

        [Test]
        public void GetAllMapsTest()
        {
            Assert.That(_manager.GetAllEntries().GetType(), Is.EqualTo(typeof(List<object>)));
            Assert.That(_manager.GetAllEntries().Count, Is.EqualTo(3));
        }

        [TestCase("Spit", MapManager.Fields.Name)]
        public void GetMapDataTest(string expectedResult, MapManager.Fields field)
        {
            //setup
            object mapObj = _context.Maps.ToList().First();

            //Assertion
            Assert.That(_manager.GetMapsDataStr(mapObj, field), Is.EqualTo(expectedResult));
        }

        [Test]
        public void AddMapTest()
        {

            //Test method call
            _manager.AddNewEntry(new MapArgs("new map"));
            object addedMap = _context.Maps.ToList().Last();

            //Assersion 
            Assert.That(_context.Maps.ToList(), Contains.Item(addedMap));


            //Undo database changes done by the test
            if (_context.Maps.ToList().Contains(addedMap))
            {
                Maps mapToremove = _context.Maps.ToList().Last();

                _context.Maps.Remove(mapToremove);
                _context.SaveChanges();
            }
        }

        [Test]
        public void RemoveMapTest()
        {
            _manager.AddNewEntry(new MapArgs("New Map"));

            object addedMap = _context.Maps.ToList().Last();

            //Test method call
            _manager.RemoveEntry(addedMap);

            //Assersion 

            //assersion 
            Assert.That(_context.Maps.ToList(), !Contains.Item(addedMap));


            //Undo database changes done by the test
            if (_context.Maps.ToList().Contains(addedMap))
            {
                Maps mapToRemove = _context.Maps.ToList().Last();
                _context.Maps.Remove(mapToRemove);
                _context.SaveChanges();
            }
        }

        [Test]
        public void UpdateMapTest()
        {
            //setup
            object addedMap = _context.Maps.ToList().First();

            string newMapName = "better map";

            _manager.UpdateEntry(addedMap, new MapArgs(newMapName));

            Maps firstMapInDB = _context.Maps.ToList().First();
            Assert.AreEqual(newMapName, firstMapInDB.MapName);
        }
    }
}
