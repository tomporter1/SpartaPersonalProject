using BussinessLayer;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using ValorantDatabase;

namespace ValorantAppTests
{
    public class MapManagerTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void GetAllMapsTest()
        {
            MapManager manager = new MapManager();
            var result = manager.GetAllEntries();
            Assert.AreEqual(result.GetType(), typeof(List<object>));
        }

        [Test]
        public void AddMapTest()
        {
            //setup
            bool testPassed = false;
            int beforeCount = -1;
            MapManager manager = new MapManager();
            using (ValorantContext db = new ValorantContext())
            {
                beforeCount = db.Maps.ToList().Count;
            }

            //Test method call
            manager.AddNewEntry("new map");

            //Assersion 
            using (ValorantContext db = new ValorantContext())
            {
                int afterCount = db.Maps.ToList().Count;

                testPassed = afterCount == beforeCount + 1;
                Assert.IsTrue(testPassed);
            }

            //Undo database changes done by the test
            if (testPassed)
            {
                using (ValorantContext db = new ValorantContext())
                {
                    Maps mapToremove = db.Maps.ToList().Last();

                    db.Maps.Remove(mapToremove);
                    db.SaveChanges();
                }
            }
        }

        [Test]
        public void RemoveMapTest()
        {
            //setup
            bool testPassed = false;
            MapManager manager = new MapManager();
            object addedAgent = null;
            int beforeCount = -1;
            using (ValorantContext db = new ValorantContext())
            {
                manager.AddNewEntry("New Map");

                beforeCount = db.Maps.ToList().Count;
                addedAgent = db.Maps.ToList().Last();
            }

            //Test method call
            manager.RemoveEntry(addedAgent);

            //Assersion 
            using (ValorantContext db = new ValorantContext())
            {
                int afterCount = db.Maps.ToList().Count;

                //assersion 
                testPassed = afterCount == beforeCount - 1;
                Assert.IsTrue(testPassed);
            }

            //Undo database changes done by the test
            if (!testPassed)
            {
                using (ValorantContext db = new ValorantContext())
                {
                    Maps mapToRemove = db.Maps.ToList().Last();

                    db.Maps.Remove(mapToRemove);
                    db.SaveChanges();
                }
            }
        }

        [Test]
        public void UpdateMapTest()
        {
            MapManager manager = new MapManager();
            object addedMap = null;
            using (ValorantContext db = new ValorantContext())
            {
                //setup
                manager.AddNewEntry("new map");
                addedMap = db.Maps.ToList().Last();
            }

            string newMapName = "better map";

            manager.UpdateEntry(addedMap, newMapName);

            using (ValorantContext db = new ValorantContext())
            {
                Maps lastMapInDB = db.Maps.ToList().Last();
                Assert.AreEqual(newMapName, lastMapInDB.MapName);

                db.Maps.Remove(lastMapInDB);
                db.SaveChanges();
            }
        }

        [TestCase("new map", MapManager.Fields.Name)]
        public void GetMapDataTest(string expectedResult, MapManager.Fields field)
        {
            //setup
            MapManager manager = new MapManager();
            object addedMap = null;

            manager.AddNewEntry(expectedResult);

            using (ValorantContext db = new ValorantContext())
            {
                addedMap = db.Maps.ToList().Last();
            }

            //Test
            string result = manager.GetMapsData(addedMap, field);

            //Assertion
            Assert.AreEqual(result, expectedResult);

            //Undo database changes done by the test
            using (ValorantContext db = new ValorantContext())
            {
                Maps lastMapInDB = db.Maps.ToList().Last();
                db.Maps.Remove(lastMapInDB);
                db.SaveChanges();
            }
        }
    }
}
