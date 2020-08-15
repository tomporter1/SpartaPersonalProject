using BussinessLayer.Args;
using BussinessLayer.Managers;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using ValorantDatabase;

namespace ValorantAppTests
{


    public class GameLogManagerTests
    {
        ValorantContext _context;
        GameLogManager _manager;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<ValorantContext> options = new DbContextOptionsBuilder<ValorantContext>()
              .UseInMemoryDatabase(databaseName: "AgentTestDb")
              .Options;
            _context = new ValorantContext(options);
            _context.GameModes.AddRange(new List<GameModes>()
            {
                new GameModes() { ModeName = "Ranked", ModeDiscription = "" },
                new GameModes() { ModeName = "Unranked", ModeDiscription = "" }
            });
            _context.Maps.AddRange(new List<Maps>()
            {
                new Maps() { MapName = "Spit", ImagePath = "/image1.png", LayoutImagePath="/image1.png" },
                new Maps() { MapName = "Bind", ImagePath = "/image2.png", LayoutImagePath="/image2.png" },
                new Maps() { MapName = "Haven", ImagePath = "/image3.png", LayoutImagePath="/image3.png" }
            });
            _context.SaveChanges();
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
            _context.GameLogs.AddRange(new List<GameLogs>() {
                new GameLogs() { GameMode = _context.GameModes.ToList()[0], Map = _context.Maps.ToList()[0], Agent = _context.Agents.ToList()[0], TeamScore = 13, OpponentScore = 12, Kills = 20, Deaths = 12, Assits = 4, Adr = 150, DateLogged = DateTime.Now },
                new GameLogs() { GameMode = _context.GameModes.ToList()[0], Map = _context.Maps.ToList()[1], Agent = _context.Agents.ToList()[1], TeamScore = 13, OpponentScore = 12, Kills = 20, Deaths = 12, Assits = 4, Adr = 150, DateLogged = DateTime.Now },
                new GameLogs() { GameMode = _context.GameModes.ToList()[0], Map = _context.Maps.ToList()[2], Agent = _context.Agents.ToList()[2], TeamScore = 13, OpponentScore = 12, Kills = 20, Deaths = 12, Assits = 4, Adr = 150, DateLogged = DateTime.Now }
            });
            _context.SaveChanges();
            _manager = new GameLogManager(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.GameLogs.RemoveRange(_context.GameLogs);
            _context.Agents.RemoveRange(_context.Agents);
            _context.AgentType.RemoveRange(_context.AgentType);
            _context.Maps.RemoveRange(_context.Maps);
            _context.SaveChanges();
        }

        [Test]
        public void GetAllGamesTest()
        {
            var result = _manager.GetAllEntries();
            Assert.That(result.GetType(), Is.EqualTo(typeof(List<object>)));
            Assert.That(result.Count, Is.EqualTo(3));
        }

        [Test]
        public void AddGameLogTest()
        {
            //setup
            GameLogArgs args = new GameLogArgs(_context.GameModes.ToList().First(), _context.Maps.ToList().Last(), _context.Agents.ToList().Last(), 13, 12, 20, 12, 4, 150, DateTime.Now, 2, new Ranks() { RankID = 1, RankName = "Wood 1" });

            //Test method call            
            _manager.AddNewEntry(args);
            GameLogs newLog = _context.GameLogs.ToList()[3];
            //Assersion 
            Assert.That(_context.GameLogs.ToList(), Contains.Item(newLog));
        }

        [Test]
        public void RemoveGameLogTest()
        {
            //setup   
            object logToRemove = _context.GameLogs.ToList().Last();

            //Test method call            
            _manager.RemoveEntry(logToRemove);

            //Assersion            
            Assert.That(_context.GameLogs.ToList(), !Contains.Item(logToRemove));
        }

        [Test]
        public void UpdateGameLogTest()
        {
            //setup
            GameLogArgs updatedArgs = new GameLogArgs(_context.GameModes.ToList().First(), _context.Maps.ToList().First(), _context.Agents.ToList().First(), 13, 12, 30, 45, 0, 150, DateTime.Now, 2, new Ranks() { RankID = 1, RankName = "Wood 1" });

            object logToUpdate = _context.GameLogs.ToList().Last();

            //Test method call            
            _manager.UpdateEntry(logToUpdate, updatedArgs);

            //Assersion 
            GameLogs lastGameInDb = (GameLogs)logToUpdate;
            Assert.AreEqual(updatedArgs.Kills, lastGameInDb.Kills);
            Assert.AreEqual(updatedArgs.Deaths, lastGameInDb.Deaths);
            Assert.AreEqual(updatedArgs.Assists, lastGameInDb.Assits);
        }
    }
}
