using System.Collections.Generic;

using GameOfLifeClans.Ai;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Map.Data;


namespace GameOfLifeClans.Simulation
{
    public class Clan
    {
        private ClanId _clanId;
        private Headquarter _headquarter;
        private List<Entity> _entitiesList = new List<Entity>();
        private bool _isDestroyed;


        public Clan(ClanId clanId, Tile spawnTile)
        {
            _clanId = clanId;
            _entitiesList.Clear();
            SpawnHeadquarter(spawnTile);
            _isDestroyed = false;
        }


        public delegate void ClanIsDestroyedEventHandler(ClanId clanId);

        public event ClanIsDestroyedEventHandler ClanIsDestroyed;


        public int EntitiesOnMap => _entitiesList.Count;
        public bool IsAlive => !_isDestroyed;


        public void CalculateStep()
        {
            if (IsAlive)
            {
                foreach (Entity entity in _entitiesList)
                {
                    entity.CalculateStep();
                }
            }
        }


        protected void OnClanIsDestroyed()
        {
            ClanIsDestroyed?.Invoke(_clanId);
        }


        private void SpawnHeadquarter(Tile tile)
        {
            EntityFactory factory = new EntityFactory();
            _headquarter = factory.Create(EntityId.Headquarter, _clanId) as Headquarter;

            // Todo: add delegates WhenEntityIsKilled, WhenEntityIsSpawned
            _entitiesList.Add(_headquarter);
            tile.SetAiEntity(_headquarter);
        }

        private void WhenEntityIsKilled(Entity entity)
        {
            if (entity.Id == EntityId.Headquarter)
            {
                _isDestroyed = true;
                OnClanIsDestroyed();
                _entitiesList.Clear();
            }
        }

        private void WhenEntityIsSpawned(Entity entity)
        {
            _entitiesList.Add(entity);
        }
    }
}
