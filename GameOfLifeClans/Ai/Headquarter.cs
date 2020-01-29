using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai.Senses.Vision;


namespace GameOfLifeClans.Ai
{
    public class Headquarter : Entity
    {
        private int _spawnCounter;
        private static EntityFactory _entityFactory = new EntityFactory();


        public Headquarter(ClanId clanId, int health, int damage) : base(clanId, health, damage)
        {
            _spawnCounter = AiConfig.HEADQUARTER_SPAWN_TRESHOLD;
        }


        public override void SimulateStep()
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
                visionResult.FreeTiles.PickRandom.SetAiEntity(_entityFactory.Create(EntityId.Soldier, Clan));
                _spawnCounter = 0;
            }
        }
    }
}
