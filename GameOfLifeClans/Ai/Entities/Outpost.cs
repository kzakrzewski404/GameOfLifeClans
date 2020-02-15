using GameOfLifeClans.Ai.Data;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai.Senses.Vision;
using GameOfLifeClans.Simulation.Clan;


namespace GameOfLifeClans.Ai.Entities
{
    public class Outpost : Headquarter
    {
        public Outpost(IClanInfo myClan, SpawnStats stats, IVisionSense visionSense, int spawnTreshold) : base(myClan, stats, visionSense, spawnTreshold)
        {
            // Empty
        }


        public override StepSummary CalculateStep()
        {
            StepSummary summary = new StepSummary();
            IVisionResult visionResult = _visionSense.GetResult(this);

            // HealAlly priority over attack
            if (visionResult.IsAllyToHealFound)
            {
                HealAlly(visionResult.GetRandomAllyWhoRequireHealing());
            }
            else if (visionResult.IsEnemyFound)
            {
                AttackEnemy(visionResult.GetRandomEnemy());
            }

            // Spawn
            HandleSpawn(visionResult, ref summary);

            return summary;
        }


        protected override void InitializePossibleSpawns()
        {
            _possibleSpawns.Add(100, EntityId.Soldier);
        }


        private void HealAlly(IHealable ally) => ally.Heal((int)(Damage * 0.25f));
    }
}
