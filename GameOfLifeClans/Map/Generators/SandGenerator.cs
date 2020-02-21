using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeClans.Map.Generators
{
    public class SandGenerator : LandspaceGenerator
    {
        private const int MIN_SAND_MASS_PERCENTAGE = 10;
        private const int MAX_SAND_MASS_PERCENTAGE = 30;
        private const int MAX_SAND_SEEDS_NUMBER = 6;


        private double RandomSandMassPercentage => _rnd.Next(MIN_SAND_MASS_PERCENTAGE, MAX_SAND_MASS_PERCENTAGE) * 0.01;

        protected override int CalculateTargetTerrainMass() => (int)(_map.Width * _map.Height * RandomSandMassPercentage);


        protected override int GenerateSeeds()
        {
            int targetSeeds = _rnd.Next(2, MAX_SAND_SEEDS_NUMBER);
            int generatedSeeds = 0;

            while (generatedSeeds != targetSeeds)
            {
                ModifyTerrain(_buffer.GetSeedFromRandomMapBorder);
                generatedSeeds++;
            }
            return generatedSeeds;
        }
    }
}
