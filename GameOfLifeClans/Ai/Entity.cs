using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai.Senses.Vision;
using GameOfLifeClans.Map.Data;


namespace GameOfLifeClans.Ai
{
    public abstract class Entity
    {
        public ulong Id { get; private set; }
        public int Health { get; private set; }
        public int Damage { get; private set; }
        public ClanId Clan { get; private set; }
        public Tile OccupiedTile { get; private set; }

        public int LocationX => OccupiedTile.LocationX;
        public int LocationY => OccupiedTile.LocationY;

        protected static Vision _vision = new Vision();
        protected static ulong _idCounter = 0;
        protected int _maxHealth;


        public void SetOccupiedTile(Tile tile) => OccupiedTile = tile;


        public Entity(ClanId clan, int health, int damage)
        {
            Id = (_idCounter++);
            Clan = clan;
            Health = health;
            _maxHealth = health;
            Damage = damage;
        }


        public abstract void SimulateStep();

        public virtual void DealDamage(int damage)
        {
            Health -= damage;

            if (Health <= 0)
            {
                OccupiedTile.RemoveAiEntity();
            }
        }


        protected virtual void PerformAttackOnRandomEnemy(Result visionResult)
        {
            visionResult.Enemies.PickRandom.DealDamage(Damage);
        }

        protected virtual void MoveToRandomFreeTile(Result visionResult)
        {
            visionResult.FreeTiles.PickRandom.MoveAiEntityHere(this);
        }
    }
}
