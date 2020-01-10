using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai.Senses;


namespace GameOfLifeClans.Ai
{
    public abstract class Entity
    {
        public int Health { get; private set; }
        public int Damage { get; private set; }
        public ClanId Id { get; private set; }

        protected static Vision _vision = new Vision();
        protected int _maxHealth;


        public Entity(ClanId id, int health, int damage)
        {
            Id = id;
            Health = health;
            _maxHealth = health;
            Damage = damage;
        }


        public abstract void SimulateStep();
    }
}
