using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai.Config;


namespace GameOfLifeClans.Ai
{
    public class EntityFactory
    {
        private static EntityConfigFactory _configFactory = new EntityConfigFactory();


        public Entity Create(EntityId entityId, ClanId clanId)
        {
            switch (entityId)
            {
                case EntityId.Headquarter:
                    return new Headquarter(entityId, clanId, _configFactory.Create(entityId));

                case EntityId.Soldier:
                    return new Soldier(entityId, clanId, _configFactory.Create(entityId));

                default:
                    throw new System.Exception("Missing entity in EntityFactory");
            }
        }
    }
}
