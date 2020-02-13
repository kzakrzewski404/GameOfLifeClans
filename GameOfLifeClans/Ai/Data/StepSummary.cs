using GameOfLifeClans.Ai.Entities;
using GameOfLifeClans.Map.Config;


namespace GameOfLifeClans.Ai.Data
{
    public struct StepSummary
    {
        public bool HasConqueredTerritory { get; private set; }
        public bool HasSpawnedEntity { get; private set; }
        public int PreviousTileClanOwnerId { get; private set; }
        public Entity SpawnedEntitiy { get; private set; }


        public void AddConqueredTerritoryInfo(int previousClanOwnerId)
        {
            if (previousClanOwnerId != MapConfig.TERRAIN_NEUTRAL_CLAN_OWNERSHIP)
            {
                HasConqueredTerritory = true;
                PreviousTileClanOwnerId = previousClanOwnerId;
            }
        }

        public void AddSpawnedEntityInfo(Entity spawned)
        {
            HasSpawnedEntity = true;
            SpawnedEntitiy = spawned;
        }
    }
}
