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
        public void CalculateStep_OnlyOneEnemyIsNearbyAndNoAnyFreeTiles_ShouldAttackEnemy()
        {
            //Arrange
            _map = new MapContainer();
            _tools.GenerateMap(3, 3, _map, TerrainId.Mountain);
            _tools.AddEntityAndChangeTerrain(1, 1, EntityId.Soldier, ClanId.Blue, TerrainId.Grass);
            _tools.AddEntityAndChangeTerrain(1, 0, EntityId.Soldier, ClanId.Red, TerrainId.Grass);

            Entity attacker = _map.Tiles[1, 1].AiEntity;
            Entity enemy = _map.Tiles[1, 0].AiEntity;
            int originalEnemyHealth = enemy.Health;

            //Act
            attacker.CalculateStep();

            //Assert
            Assert.IsTrue(enemy.Health < originalEnemyHealth);
        }

        [Test]
        public void CalculateStep_AfterEnemyIsAttacked_SoldierShouldntMove()
        {
            //Arrange
            _map = new MapContainer();
            _tools.GenerateMap(3, 3, _map, TerrainId.Mountain);
            _tools.AddEntityAndChangeTerrain(1, 1, EntityId.Soldier, ClanId.Blue, TerrainId.Grass);
            _tools.AddEntityAndChangeTerrain(1, 0, EntityId.Soldier, ClanId.Red, TerrainId.Grass);

            Entity attacker = _map.Tiles[1, 1].AiEntity;
            int originalX = attacker.LocationX;
            int originalY = attacker.LocationY;

            //Act
            attacker.CalculateStep();

            //Assert
            Assert.IsTrue(attacker.LocationX == originalX && attacker.LocationY == originalY);
        }

        [Test]
        public void CalculateStep_OnlyOneAllyIsNearbyAndNoAnyFreeTiles_AllyIsNotAttacked()
        {
            //Arrange
            _map = new MapContainer();
            _tools.GenerateMap(3, 3, _map, TerrainId.Mountain);
            _tools.AddEntityAndChangeTerrain(1, 1, EntityId.Soldier, ClanId.Blue, TerrainId.Grass);
            _tools.AddEntityAndChangeTerrain(1, 0, EntityId.Soldier, ClanId.Blue, TerrainId.Grass);

            Entity attacker = _map.Tiles[1, 1].AiEntity;
            Entity ally = _map.Tiles[1, 0].AiEntity;
            int originalAllyHealth = ally.Health;

            //Act
            attacker.CalculateStep();

            //Assert
            Assert.IsTrue(ally.Health == originalAllyHealth);
        }

        [Test]
        public void CalculateStep_OnlyOneAllyIsNearbyAndNoAnyFreeTiles_SoldierShouldtMove()
        {
            //Arrange
            _map = new MapContainer();
            _tools.GenerateMap(3, 3, _map, TerrainId.Mountain);
            _tools.AddEntityAndChangeTerrain(1, 1, EntityId.Soldier, ClanId.Blue, TerrainId.Grass);
            _tools.AddEntityAndChangeTerrain(1, 0, EntityId.Soldier, ClanId.Blue, TerrainId.Grass);

            Entity soldier = _map.Tiles[1, 1].AiEntity;
            int originalX = soldier.LocationX;
            int originalY = soldier.LocationY;

            //Act
            soldier.CalculateStep();

            //Assert
            Assert.IsTrue(soldier.LocationX == originalX && soldier.LocationY == originalY);
        }

        [Test]
        public void CalculateStep_NoAnyFreeTilesNearby_SoldierStaysInTheSameTile()
        {
            //Arrange
            _map = new MapContainer();
            _tools.GenerateMap(3, 3, _map, TerrainId.Water);
            _tools.AddEntityAndChangeTerrain(1, 1, EntityId.Soldier, ClanId.Blue, TerrainId.Grass);

            Entity soldier = _map.Tiles[1, 1].AiEntity;
            int originalX = soldier.LocationX;
            int originalY = soldier.LocationY;

            //Act
            soldier.CalculateStep();

            //Assert
            Assert.IsTrue((originalX == soldier.LocationX) && (originalY == soldier.LocationY));
        }

        [Test]
        public void CalculateStep_OnlyOneFreeTileNearby_Expect_SoldierMoveToThatTile()
        {
            //Arrange
            _map = new MapContainer();
            _tools.GenerateMap(3, 3, _map, TerrainId.Water);
            _tools.AddEntityAndChangeTerrain(1, 1, EntityId.Soldier, ClanId.Blue, TerrainId.Grass);
            _tools.SetTerrain(1, 0, TerrainId.Grass); //Free tile

            Entity soldier = _map.Tiles[1, 1].AiEntity;
            int originalX = soldier.LocationX;
            int originalY = soldier.LocationY;

            //Act
            soldier.CalculateStep();

            //Assert
            Assert.IsTrue((soldier.LocationX == 1) && (soldier.LocationY == 0));
        }
    }
}
