using GameOfLifeClans.Map.Data;


namespace GameOfLifeClans.Map
{
    public class MapContainer
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Tile[,] Tiles;
    }
}
