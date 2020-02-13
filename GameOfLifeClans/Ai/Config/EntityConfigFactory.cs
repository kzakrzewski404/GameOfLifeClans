using GameOfLifeClans.Ai.Enums;


namespace GameOfLifeClans.Ai.Config
{
    public static class EntityConfigFactory
    {
        private static EntityConfig[] _configs;


        static EntityConfigFactory()
        {
            InitializeConfig(EntityId.Headquarter, AiConfig.HEADQUARTER_HEALTH, AiConfig.HEADQUARTER_DAMAGE, AiConfig.HEADQUARTER_DEFENCE);
            InitializeConfig(EntityId.Soldier, AiConfig.SOLDIER_HEALTH, AiConfig.SOLDIER_DAMAGE, AiConfig.SOLDIER_DEFENCE);
        }


        public static EntityConfig Create(EntityId id) => _configs[(int)id];


        private static void InitializeConfig(EntityId id, int hp, int dmg, int def) => _configs[(int)id] = new EntityConfig(id, hp, dmg, def);
    }
}
