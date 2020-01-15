using NUnit.Framework;

using GameOfLifeClans.Ai;
using GameOfLifeClans.Ai.Senses;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Map;
using GameOfLifeClans.Map.Enums;
using GameOfLifeClans.Map.Data;


namespace GameOfLifeClans.Tests.Ai
{
    public class VisionTests
    {
        private MapContainer map;
        private Vision vision;
        private TileTerrainFactory terrainFactory;

        private void GenerateNewMap3x3WithBlueAiInMiddleFillWith(TerrainId id)
        {
            map.Generate(3, 3);
            FillMapWith(id);
            AddAiIntoTile(1, 1, ClanId.Blue);
        }
        private void FillMapWith(TerrainId id)
        {
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    SetTerrain(x, y, id);
                }
            }
        }
        private void AddAiIntoTile(int x, int y, ClanId id) => map.Tiles[x, y].SetAiEntity(new Headquarter(id, 100, 100));
        private void SetTerrain(int x, int y, TerrainId id) => map.Tiles[x, y].SetTerrain(terrainFactory.Terrain(id));


        [OneTimeSetUp]
        public void Init()
        {
            map = new MapContainer();
            vision = new Vision();
            terrainFactory = new TileTerrainFactory();
        }


        [Test]
        public void When_SurroundedByAllUnoccupiedAndPassableTiles_Expect_Free8Allies0Enemies0()
        {
            //Arange
            GenerateNewMap3x3WithBlueAiInMiddleFillWith(TerrainId.Grass);

            //Act
            VisionResult free = vision.GetNearbyFreeTiles(map.Tiles[1, 1]);
            VisionResult enemies = vision.GetNearbyEnemies(map.Tiles[1, 1]);
            VisionResult allies = vision.GetNearbyAllies(map.Tiles[1, 1]);

            //Assert
            Assert.IsTrue((free.Results.Count == 8) && (allies.Results.Count == 0) && (enemies.Results.Count == 0));
        }

        [Test]
        public void When_SurroundedByAllImpassableTiles_Expect_Free0Allies0Enemies0()
        {
            //Arange
            GenerateNewMap3x3WithBlueAiInMiddleFillWith(TerrainId.Water);
            SetTerrain(1, 1, TerrainId.Grass);

            //Act
            VisionResult free = vision.GetNearbyFreeTiles(map.Tiles[1, 1]);
            VisionResult enemies = vision.GetNearbyEnemies(map.Tiles[1, 1]);
            VisionResult allies = vision.GetNearbyAllies(map.Tiles[1, 1]);

            //Assert
            Assert.IsTrue((free.Results.Count == 0) && (allies.Results.Count == 0) && (enemies.Results.Count == 0));
        }

        [Test]
        public void When_SurroundedByThreeImpassableTiles_Expect_Free5Allies0Enemies0()
        {
            //Arange
            GenerateNewMap3x3WithBlueAiInMiddleFillWith(TerrainId.Grass);
            for (int x = 0; x < 3; x++)
            {
                SetTerrain(x, 0, TerrainId.Water);
            }

            //Act
            VisionResult free = vision.GetNearbyFreeTiles(map.Tiles[1, 1]);
            VisionResult enemies = vision.GetNearbyEnemies(map.Tiles[1, 1]);
            VisionResult allies = vision.GetNearbyAllies(map.Tiles[1, 1]);

            //Assert
            Assert.IsTrue((free.Results.Count == 5) && (allies.Results.Count == 0) && (enemies.Results.Count == 0));
        }

        [Test]
        public void When_SurroundedByOneEnemyAndAllPassableTiles_Exect_Free7Allies0Enemies1()
        {
            //Arange
            GenerateNewMap3x3WithBlueAiInMiddleFillWith(TerrainId.Grass);
            AddAiIntoTile(0, 0, ClanId.Red);

            //Act
            VisionResult free = vision.GetNearbyFreeTiles(map.Tiles[1, 1]);
            VisionResult enemies = vision.GetNearbyEnemies(map.Tiles[1, 1]);
            VisionResult allies = vision.GetNearbyAllies(map.Tiles[1, 1]);

            //Assert
            Assert.IsTrue((free.Results.Count == 7) && (allies.Results.Count == 0) && (enemies.Results.Count == 1));
        }

        [Test]
        public void When_SurroundedByOneEnemyOneAllyAndAllPassableTiles_Exect_Free6Allies1Enemies1()
        {
            //Arange
            GenerateNewMap3x3WithBlueAiInMiddleFillWith(TerrainId.Grass);
            AddAiIntoTile(0, 0, ClanId.Red);
            AddAiIntoTile(0, 1, ClanId.Blue);

            //Act
            VisionResult free = vision.GetNearbyFreeTiles(map.Tiles[1, 1]);
            VisionResult enemies = vision.GetNearbyEnemies(map.Tiles[1, 1]);
            VisionResult allies = vision.GetNearbyAllies(map.Tiles[1, 1]);

            //Assert
            Assert.IsTrue((free.Results.Count == 6) && (allies.Results.Count == 1) && (enemies.Results.Count == 1));
        }

        public void When_SurroundedByThreeAlliesAndImpassableTiles_Exect_Free0Allies3Enemies0()
        {
            //Arange
            GenerateNewMap3x3WithBlueAiInMiddleFillWith(TerrainId.Water);
            AddAiIntoTile(0, 0, ClanId.Blue);
            AddAiIntoTile(0, 1, ClanId.Blue);
            AddAiIntoTile(0, 2, ClanId.Blue);

            //Act
            VisionResult free = vision.GetNearbyFreeTiles(map.Tiles[1, 1]);
            VisionResult enemies = vision.GetNearbyEnemies(map.Tiles[1, 1]);
            VisionResult allies = vision.GetNearbyAllies(map.Tiles[1, 1]);

            //Assert
            Assert.IsTrue((free.Results.Count == 6) && (allies.Results.Count == 1) && (enemies.Results.Count == 1));
        }
    }
}