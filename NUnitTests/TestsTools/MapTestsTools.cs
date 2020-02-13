using GameOfLifeClans.Ai.Entities;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Map;
using GameOfLifeClans.Map.Data.Enums;
using GameOfLifeClans.Map.Data;


namespace GameOfLifeClans.UnitTests.TestsTools
{
    public class MapTestsTools
    {
        private MapContainer _map;
        private TerrainFactory _terrainFactory = new TerrainFactory();
        private EntityFactory _entityFactory = new EntityFactory();


        public void SetTerrain(int x, int y, TerrainId terrain) => _map.Tiles[x, y].SetTerrain(_terrainFactory.Create(terrain));

        public Entity AddEntity(int x, int y, EntityId entity, int clanId)
        {
            _map.Tiles[x, y].SetAiEntity(_entityFactory.Create(entity, clanId));
            return _map.Tiles[x, y].AiEntity;
        }

        public Entity AddEntityAndChangeTerrain(int x, int y, EntityId entity, int clanId, TerrainId terrain = TerrainId.Grass)
        {
            SetTerrain(x, y, terrain);
            return AddEntity(x, y, entity, clanId);
        }


        public MapContainer GenerateMap(int width, int height, TerrainId defaultFill)
        {
            _map = new MapContainer();
            _map.Generate(width, height);
            FillTerrain(defaultFill);
            return _map;
        }

        public void FillTerrain(TerrainId fill)
        {
            for (int x = 0; x < _map.Width; x++)
            {
                for (int y = 0; y < _map.Height; y++)
                {
                    SetTerrain(x, y, fill);
                }
            }
        }

        public int CountEntitiesOnMap()
        {
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
            return entitiesOnMap;
        }
    }
}
