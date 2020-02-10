using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GameOfLifeClans.Map.Data;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai;


namespace GameOfLifeClans.Simulation
{
    public class Clan
    {
        private ClanId _clanId;
        private Headquarter _headquarter;
        private List<Entity> _entitiesList = new List<Entity>();
        private bool _isDestroyed;


        public event ClanIsDestroyedEventHandler ClanIsDestroyed;
        public delegate void ClanIsDestroyedEventHandler(ClanId clanId);


        public int EntitiesOnMap => _entitiesList.Count;
        public bool IsAlive => !_isDestroyed;


        public Clan(ClanId clanId, Tile spawnTile)
        {
            _clanId = clanId;
            _entitiesList.Clear();
            SpawnHeadquarter(spawnTile);
            _isDestroyed = false;
        }


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
            _entitiesList.Add(_headquarter);

            tile.SetAiEntity(_headquarter);
        }

        private void WhenEntityIsKilled(Entity entity)
        {
            if(entity.Id == EntityId.Headquarter)
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
