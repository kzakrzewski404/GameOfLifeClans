using NUnit.Framework;

using GameOfLifeClans.Ai.Senses;
using GameOfLifeClans.Map.Data;


namespace GameOfLifeClans.UnitTests.Ai.Senses
{
    public class VisionResultTests
    {
        [Test]
        public void IsEnemyFound_NothingInList_ReturnsFalse()
        {
            //Arrange
            VisionResult result = new VisionResult();

            //Act

            //Assert
            Assert.IsTrue(!result.IsEnemyFound);
        }

        [Test]
        public void IsNotEmpty_OneFreeTileInList_Expect_IsNotEmpty_True()
        {
            //Arrange
            VisionResult result = new VisionResult();

            //Act
            result.FreeTiles.Add(new Tile(0, 0, null, null));

            //Assert
            Assert.IsTrue(result.FreeTiles.IsNotEmpty);
        }

        [Test]
        public void When_ThreeTilesInList_Expect_IsNotEmpty_True()
        {
            //Arrange
            VisionResult result = new VisionResult();

            //Act
            result.FreeTiles.Add(new Tile(0, 0, null, null));
            result.FreeTiles.Add(new Tile(0, 0, null, null));
            result.FreeTiles.Add(new Tile(0, 0, null, null));

            //Assert
            Assert.IsTrue(result.FreeTiles.IsNotEmpty);
        }

        [Test]
        public void When_OneTileInList_Expect_PickRandom_ReturnsSameTile()
        {
            //Arrange
            VisionResult result = new VisionResult();

            //Act
            Tile t = new Tile(0, 0, null, null);
            result.FreeTiles.Add(t);

            //Assert
            Assert.AreSame(t, result.FreeTiles.PickRandom);
        }

        public void When_Add_Expect_ListCountEquals1()
        {
            //Arrange
            VisionResult result = new VisionResult();

            //Act
            result.FreeTiles.Add(new Tile(0, 0, null, null));

            //Assert
            Assert.IsTrue(result.FreeTiles.Count == 1);
        }

        [Test]
        public void When_ThreeTilesInList_Expect_ListCountEquals3()
        {
            //Arrange
            VisionResult result = new VisionResult();

            //Act
            result.FreeTiles.Add(new Tile(0, 0, null, null));
            result.FreeTiles.Add(new Tile(0, 0, null, null));
            result.FreeTiles.Add(new Tile(0, 0, null, null));

            //Assert
            Assert.IsTrue(result.FreeTiles.Count == 3);
        }
    }
}
