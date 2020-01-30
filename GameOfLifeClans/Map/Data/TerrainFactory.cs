using System;
using System.Linq;
using GameOfLifeClans.Map.Enums;


namespace GameOfLifeClans.Map.Data
{
    public class TerrainFactory
    {
        private Terrain[] _terrains;


        public Terrain Create(TerrainId id) => _terrains[(int)id];


        public TerrainFactory()
        {
            int count = Enum.GetNames(typeof(TerrainId)).Length;
            _terrains = new Terrain[count];

            _terrains[(int)TerrainId.Grass]     = new Terrain(TerrainId.Grass,      true);
            _terrains[(int)TerrainId.Sand]      = new Terrain(TerrainId.Sand,       true);
            _terrains[(int)TerrainId.Water]     = new Terrain(TerrainId.Water,      false);
            _terrains[(int)TerrainId.Mountain]  = new Terrain(TerrainId.Mountain,   false);

            if (_terrains.Contains(null))
            {
                throw new Exception("_terrains[] in TileTerrainFactory contains NULL, not enough defined terrains");
            }
        }
    }
}
