using GameOfLifeClans.Ai.Data;
using GameOfLifeClans.Ai.Entities.Config;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai.Senses.Vision;


namespace GameOfLifeClans.Ai.Entities
{
    public class Headquarter : Entity
    {
        private static EntityFactory _entityFactory = new EntityFactory();
        private int _nextSpawnCounter;


        public Headquarter(int clanId, SpawnStats stats, IVisionSense visionSense) : base(clanId, stats, visionSense)
        {
            // Force entity spawn on first CalculateStep()
            _nextSpawnCounter = Behaviour.HEADQUARTER_SPAWN_TRESHOLD;
        }


        public override StepSummary CalculateStep()
        {
            StepSummary summary = new StepSummary();
            IVisionResult visionResult = _visionSense.GetResult(this);

            // Attack
            if (visionResult.IsEnemyFound)
            {
                AttackEnemy(visionResult.GetRandomEnemy());
            }

            // Spawn
            if (_nextSpawnCounter < Behaviour.HEADQUARTER_SPAWN_TRESHOLD)
            {
                _nextSpawnCounter++;
            }
            else if(visionResult.IsFreeTileFound)
            {
                Entity spawned = _entityFactory.Create(EntityId.Soldier, this.ClanId);
                summary.AddSpawnedEntityInfo(spawned);

                visionResult.GetRandomFreeTile().SetAiEntity(spawned);
                _nextSpawnCounter = 0;
            }

            return summary;
        }
    }
}
