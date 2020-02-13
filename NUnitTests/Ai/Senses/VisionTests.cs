using NUnit.Framework;

using GameOfLifeClans.Ai;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai.Senses;
using GameOfLifeClans.Map;
using GameOfLifeClans.Map.Data.Enums;

using GameOfLifeClans.UnitTests.TestsTools;


namespace GameOfLifeClans.UnitTests.Ai.Senses
{
    public class VisionTests
    {
        private MapContainer _map;
        private Vision _vision = new Vision();
        private MapTestsTools _tools = new MapTestsTools();


        [Test]
        public void GetResult_SurroundedByAllUnoccupiedAndPassableTiles_ResultShouldContainFree8Allies0Enemies0()
        {
            //Arange
            _map = _tools.GenerateMap(3, 3, TerrainId.Grass);
            Entity entity = _tools.AddEntity(1, 1, EntityId.Headquarter, 0);

            //Act
            IReadableVisionResult result = _vision.GetResult(entity);

            //Assert
            Assert.IsTrue((result.NumberOfFreeTiles == 8) && (result.NumberOfAllies == 0) && (result.NumberOfEnemies == 0));
        }

        [Test]
        public void GetResult_SurroundedByAllImpassableTiles_ResultShouldContainFree0Allies0Enemies0()
        {
            //Arange
            _map = _tools.GenerateMap(3, 3, TerrainId.Water);
            Entity entity = _tools.AddEntityAndChangeTerrain(1, 1, EntityId.Headquarter, 0, TerrainId.Grass);

            //Act
            IReadableVisionResult result = _vision.GetResult(entity);

            //Assert
            Assert.IsTrue((result.NumberOfFreeTiles == 0) && (result.NumberOfAllies == 0) && (result.NumberOfEnemies == 0));
        }

        [Test]
        public void GetResult_SurroundedByThreeImpassableTiles_ResultShouldContainFree5Allies0Enemies0()
        {
            //Arange
            _map = _tools.GenerateMap(3, 3, TerrainId.Grass);
            Entity entity = _tools.AddEntity(1, 1, EntityId.Headquarter, 0);

            for (int x = 0; x < 3; x++)
            {
                _tools.SetTerrain(x, 0, TerrainId.Water);
            }

            //Act
            IReadableVisionResult result = _vision.GetResult(entity);

            //Assert
            Assert.IsTrue((result.NumberOfFreeTiles == 5) && (result.NumberOfAllies == 0) && (result.NumberOfEnemies == 0));
        }

        [Test]
        public void GetResult_SurroundedByOneEnemyAndAllPassableTiles_ResultShouldContainFree7Allies0Enemies1()
        {
            //Arange
            _map = _tools.GenerateMap(3, 3, TerrainId.Grass);
            Entity entity = _tools.AddEntity(1, 1, EntityId.Headquarter, 0);
            Entity enemy = _tools.AddEntity(0, 0, EntityId.Headquarter, 1);

            //Act
            IReadableVisionResult result = _vision.GetResult(entity);

            //Assert
            Assert.IsTrue((result.NumberOfFreeTiles == 7) && (result.NumberOfAllies == 0) && (result.NumberOfEnemies == 1));
        }

        [Test]
        public void GetResult_SurroundedByOneEnemyOneAllyAndAllPassableTiles_ResultShouldContainFree6Allies1Enemies1()
        {
            //Arange
            _map = _tools.GenerateMap(3, 3, TerrainId.Grass);
            Entity entity = _tools.AddEntity(1, 1, EntityId.Headquarter, 0);
            Entity ally = _tools.AddEntity(0, 0, EntityId.Headquarter, 0);
            Entity enemy = _tools.AddEntity(0, 1, EntityId.Headquarter, 1);

            //Act
            IReadableVisionResult result = _vision.GetResult(entity);

            //Assert
            Assert.IsTrue((result.NumberOfFreeTiles == 6) && (result.NumberOfAllies == 1) && (result.NumberOfEnemies == 1));
        }

        public void GetResult_SurroundedByThreeAlliesAndImpassableTiles_ResultShouldContainFree0Allies3Enemies0()
        {
            //Arange
            _map = _tools.GenerateMap(3, 3, TerrainId.Water);
            Entity entity = _tools.AddEntityAndChangeTerrain(1, 1, EntityId.Headquarter, 0, TerrainId.Grass);
            Entity ally1 = _tools.AddEntityAndChangeTerrain(0, 0, EntityId.Headquarter, 0, TerrainId.Grass);
            Entity ally2 = _tools.AddEntityAndChangeTerrain(0, 1, EntityId.Headquarter, 0, TerrainId.Grass);
            Entity ally3 = _tools.AddEntityAndChangeTerrain(0, 2, EntityId.Headquarter, 0, TerrainId.Grass);

            //Act
            IReadableVisionResult result = _vision.GetResult(entity);

            //Assert
            Assert.IsTrue((result.NumberOfFreeTiles == 6) && (result.NumberOfAllies == 1) && (result.NumberOfEnemies == 1));
        }

        [Test]
        public void GetResult_InTopLeftCornerSurroundedByOneEnemyOneAlly_ResultShouldContainFree1Allies1Enemies1()
        {
            //Arange
            _map = _tools.GenerateMap(3, 3, TerrainId.Grass);
            Entity entity = _tools.AddEntity(0, 0, EntityId.Headquarter, 0);
            Entity ally = _tools.AddEntity(0, 1, EntityId.Headquarter, 0);
            Entity enemy = _tools.AddEntity(1, 0, EntityId.Headquarter, 1);

            //Act
            IReadableVisionResult result = _vision.GetResult(entity);

            //Assert
            Assert.IsTrue((result.NumberOfFreeTiles == 1) && (result.NumberOfAllies == 1) && (result.NumberOfEnemies == 1));
        }

