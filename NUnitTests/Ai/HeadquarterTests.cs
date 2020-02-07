using NUnit.Framework;

using GameOfLifeClans.Ai;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Map;
using GameOfLifeClans.Map.Enums;

using GameOfLifeClans.UnitTests.TestsTools;


namespace GameOfLifeClans.UnitTests.Ai
{
    public class HeadquarterTests
    {
        private MapContainer _map;
        private MapTestsTools _tools = new MapTestsTools();


        [Test]
        public void CalculateStep_AfterSpawnTresholdTriggers_TotalEntitiesOnMapShouldBe2()
        {
            //Arrange
            _map = new MapContainer();
            _tools.GenerateMap(3, 3, _map, TerrainId.Grass);
            _tools.AddEntity(1, 1, EntityId.Headquarter, ClanId.Blue);
            Entity headquarter = _map.Tiles[1, 1].AiEntity;

            //Act
            for (int i = 0; i <= AiConfig.HEADQUARTER_SPAWN_TRESHOLD; i++)
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
            _map = new MapContainer();
            _tools.GenerateMap(3, 3, _map, TerrainId.Grass);
            _tools.AddEntity(1, 1, EntityId.Headquarter, ClanId.Blue);
            Entity headquarter = _map.Tiles[1, 1].AiEntity;

            //Act
            for (int i = 0; i <= (AiConfig.HEADQUARTER_SPAWN_TRESHOLD * 20); i++)
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
            _map = new MapContainer();
            _tools.GenerateMap(3, 3, _map, TerrainId.Mountain);
            _tools.AddEntityAndChangeTerrain(1, 1, EntityId.Headquarter, ClanId.Blue, TerrainId.Grass);
            _tools.SetTerrain(1, 2, TerrainId.Grass); //only one free tile for spawn
            Entity headquarter = _map.Tiles[1, 1].AiEntity;

            //Act
            for (int i = 0; i <= AiConfig.HEADQUARTER_SPAWN_TRESHOLD; i++)
            {
                headquarter.CalculateStep();
            }
            Entity spawned = _map.Tiles[1, 2].AiEntity;

            //Assert
            Assert.IsTrue(headquarter.Clan == spawned.Clan, 
                $"Headquarter clan: {headquarter.Clan}\nSpawned clan: {spawned.Clan}");
        }

        [Test]
        public void CalculateStep_AfterSpawnTresholdTriggersAndThereIsNoSpaceForSpawning_TotalEntitiesOnMapShouldBe1()
        {
            //Arrange
            _map = new MapContainer();
            _tools.GenerateMap(3, 3, _map, TerrainId.Mountain);
            _tools.AddEntityAndChangeTerrain(1, 1, EntityId.Headquarter, ClanId.Blue, TerrainId.Grass);
            Entity headquarter = _map.Tiles[1, 1].AiEntity;

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
            _map = new MapContainer();
            _tools.GenerateMap(3, 3, _map, TerrainId.Grass);
            _tools.AddEntityAndChangeTerrain(1, 1, EntityId.Headquarter, ClanId.Blue, TerrainId.Grass);
            Entity headquarter = _map.Tiles[1, 1].AiEntity;
            int originalX = headquarter.LocationX;
            int originalY = headquarter.LocationY;

            //Act
            for (int i = 0; i <= AiConfig.HEADQUARTER_SPAWN_TRESHOLD; i++)
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
            _map = new MapContainer();
            _tools.GenerateMap(3, 3, _map, TerrainId.Mountain);
            _tools.AddEntityAndChangeTerrain(1, 1, EntityId.Headquarter, ClanId.Blue, TerrainId.Grass);
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
        public void CalculateStep_OnlyOneAllyIsNearbyAndNoAnyFreeTiles_ShouldAttackEnemy()
        {
            //Arrange
            _map = new MapContainer();
            _tools.GenerateMap(3, 3, _map, TerrainId.Mountain);
            _tools.AddEntityAndChangeTerrain(1, 1, EntityId.Headquarter, ClanId.Blue, TerrainId.Grass);
            _tools.AddEntityAndChangeTerrain(1, 0, EntityId.Soldier, ClanId.Blue, TerrainId.Grass);

            Entity headquarter = _map.Tiles[1, 1].AiEntity;
            Entity ally = _map.Tiles[1, 0].AiEntity;
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
            _map = new MapContainer();
            _tools.GenerateMap(3, 3, _map, TerrainId.Mountain);
            _tools.AddEntityAndChangeTerrain(1, 1, EntityId.Headquarter, ClanId.Blue, TerrainId.Grass);
            _tools.AddEntityAndChangeTerrain(1, 0, EntityId.Soldier, ClanId.Blue, TerrainId.Grass);
            _tools.AddEntityAndChangeTerrain(1, 2, EntityId.Soldier, ClanId.Red, TerrainId.Grass);

            Entity headquarter = _map.Tiles[1, 1].AiEntity;
            Entity enemy = _map.Tiles[1, 2].AiEntity;
            Entity ally = _map.Tiles[1, 0].AiEntity;
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
            _map = new MapContainer();
            _tools.GenerateMap(3, 3, _map, TerrainId.Mountain);
            _tools.AddEntityAndChangeTerrain(1, 1, EntityId.Headquarter, ClanId.Blue, TerrainId.Grass);
            _tools.AddEntityAndChangeTerrain(1, 0, EntityId.Soldier, ClanId.Red, TerrainId.Grass);

            Entity headquarter = _map.Tiles[1, 1].AiEntity;
            Entity enemy = _map.Tiles[1, 0].AiEntity;
            int originalX = headquarter.LocationX;
            int originalY = headquarter.LocationY;

            //Act
            headquarter.CalculateStep();

            //Assert
            Assert.IsTrue(headquarter.LocationX == originalX && headquarter.LocationY == originalY);
        }
    }
}
