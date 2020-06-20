using BussinessLayer;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using ValorantDatabase;

namespace ValorantAppTests
{
    public class GameLogManagerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetAllGamesTest()
        {
            GameLogManager manager = new GameLogManager();
            var result = manager.GetAllEntries();
            Assert.AreEqual(result.GetType(), typeof(List<object>));
        }


        [Test]
        public void AddGameLogTest()
        {
            //setup
            bool testPassed = false;
            GameLogManager logManager = new GameLogManager();
            int beforeCount = -1;
            GameLogArgs args = null;
            using (ValorantContext db = new ValorantContext())
            {
                db.AgentType.Add(new AgentType() { TypeName = "New Type" });
                db.Maps.Add(new Maps() { MapName = "New Map" });
                db.SaveChanges();

                AgentArgs agentArgs = new AgentArgs("Name", db.AgentType.ToList().Last(), "sigName", "signatureDisc", "ultName", "ultDisc", "normal1Name", "normal1Disc", "normal2Name", "normal2Disc", "bio");

                new AgentManager().AddNewEntry(agentArgs);
                beforeCount = db.GameLogs.ToList().Count;

                args = new GameLogArgs(db.Maps.ToList().Last(), db.Agents.ToList().Last(), 13, 12, 20, 12, 4, 150, DateTime.Now);
            }

            //Test method call            
            logManager.AddNewEntry(args);

            //Assersion 
            using (ValorantContext db = new ValorantContext())
            {
                int afterCount = db.GameLogs.ToList().Count;

                testPassed = afterCount == beforeCount + 1;
                Assert.IsTrue(testPassed);
            }

            //Undo database changes done by the test
            if (testPassed)
            {
                using (ValorantContext db = new ValorantContext())
                {
                    GameLogs addedGame = db.GameLogs.ToList().Last();
                    db.Remove(addedGame);
                    db.SaveChanges();
                }
            }
            using (ValorantContext db = new ValorantContext())
            {
                Agents agentToRemove = db.Agents.ToList().Last();
                db.Agents.Remove(agentToRemove);
                AgentType lastTypeInDB = db.AgentType.ToList().Last();
                db.AgentType.Remove(lastTypeInDB);
                Maps lastMapInDb = db.Maps.ToList().Last();
                db.Maps.Remove(lastMapInDb);
                db.SaveChanges();
            }
        }

        [Test]
        public void RemoveGameLogTest()
        {
            //setup
            bool testPassed = false;
            GameLogManager logManager = new GameLogManager();
            int beforeCount = -1;
            GameLogArgs args = null;
            object addedGame = null;
            using (ValorantContext db = new ValorantContext())
            {
                db.AgentType.Add(new AgentType() { TypeName = "New Type" });
                db.Maps.Add(new Maps() { MapName = "New Map" });
                db.SaveChanges();

                AgentArgs agentArgs = new AgentArgs("Name", db.AgentType.ToList().Last(), "sigName", "signatureDisc", "ultName", "ultDisc", "normal1Name", "normal1Disc", "normal2Name", "normal2Disc", "bio");

                new AgentManager().AddNewEntry(agentArgs);

                args = new GameLogArgs(db.Maps.ToList().Last(), db.Agents.ToList().Last(), 13, 12, 20, 12, 4, 150, DateTime.Now);
                logManager.AddNewEntry(args);
                beforeCount = db.GameLogs.ToList().Count;
                addedGame = db.GameLogs.ToList().Last();
            }

            //Test method call            
            logManager.RemoveEntry(addedGame);

            //Assersion 
            using (ValorantContext db = new ValorantContext())
            {
                int afterCount = db.GameLogs.ToList().Count;

                testPassed = afterCount == beforeCount - 1;
                Assert.IsTrue(testPassed);
            }

            //Undo database changes done by the test
            if (!testPassed)
            {
                using (ValorantContext db = new ValorantContext())
                {
                    GameLogs gameToRemove = db.GameLogs.ToList().Last();
                    db.Remove(gameToRemove);
                    db.SaveChanges();
                }
            }
            using (ValorantContext db = new ValorantContext())
            {
                Agents agentToRemove = db.Agents.ToList().Last();
                db.Agents.Remove(agentToRemove);
                AgentType lastTypeInDB = db.AgentType.ToList().Last();
                db.AgentType.Remove(lastTypeInDB);
                Maps lastMapInDb = db.Maps.ToList().Last();
                db.Maps.Remove(lastMapInDb);
                db.SaveChanges();
            }
        }

        [Test]
        public void UpdateGameLogTest()
        {
            //setup
            GameLogManager logManager = new GameLogManager();
            GameLogArgs updatedArgs = null;
            object addedGame = null;
            using (ValorantContext db = new ValorantContext())
            {
                db.AgentType.Add(new AgentType() { TypeName = "New Type" });
                db.Maps.Add(new Maps() { MapName = "New Map" });
                db.SaveChanges();

                AgentArgs agentArgs = new AgentArgs("Name", db.AgentType.ToList().Last(), "sigName", "signatureDisc", "ultName", "ultDisc", "normal1Name", "normal1Disc", "normal2Name", "normal2Disc", "bio");

                new AgentManager().AddNewEntry(agentArgs);

                GameLogArgs args = new GameLogArgs(db.Maps.ToList().Last(), db.Agents.ToList().Last(), 13, 12, 20, 12, 4, 150, DateTime.Now);
                updatedArgs = new GameLogArgs(db.Maps.ToList().Last(), db.Agents.ToList().Last(), 13, 12, 30, 45, 0, 150, DateTime.Now);
                
                logManager.AddNewEntry(args);
                
                addedGame = db.GameLogs.ToList().Last();
            }

            //Test method call            
            logManager.UpdateEntry(addedGame, updatedArgs);

            //Assersion 
            using (ValorantContext db = new ValorantContext())
            {
                GameLogs lastGameInDb = db.GameLogs.ToList().Last();

                Assert.AreEqual(updatedArgs.Kills, lastGameInDb.Kills);
                Assert.AreEqual(updatedArgs.Deaths, lastGameInDb.Deaths);
                Assert.AreEqual(updatedArgs.Assists, lastGameInDb.Assits);
            
                db.Remove(lastGameInDb);
                Agents agentToRemove = db.Agents.ToList().Last();
                db.Agents.Remove(agentToRemove);
                AgentType lastTypeInDB = db.AgentType.ToList().Last();
                db.AgentType.Remove(lastTypeInDB);
                Maps lastMapInDb = db.Maps.ToList().Last();
                db.Maps.Remove(lastMapInDb);
                db.SaveChanges();
            }
        }
    }
}
