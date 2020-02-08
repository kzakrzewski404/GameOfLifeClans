using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai.Config;


namespace GameOfLifeClans.Ai
{
    public class EntityFactory
    {
        public Entity Create(EntityId entityId, ClanId clanId)
        {
            switch (entityId)
            {
                case EntityId.Headquarter:
                    return new Headquarter(entityId, clanId, AiConfig.HEADQUARTER_HEALTH, AiConfig.HEADQUARTER_DAMAGE, AiConfig.HEADQUARTER_DEFENCE);

                case EntityId.Soldier:
                    return new Soldier(entityId, clanId, AiConfig.SOLDIER_HEALTH, AiConfig.SOLDIER_DAMAGE, AiConfig.SOLDIER_DEFENCE);

                default:
                    throw new System.Exception("Missing entity in EntityFactory");
            }
        }
    }
}
