using GameOfLifeClans.Ai.Data;
using GameOfLifeClans.Ai.Entities.Config;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai.Senses.Vision;
using GameOfLifeClans.Map.Data;
using GameOfLifeClans.Simulation.Clan;


namespace GameOfLifeClans.Ai.Entities
{
    public class Builder : Entity
    {
        private static EntityFactory _entityFactory = new EntityFactory();
        private int _minimalDistanceFromHeadquarterRequiredToBuildOutpost;


        public Builder(IClanInfo myClan, SpawnStats stats, IBuilderVisionSense visionSense) : base(myClan, stats, visionSense)
        {
            _minimalDistanceFromHeadquarterRequiredToBuildOutpost = Behaviour.BUILDER_MINIMAL_REQUIRED_DISTANCE_FROM_HEADQUARTER;
        }


        public override StepSummary CalculateStep()
        {
            StepSummary summary = new StepSummary();
            IBuilderVisionResult visionResult = ((IBuilderVisionSense)_visionSense).GetResult(this, _minimalDistanceFromHeadquarterRequiredToBuildOutpost);

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


        private void TransformBuilderIntoOutpost(IBuilderVisionResult visionResult, ref StepSummary summary)
        {
            Entity buildedOutpost = _entityFactory.Create(EntityId.Outpost, this.ClanInfo);
            summary.AddSpawnedEntityInfo(buildedOutpost);

            Tile transformTile = OccupiedTile;
            this.ForceKill();
            transformTile.SetAiEntity(buildedOutpost);
        }
    }
}
