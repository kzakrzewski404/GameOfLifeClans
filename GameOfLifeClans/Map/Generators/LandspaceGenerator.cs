using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GameOfLifeClans.Map;


namespace GameOfLifeClans.Map.Generators
{
    public abstract class LandspaceGenerator
    {
        private static Random _rnd = new Random();
        private bool[] _markedTerrainBuffor;

        public abstract void Generate(MapContainer map);


    }
}
