﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeClans.Map.Generators
{
    public class WaterGenerator : LandspaceGenerator
    {
        private const int MIN_WATER_MASS_PERCENTAGE = 1;
        private const int MAX_WATER_MASS_PERCENTAGE = 20;


        private double RandomWaterMassPercentage => _rnd.Next(MIN_WATER_MASS_PERCENTAGE, MAX_WATER_MASS_PERCENTAGE) * 0.01;


        protected override void GenerateSeeds()
        {
            _targetModifiedTilesCounter = (int)(_map.Width * _map.Height * RandomWaterMassPercentage);

            int targetSeeds = _rnd.Next(1, (_map.Width / 10)); //1 seed per 25 map width
            int addedSeeds = 0;

            while (addedSeeds != targetSeeds)
            {
                int x, y;
                GetRandomXY(out x, out y);
                if (IsAvailableToModify(x, y))
                {
                    ModifyTile(_map.Tiles[x, y]);
                    addedSeeds++;
                }
            }
        }
    }
}
