using GameOfLifeClans.Ai.Data;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai.Senses.Vision;
using GameOfLifeClans.Map.Data;
using GameOfLifeClans.Simulation.Clan;


namespace GameOfLifeClans.Ai.Entities
{
    public class Builder : Entity
    {
        private static EntityFactory _entityFactory = new EntityFactory();


        public Builder(IClanInfo myClan, SpawnStats stats, IVisionSense visionSense) : base(myClan, stats, visionSense)
        {
            // Empty
        }


        public override StepSummary CalculateStep()
        {
            StepSummary summary = new StepSummary();
            IVisionResult visionResult = _visionSense.GetResult(this);

            // Build
            if (visionResult.IsAwayFromClosestHeadquarter)
            {
                TransformBuilderIntoOutpost(visionResult, ref summary);
            }
            else if (visionResult.IsFreeTileFound)
            {
                MoveToTile(visionResult.GetRandomFreeTile(), ref summary);
            }

            return summary;
        }


        private void TransformBuilderIntoOutpost(IVisionResult visionResult, ref StepSummary summary)
        {
            Entity buildedOutpost = _entityFactory.Create(EntityId.Outpost, this.ClanInfo);
            summary.AddSpawnedEntityInfo(buildedOutpost);

            Tile transformTile = OccupiedTile;
            this.ForceKill();
            transformTile.SetAiEntity(buildedOutpost);
        }
    }
}
