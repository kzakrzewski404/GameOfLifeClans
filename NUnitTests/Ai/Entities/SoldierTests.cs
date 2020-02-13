using NUnit.Framework;

using GameOfLifeClans.Ai.Entities;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Map;
using GameOfLifeClans.Map.Data.Enums;

using GameOfLifeClans.UnitTests.TestsTools;


namespace GameOfLifeClans.UnitTests.Ai.Entities
{
    public class SoldierTests
    {
        private MapContainer _map;
        private MapTestsTools _tools = new MapTestsTools();

        [Test]
        public void CalculateStep_OnlyOneEnemyIsNearbyAndNoAnyFreeTiles_ShouldAttackEnemy()
        {
            //Arrange
            _map = _tools.GenerateMap(3, 3, TerrainId.Mountain);
            Entity attacker = _tools.AddAllyEntityAndChangeTerrainToGrass(1, 1, EntityId.Soldier);
            Entity enemy = _tools.AddEnemyEntityAndChangeTerrainToGrass(1, 0, EntityId.Soldier);
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
            _map = _tools.GenerateMap(3, 3, TerrainId.Mountain);
            Entity attacker = _tools.AddAllyEntityAndChangeTerrainToGrass(1, 1, EntityId.Soldier);
            Entity enemy = _tools.AddEnemyEntityAndChangeTerrainToGrass(1, 0, EntityId.Soldier);
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
            _map = _tools.GenerateMap(3, 3, TerrainId.Mountain);
            Entity attacker = _tools.AddAllyEntityAndChangeTerrainToGrass(1, 1, EntityId.Soldier);
            Entity ally = _tools.AddAllyEntityAndChangeTerrainToGrass(1, 0, EntityId.Soldier);
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
            _map = _tools.GenerateMap(3, 3, TerrainId.Mountain);
            Entity soldier = _tools.AddAllyEntityAndChangeTerrainToGrass(1, 1, EntityId.Soldier);
            Entity ally = _tools.AddAllyEntityAndChangeTerrainToGrass(1, 0, EntityId.Soldier);
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
            _map = _tools.GenerateMap(3, 3, TerrainId.Water);
            Entity soldier = _tools.AddAllyEntityAndChangeTerrainToGrass(1, 1, EntityId.Soldier);
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
            _map = _tools.GenerateMap(3, 3, TerrainId.Water);
            Entity soldier = _tools.AddAllyEntityAndChangeTerrainToGrass(1, 1, EntityId.Soldier);
            _tools.SetTerrain(1, 0, TerrainId.Grass); //Free tile
            int originalX = soldier.LocationX;
            int originalY = soldier.LocationY;

            //Act
            soldier.CalculateStep();

            //Assert
            Assert.IsTrue((soldier.LocationX == 1) && (soldier.LocationY == 0));
        }
    }
}
