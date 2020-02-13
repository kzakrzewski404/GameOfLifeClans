using System;
using System.Linq;
using System.Collections.Generic;

using GameOfLifeClans.Map;
using GameOfLifeClans.Map.Config;
using GameOfLifeClans.Map.Data;


namespace GameOfLifeClans.Simulation
{
    public class SimulationHandler
    {
        private static Random _rnd = new Random();
        private List<Clan> _clansList = new List<Clan>();


        public MapContainer Map { get; private set; }


        public bool IsSimulationRunning => _clansList.Count > 1;


        public SimulationHandler()
        {
            Map = new MapContainer();
        }


        public void GenerateMap(int mapWidth, int mapHeight, int numberOfClans = 2)
        {
            Map.Generate(mapWidth, mapHeight);

            _clansList.Clear();
            for (int i = 0; i < numberOfClans; i++)
            {
                Tile headquarterTile = FindTileForHeadquarter();
                Clan clan = new Clan(i, headquarterTile);
                clan.ClanIsDestroyed += NotifyWhenClanIsDestroyed;
                clan.ConqueredOtherClansTerritory += NotifyWhenOtherClansTerritoryIsConquered;
                _clansList.Add(clan);
            }
        }

        public void CalculateStep(int numberOfSteps = 1)
        {
            for (int s = 0; s < numberOfSteps; s++)
            {
                if (IsSimulationRunning)
                {
                    for (int i = 0; i < _clansList.Count; i++)
                    {
                        _clansList[i].CalculateStep();
                    }
                }
            }
        }


        private void NotifyWhenClanIsDestroyed(Clan destroyed) => _clansList.Remove(destroyed);

        private void NotifyWhenOtherClansTerritoryIsConquered(int clanIdThatLostHisTerritory)
        {
            if (clanIdThatLostHisTerritory != MapConfig.TERRAIN_NEUTRAL_CLAN_OWNERSHIP)
            {
                Clan loser = _clansList.FirstOrDefault(x => x.Id == clanIdThatLostHisTerritory);
                loser?.StrengthController.LoseTerritory();
            }
        }

        private Tile FindTileForHeadquarter()
        {
            int xRandom, yRandom;
            bool foundError = false;
            do
            {
                foundError = false;

                // Never search tile for headquarter on map border
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
    }
}
