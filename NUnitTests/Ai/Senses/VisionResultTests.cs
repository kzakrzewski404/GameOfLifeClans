using NUnit.Framework;

using GameOfLifeClans.Ai.Senses;
using GameOfLifeClans.Map.Data;


namespace GameOfLifeClans.UnitTests.Ai.Senses
{
    public class VisionResultTests
    {
        [Test]
        public void When_NoTilesInList_Expect_IsNotEmpty_False()
        {
            //Arrange
            VisionResultItems vr = new VisionResultItems();

            //Act

            //Assert
            Assert.IsFalse(vr.IsNotEmpty);
        }

        [Test]
        public void When_OneTileInList_Expect_IsNotEmpty_True()
        {
            //Arrange
            VisionResultItems vr = new VisionResultItems();

            //Act
            vr.Add(new Tile(0, 0, null, null));

            //Assert
            Assert.IsTrue(vr.IsNotEmpty);
        }

        [Test]
        public void When_ThreeTileInList_Expect_IsNotEmpty_True()
        {
            //Arrange
            VisionResultItems vr = new VisionResultItems();

            //Act
            vr.Add(new Tile(0, 0, null, null));
            vr.Add(new Tile(0, 0, null, null));
            vr.Add(new Tile(0, 0, null, null));

            //Assert
            Assert.IsTrue(vr.IsNotEmpty);
        }

        [Test]
        public void When_OneTileInList_Expect_PickRandom_ReturnsSameTile()
        {
            //Arrange
            VisionResultItems vr = new VisionResultItems();

            //Act
            Tile t = new Tile(0, 0, null, null);
            vr.Add(t);

            //Assert
            Assert.AreSame(t, vr.PickRandom);
        }

        public void When_Add_Expect_ListWithOneElement()
        {
            //Arrange
            VisionResultItems vr = new VisionResultItems();

            //Act
            vr.Add(new Tile(0, 0, null, null));

            //Assert
            Assert.IsTrue(vr.Results.Count == 1);
        }
    }
}
