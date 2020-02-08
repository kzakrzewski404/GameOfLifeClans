using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai.Senses.Vision;
using GameOfLifeClans.Ai.Config;


namespace GameOfLifeClans.Ai
{
    public class Headquarter : Entity
    {
        public delegate void WhenSpawnedEntityEventHandler(Entity spawnedEntity);

        private int _spawnCounter;
        private static EntityFactory _entityFactory = new EntityFactory();
        private WhenSpawnedEntityEventHandler WhenSpawnedCallback;
        private WhenKilledEventHandler WhenKilledForSpawnedEntities;


        public void SetWhenSpawnedEntityCallback(WhenSpawnedEntityEventHandler callback) => WhenSpawnedCallback = callback;
        public void SetWhenSpawnedEntityIsKilledCallback(WhenKilledEventHandler spawnedCallback) => WhenKilledForSpawnedEntities = spawnedCallback;


        public Headquarter(EntityId id, ClanId clanId, int health, int damage, int defence) : base(id, clanId, health, damage, defence)
        {
            _spawnCounter = AiConfig.HEADQUARTER_SPAWN_TRESHOLD;
        }


        public override void CalculateStep()
        {
            Result visionResult = _vision.GetResult(this);

            //Attack
            if (visionResult.Enemies.IsNotEmpty)
            {
                PerformAttackOnRandomEnemy(visionResult);
            }

            //Spawn
            if (_spawnCounter < AiConfig.HEADQUARTER_SPAWN_TRESHOLD)
            {
                _spawnCounter++;
            }
            else if(visionResult.FreeTiles.IsNotEmpty)
            {
                Entity spawned = _entityFactory.Create(EntityId.Soldier, Clan);
                spawned.SetWhenIsKilledCallback(WhenKilledForSpawnedEntities);
                On_WhenSpawnedEntity(spawned);

                visionResult.FreeTiles.PickRandom.SetAiEntity(spawned);
                _spawnCounter = 0;
            }
        }


        protected virtual void On_WhenSpawnedEntity(Entity spawned)
        {
            if (WhenSpawnedCallback != null)
            {
                WhenSpawnedCallback.Invoke(spawned);
            }
        }
    }
}
