using GameOfLifeClans.Ai.Entities.Config;
using GameOfLifeClans.Ai.Enums;


namespace GameOfLifeClans.Ai.Data
{
    public static class SpawnStatsFactory
    {
        private static SpawnStats[] _configs;
        private static int _numberOfInitializedConfigs = 0;


        static SpawnStatsFactory()
        {
            _configs = new SpawnStats[Stats.NUMBER_OF_ENTITIES];
            InitializeConfig(EntityId.Headquarter, Stats.HEADQUARTER_HEALTH, Stats.HEADQUARTER_DAMAGE, Stats.HEADQUARTER_DEFENCE);
            InitializeConfig(EntityId.Soldier, Stats.SOLDIER_HEALTH, Stats.SOLDIER_DAMAGE, Stats.SOLDIER_DEFENCE);
            InitializeConfig(EntityId.Builder, Stats.BUILDER_HEALTH, Stats.BUILDER_DAMAGE, Stats.BUILDER_DEFENCE);
            InitializeConfig(EntityId.Outpost, Stats.OUTPOST_HEALTH, Stats.OUTPOST_DAMAGE, Stats.OUTPOST_DEFENCE);

            if (_numberOfInitializedConfigs != Stats.NUMBER_OF_ENTITIES)
            {
                throw new System.NotImplementedException();
            }
        }


        public static SpawnStats Create(EntityId id) => _configs[(int)id];


        private static void InitializeConfig(EntityId id, int hp, int dmg, int def)
        {
            _configs[(int)id] = new SpawnStats(id, hp, dmg, def);
            _numberOfInitializedConfigs++;
        }
    }
}
