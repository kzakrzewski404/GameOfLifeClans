using NUnit.Framework;

using GameOfLifeClans.Ai.Entities;
using GameOfLifeClans.Ai.Entities.Config;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Map;
using GameOfLifeClans.Map.Data.Enums;

using GameOfLifeClans.UnitTests.TestsTools;


namespace GameOfLifeClans.UnitTests.Ai.Entities
{
    public class HeadquarterTests
    {
        private MapContainer _map;
        private MapTestsTools _tools = new MapTestsTools();
        private int _spawnTreshold;


        [OneTimeSetUp]
        public void Initialize()
        {
            _spawnTreshold = Behaviour.HEADQUARTER_SPAWN_TRESHOLD;
        }


        [Test]
        public void CalculateStep_AfterSpawnTresholdTriggers_TotalEntitiesOnMapShouldBe2()
        {
            //Arrange
            _map = _tools.GenerateMap(3, 3, TerrainId.Grass);
            Entity headquarter = _tools.AddEntity(1, 1, EntityId.Headquarter, 0);

            //Act
            for (int i = 0; i <= _spawnTreshold; i++)
            {
                headquarter.CalculateStep();
            }

            //Assert
            Assert.IsTrue(_tools.CountEntitiesOnMap() == 2, 
                $"Found total: {_tools.CountEntitiesOnMap()} on map");
        }

        [Test]
        public void CalculateStep_AfterSpawnTresholdTriggers20Times_TotalEntitiesOnMapShouldBe9()
        {
            //Arrange
            _map = _tools.GenerateMap(3, 3, TerrainId.Grass);
            Entity headquarter = _tools.AddEntity(1, 1, EntityId.Headquarter, 0);

            //Act
            for (int i = 0; i <= (_spawnTreshold * 20); i++)
            {
                headquarter.CalculateStep();
            }

            //Assert
            Assert.IsTrue(_tools.CountEntitiesOnMap() == 9, 
                $"Found total: {_tools.CountEntitiesOnMap()} on map");
        }

        [Test]
        public void CalculateStep_AfterSpawnTresholdTriggers_SpawnedEntityIsOfTheSameClan()
        {
            //Arrange
            _map = _tools.GenerateMap(3, 3, TerrainId.Mountain);
            Entity headquarter = _tools.AddEntityAndChangeTerrain(1, 1, EntityId.Headquarter, 0, TerrainId.Grass);
            _tools.SetTerrain(1, 2, TerrainId.Grass); //only one free tile for spawn

            //Act
            for (int i = 0; i <= _spawnTreshold; i++)
            {
                headquarter.CalculateStep();
            }
            Entity spawned = _map.Tiles[1, 2].AiEntity;

            //Assert
            Assert.IsTrue(headquarter.ClanId == spawned.ClanId, 
                $"Headquarter clan: {headquarter.ClanId}\nSpawned clan: {spawned.ClanId}");
        }

        [Test]
        public void CalculateStep_AfterSpawnTresholdTriggersAndThereIsNoSpaceForSpawning_TotalEntitiesOnMapShouldBe1()
        {
            //Arrange
            _map = _tools.GenerateMap(3, 3, TerrainId.Mountain);
            Entity headquarter = _tools.AddEntityAndChangeTerrain(1, 1, EntityId.Headquarter, 0, TerrainId.Grass);

            //Act
            headquarter.CalculateStep();

            //Assert
            Assert.IsTrue(_tools.CountEntitiesOnMap() == 1,
                $"Found total: {_tools.CountEntitiesOnMap()} on map");
        }

        [Test]
        public void CalculateStep_NormalCall_HeadquarterDoesntMove()
        {
            //Arrange
            _map = _tools.GenerateMap(3, 3, TerrainId.Grass);
            Entity headquarter = _tools.AddEntityAndChangeTerrain(1, 1, EntityId.Headquarter, 0, TerrainId.Grass);
            int originalX = headquarter.LocationX;
            int originalY = headquarter.LocationY;

            //Act
            for (int i = 0; i <= _spawnTreshold; i++)
            {
                headquarter.CalculateStep();
            }

            //Assert
            Assert.IsTrue(headquarter.LocationX == originalX && headquarter.LocationY == originalY, 
                $"originalX: {originalX} was: {headquarter.LocationX}\noriginalY: {originalY} was: {headquarter.LocationY}");
        }

