using GameOfLifeClans.Ai.Config;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai.Senses.Vision;


namespace GameOfLifeClans.Ai
{
    public class Headquarter : Entity
    {
        private static EntityFactory _entityFactory = new EntityFactory();
        private int _spawnCounter;
        private WhenSpawnedEntityEventHandler _whenSpawnedCallback;
        private WhenKilledEventHandler _whenKilledForSpawnedEntities;


        public Headquarter(EntityId id, ClanId clanId, int health, int damage, int defence)
            : base(id, clanId, health, damage, defence)
        {
            _spawnCounter = AiConfig.HEADQUARTER_SPAWN_TRESHOLD;
        }


        public delegate void WhenSpawnedEntityEventHandler(Entity spawnedEntity);


        public void SetWhenSpawnedEntityCallback(WhenSpawnedEntityEventHandler callback) => _whenSpawnedCallback = callback;
        public void SetWhenSpawnedEntityIsKilledCallback(WhenKilledEventHandler spawnedCallback) => _whenKilledForSpawnedEntities = spawnedCallback;

        public override void CalculateStep()
        {
            Result visionResult = _vision.GetResult(this);

            // Attack
            if (visionResult.Enemies.IsNotEmpty)
            {
                PerformAttackOnRandomEnemy(visionResult);
            }

            // Spawn
            if (_spawnCounter < AiConfig.HEADQUARTER_SPAWN_TRESHOLD)
            {
                _spawnCounter++;
            }
            else if (visionResult.FreeTiles.IsNotEmpty)
            {
                Entity spawned = _entityFactory.Create(EntityId.Soldier, Clan);
                spawned.SetWhenIsKilledCallback(_whenKilledForSpawnedEntities);
                On_WhenSpawnedEntity(spawned);

                visionResult.FreeTiles.PickRandom.SetAiEntity(spawned);
                _spawnCounter = 0;
            }
        }


        protected virtual void On_WhenSpawnedEntity(Entity spawned)
        {
            if (_whenSpawnedCallback != null)
            {
                _whenSpawnedCallback.Invoke(spawned);
            }
        }
    }
}
