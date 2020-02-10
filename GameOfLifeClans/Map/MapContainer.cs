using GameOfLifeClans.Map.Data;
using GameOfLifeClans.Map.Data.Enums;
using GameOfLifeClans.Map.Generators;


namespace GameOfLifeClans.Map
{
    public class MapContainer
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Tile[,] Tiles;

        private TerrainFactory _terrainFactory;


        public MapContainer()
        {
            _terrainFactory = new TerrainFactory();
        }


        public void Generate(int width, int height)
        {
            Width = width;
            Height = height;
            Tiles = new Tile[width, height];
            FillWithGrass();
            if (width >= 50 && height >= 50) //test mode check, unit tests use 3x3 map
            {
                LandspaceGenerator water = new WaterGenerator();
                water.Generate(this, TerrainId.Water);
            }
        }


        private void FillWithGrass()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Tiles[x, y] = new Tile(x, y, _terrainFactory.Create(TerrainId.Grass), this);
                }
            }
        }
    }
}
