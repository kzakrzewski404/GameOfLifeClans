using System.Collections.Generic;
using System;

using GameOfLifeClans.Map.Data;


namespace GameOfLifeClans.Ai.Senses
{
    public class VisionResult
    {
        public List<Tile> Results { get; private set; }
        private static Random rnd = new Random();

        public bool IsNotEmpty => Results.Count > 0;
        public Tile PickRandom => Results[rnd.Next(0, Results.Count)];

        public void Add(Tile tile) => Results.Add(tile);


        public VisionResult()
        {
            Results = new List<Tile>();
        }
    }
}
