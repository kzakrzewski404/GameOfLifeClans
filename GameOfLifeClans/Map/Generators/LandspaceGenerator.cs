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
            _buffer = new TileBuffer(_map.Width, _map.Height);

            int targetMass = CalculateTargetTerrainMass();
            int currentMass = GenerateSeeds();

            while(currentMass < targetMass)
            {
                ModifyTileAndCheckNeighbours(_tilesPool.PickRandom);
                currentMass++;
            }
        }


        protected abstract int GenerateSeeds();

        protected abstract int CalculateTargetTerrainMass();


        protected bool CanBeEdited(int x, int y) => IsInsideMapBorders(x, y) && !IsMarkedByBuffer(x, y) && IsGrass(x, y);


        protected void GetRandomXY(out int x, out int y)
        {
            x = _rnd.Next(0, _map.Width);
            y = _rnd.Next(0, _map.Height);
        }

        protected void ModifyTileAndCheckNeighbours(Tile tile)
        {
            tile.SetTerrain(_terrainFactory.Create(_terrain));

            CheckTileAndAddToPool(tile.LocationX - 1, tile.LocationY);
            CheckTileAndAddToPool(tile.LocationX + 1, tile.LocationY);
            CheckTileAndAddToPool(tile.LocationX, tile.LocationY - 1);
            CheckTileAndAddToPool(tile.LocationX, tile.LocationY + 1);
        }


        private bool IsInsideMapBorders(int x, int y) => (x >= 0 && x < _map.Width) && (y >= 0 && y < _map.Height);
        private bool IsMarkedByBuffer(int x, int y) => _buffer.IsMarked(x, y);
        private bool IsGrass(int x, int y) => _map.Tiles[x, y].Terrain.Id == TerrainId.Grass;


        private void CheckTileAndAddToPool(int x, int y)
        {
            if (CanBeEdited(x, y))
            {
                _tilesPool.Add(_map.Tiles[x, y]);
                _buffer.Add(x, y);
            }
        }
    }
}
