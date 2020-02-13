using GameOfLifeClans.Ai.Config;
using GameOfLifeClans.Ai.Enums;


namespace GameOfLifeClans.Ai.Entities.Config;
{
    public static class SpawnStatsFactory
    {
        private static SpawnStats[] _configs;


        static SpawnStatsFactory()
        {
            InitializeConfig(EntityId.Headquarter, AiConfig.HEADQUARTER_HEALTH, AiConfig.HEADQUARTER_DAMAGE, AiConfig.HEADQUARTER_DEFENCE);
            InitializeConfig(EntityId.Soldier, AiConfig.SOLDIER_HEALTH, AiConfig.SOLDIER_DAMAGE, AiConfig.SOLDIER_DEFENCE);
        }


        public static SpawnStats Create(EntityId id) => _configs[(int)id];


        private static void InitializeConfig(EntityId id, int hp, int dmg, int def) => _configs[(int)id] = new SpawnStats(id, hp, dmg, def);
    }
}
