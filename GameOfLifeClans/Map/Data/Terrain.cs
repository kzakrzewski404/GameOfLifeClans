using GameOfLifeClans.Map.Enums;


namespace GameOfLifeClans.Map.Data
{
    public class Terrain
    {
        public TerrainId Id { get; private set; }
        public bool IsPassable { get; private set; }


        public Terrain(TerrainId id, bool isPassable)
        {
            Id = id;
            IsPassable = isPassable;
        }
    }
}
