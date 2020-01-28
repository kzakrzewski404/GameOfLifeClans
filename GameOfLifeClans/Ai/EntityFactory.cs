using GameOfLifeClans.Ai.Enums;


namespace GameOfLifeClans.Ai
{
    public class EntityFactory
    {
        public Entity Create(EntityId entityId, ClanId clanId)
        {
            switch (entityId)
            {
                case EntityId.Headquarter:
                    return new Headquarter(clanId, 200, 20);

                case EntityId.Soldier:
                    return new Soldier(clanId, 20, 5);

                default:
                    throw new System.Exception("Missing entity in EntityFactory");
            }
        }
    }
}
