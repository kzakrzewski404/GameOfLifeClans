using System;

using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai.Senses.Vision;


namespace GameOfLifeClans.Ai
{
    public class Soldier : Entity
    {
        private static Random _rnd = new Random();
        private int _willingnessToAttactk;


        public Soldier(ClanId id, int health, int damage) : base(id, health, damage)
        {
            _willingnessToAttactk = _rnd.Next(AiConfig.SOLDIER_MINIMAL_WILLIGNESS_TO_ATTACK, 100);
        }


        public override void SimulateStep()
        {
            Result visionResult = _vision.GetResult(this);

            //Attack
            bool isEnemyAttacked = false;
            if (IsWillToAttack && visionResult.Enemies.IsNotEmpty)
            {
                PerformAttackOnRandomEnemy(visionResult);
                isEnemyAttacked = true;
            }

            //Move
            if (!isEnemyAttacked && visionResult.FreeTiles.IsNotEmpty)
            {
                MoveToRandomFreeTile(visionResult);
            }
        }


        private bool IsWillToAttack => _rnd.Next(0, 100) <= _willingnessToAttactk;
    }
}
