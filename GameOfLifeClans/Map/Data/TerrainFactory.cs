using System;
using System.Linq;

using GameOfLifeClans.Map.Config;
using GameOfLifeClans.Map.Data.Enums;


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

            InitializeTerrain(TerrainId.Grass, true, TerrainConfig.GRASS_DAMAGE_MULTIPLIER, TerrainConfig.GRASS_DEFENCE_MULTIPLIER);
            InitializeTerrain(TerrainId.Sand, true, TerrainConfig.SAND_DAMAGE_MULTIPLIER, TerrainConfig.SAND_DEFENCE_MULTIPLIER);
            InitializeTerrain(TerrainId.Water, false);
            InitializeTerrain(TerrainId.Mountain, false);

            if (_terrains.Contains(null))
            {
                throw new Exception("_terrains[] in TileTerrainFactory contains NULL, not enough defined terrains");
            }
        }


        private void InitializeTerrain(TerrainId id, bool isPassable, float dmgMultiplier = 1.0f, float defMultiplier = 1.0f)
        {
            _terrains[(int)id] = new Terrain(id, isPassable, dmgMultiplier, defMultiplier);
        }
    }
}
