using GameOfLifeClans.Map.Data.Enums;


namespace GameOfLifeClans.Map.Data
{
    public class Terrain
    {
        public TerrainId Id { get; private set; }
        public float DamageMultiplier { get; private set; }
        public float DefenceMultiplier { get; private set; }
        public bool IsPassable { get; private set; }


        public Terrain(TerrainId id, bool isPassable, float damageMultiplier = 1.0f, float defenceMultiplier = 1.0f)
        {
            Id = id;
            DamageMultiplier = damageMultiplier;
            DefenceMultiplier = defenceMultiplier;
            IsPassable = isPassable;
        }
    }
}
