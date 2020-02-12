using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai.Config;
using GameOfLifeClans.Ai.Senses.Vision;


namespace GameOfLifeClans.Ai
{
    public class Headquarter : Entity
    {
        private static EntityFactory _entityFactory = new EntityFactory();
        private int _spawnTimeCounter;
        private WhenEntityIsSpawnedCallback _whenEntityIsSpawnedCallback;


        public delegate void WhenEntityIsSpawnedCallback(Entity spawnedEntity);


        public Headquarter(EntityId id, int clanId, int health, int damage, int defence) 
            : base(id, clanId, health, damage, defence)
        {
            // Force entity spawn on first CalculateStep()
            _spawnTimeCounter = AiConfig.HEADQUARTER_SPAWN_TRESHOLD;
        }


        public void SetWhenEntityIsSpawnedCallback(WhenEntityIsSpawnedCallback callback) => _whenEntityIsSpawnedCallback = callback;

        public override void CalculateStep()
        {
            IReadableResult visionResult = _vision.GetResult(this);

            // Attack
            if (visionResult.IsEnemyFound)
            {
                AttackEnemy(visionResult.GetRandomEnemy());
            }

            // Spawn
            if (_spawnTimeCounter < AiConfig.HEADQUARTER_SPAWN_TRESHOLD)
            {
                _spawnTimeCounter++;
            }
            else if(visionResult.IsFreeTileFound)
            {
                Entity spawned = _entityFactory.Create(EntityId.Soldier, this.ClanId);
                On_WhenEntityIsSpawned(spawned);

                visionResult.GetRandomFreeTile().SetAiEntity(spawned);
                _spawnTimeCounter = 0;
            }
        }


        private void On_WhenEntityIsSpawned(Entity spawned) => _whenEntityIsSpawnedCallback?.Invoke(spawned);
    }
}
