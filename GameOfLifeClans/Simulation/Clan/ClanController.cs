using System.Collections.Generic;

using GameOfLifeClans.Ai.Entities;
using GameOfLifeClans.Ai.Data;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Map.Data;


namespace GameOfLifeClans.Simulation.Clan
{
    public class ClanController : IClanInfo
    {
        private Headquarter _headquarter;
        private List<Entity> _entitiesList = new List<Entity>();
        private bool _isAlive;


        public int Id { get; private set; }


        public int EntitiesOnMap => _entitiesList.Count;
        public IClanStrengthController StrengthController { get; private set; }
        public IClanStrength Strength => StrengthController;



        public event ClanIsDestroyedEventHandler ClanIsDestroyed;
        public event ConqueredOtherClansTerritoryEventHandler ConqueredOtherClansTerritory;
        public delegate void ClanIsDestroyedEventHandler(ClanController invoker, ClanIsDestroyedEventArgs args);
        public delegate void ConqueredOtherClansTerritoryEventHandler(int clanIdThatLostTerritory);


        public ClanController(int clanId, Tile spawnTile)
        {
            StrengthController = new ClanStrength();
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

                    if (summary.HasConqueredTerritory)
                    {
                        StrengthController.GainTerritory();
                        OnOtherClansTerritoryIsConquered(summary.PreviousTileClanOwnerId);
                    }
                }
            }
        }

        
        private void OnClanIsDestroyed(ClanIsDestroyedEventArgs args) => ClanIsDestroyed?.Invoke(this, args);

        private void OnOtherClansTerritoryIsConquered(int clanIdThatLostTerritory) => ConqueredOtherClansTerritory?.Invoke(clanIdThatLostTerritory);
        
        private void SpawnHeadquarter(Tile tile)
        {
            EntityFactory factory = new EntityFactory();
            _headquarter = factory.Create(EntityId.Headquarter, this) as Headquarter;
            _headquarter.SetWhenIsKilledCallback(NotifyWhenEntityIsKilled);

            _entitiesList.Add(_headquarter);
            tile.SetAiEntity(_headquarter);
            StrengthController.GainTerritory();
        }

        private void NotifyWhenEntityIsKilled(Entity killed, int killedByMemberOfClanId)
        {
            if (_isAlive)
            {
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
                OnClanIsDestroyed(new ClanIsDestroyedEventArgs(killedByMemberOfClanId));
            }
        }

        private void AddSpawnedEntityToClan(Entity entity)
        {
            _entitiesList.Add(entity);
            entity.SetWhenIsKilledCallback(NotifyWhenEntityIsKilled);
        }
    }
}