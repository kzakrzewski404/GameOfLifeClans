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

            int entitiesOnMap = 0;
            for (int x = 0; x < _map.Width; x++)
            {
                for (int y = 0; y < _map.Height; y++)
                {
                    if (_map.Tiles[x, y].IsOccupied)
                    {
                        entitiesOnMap++;
                    }
                }
            }

            //Assert
            Assert.IsTrue(entitiesOnMap == 2);
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

            int entitiesOnMap = 0;
            for (int x = 0; x < _map.Width; x++)
            {
                for (int y = 0; y < _map.Height; y++)
                {
                    if (_map.Tiles[x, y].IsOccupied)
                    {
                        entitiesOnMap++;
                    }
                }
            }

            //Assert
            Assert.IsTrue(entitiesOnMap == 9);
        }

        [Test]
        public void CalculateStep_AfterSpawnTresholdTriggers_SpawnedEntityIsOfTheSameClan()
        {
            //Arrange
            _map = new MapContainer();
            _tools.GenerateMap(3, 3, _map, TerrainId.Mountain);
            _tools.SetTerrain(1, 1, TerrainId.Grass);
            _tools.SetTerrain(1, 2, TerrainId.Grass);
            _tools.AddEntity(1, 1, EntityId.Headquarter, ClanId.Blue);
            Entity headquarter = _map.Tiles[1, 1].AiEntity;

            //Act
            for (int i = 0; i <= AiConfig.HEADQUARTER_SPAWN_TRESHOLD; i++)
            {
                headquarter.CalculateStep();
            }

            //Assert
            Assert.IsTrue(_map.Tiles[1, 1].AiEntity.Clan == _map.Tiles[1, 2].AiEntity.Clan);
        }

        [Test]
        public void CalculateStep_AfterSpawnTresholdTriggersAndThereIsNoSpaceForSpawning_TotalEntitiesOnMapShouldBe1()
        {

        }

        [Test]
        public void CalculateStep_NormalCall_HeadquarterDoesntMove()
        {

        }

        [Test]
        public void CalculateStep_OnlyOneEnemyIsNearby_EnemyIsAttacked()
        {

        }

        [Test]
        public void CalculateStep_OnlyOneAllyIsNearby_AllyIsNotAttacked()
        {

        }

        [Test]
        public void CalculateStep_ThereIsOneEnemyAndOneAlly_EnemyIsAttackedAndAllyNot()
        {

        }

        [Test]
        public void CalculateStep_AfterAttackingEnemy_HeadquarterShouldntMove()
        {

        }
    }
}
