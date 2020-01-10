using GameOfLifeClans.Map.Enums;


namespace GameOfLifeClans.Map.Data
{
    public class TileTerrain
    {
        public TerrainId Id { get; private set; }
        public bool IsPassable { get; private set; }


        public TileTerrain(TerrainId id, bool isPassable)
        {
            Id = id;
            IsPassable = isPassable;
        }
    }
}