        [Test]
        public void GetResult_InTopRightCornerSurroundedByOneEnemyOneAlly_ResultShouldContainFree1Allies1Enemies1()
        {
            //Arange
            _map = _tools.GenerateMap(3, 3, TerrainId.Grass);
            Entity entity = _tools.AddEntity(2, 0, EntityId.Headquarter, 0);
            Entity ally = _tools.AddEntity(1, 0, EntityId.Headquarter, 0);
            Entity enemy = _tools.AddEntity(2, 1, EntityId.Headquarter, 1);

            //Act
            IReadableVisionResult result = _vision.GetResult(entity);

            //Assert
            Assert.IsTrue((result.NumberOfFreeTiles == 1) && (result.NumberOfEnemies == 1) && (result.NumberOfAllies == 1));
        }

        [Test]
        public void GetResult_InBottomLeftCornerSurroundedByOneEnemyOneAlly_ResultShouldContainFree1Allies1Enemies1()
        {
            //Arange
            _map = _tools.GenerateMap(3, 3, TerrainId.Grass);
            Entity entity = _tools.AddEntity(0, 2, EntityId.Headquarter, 0);
            Entity ally = _tools.AddEntity(0, 1, EntityId.Headquarter, 0);
            Entity enemy = _tools.AddEntity(1, 2, EntityId.Headquarter, 1);

            //Act
            IReadableVisionResult result = _vision.GetResult(entity);

            //Assert
            Assert.IsTrue((result.NumberOfFreeTiles == 1) && (result.NumberOfEnemies == 1) && (result.NumberOfAllies == 1));
        }

        [Test]
        public void GetResult_InBottomRightCornerSurroundedByOneEnemyOneAlly_ResultShouldContainFree1Allies1Enemies1()
        {
            //Arange
            _map = _tools.GenerateMap(3, 3, TerrainId.Grass);
            Entity entity = _tools.AddEntity(2, 2, EntityId.Headquarter, 0);
            Entity ally = _tools.AddEntity(2, 1, EntityId.Headquarter, 0);
            Entity enemy = _tools.AddEntity(1, 2, EntityId.Headquarter, 1);

            //Act
            IReadableVisionResult result = _vision.GetResult(entity);

            //Assert
            Assert.IsTrue((result.NumberOfFreeTiles == 1) && (result.NumberOfEnemies == 1) && (result.NumberOfAllies == 1));
        }

        [Test]
        public void GetResult_OnLeftBorderSurroundedByOneEnemyOneAlly_ResultShouldContainFree3Allies1Enemies1()
        {
            //Arange
            _map = _tools.GenerateMap(3, 3, TerrainId.Grass);
            Entity entity = _tools.AddEntity(0, 1, EntityId.Headquarter, 0);
            Entity ally = _tools.AddEntity(0, 0, EntityId.Headquarter, 0);
            Entity enemy = _tools.AddEntity(0, 2, EntityId.Headquarter, 1);

            //Act
            IReadableVisionResult result = _vision.GetResult(entity);

            //Assert
            Assert.IsTrue((result.NumberOfFreeTiles == 3) && (result.NumberOfEnemies == 1) && (result.NumberOfAllies == 1));
        }

        [Test]
        public void GetResult_OnBottomBorderSurroundedByOneEnemyOneAlly_ResultShouldContainFree3Allies1Enemies1()
        {
            //Arange
            _map = _tools.GenerateMap(3, 3, TerrainId.Grass);
            Entity entity = _tools.AddEntity(1, 2, EntityId.Headquarter, 0);
            Entity ally = _tools.AddEntity(0, 2, EntityId.Headquarter, 0);
            Entity enemy = _tools.AddEntity(2, 2, EntityId.Headquarter, 1);

            //Act
            IReadableVisionResult result = _vision.GetResult(entity);

            //Assert
            Assert.IsTrue((result.NumberOfFreeTiles == 3) && (result.NumberOfEnemies == 1) && (result.NumberOfAllies == 1));
        }

        [Test]
        public void GetResult_OnRightBorderSurroundedByOneEnemyOneAlly_ResultShouldContainFree3Allies1Enemies1()
        {
            //Arange
            _map = _tools.GenerateMap(3, 3, TerrainId.Grass);
            Entity entity = _tools.AddEntity(2, 1, EntityId.Headquarter, 0);
            Entity ally = _tools.AddEntity(2, 0, EntityId.Headquarter, 0);
            Entity enemy = _tools.AddEntity(2, 2, EntityId.Headquarter, 1);

            //Act
            IReadableVisionResult result = _vision.GetResult(entity);

            //Assert
            Assert.IsTrue((result.NumberOfFreeTiles == 3) && (result.NumberOfEnemies == 1) && (result.NumberOfAllies == 1));
        }

        [Test]
        public void GetResult_OnTopBorderSurroundedByOneEnemyOneAlly_ResultShouldContainFree3Allies1Enemies1()
        {
            //Arange
            _map = _tools.GenerateMap(3, 3, TerrainId.Grass);
            Entity entity = _tools.AddEntity(1, 0, EntityId.Headquarter, 0);
            Entity ally = _tools.AddEntity(0, 0, EntityId.Headquarter, 0);
            Entity enemy = _tools.AddEntity(2, 0, EntityId.Headquarter, 1);

            //Act
            IReadableVisionResult result = _vision.GetResult(entity);

            //Assert
            Assert.IsTrue((result.NumberOfFreeTiles == 3) && (result.NumberOfEnemies == 1) && (result.NumberOfAllies == 1));
        }
    }
}