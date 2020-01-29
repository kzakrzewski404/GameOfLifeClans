using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai.Senses;
using GameOfLifeClans.Map.Data;


namespace GameOfLifeClans.Ai
{
    public abstract class Entity
    {
        public int Health { get; private set; }
        public int Damage { get; private set; }
        public ClanId Clan { get; private set; }
        public Tile OccupiedTile { get; private set; }

        protected static Vision _vision = new Vision();
        protected int _maxHealth;


        public void SetOccupiedTile(Tile tile) => OccupiedTile = tile;


        public Entity(ClanId id, int health, int damage)
        {
            Clan = id;
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


        protected virtual void PerformAttackOnRandomEnemy(VisionResult enemies)
        {
            enemies.PickRandom.AiEntity.DealDamage(Damage);
        }

        protected virtual void MoveToRandomFreeTile(VisionResult freeTiles)
        {
            freeTiles.PickRandom.MoveAiEntityHere(this.OccupiedTile);
        }
    }
}
