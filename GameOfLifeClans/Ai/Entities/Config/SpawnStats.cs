using GameOfLifeClans.Ai.Enums;


namespace GameOfLifeClans.Ai.Entities.Config;
{
    public class SpawnStats
    {
        public int Health { get; private set; }
        public int Damage { get; private set; }
        public int Defence { get; private set; }
        public EntityId Id { get; private set; }


        public SpawnStats(EntityId id, int healht, int damage, int defence)
        {
            Id = id;
            Health = healht;
            Damage = damage;
            Defence = defence;
        }
    }
}
