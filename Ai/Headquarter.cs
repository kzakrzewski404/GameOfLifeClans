using GameOfLifeClans.Ai.Enums;


namespace GameOfLifeClans.Ai
{
    public class Headquarter : Entity
    {
        private int _spawnCounter;
        private const int _spawnTreshold = 15;


        public Headquarter(ClanId clanId, int health, int damage) : base(clanId, health, damage)
        {
            _spawnCounter = _spawnTreshold;
        }


        public override void SimulateStep()
        {
            if (_spawnCounter < _spawnTreshold)
            {
                _spawnCounter++;
            }
            else
            {
                //TODO
                //1. check nearby tiles for empty space
                //  exist? > spawn and reset counter
                // else > skip this frame
            }
        }


        private void SpawnEntity()
        {

        }
    }
}
