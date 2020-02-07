using System;

using GameOfLifeClans.Map.Data;
using GameOfLifeClans.Generics;


namespace GameOfLifeClans.Map.Generators
{
    public abstract class LandspaceGenerator
    {
        private static Random _rnd = new Random();
        private bool[,] _markedTerrainBuffer;
        private ItemsContainer<Tile> _availableTilesToModify = new ItemsContainer<Tile>();
        private MapContainer _map;

        public virtual void Generate(MapContainer map)
        {
            _map = map;
            _markedTerrainBuffer = new bool[_map.Width, _map.Height];
        }
    }
}
