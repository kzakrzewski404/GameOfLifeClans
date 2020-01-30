using System.Collections.Generic;
using System;

using GameOfLifeClans.Map;
using GameOfLifeClans.Map.Data;
using GameOfLifeClans.Ai;
using GameOfLifeClans.Ai.Enums;


namespace GameOfLifeClans.Simulation
{
    public class Simulation
    {
        public MapContainer Map { get; private set; }
        public bool IsSimulationRunning { get; private set; }

        public int EntitiesOnMap => _entitiesList.Count;

        private int _headquartersOnMap;
        private List<Entity> _entitiesList = new List<Entity>();
        private static EntityFactory _entityFactory = new EntityFactory();
        private static Random _rnd = new Random();


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
            _entitiesList.Clear();
            _headquartersOnMap = 0;
            AddHeadquarterToRandomLocation(ClanId.Blue);
            AddHeadquarterToRandomLocation(ClanId.Red);
        }

        public void CalculateStep(int numberOfSteps = 1)
        {
            for (int s = 0; s < numberOfSteps; s++)
            {
                if (IsSimulationRunning)
                {
                    for (int i = 0; i < _entitiesList.Count; i++)
                    {
                        _entitiesList[i].CalculateStep();
                    }
                }
            }
        }


        private void AddHeadquarterToRandomLocation(ClanId clan)
        {
            Headquarter headquarter = _entityFactory.Create(EntityId.Headquarter, clan) as Headquarter;
            headquarter.SetWhenKilledCallback(WhenHeadquarterIsDestroyed);
            headquarter.SetWhenKilledForSpawnedEntities(WhenEntityIsKilled);
            headquarter.SetWhenSpawnedEntityCallback(WhenEntityIsSpawned);

            FindTileForHeadquarter().SetAiEntity(headquarter);
            _entitiesList.Add(headquarter);
            _headquartersOnMap++;
        }

        private Tile FindTileForHeadquarter()
        {
            int x, y, entitiesNearby;
            bool isFreeTileFound = false;
            do
            {
                //Never search tile for headquarter on map border
                x = _rnd.Next(1, Map.Width - 1);
                y = _rnd.Next(1, Map.Height - 1);

                entitiesNearby = 0;
                for (int mapX = (x - 1); mapX <= (x + 1); mapX++)
                {
                    for (int mapY = (y - 1); mapY < (y + 1); mapY++)
                    {
                        if (Map.Tiles[mapX, mapY].IsOccupied)
                        {
                            entitiesNearby++;
                        }
                    }
                }
                isFreeTileFound = entitiesNearby == 0;
            } while (!isFreeTileFound);
            return Map.Tiles[x, y];
        }

        private void WhenHeadquarterIsDestroyed(Entity destroyedHeadquarter)
        {
            _headquartersOnMap--;
            IsSimulationRunning = _headquartersOnMap > 0;
            WhenEntityIsKilled(destroyedHeadquarter);
        }

        private void WhenEntityIsKilled(Entity killedEntity)
        {
            _entitiesList.Remove(killedEntity);
        }

        private void WhenEntityIsSpawned(Entity spawned)
        {
            _entitiesList.Add(spawned);
        }
    }
}
