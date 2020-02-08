namespace GameOfLifeClans.Ai.Config
{
    public class EntityConfig
    {
        public int Health { get; private set; }
        public int Damage { get; private set; }
        public int Defence { get; private set; }


        public EntityConfig(int health, int damage, int defence)
        {
            Health = health;
            Damage = damage;
            Defence = defence;
        }
    }
}
