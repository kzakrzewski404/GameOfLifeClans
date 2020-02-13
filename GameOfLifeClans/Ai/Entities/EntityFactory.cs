using GameOfLifeClans.Ai.Data;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai.Senses.Vision;


namespace GameOfLifeClans.Ai.Entities
{
    public class EntityFactory
    {
        private static IVisionSense _defaultVisionSense = new VisionOfSurrounding();


        public Entity Create(EntityId entityId, int clanId)
        {
            switch (entityId)
            {
                case EntityId.Headquarter:
                    return new Headquarter(clanId, SpawnStatsFactory.Create(entityId), _defaultVisionSense);

                case EntityId.Soldier:
                    return new Soldier(clanId, SpawnStatsFactory.Create(entityId), _defaultVisionSense);

                default:
                    throw new System.Exception("Missing entity in EntityFactory");
            }
        }
    }
}
