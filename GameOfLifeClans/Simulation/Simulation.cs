using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GameOfLifeClans.Map;
using GameOfLifeClans.Ai;
using GameOfLifeClans.Ai.Enums;


namespace GameOfLifeClans.Simulation
{
    public class Simulation
    {
        public MapContainer Map { get; private set; }
        public bool IsSimulationRunning { get; private set; }

        public int EntitiesOnMap => entitiesList.Count;

        private List<Entity> entitiesList;
        private static EntityFactory _entityFactory = new EntityFactory();
        private int _headquartersOnMap;

        


        public Simulation()
        {
            Map = new MapContainer();
        }


        public void Generate(int mapWidth, int mapHeight)
        {
            //setting map
            Map.Generate(mapWidth, mapHeight);
            IsSimulationRunning = true;

            //spawning units
            entitiesList.Clear();
            _headquartersOnMap = 0;
            AddHeadquarterToRandomLocation(ClanId.Blue);
            AddHeadquarterToRandomLocation(ClanId.Red);
        }

        public void CalculateStep()
        {
            for (int i = 0; i < entitiesList.Count; i++)
            {
                entitiesList[i].CalculateStep();
            }
        }


        private void AddHeadquarterToRandomLocation(ClanId clan)
        {
            Headquarter headquarter = _entityFactory.Create(EntityId.Headquarter, clan) as Headquarter;
            headquarter.SetWhenKilledCallback(WhenHeadquarterIsDestroyed);
            headquarter.SetWhenKilledForSpawnedEntities(WhenEntityIsKilled);
            headquarter.SetWhenSpawnedEntityCallback(WhenEntityIsSpawned);

            //TODO: search and add to random location;
            entitiesList.Add(headquarter);
            _headquartersOnMap++;
        }

        private void WhenHeadquarterIsDestroyed(Entity destroyedHeadquarter)
        {
            _headquartersOnMap--;
            IsSimulationRunning = _headquartersOnMap > 0;
            WhenEntityIsKilled(destroyedHeadquarter);
        }

        private void WhenEntityIsKilled(Entity killedEntity)
        {
            entitiesList.Remove(killedEntity);
        }

        private void WhenEntityIsSpawned(Entity spawned)
        {
            entitiesList.Add(spawned);
        }
    }
}
