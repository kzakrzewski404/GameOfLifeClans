using System;

using GameOfLifeClans.Ai.Data;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai.Senses.Vision;
using GameOfLifeClans.Map.Data;
using GameOfLifeClans.Simulation.Clan;


namespace GameOfLifeClans.Ai.Entities
{
    public abstract class Entity : IAttackable, IForceKillable
    {
        protected static IVisionSense _visionSense;
        protected WhenKilledCallback _whenIsKilledCallback;


        public int Health { get; private set; }
        public int Damage { get; private set; }
        public int Defence { get; private set; }
        public EntityId Id { get; private set; }
        public IClanInfo ClanInfo { get; private set; }
        public Tile OccupiedTile { get; private set; }


        public int LocationX => OccupiedTile.LocationX;
        public int LocationY => OccupiedTile.LocationY;


        public delegate void WhenKilledCallback(Entity entity, int killedByMemberOfClanId);


        public Entity(IClanInfo myClan, SpawnStats stats, IVisionSense visionSense)
        {
            ClanInfo = myClan;
            Id = stats.Id;
            Health = stats.Health;
            Damage = stats.Damage;
            Defence = stats.Defence;

            _visionSense = visionSense;
        }


        public abstract StepSummary CalculateStep();

        public void DealDamage(int damage, int attackedByMemberOfClanId) => TakeDamage(damage, attackedByMemberOfClanId);

        public void ForceKill() => TakeDamage(0, forceKill:true);

        public void SetOccupiedTile(Tile tile) => OccupiedTile = tile;

        public void SetWhenIsKilledCallback(WhenKilledCallback callback) => _whenIsKilledCallback = callback;


        protected virtual void OnWhenKilled(int killedByMemberOfClanId) => _whenIsKilledCallback?.Invoke(this, killedByMemberOfClanId);

        protected virtual void AttackEnemy(IAttackable enemy) => enemy.DealDamage((int)(Damage * OccupiedTile.Terrain.DamageMultiplier * ClanInfo.Strength.DamageBonusMultiplier), this.ClanInfo.Id);

        protected virtual void MoveToTile(IOccupiable targetTile, ref StepSummary summary)
        {
            if (targetTile.ClanOwnershipId != this.ClanInfo.Id)
            {
                summary.AddConqueredTerritoryInfo(targetTile.ClanOwnershipId);
            }

            targetTile.MoveHere(this);
        }

        protected virtual void TakeDamage(int damage, int attackedByMemberOfClanId = -1, bool forceKill = false)
        {
            if (forceKill)
            {
                Health = 0;
            }
            else
            {
                damage -= (int)(Defence * OccupiedTile.Terrain.DefenceMultiplier * ClanInfo.Strength.DefenceBonusMultiplier);
                if (damage <= 0)
                {
                    damage = 1;
                }

                Health -= damage;
            }

            if (Health <= 0)
            {
                OccupiedTile.RemoveAiEntity();
                OnWhenKilled(attackedByMemberOfClanId);
            }
        }
    }
}