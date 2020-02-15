using GameOfLifeClans.Ai.Data;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai.Senses.Vision;
using GameOfLifeClans.Simulation.Clan;


namespace GameOfLifeClans.Ai.Entities
{
    public class Outpost : Entity
    {
        private static EntityFactory _entityFactory = new EntityFactory();
        private int _nextSpawnCounter;
        private int _spawnTreshold;


        public Outpost(IClanInfo myClan, SpawnStats stats, IVisionSense visionSense, int spawnTreshold) : base(myClan, stats, visionSense)
        {
            _spawnTreshold = spawnTreshold;
        }


        public override StepSummary CalculateStep()
        {
            StepSummary summary = new StepSummary();
            IVisionResult visionResult = _visionSense.GetResult(this);

            // HealAlly priority over attack
            if (visionResult.IsAllyFound)
            {
                HealAlly(visionResult.GetRandomAlly());
            }
            else if (visionResult.IsEnemyFound)
            {
                AttackEnemy(visionResult.GetRandomEnemy());
            }


            // Spawn
            if (_nextSpawnCounter < _spawnTreshold)
            {
                _nextSpawnCounter++;
            }
            else if (visionResult.IsFreeTileFound)
            {
                Entity spawned = _entityFactory.Create(EntityId.Soldier, this.ClanInfo);
                summary.AddSpawnedEntityInfo(spawned);

                visionResult.GetRandomFreeTile().SetAiEntity(spawned);
                _nextSpawnCounter = 0;
            }

            return summary;
        }


        private void HealAlly(IHealable ally) => ally.Heal((int)(Damage * 0.25f));
    }
}
