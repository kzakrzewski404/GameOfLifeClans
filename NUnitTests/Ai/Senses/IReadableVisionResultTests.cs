using NUnit.Framework;

using GameOfLifeClans.Ai.Senses;
using GameOfLifeClans.Map.Data;

using GameOfLifeClans.UnitTests.TestsTools;


namespace GameOfLifeClans.UnitTests.Ai.Senses
{
    public class IReadableVisionResultTests
    {
        [Test]
        public void IsEnemyFound_NothingInResult_ReturnsFalse()
        {
            //Arrange
            IReadableVisionResult result = new VisionResultTestTools(0, 0, 0).GetAsReadableResult();

            //Act

            //Assert
            Assert.IsTrue(!result.IsEnemyFound);
        }

        [Test]
        public void IsEnemyFound_OneEnemyInResult_ReturnsTrue()
        {
            //Arrange
            IReadableVisionResult result = new VisionResultTestTools(0, 0, 1).GetAsReadableResult();

            //Act

            //Assert
            Assert.IsTrue(result.IsEnemyFound);
        }


        [Test]
        public void IsAllyFound_NothingInResult_ReturnsFalse()
        {
            //Arrange
            IReadableVisionResult result = new VisionResultTestTools(0, 0, 0).GetAsReadableResult();

            //Act

            //Assert
            Assert.IsTrue(!result.IsAllyFound);
        }

        [Test]
        public void IsAllyFound_OneAllyInResult_ReturnsTrue()
        {
            //Arrange
            IReadableVisionResult result = new VisionResultTestTools(0, 1, 0).GetAsReadableResult();

            //Act

            //Assert
            Assert.IsTrue(result.IsAllyFound);
        }


        [Test]
        public void IsFreeTileFound_NothingInResult_ReturnsFalse()
        {
            //Arrange
            IReadableVisionResult result = new VisionResultTestTools(0, 0, 0).GetAsReadableResult();

            //Act

            //Assert
            Assert.IsTrue(!result.IsEnemyFound);
        }

        [Test]
        public void IsFreeTileFound_OneTileInResult_ReturnsTrue()
        {
            //Arrange
            IReadableVisionResult result = new VisionResultTestTools(1, 0, 0).GetAsReadableResult();

            //Act

            //Assert
            Assert.IsTrue(result.IsFreeTileFound);
        }


        [Test]
        public void NumberOfEnemies_OneEnemyInResult_Returns1()
        {
            //Arrange

            //Act

            //Assert
            Assert.Warn("TODO");
        }

        [Test]
        public void NumberOfEnemies_NoEnemiesInResult_Returns0()
        {
            //Arrange

            //Act

            //Assert
            Assert.Warn("TODO");
        }


        [Test]
        public void NumberOfAllies_OneAllyInResult_Returns1()
        {
            //Arrange

            //Act

            //Assert
            Assert.Warn("TODO");
        }

        [Test]
        public void NumberOfAllies_NoAlliesInResult_Returns0()
        {
            //Arrange

            //Act

            //Assert
            Assert.Warn("TODO");
        }


        [Test]
        public void NumberOfFreeTiles_OneFreeTileInResult_Returns1()
        {
            //Arrange

            //Act

            //Assert
            Assert.Warn("TODO");
        }

        [Test]
        public void NumberOfFreeTiles_NoFreeTilesInResult_Returns0()
        {
            //Arrange

            //Act

            //Assert
            Assert.Warn("TODO");
        }


        [Test]
        public void GetRandomEnemy_OneEnemyInResult_ReturnTheSameEnemy()
        {
            //Arrange

            //Act

            //Assert
            Assert.Warn("TODO");
        }

        [Test]
        public void GetRandomAlly_OneEnemyInResult_ReturnTheSameEnemy()
        {
            //Arrange

            //Act

            //Assert
            Assert.Warn("TODO");
        }

        [Test]
        public void GetRandomFreeTile_OneTileInResult_ReturnTheSameEnemy()
        {
            //Arrange

            //Act

            //Assert
            Assert.Warn("TODO");
        }
    }
}
