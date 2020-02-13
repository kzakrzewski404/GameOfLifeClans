using System;

using GameOfLifeClans.Ai.Config;
using GameOfLifeClans.Ai.Data;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai.Senses;


namespace GameOfLifeClans.Ai
{
    public class Soldier : Entity
    {
        private static Random _rnd = new Random();
        private int _willingnessToAttactk;


        private bool IsWillingToAttack => _rnd.Next(0, 100) <= _willingnessToAttactk;


        public Soldier(EntityId id, int clan, int health, int damage, int defence) : base(id, clan, health, damage, defence)
        {
            _willingnessToAttactk = _rnd.Next(AiConfig.SOLDIER_MINIMAL_WILLIGNESS_TO_ATTACK, 100);
        }


        public override StepSummary CalculateStep()
        {
            StepSummary summary = new StepSummary();
            IReadableVisionResult visionResult = _vision.GetResult(this);

            // Attack
            bool isEnemyAttacked = false;
            if (visionResult.IsEnemyFound && (IsWillingToAttack || !visionResult.IsFreeTileFound))
            {
                AttackEnemy(visionResult.GetRandomEnemy());
                isEnemyAttacked = true;
            }

            // Move
            if (!isEnemyAttacked && visionResult.IsFreeTileFound)
            {
                MoveToTile(visionResult.GetRandomFreeTile(), ref summary);
            }

            return summary;
        }
    }
}
