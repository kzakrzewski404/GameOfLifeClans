using System;

using GameOfLifeClans.Ai.Data;
using GameOfLifeClans.Ai.Entities.Config;
using GameOfLifeClans.Ai.Senses.Vision;
using GameOfLifeClans.Simulation.Clan;


namespace GameOfLifeClans.Ai.Entities
{
    public class Soldier : Entity
    {
        private static Random _rnd = new Random();
        private int _willingnessToAttactk;


        private bool IsWillingToAttack => _rnd.Next(0, 100) <= _willingnessToAttactk;


        public Soldier(IClanInfo myClan, SpawnStats stats, IVisionSense visionSense) : base(myClan, stats, visionSense)
        {
            _willingnessToAttactk = _rnd.Next(Behaviour.SOLDIER_MINIMAL_WILLIGNESS_TO_ATTACK, 100);
        }


        public override StepSummary CalculateStep()
        {
            StepSummary summary = new StepSummary();
            IVisionResult visionResult = _visionSense.GetResult(this);

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
