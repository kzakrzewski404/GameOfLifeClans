using GameOfLifeClans.Map.Config;


namespace GameOfLifeClans.Ai.Data
{
    public struct StepSummary
    {
        public bool HasConqueredTerrain { get; private set; }
        public bool HasSpawnedEntity { get; private set; }
        public int ConqueredTerrainPreviousClanOwnerId { get; private set; }
        public Entity SpawnedEntitiy { get; private set; }


        public void AddConqueredTerrainInfo(int previousClanOwnerId)
        {
            if (previousClanOwnerId != MapConfig.TERRAIN_NEUTRAL_CLAN_OWNERSHIP)
            {
                HasConqueredTerrain = true;
                ConqueredTerrainPreviousClanOwnerId = previousClanOwnerId;
            }
        }

        public void AddSpawnedEntityInfo(Entity spawned)
        {
            HasSpawnedEntity = true;
            SpawnedEntitiy = spawned;
        }
    }
}
