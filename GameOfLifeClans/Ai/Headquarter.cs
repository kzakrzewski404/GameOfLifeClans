using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai.Senses;


namespace GameOfLifeClans.Ai
{
    public class Headquarter : Entity
    {
        private int _spawnCounter;
        private const int _spawnTreshold = 15;
        private static EntityFactory _entityFactory = new EntityFactory();


        public Headquarter(ClanId clanId, int health, int damage) : base(clanId, health, damage)
        {
            _spawnCounter = _spawnTreshold;
        }


        public override void SimulateStep()
        {
            //Attack
            VisionResultItems tilesWithEnemies = _vision.GetNearbyEnemies(OccupiedTile);
            if (tilesWithEnemies.IsNotEmpty)
            {
                PerformAttackOnRandomEnemy(tilesWithEnemies);
            }

            //Spawn
            if (_spawnCounter < _spawnTreshold)
            {
                _spawnCounter++;
            }
            else
            {
                VisionResultItems freeTiles = _vision.GetNearbyFreeTiles(OccupiedTile);

                if(freeTiles.IsNotEmpty)
                {
                    freeTiles.PickRandom.SetAiEntity(_entityFactory.Create(EntityId.Soldier, Clan));
                    _spawnCounter = 0;
                }
            }
        }
    }
}
