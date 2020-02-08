using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai.Config;
using GameOfLifeClans.Ai.Senses.Vision;
using GameOfLifeClans.Map.Data;


namespace GameOfLifeClans.Ai
{
    public abstract class Entity
    {
        public int Health { get; private set; }
        public int Damage { get; private set; }
        public int Defence { get; private set; }
        public ClanId Clan { get; private set; }
        public EntityId Id { get; private set; }
        public Tile OccupiedTile { get; private set; }

        public int LocationX => OccupiedTile.LocationX;
        public int LocationY => OccupiedTile.LocationY;

        public delegate void WhenKilledEventHandler(Entity entity);

        protected static Vision _vision = new Vision();
        protected static ulong _idCounter = 0;
        protected int _maxHealth;
        protected WhenKilledEventHandler WhenKilledCallback;


        public void SetOccupiedTile(Tile tile) => OccupiedTile = tile;
        public void SetWhenKilledCallback(WhenKilledEventHandler callback) => WhenKilledCallback = callback;


        public Entity(EntityId id, ClanId clan, int health, int damage, int defence)
        {
            Id = id;
            Clan = clan;
            Health = health;
            Damage = damage;
            Defence = defence;
            _maxHealth = Health;
        }


        public abstract void CalculateStep();

        public virtual void DealDamage(int damage)
        {
            Health -= damage;

            if (Health <= 0)
            {
                OccupiedTile.RemoveAiEntity();
                On_WhenKilled();
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


        protected virtual void On_WhenKilled()
        {
            if (WhenKilledCallback != null)
            {
                WhenKilledCallback.Invoke(this);
            }
        }
    }
}
