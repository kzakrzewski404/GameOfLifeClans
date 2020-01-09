namespace GameOfLifeClans.Map.Data
{
    public class Tile
    {
        public int LocationX { get; private set; }
        public int LocationY { get; private set; }
        public TileTerrain Terrain { get; private set; }
        public Ai.Entity AiEntity { get; private set; }


        public bool IsOccupied => !(AiEntity == null);
        public void SetTerrain(TileTerrain terrain) => Terrain = terrain;


        public Tile(int x, int y, TileTerrain terrain)
        {
            LocationX = x;
            LocationY = y;
            SetTerrain(terrain);
        }
    }
}
