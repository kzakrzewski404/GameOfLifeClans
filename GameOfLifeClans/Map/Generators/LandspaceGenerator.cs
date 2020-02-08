using System;

using GameOfLifeClans.Map.Data;
using GameOfLifeClans.Map.Data.Enums;
using GameOfLifeClans.Generics;
using GameOfLifeClans.Map.Generators.Data;


namespace GameOfLifeClans.Map.Generators
{
    public abstract class LandspaceGenerator
    {
        protected MapContainer _map;
        protected TerrainId _terrain;
        protected ItemsContainer<Tile> _tilesPool = new ItemsContainer<Tile>();
        protected TileBuffer _buffer;
        protected static TerrainFactory _terrainFactory = new TerrainFactory();
        protected static Random _rnd = new Random();


        public virtual void Generate(MapContainer map, TerrainId terrain)
        {
            _map = map;
            _terrain = terrain;
            _buffer = new TileBuffer(_map);

            int targetMass = CalculateTargetTerrainMass();
            int currentMass = GenerateSeeds();

            while(currentMass < targetMass)
            {
                ModifyTerrain(_buffer.GetRandom());
                currentMass++;
            }
        }


        protected abstract int GenerateSeeds();

        protected abstract int CalculateTargetTerrainMass();


        protected void ModifyTerrain(Tile tile) => tile.SetTerrain(_terrainFactory.Create(_terrain));

        protected void GetRandomXY(out int x, out int y)
        {
            x = _rnd.Next(0, _map.Width);
            y = _rnd.Next(0, _map.Height);
        }
    }
}
