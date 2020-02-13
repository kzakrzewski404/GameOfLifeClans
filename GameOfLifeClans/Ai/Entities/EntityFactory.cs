using GameOfLifeClans.Ai.Data;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai.Senses.Vision;
using GameOfLifeClans.Simulation.Clan;


namespace GameOfLifeClans.Ai.Entities
{
    public class EntityFactory
    {
        private static IVisionSense _defaultVisionSense = new VisionOfSurrounding();


        public Entity Create(EntityId entityId, IClanInfo memberOfClan)
        {
            switch (entityId)
            {
                case EntityId.Headquarter:
                    return new Headquarter(memberOfClan, SpawnStatsFactory.Create(entityId), _defaultVisionSense);

                case EntityId.Soldier:
                    return new Soldier(memberOfClan, SpawnStatsFactory.Create(entityId), _defaultVisionSense);

                default:
                    throw new System.Exception("Missing entity in EntityFactory");
            }
        }
    }
}
