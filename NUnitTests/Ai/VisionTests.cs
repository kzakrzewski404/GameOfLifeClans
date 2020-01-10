using System.Collections.Generic;
using NUnit.Framework;

using GameOfLifeClans.Ai;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai.Senses;
using GameOfLifeClans.Map;
using GameOfLifeClans.Map.Data;

namespace GameOfLifeClans.Tests.Ai
{
    public class VisionTests
    {
        private MapContainer map;
        private Vision vision;

        [OneTimeSetUp]
        public void Init()
        {
            map = new MapContainer();
            vision = new Vision();
        }


        [Test]
        public void VisionListShouldContain8_WhenSurroundedByUnoccupiedAndPassableTiles()
        {
            //Arange

            //Act
            map.Generate(3, 3);
            List<Tile> result = vision.GetUnoccupiedAndPassableTiles(map.Tiles[1, 1]);

            //Prepare msg
            string msg = "";
            for (int i = 0; i < result.Count; i++)
            {
                msg += $"[{i}] - x:{result[i].LocationX} y:{result[i].LocationY}\n";
            }

            //Assert
            Assert.IsTrue(result.Count == 8, $"List contained [{result.Count}] elements\n" + msg);
        }
    }
}