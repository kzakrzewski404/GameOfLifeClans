using GameOfLifeClans.Ai.Enums;


namespace GameOfLifeClans.Ai.Config
{
    public class EntityConfig
    {
        public int Health { get; private set; }
        public int Damage { get; private set; }
        public int Defence { get; private set; }
        public EntityId Id { get; private set; }


        public EntityConfig(EntityId id, int healht, int damage, int defence)
        {
            Id = id;
            Health = healht;
            Damage = damage;
            Defence = defence;
        }
    }
}
