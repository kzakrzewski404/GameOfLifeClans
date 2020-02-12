using System.Collections.Generic;

using GameOfLifeClans.Ai;
using GameOfLifeClans.Ai.Data;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Map.Data;


namespace GameOfLifeClans.Simulation
{
    public class Clan
    {
        private Headquarter _headquarter;
        private List<Entity> _entitiesList = new List<Entity>();
        private bool _isAlive;


        public int Id { get; private set; }
        public TerritoryControl Territory { get; private set; }


        public int EntitiesOnMap => _entitiesList.Count;


        public event ClanIsDestroyedEventHandler ClanIsDestroyed;
        public event ConqueredOtherClansTerritoryEventHandler ConqueredOtherClansTerritory;
        public delegate void ClanIsDestroyedEventHandler(Clan invoker);
        public delegate void ConqueredOtherClansTerritoryEventHandler(int clanIdThatLostTerritory);


        public Clan(int clanId, Tile spawnTile)
        {
            Territory = new TerritoryControl();
            Id = clanId;
            _isAlive = true;
            _entitiesList.Clear();
            SpawnHeadquarter(spawnTile);
        }


        public void CalculateStep()
        {
            if (_isAlive)
            {
                for (int i = 0; i < _entitiesList.Count; i++)
                {
                    StepSummary summary = _entitiesList[i].CalculateStep();

                    if (summary.HasSpawnedEntity)
                    {
                        AddSpawnedEntityToClan(summary.SpawnedEntitiy);
                    }

                    if (summary.HasConqueredTerrain)
                    {
                        Territory.Gain();
                        OnOtherClansTerritoryIsConquered(summary.ConqueredTerrainPreviousClanOwnerId);
                    }
                }
            }
        }

        
        private void OnClanIsDestroyed() => ClanIsDestroyed?.Invoke(this);

        private void OnOtherClansTerritoryIsConquered(int clanIdThatLostTerritory) => ConqueredOtherClansTerritory?.Invoke(clanIdThatLostTerritory);
        
        private void SpawnHeadquarter(Tile tile)
        {
            EntityFactory factory = new EntityFactory();
            _headquarter = factory.Create(EntityId.Headquarter, Id) as Headquarter;
            _headquarter.SetWhenIsKilledCallback(NotifyWhenEntityIsKilled);

            _entitiesList.Add(_headquarter);
            tile.SetAiEntity(_headquarter);
        }

        private void NotifyWhenEntityIsKilled(Entity killed)
        {
            if (_isAlive)
            {
                killed.OccupiedTile.RemoveAiEntity();
                _entitiesList.Remove(killed);
            }

            if(killed.Id == EntityId.Headquarter)
            {
                _isAlive = false;

                foreach(IForceKillable clanMember in _entitiesList)
                {
                    clanMember.ForceKill();
                }

                _entitiesList.Clear();
                OnClanIsDestroyed();
            }
        }

        private void AddSpawnedEntityToClan(Entity entity)
        {
            _entitiesList.Add(entity);
            entity.SetWhenIsKilledCallback(NotifyWhenEntityIsKilled);
        }
    }
}