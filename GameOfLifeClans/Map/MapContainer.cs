using GameOfLifeClans.Map.Data;
using GameOfLifeClans.Map.Enums;


namespace GameOfLifeClans.Map
{
    public class MapContainer
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Tile[,] Tiles;

        private TileTerrainFactory _terrainFactory;


        public MapContainer()
        {
            _terrainFactory = new TileTerrainFactory();
        }


        public void Generate(int width, int height)
        {
            Width = width;
            Height = height;
            Tiles = new Tile[width, height];
            FillWithGrass();
        }


        private void FillWithGrass()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Tiles[x, y] = new Tile(x, y, _terrainFactory.Terrain(TerrainId.Grass), this);
                }
            }
        }
    }
}
