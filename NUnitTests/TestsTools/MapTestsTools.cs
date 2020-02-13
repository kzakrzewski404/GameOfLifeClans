using GameOfLifeClans.Ai.Entities;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Map;
using GameOfLifeClans.Map.Data.Enums;
using GameOfLifeClans.Map.Data;
using GameOfLifeClans.Simulation.Clan;


namespace GameOfLifeClans.UnitTests.TestsTools
{
    public class MapTestsTools
    {
        private MapContainer _map;
        private TerrainFactory _terrainFactory = new TerrainFactory();
        private EntityFactory _entityFactory = new EntityFactory();
        private IClanInfo _fakedAllyClanInfo = new ClanInfoFaker(0);
        private IClanInfo _fakedEnemyClanInfo = new ClanInfoFaker(1);


        public void SetTerrain(int x, int y, TerrainId terrain) => _map.Tiles[x, y].SetTerrain(_terrainFactory.Create(terrain));

        public Entity AddAllyEntity(int x, int y, EntityId entity) => AddEntity(x, y, entity, _fakedAllyClanInfo);

        public Entity AddEnemyEntity(int x, int y, EntityId entity) => AddEntity(x, y, entity, _fakedEnemyClanInfo);

        public Entity AddAllyEntityAndChangeTerrainToGrass(int x, int y, EntityId entity) => AddEntityAndChangeTerrain(x, y, entity, _fakedAllyClanInfo);

        public Entity AddEnemyEntityAndChangeTerrainToGrass(int x, int y, EntityId entity) => AddEntityAndChangeTerrain(x, y, entity, _fakedEnemyClanInfo);


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


        private Entity AddEntity(int x, int y, EntityId entity, IClanInfo clanInfo)
        {
            _map.Tiles[x, y].SetAiEntity(_entityFactory.Create(entity, clanInfo));
            return _map.Tiles[x, y].AiEntity;
        }

        private Entity AddEntityAndChangeTerrain(int x, int y, EntityId entity, IClanInfo clanInfo)
        {
            SetTerrain(x, y, TerrainId.Grass);
            return AddEntity(x, y, entity, clanInfo);
        }
    }
}
