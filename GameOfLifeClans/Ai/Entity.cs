using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai.Senses.Vision;
using GameOfLifeClans.Map.Data;


namespace GameOfLifeClans.Ai
{
    public abstract class Entity
    {
        protected static Vision _vision = new Vision();
        protected static ulong _idCounter = 0;
        protected int _maxHealth;
        protected WhenKilledCallback _whenIsKilledCallback;


        public int Health { get; private set; }
        public int Damage { get; private set; }
        public int Defence { get; private set; }
        public ClanId ClanId { get; private set; }
        public EntityId Id { get; private set; }
        public Tile OccupiedTile { get; private set; }


        public int LocationX => OccupiedTile.LocationX;
        public int LocationY => OccupiedTile.LocationY;


        public delegate void WhenKilledCallback(Entity entity);


        public Entity(EntityId id, ClanId clan, int health, int damage, int defence)
        {
            Id = id;
            ClanId = clan;
            Health = health;
            Damage = damage;
            Defence = defence;
            _maxHealth = Health;
        }


        public abstract void CalculateStep();

        public void SetOccupiedTile(Tile tile) => OccupiedTile = tile;

        public void SetWhenIsKilledCallback(WhenKilledCallback callback) => _whenIsKilledCallback = callback;

        public virtual void DealDamage(int damage, bool forceKill = false)
        {
            if (forceKill)
            {
                Health = 0;
            }
            else
            {
                Health -= damage;
            }

            if (Health <= 0)
            {
                OccupiedTile.RemoveAiEntity();
                On_WhenKilled();
            }
        }


        protected virtual void On_WhenKilled() => _whenIsKilledCallback?.Invoke(this);

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
