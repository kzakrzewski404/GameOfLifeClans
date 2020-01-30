using NUnit.Framework;

using GameOfLifeClans.Ai;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Map;
using GameOfLifeClans.Map.Enums;
using GameOfLifeClans.Map.Data;


namespace GameOfLifeClans.UnitTests.Ai
{
    public class HeadquarterTests
    {
        private EntityFactory _factory = new EntityFactory();
        private TerrainFactory _terrainFactory = new TerrainFactory();
        private MapContainer _map;

        
        private void GenerateMap(int sizeX, int sizeY)
        {
            _map = new MapContainer();
            _map.Generate(sizeX, sizeY);
        }

        private void FillMapWith(TerrainId id)
        {
            for (int x = 0; x < _map.Width; x++)
            {
                for (int y = 0; y < _map.Height; y++)
                {
                    _map.Tiles[x, y].SetTerrain(_terrainFactory.Create(id));
                }
            }
        }

        private void SetTerrainInto(TerrainId id, int x, int y) => _map.Tiles[x, y].SetTerrain(_terrainFactory.Create(id));
        private void AddEntityIntoMap(int x, int y, Entity entity) => _map.Tiles[x, y].SetAiEntity(entity);


        [Test]
        public void When_SpawnTresholdTriggers_Expect_Total2EntitiesOnMap()
        {
            //Arrange
            Entity headquarter = _factory.Create(EntityId.Headquarter, ClanId.Blue);

            GenerateMap(3, 3);
            FillMapWith(TerrainId.Grass);
            AddEntityIntoMap(1, 1, headquarter);

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
        public void When_SpawnTresholdTriggers20Times_Expect_Total9EntitiesOnMap()
        {
            //Arrange
            Entity headquarter = _factory.Create(EntityId.Headquarter, ClanId.Blue);

            GenerateMap(3, 3);
            FillMapWith(TerrainId.Grass);
            AddEntityIntoMap(1, 1, headquarter);

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
        public void When_SpawnTresholdTriggers_Expect_SpawnedEntityIsTheSameClan()
        {
            //Arrange
            Entity headquarter = _factory.Create(EntityId.Headquarter, ClanId.Blue);

            GenerateMap(3, 3);
            FillMapWith(TerrainId.Mountain);
            SetTerrainInto(TerrainId.Grass, 1, 1);
            SetTerrainInto(TerrainId.Grass, 1, 2);
            AddEntityIntoMap(1, 1, headquarter);

            //Act
            for (int i = 0; i <= AiConfig.HEADQUARTER_SPAWN_TRESHOLD; i++)
            {
                headquarter.CalculateStep();
            }

            //Assert
            Assert.IsTrue(_map.Tiles[1, 1].AiEntity.Clan == _map.Tiles[1, 2].AiEntity.Clan);
        }

        [Test]
        public void When_SpawnTresholdTriggers_Expect_SpawnedEntityHasHigherId()
        {
            //Arrange
            Entity headquarter = _factory.Create(EntityId.Headquarter, ClanId.Blue);

            GenerateMap(3, 3);
            FillMapWith(TerrainId.Mountain);
            SetTerrainInto(TerrainId.Grass, 1, 1);
            SetTerrainInto(TerrainId.Grass, 1, 2);
            AddEntityIntoMap(1, 1, headquarter);

            //Act
            for (int i = 0; i <= AiConfig.HEADQUARTER_SPAWN_TRESHOLD; i++)
            {
                headquarter.CalculateStep();
            }

            //Assert
            Assert.IsTrue(_map.Tiles[1, 1].AiEntity.Id < _map.Tiles[1, 2].AiEntity.Id);
        }
    }
}
