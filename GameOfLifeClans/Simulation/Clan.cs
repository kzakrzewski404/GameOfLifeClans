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
            foreach(Entity entity in _entitiesList)
            {
                entity.CalculateStep();
            }
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
            if(entity.GetType() == typeof(Headquarter))
            {

            }
        }

        private void WhenEntityIsSpawned()
        {

        }
    }
}
