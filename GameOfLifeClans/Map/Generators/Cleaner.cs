using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GameOfLifeClans.Map.Data;
using GameOfLifeClans.Map.Data.Enums;
using GameOfLifeClans.Map.Generators.Data;


namespace GameOfLifeClans.Map.Generators
{
    public class Cleaner
    {
        private MapContainer _map;
        private TerrainId _terrainToFix;
        private TileBuffer _buffer;
        private static TerrainFactory _terrainFactory = new TerrainFactory();
        private static Random _rnd = new Random();


        public Cleaner(MapContainer map, TerrainId terrainToFix)
        {
            _map = map;
            _terrainToFix = terrainToFix;
        }

        public void Clean()
        {

        }
    }
}
