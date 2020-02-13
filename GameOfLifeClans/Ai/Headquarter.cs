using GameOfLifeClans.Ai.Config;
using GameOfLifeClans.Ai.Data;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai.Senses;


namespace GameOfLifeClans.Ai
{
    public class Headquarter : Entity
    {
        private static EntityFactory _entityFactory = new EntityFactory();
        private int _nextSpawnCounter;


        public Headquarter(EntityId id, int clanId, int health, int damage, int defence) 
            : base(id, clanId, health, damage, defence)
        {
            // Force entity spawn on first CalculateStep()
            _nextSpawnCounter = AiConfig.HEADQUARTER_SPAWN_TRESHOLD;
        }


        public override StepSummary CalculateStep()
        {
            StepSummary summary = new StepSummary();
            IReadableVisionResult visionResult = _vision.GetResult(this);

            // Attack
            if (visionResult.IsEnemyFound)
            {
                AttackEnemy(visionResult.GetRandomEnemy());
            }

            // Spawn
            if (_nextSpawnCounter < AiConfig.HEADQUARTER_SPAWN_TRESHOLD)
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
