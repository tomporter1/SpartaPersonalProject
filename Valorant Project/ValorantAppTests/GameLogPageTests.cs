using BussinessLayer.Interfaces;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using ValorantGUI;

namespace ValorantAppTests
{
    public class GameLogPageTests
    {
        private GameLogPage _gameLogPage;

        [Ignore("Test not complete yet")]
        [Test]
        public void ConstructorTest()
        {
            Mock<IGameLogManager> mockGameManager = new Mock<IGameLogManager>();
            Mock<IModeManager> mockModeManager = new Mock<IModeManager>();
            Mock<IStats> mockStatsManager = new Mock<IStats>();
            Mock<IAgentManager> mockAgentManager = new Mock<IAgentManager>();
            Mock<IMapManager> mockMapManager = new Mock<IMapManager>();
            Mock<IRanksManager> mockRankManager = new Mock<IRanksManager>();
            Mock<IRankAdjustmentManager> mockRankAdjustmentManager = new Mock<IRankAdjustmentManager>();

            mockModeManager.Setup(x => x.GetAllEntries()).Returns(new List<object>());

            _gameLogPage = new GameLogPage(new MainWindow(), mockGameManager.Object, mockAgentManager.Object, mockStatsManager.Object, mockMapManager.Object, mockRankManager.Object, mockModeManager.Object, mockRankAdjustmentManager.Object);

            mockGameManager.Verify(x => x.GetAllEntries(), Times.Once);
        }
    }
}