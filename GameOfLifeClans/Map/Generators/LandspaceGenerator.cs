using System;

using GameOfLifeClans.Map.Data;
using GameOfLifeClans.Map.Data.Enums;
using GameOfLifeClans.Generics;


namespace GameOfLifeClans.Map.Generators
{
    public abstract class LandspaceGenerator
    {
        protected bool[,] _markedTerrainBuffer;
        protected ItemsContainer<Tile> _availableTilesToModify = new ItemsContainer<Tile>();
        protected int _modifiedTilesCounter = 0;
        protected int _targetModifiedTilesCounter = 0;
        protected MapContainer _map;
        protected TerrainId _terrainToGenerate;
        protected static TerrainFactory _terrainFactory = new TerrainFactory();
        protected static Random _rnd = new Random();


        protected bool IsAvailableToModify(int x, int y) => IsInsideMapBorders(x, y) && !IsMarkedByBuffer(x, y) && IsGrass(x, y);

        private bool IsInsideMapBorders(int x, int y) => (x >= 0 && x < _map.Width) && (y >= 0 && y < _map.Height);
        private bool IsMarkedByBuffer(int x, int y) => _markedTerrainBuffer[x, y];
        private bool IsGrass(int x, int y) => _map.Tiles[x, y].Terrain.Id == TerrainId.Grass;


        public virtual void Generate(MapContainer map, TerrainId terrainToGenerate)
        {
            _map = map;
            _terrainToGenerate = terrainToGenerate;
            _markedTerrainBuffer = new bool[_map.Width, _map.Height];

            GenerateSeeds();
            while(_modifiedTilesCounter < _targetModifiedTilesCounter)
            {
                ModifyTile(_availableTilesToModify.PickRandom);
                _modifiedTilesCounter++;
            }
        }


        protected abstract void GenerateSeeds();

        protected void GetRandomXY(out int x, out int y)
        {
            x = _rnd.Next(0, _map.Width);
            y = _rnd.Next(0, _map.Height);
        }


        protected void ModifyTile(Tile tile)
        {
            tile.SetTerrain(_terrainFactory.Create(_terrainToGenerate));
            CheckAndAddTileToBuffer(tile.LocationX - 1, tile.LocationY);
            CheckAndAddTileToBuffer(tile.LocationX + 1, tile.LocationY);
            CheckAndAddTileToBuffer(tile.LocationX, tile.LocationY - 1);
            CheckAndAddTileToBuffer(tile.LocationX, tile.LocationY + 1);
        }

        private void CheckAndAddTileToBuffer(int x, int y)
        {
            if (IsAvailableToModify(x, y))
            {
                _availableTilesToModify.Add(_map.Tiles[x, y]);
                _markedTerrainBuffer[x, y] = true;
            }
        }
    }
}
