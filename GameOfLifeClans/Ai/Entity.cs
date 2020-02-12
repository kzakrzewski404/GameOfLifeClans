using GameOfLifeClans.Ai.Data;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai.Senses.Vision;
using GameOfLifeClans.Map.Data;


namespace GameOfLifeClans.Ai
{
    public abstract class Entity : IAttackable, IForceKillable
    {
        protected static Vision _vision = new Vision();
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
        }


        public abstract StepSummary CalculateStep();

        public void DealDamage(int damage) => TakeDamage(damage);

        public void ForceKill() => TakeDamage(0, forceKill:true);

        public void SetOccupiedTile(Tile tile) => OccupiedTile = tile;

        public void SetWhenIsKilledCallback(WhenKilledCallback callback) => _whenIsKilledCallback = callback;


        protected virtual void OnWhenKilled() => _whenIsKilledCallback?.Invoke(this);

        protected virtual void AttackEnemy(IAttackable enemy) => enemy.DealDamage(Damage);

        protected virtual void MoveToTile(IOccupiable targetTile, ref StepSummary summary)
        {
            if (targetTile.ClanOwnershipId != this.ClanId)
            {
                summary.AddConqueredTerrainInfo(targetTile.ClanOwnershipId);
            }

            targetTile.MoveHere(this);
        }

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
                OnWhenKilled();
            }
        }
    }
}