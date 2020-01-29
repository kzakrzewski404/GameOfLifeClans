using System;
using System.Linq;
using GameOfLifeClans.Map.Enums;


namespace GameOfLifeClans.Map.Data
{
    public class TileTerrainFactory
    {
        private TileTerrain[] _terrains;


        public TileTerrain Terrain(TerrainId id) => _terrains[(int)id];


        public TileTerrainFactory()
        {
            int count = Enum.GetNames(typeof(TerrainId)).Length;
            _terrains = new TileTerrain[count];

            _terrains[(int)TerrainId.Grass]     = new TileTerrain(TerrainId.Grass,      true);
            _terrains[(int)TerrainId.Sand]    = new TileTerrain(TerrainId.Sand,     true);
            _terrains[(int)TerrainId.Water]     = new TileTerrain(TerrainId.Water,      false);
            _terrains[(int)TerrainId.Mountain]  = new TileTerrain(TerrainId.Mountain,   false);

            if (_terrains.Contains(null))
            {
                throw new Exception("_terrains[] in TileTerrainFactory contains NULL, not enough defined terrains");
            }
        }
    }
}
