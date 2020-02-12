using System.Collections.Generic;

using GameOfLifeClans.Ai;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Map.Data;


namespace GameOfLifeClans.Simulation
{
    public class Clan
    {
        private Headquarter _headquarter;
        private List<Entity> _entitiesList = new List<Entity>();


        public int ClanId { get; private set; }
        public bool IsAlive { get; private set; }
        public TerritoryControl Territory { get; private set; }


        public int EntitiesOnMap => _entitiesList.Count;


        public event ClanIsDestroyedEventHandler ClanIsDestroyed;
        public event OtherClansTerritoryIsConqueredEventHandler OtherClansTerritoryIsConquered;
        public delegate void ClanIsDestroyedEventHandler(Clan invoker);
        public delegate void OtherClansTerritoryIsConqueredEventHandler(int loserClanId);


        public Clan(int clanId, Tile spawnTile)
        {
            Territory = new TerritoryControl();
            ClanId = clanId;
            IsAlive = true;
            _entitiesList.Clear();
            SpawnHeadquarter(spawnTile);
        }


        public void CalculateStep()
        {
            if (IsAlive)
            {
                for (int i = 0; i < _entitiesList.Count; i++)
                {
                    _entitiesList[i].CalculateStep();
                }
            }
        }

        
        private void On_ClanIsDestroyed() => ClanIsDestroyed?.Invoke(this);

        private void On_OtherClansTerritoryIsConquered(int loserId) => OtherClansTerritoryIsConquered?.Invoke(loserId);
        
        private void SpawnHeadquarter(Tile tile)
        {
            EntityFactory factory = new EntityFactory();
            _headquarter = factory.Create(EntityId.Headquarter, ClanId) as Headquarter;
            _headquarter.SetWhenIsKilledCallback(WhenEntityIsKilled);
            _headquarter.SetWhenEntityIsSpawnedCallback(WhenEntityIsSpawned);
            _headquarter.SetWhenConqueredTerritoryCallback(WhenConqueredNewTerritory);

            _entitiesList.Add(_headquarter);
            tile.SetAiEntity(_headquarter);
        }

        private void WhenEntityIsKilled(Entity entity)
        {
            // If headquarter is still alive, just remove entity from list, whole clan removal is below
            if (IsAlive)
            {
                _entitiesList.Remove(entity);
            }

            if(entity.Id == EntityId.Headquarter)
            {
                IsAlive = false;

                foreach(IForceKillable clanMember in _entitiesList)
                {
                    clanMember.ForceKill();
                }

                _entitiesList.Clear();
                On_ClanIsDestroyed();
            }
        }

        private void WhenEntityIsSpawned(Entity entity)
        {
            _entitiesList.Add(entity);
            entity.SetWhenIsKilledCallback(WhenEntityIsKilled);
            entity.SetWhenConqueredTerritoryCallback(WhenConqueredNewTerritory);
        }

        private void WhenConqueredNewTerritory(int clanThatLostTerritory)
        {
            Territory.GainTerritory();
            On_OtherClansTerritoryIsConquered(clanThatLostTerritory);
        }
    }
}
