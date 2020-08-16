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
            Mock<IBasicManager> mockGameManager = new Mock<IBasicManager>();
            Mock<IBasicManager> mockModeManager = new Mock<IBasicManager>();

            mockModeManager.Setup(x => x.GetAllEntries()).Returns(new List<object>());

            _gameLogPage = new GameLogPage(new MainWindow(), mockGameManager.Object, mockModeManager.Object);

            mockGameManager.Verify(x => x.GetAllEntries(), Times.Once);
        }
    }
}