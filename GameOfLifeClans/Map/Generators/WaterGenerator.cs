using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeClans.Map.Generators
{
    public class WaterGenerator : LandspaceGenerator
    {
        private const int MIN_WATER_MASS_PERCENTAGE = 10;
        private const int MAX_WATER_MASS_PERCENTAGE = 30;


        private double RandomWaterMassPercentage => _rnd.Next(MIN_WATER_MASS_PERCENTAGE, MAX_WATER_MASS_PERCENTAGE) * 0.01;

        protected override int CalculateTargetTerrainMass() => (int)(_map.Width * _map.Height * RandomWaterMassPercentage);


        protected override int GenerateSeeds()
        {
            int targetSeeds = _rnd.Next(1, (_map.Width / 10)); //1 seed per 25 map width
            int generatedSeeds = 0;

            while (generatedSeeds != targetSeeds)
            {
                ModifyTerrain(_buffer.GetFromGeneratedSeed);
                generatedSeeds++;
            }
            return generatedSeeds;
        }
    }
}
