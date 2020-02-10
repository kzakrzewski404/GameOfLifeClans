using System.Collections.Generic;

using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai;
using GameOfLifeClans.Map.Data;


namespace GameOfLifeClans.Simulation
{
    public class Clan
    {
        private Headquarter _headquarter;
        private List<Entity> _entitiesList = new List<Entity>();


        public ClanId Id { get; private set; }
        public bool IsAlive { get; private set; }


        public int EntitiesOnMap => _entitiesList.Count;


        public event ClanIsDestroyedEventHandler ClanIsDestroyed;
        public delegate void ClanIsDestroyedEventHandler(Clan invoker);


        public Clan(ClanId clanId, Tile spawnTile)
        {
            Id = clanId;
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

        
        private void OnClanIsDestroyed() => ClanIsDestroyed?.Invoke(this);
        
        private void SpawnHeadquarter(Tile tile)
        {
            EntityFactory factory = new EntityFactory();
            _headquarter = factory.Create(EntityId.Headquarter, Id) as Headquarter;
            _headquarter.SetWhenIsKilledCallback(WhenEntityIsKilled);
            _headquarter.SetWhenEntityIsSpawnedCallback(WhenEntityIsSpawned);

            _entitiesList.Add(_headquarter);
            tile.SetAiEntity(_headquarter);
        }

        private void WhenEntityIsKilled(Entity entity)
        {
            _entitiesList.Remove(entity);

            if(entity.Id == EntityId.Headquarter)
            {
                IsAlive = false;

                for (int i = 0; i < _entitiesList.Count; i++)
                {
                    // Force kill all remaining entities of this clan
                    _entitiesList[i].DealDamage(0, forceKill:true);
                }
                _entitiesList.Clear();
                OnClanIsDestroyed();
            }
        }

        private void WhenEntityIsSpawned(Entity entity)
        {
            _entitiesList.Add(entity);
            entity.SetWhenIsKilledCallback(WhenEntityIsKilled);
        }
    }
}
