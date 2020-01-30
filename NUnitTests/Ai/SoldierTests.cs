using NUnit.Framework;

using GameOfLifeClans.Ai;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Map;
using GameOfLifeClans.Map.Enums;

using GameOfLifeClans.UnitTests.TestsTools;


namespace GameOfLifeClans.UnitTests.Ai
{
    public class SoldierTests
    {
        private MapContainer _map;
        private MapTestsTools _tools = new MapTestsTools();


        [Test]
        public void When_ThereIsOneEnemyNearbyAndNoFreeTiles_Expect_EnemyIsAttacked()
        {
            //Arrange
            _map = new MapContainer();
            _tools.GenerateMap(3, 3, _map, TerrainId.Mountain);

            _tools.SetTerrain(1, 1, TerrainId.Grass);
            _tools.AddEntity(1, 1, EntityId.Soldier, ClanId.Blue);

            _tools.SetTerrain(1, 0, TerrainId.Grass);
            _tools.AddEntity(1, 0, EntityId.Soldier, ClanId.Red);

            Entity attacker = _map.Tiles[1, 1].AiEntity;
            Entity enemy = _map.Tiles[1, 0].AiEntity;
            int originalEnemyHealth = enemy.Health;

            //Act
            attacker.CalculateStep();

            //Assert
            Assert.IsTrue(enemy.Health < originalEnemyHealth);
        }

        [Test]
        public void When_ThereIsOnlyOneAllyNearbyAndNoFreeTiles_Expect_AllyIsNotAttacked()
        {
            //Arrange
            _map = new MapContainer();
            _tools.GenerateMap(3, 3, _map, TerrainId.Mountain);

            _tools.SetTerrain(1, 1, TerrainId.Grass);
            _tools.AddEntity(1, 1, EntityId.Soldier, ClanId.Blue);

            _tools.SetTerrain(1, 0, TerrainId.Grass);
            _tools.AddEntity(1, 0, EntityId.Soldier, ClanId.Blue);

            Entity attacker = _map.Tiles[1, 1].AiEntity;
            Entity ally = _map.Tiles[1, 0].AiEntity;
            int originalAllyHealth = ally.Health;

            //Act
            attacker.CalculateStep();

            //Assert
            Assert.IsTrue(ally.Health == originalAllyHealth);
        }

        [Test]
        public void When_ThereIsNoFreeTilesNearby_Expect_SoldierDoesntMove()
        {
            //Arrange
            _map = new MapContainer();
            _tools.GenerateMap(3, 3, _map, TerrainId.Water);

            _tools.SetTerrain(1, 1, TerrainId.Grass);
            _tools.AddEntity(1, 1, EntityId.Soldier, ClanId.Blue);

            Entity soldier = _map.Tiles[1, 1].AiEntity;
            int originalX = soldier.LocationX;
            int originalY = soldier.LocationY;

            //Act
            soldier.CalculateStep();

            //Assert
            Assert.IsTrue((originalX == soldier.LocationX) && (originalY == soldier.LocationY));
        }

        [Test]
        public void When_ThereIsOnlyOneFreeTileNearby_Expect_SoldierMoveToThatTile()
        {
            //Arrange
            _map = new MapContainer();
            _tools.GenerateMap(3, 3, _map, TerrainId.Water);

            _tools.SetTerrain(1, 1, TerrainId.Grass);
            _tools.AddEntity(1, 1, EntityId.Soldier, ClanId.Blue);

            _tools.SetTerrain(1, 0, TerrainId.Grass); //Free tile

            Entity soldier = _map.Tiles[1, 1].AiEntity;

            //Act
            soldier.CalculateStep();

            //Assert
            Assert.IsTrue((soldier.LocationX == 1) && (soldier.LocationY == 0));
        }
    }
}
