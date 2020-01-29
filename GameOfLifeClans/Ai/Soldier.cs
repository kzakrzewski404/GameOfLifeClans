using System;

using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai.Senses;


namespace GameOfLifeClans.Ai
{
    public class Soldier : Entity
    {
        private static Random _rnd = new Random();
        private int _willingnessToAttact;


        public Soldier(ClanId id, int health, int damage) : base(id, health, damage)
        {
            _willingnessToAttact = _rnd.Next(25, 100);
        }


        public override void SimulateStep()
        {
            //Attack
            bool enemyKilled = false;
            if (_rnd.Next(0, 100) > _willingnessToAttact)
            {
                VisionResultItems TilesWithEnemies = _vision.GetNearbyEnemies(OccupiedTile);
                if (TilesWithEnemies.IsNotEmpty)
                {
                    PerformAttackOnRandomEnemy(TilesWithEnemies);
                    enemyKilled = true;
                }
            }

            //Move
            if (!enemyKilled)
            {
                VisionResultItems freeTiles = _vision.GetNearbyFreeTiles(OccupiedTile);
                if (freeTiles.IsNotEmpty)
                {
                    MoveToRandomFreeTile(freeTiles);
                }
            }
        }
    }
}
