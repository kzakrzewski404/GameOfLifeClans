using GameOfLifeClans.Map.Data.Enums;


namespace GameOfLifeClans.Map.Data
{
    public class Terrain
    {
        public Terrain(TerrainId id, bool isPassable)
        {
            Id = id;
            IsPassable = isPassable;
        }


        public TerrainId Id { get; private set; }
        public bool IsPassable { get; private set; }
    }
}
