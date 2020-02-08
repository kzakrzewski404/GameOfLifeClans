using System;
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

        protected override int CalculateTargetTerrainMass() => (int)(_map.Width * _map.Height * RandomWaterMassPercentage);


        protected override int GenerateSeeds()
        {
            int targetSeeds = _rnd.Next(1, (_map.Width / 10)); //1 seed per 25 map width
            int generatedSeeds = 0;

            while (generatedSeeds != targetSeeds)
            {
                int x, y;
                GetRandomXY(out x, out y);
                if (CanBeEdited(x, y))
                {
                    ModifyTileAndCheckNeighbours(_map.Tiles[x, y]);
                    generatedSeeds++;
                }
            }
            return generatedSeeds;
        }
    }
}
