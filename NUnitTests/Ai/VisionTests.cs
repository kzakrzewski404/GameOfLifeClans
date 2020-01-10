using System.Collections.Generic;
using NUnit.Framework;

using GameOfLifeClans.Ai.Senses;
using GameOfLifeClans.Map;
using GameOfLifeClans.Map.Enums;
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
        public void When_SurroundedByAllUnoccupiedAndPassableTiles_Expect_ListWithEightElements()
        {
            //Arange
            map.Generate(3, 3);

            //Act
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

        [Test]
        public void When_SurroundedByAllImpassableTiles_Expect_ListWithZeroElements()
        {
            //Arange
            TileTerrainFactory factory = new TileTerrainFactory();
            map.Generate(3, 3);
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    map.Tiles[x, y].SetTerrain(factory.Terrain(TerrainId.Water));
                }
            }
            map.Tiles[1, 1].SetTerrain(factory.Terrain(TerrainId.Grass));

            //Act
            List<Tile> result = vision.GetUnoccupiedAndPassableTiles(map.Tiles[1, 1]);

            //Assert
            Assert.IsTrue(result.Count == 0, $"List contained [{result.Count}] elements\n");
        }

        [Test]
        public void When_SurroundedByThreeImpassableTiles_Expect_ListWithFiveElements()
        {
            //Arange
            TileTerrainFactory factory = new TileTerrainFactory();
            map.Generate(3, 3);
            for (int x = 0; x < 3; x++)
            {
                map.Tiles[x, 0].SetTerrain(factory.Terrain(TerrainId.Water));
            }

            //Act
            List<Tile> result = vision.GetUnoccupiedAndPassableTiles(map.Tiles[1, 1]);

            //Assert
            Assert.IsTrue(result.Count == 5, $"List contained [{result.Count}] elements\n");
        }
    }
}