        [Test]
        public void CalculateStep_OnlyOneEnemyIsNearbyAndNoAnyFreeTiles_ShouldAttackEnemy()
        {
            //Arrange
            _map = _tools.GenerateMap(3, 3, TerrainId.Mountain);
            Entity attacker = _tools.AddEntityAndChangeTerrain(1, 1, EntityId.Headquarter, 0, TerrainId.Grass);
            Entity enemy = _tools.AddEntityAndChangeTerrain(1, 0, EntityId.Soldier, 1, TerrainId.Grass);
            int originalEnemyHealth = enemy.Health;

            //Act
            attacker.CalculateStep();

            //Assert
            Assert.IsTrue(enemy.Health < originalEnemyHealth);
        }

        [Test]
        public void CalculateStep_OnlyOneAllyIsNearbyAndNoAnyFreeTiles_ShouldAttackEnemy()
        {
            //Arrange
            _map = _tools.GenerateMap(3, 3, TerrainId.Mountain);
            Entity headquarter = _tools.AddEntityAndChangeTerrain(1, 1, EntityId.Headquarter, 0, TerrainId.Grass);
            Entity ally = _tools.AddEntityAndChangeTerrain(1, 0, EntityId.Soldier, 0, TerrainId.Grass);
            int originalAllyHealth = ally.Health;

            //Act
            headquarter.CalculateStep();

            //Assert
            Assert.IsTrue(ally.Health == originalAllyHealth);
        }

        [Test]
        public void CalculateStep_ThereIsOneEnemyAndOneAlly_EnemyIsAttackedAndAllyNot()
        {
            //Arrange
            _map = _tools.GenerateMap(3, 3, TerrainId.Mountain);
            Entity headquarter = _tools.AddEntityAndChangeTerrain(1, 1, EntityId.Headquarter, 0, TerrainId.Grass);
            Entity enemy = _tools.AddEntityAndChangeTerrain(1, 0, EntityId.Soldier, 1, TerrainId.Grass);
            Entity ally = _tools.AddEntityAndChangeTerrain(1, 2, EntityId.Soldier, 0, TerrainId.Grass);
            int originalAllyHealth = ally.Health;
            int originalEnemyHealth = enemy.Health;

            //Act
            headquarter.CalculateStep();

            //Assert
            Assert.IsTrue(ally.Health == originalAllyHealth && enemy.Health != originalEnemyHealth);
        }

        [Test]
        public void CalculateStep_AfterAttackingEnemy_HeadquarterShouldntMove()
        {
            //Arrange
            _map = _tools.GenerateMap(3, 3, TerrainId.Mountain);
            Entity headquarter = _tools.AddEntityAndChangeTerrain(1, 1, EntityId.Headquarter, 0, TerrainId.Grass);
            Entity enemy = _tools.AddEntityAndChangeTerrain(1, 0, EntityId.Soldier, 1, TerrainId.Grass);
            int originalX = headquarter.LocationX;
            int originalY = headquarter.LocationY;

            //Act
            headquarter.CalculateStep();

            //Assert
            Assert.IsTrue(headquarter.LocationX == originalX && headquarter.LocationY == originalY);
        }

        [Test]
        public void Headquarter_AfterConstructor_OccupiedTileOwnershipShouldBeTheSameAsHeadquarter()
        {
            //Arrange
            _map = _tools.GenerateMap(3, 3, TerrainId.Mountain);
            Entity headquarter = _tools.AddEntityAndChangeTerrain(1, 1, EntityId.Headquarter, 0, TerrainId.Grass);

            //Act

            //Assert
            Assert.IsTrue(headquarter.ClanId == _map.Tiles[1, 1].ClanOwnershipId);
        }
    }
}
