using GameOfLifeClans.Ai.Data;
using GameOfLifeClans.Ai.Entities.Config;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai.Senses.Vision;
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
                //Todo spawn
                /*
                 * Entity spawned = _entityFactory.Create(EntityId.Soldier, this.ClanInfo);
                summary.AddSpawnedEntityInfo(spawned);

                visionResult.GetRandomFreeTile().SetAiEntity(spawned);
                 */
            }
            else if (visionResult.IsFreeTileFound)
            {
                MoveToTile(visionResult.GetRandomFreeTile(), ref summary);
            }

            return summary;
        }
    }
}
