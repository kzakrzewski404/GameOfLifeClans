using GameOfLifeClans.Ai;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Map;
using GameOfLifeClans.Map.Enums;
using GameOfLifeClans.Map.Data;


namespace GameOfLifeClans.UnitTests.TestsTools
{
    public class MapTestsTools
    {
        private MapContainer _map;
        private TerrainFactory _terrainFactory = new TerrainFactory();
        private EntityFactory _entityFactory = new EntityFactory();


        public void SetTerrain(int x, int y, TerrainId terrain) => _map.Tiles[x, y].SetTerrain(_terrainFactory.Create(terrain));
        public void AddEntity(int x, int y, EntityId entity, ClanId clan) => _map.Tiles[x, y].SetAiEntity(_entityFactory.Create(entity, clan));


        public void GenerateMap(int width, int height, MapContainer mapContainer, TerrainId defaultFill)
        {
            _map = mapContainer;
            _map.Generate(width, height);
            FillTerrain(defaultFill);
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
    }
}
