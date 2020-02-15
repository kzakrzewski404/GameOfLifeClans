using GameOfLifeClans.Ai.Data;
using GameOfLifeClans.Ai.Entities.Config;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai.Senses.Vision;
using GameOfLifeClans.Simulation.Clan;


namespace GameOfLifeClans.Ai.Entities
{
    public class EntityFactory
    {
        private static IVisionSense _defaultVisionSense = new VisionOfSurrounding();
        private static IVisionSense _builderVisionSense = new VisionOfBuilder(Behaviour.BUILDER_HEADQUARTER_CHECK_RANGE);


        public Entity Create(EntityId entityId, IClanInfo memberOfClan)
        {
            switch (entityId)
            {
                case EntityId.Headquarter:
                    return new Headquarter(memberOfClan, SpawnStatsFactory.Create(entityId), _defaultVisionSense, Behaviour.HEADQUARTER_SPAWN_TRESHOLD);

                case EntityId.Outpost:
                    return new Outpost(memberOfClan, SpawnStatsFactory.Create(entityId), _defaultVisionSense, Behaviour.OUTPOST_SPAWN_TRESHOLD);

                case EntityId.Soldier:
                    return new Soldier(memberOfClan, SpawnStatsFactory.Create(entityId), _defaultVisionSense);

                case EntityId.Builder:
                    return new Builder(memberOfClan, SpawnStatsFactory.Create(entityId), _builderVisionSense);

                default:
                    throw new System.Exception("Missing entity in EntityFactory");
            }
        }
    }
}
