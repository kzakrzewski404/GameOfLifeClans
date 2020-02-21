using System;

using GameOfLifeClans.Map.Data;
using GameOfLifeClans.Map.Data.Enums;
using GameOfLifeClans.Generics;


namespace GameOfLifeClans.Map.Generators.Data
{
    public class TileBuffer
    {
        private bool[,] _checkedTiles;
        private MapContainer _map;
        private Random _rnd = new Random();
        private ItemsContainer<Tile> _tilesPool = new ItemsContainer<Tile>();


        public TileBuffer(MapContainer map)
        {
            _map = map;
            _checkedTiles = new bool[_map.Width, _map.Height];
        }


        public Tile GetRandom => GetAndManageBuffer();
        public Tile GetRandomSeedFromWholeMap => GetAndManageBuffer(RandomSeedFromWholeMap);
        public Tile GetSeedFromRandomMapBorder => GetAndManageBuffer(RandomSeedFromMapBorder);


        private bool CanBeEdited(int x, int y) => IsInsideMapBorders(x, y) && !IsMarkedByBuffer(x, y) && IsGrass(x, y);
        private bool IsInsideMapBorders(int x, int y) => (x >= 0 && x < _map.Width) && (y >= 0 && y < _map.Height);
        private bool IsMarkedByBuffer(int x, int y) => _checkedTiles[x, y];
        private bool IsGrass(int x, int y) => _map.Tiles[x, y].Terrain.Id == TerrainId.Grass;


        private Tile GetAndManageBuffer(Func<Tile> seedSearchingMethod = null)
        {
            Tile seed;
            if (seedSearchingMethod != null)
            {
                seed = seedSearchingMethod();
                _checkedTiles[seed.LocationX, seed.LocationY] = true;
            }
            else
            {
                seed = _tilesPool.PickRandomAndRemoveFromList();
            }

            BufferNeighbours(seed);
            return seed;
        }

        private Tile RandomSeedFromWholeMap()
        {
            while (true)
            {
                int x, y;
                GetRandomXY(out x, out y);

                if (CanBeEdited(x, y))
                {
                    return _map.Tiles[x, y];
                }
            }
        }

        private Tile RandomSeedFromMapBorder()
        {
            while (true)
            {
                int randomBorder = _rnd.Next(0, 4);
                int x, y;
                if (randomBorder <= 1) // x will be locked
                {
                    x = randomBorder == 0 ? 0 : _map.Width - 1;
                    y = _rnd.Next(0, _map.Height);
                }
                else
                {
                    x = _rnd.Next(0, _map.Width - 1);
                    y = randomBorder == 2 ? 0 : _map.Height - 1;
                }

                if (CanBeEdited(x, y))
                {
                    return _map.Tiles[x, y];
                }
            }
        }

        private void BufferNeighbours(Tile tile)
        {
            BufferTile(tile.LocationX - 1, tile.LocationY);
            BufferTile(tile.LocationX + 1, tile.LocationY);
            BufferTile(tile.LocationX, tile.LocationY - 1);
            BufferTile(tile.LocationX, tile.LocationY + 1);
        }

        private void BufferTile(int x, int y)
        {
            if (CanBeEdited(x, y))
            {
                _tilesPool.Add(_map.Tiles[x, y]);
                _checkedTiles[x, y] = true;
            }
        }

        private void GetRandomXY(out int x, out int y)
        {
            x = _rnd.Next(0, _map.Width);
            y = _rnd.Next(0, _map.Height);
        }
    }
}
