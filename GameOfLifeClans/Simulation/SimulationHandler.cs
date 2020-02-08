using System.Collections.Generic;
using System;

using GameOfLifeClans.Map;
using GameOfLifeClans.Map.Data;
using GameOfLifeClans.Ai;
using GameOfLifeClans.Ai.Enums;


namespace GameOfLifeClans.Simulation
{
    public class SimulationHandler
    {
        public MapContainer Map { get; private set; }
        public bool IsSimulationRunning { get; private set; }

        public int EntitiesOnMap => _entitiesList.Count;

        private int _headquartersOnMap;
        private List<Entity> _entitiesList = new List<Entity>();
        private static EntityFactory _entityFactory = new EntityFactory();
        private static Random _rnd = new Random();


        public SimulationHandler()
        {
            Map = new MapContainer();
        }


        public void GenerateMap(int mapWidth, int mapHeight)
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
            int xRandom, yRandom;
            bool foundError = false;
            do
            {
                foundError = false;

                //Never search tile for headquarter on map border
                xRandom = _rnd.Next(1, Map.Width - 1);
                yRandom = _rnd.Next(1, Map.Height - 1);
                
                for (int x = (xRandom - 1); (x <= (xRandom + 1) && !foundError); x++)
                {
                    for (int y = (yRandom - 1); (y <= (yRandom + 1) && !foundError); y++)
                    {
                        Tile check = Map.Tiles[x, y];
                        if (check.IsOccupied || !check.Terrain.IsPassable)
                        {
                            foundError = true;
                        }
                    }
                }

            } while (foundError);
            return Map.Tiles[xRandom, yRandom];
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
