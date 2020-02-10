using System;
using System.Linq;

using GameOfLifeClans.Map.Data.Enums;


namespace GameOfLifeClans.Map.Data
{
    public class TerrainFactory
    {
        private Terrain[] _terrains;


        public TerrainFactory()
        {
            int count = Enum.GetNames(typeof(TerrainId)).Length;
            _terrains = new Terrain[count];

            InitializeTerrain(TerrainId.Grass, true);
            InitializeTerrain(TerrainId.Sand, true);
            InitializeTerrain(TerrainId.Water, false);
            InitializeTerrain(TerrainId.Mountain, false);

            if (_terrains.Contains(null))
            {
                throw new Exception("_terrains[] in TileTerrainFactory contains NULL, not enough defined terrains");
            }
        }


        public Terrain Create(TerrainId id) => _terrains[(int)id];


        private void InitializeTerrain(TerrainId id, bool isPassable) => _terrains[(int)id] = new Terrain(id, isPassable);
    }
}
