using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai.Senses.Vision;
using GameOfLifeClans.Map.Data;


namespace GameOfLifeClans.Ai
{
    public abstract class Entity : IAttackable, IForceKillable
    {
        protected static Vision _vision = new Vision();
        protected static ulong _idCounter = 0;
        protected int _maxHealth;
        protected WhenKilledCallback _whenIsKilledCallback;


        public int Health { get; private set; }
        public int Damage { get; private set; }
        public int Defence { get; private set; }
        public int ClanId { get; private set; }
        public EntityId Id { get; private set; }
        public Tile OccupiedTile { get; private set; }


        public int LocationX => OccupiedTile.LocationX;
        public int LocationY => OccupiedTile.LocationY;


        public delegate void WhenKilledCallback(Entity entity);


        public Entity(EntityId id, int clanId, int health, int damage, int defence)
        {
            Id = id;
            ClanId = clanId;
            Health = health;
            Damage = damage;
            Defence = defence;
            _maxHealth = Health;
        }


        public abstract void CalculateStep();

        public void DealDamage(int damage) => TakeDamage(damage);

        public void ForceKill() => TakeDamage(0, forceKill:true);

        protected virtual void TakeDamage(int damage, bool forceKill = false)
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



        public void SetOccupiedTile(Tile tile) => OccupiedTile = tile;

        public void SetWhenIsKilledCallback(WhenKilledCallback callback) => _whenIsKilledCallback = callback;


        protected virtual void On_WhenKilled() => _whenIsKilledCallback?.Invoke(this);

        protected virtual void AttackEnemy(IAttackable enemy) => enemy.DealDamage(Damage);

        protected virtual void MoveToTile(Tile tile) => tile.MoveAiEntityHere(this);
    }
}