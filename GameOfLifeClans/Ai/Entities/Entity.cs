﻿using GameOfLifeClans.Ai.Data;
using GameOfLifeClans.Ai.Entities.Config;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai.Senses.Vision;
using GameOfLifeClans.Map.Data;


namespace GameOfLifeClans.Ai.Entities
{
    public abstract class Entity : IAttackable, IForceKillable
    {
        protected static IVisionSense _visionSense;
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


        public Entity(int clanId, SpawnStats stats, IVisionSense visionSense)
        {
            ClanId = clanId;
            Id = stats.Id;
            Health = stats.Health;
            Damage = stats.Damage;
            Defence = stats.Defence;

            _visionSense = visionSense;
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
                summary.AddConqueredTerritoryInfo(targetTile.ClanOwnershipId);
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