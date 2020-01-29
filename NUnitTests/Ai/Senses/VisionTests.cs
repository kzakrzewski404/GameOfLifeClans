using NUnit.Framework;

using GameOfLifeClans.Ai;
using GameOfLifeClans.Ai.Senses.Vision;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Map;
using GameOfLifeClans.Map.Enums;
using GameOfLifeClans.Map.Data;


namespace GameOfLifeClans.UnitTests.Ai.Senses
{
    public class VisionTests
    {
        private MapContainer _map;
        private Vision _vision;
        private TileTerrainFactory _terrainFactory;

        private void GenerateNewMap3x3WithFill(TerrainId id)
        {
            _map.Generate(3, 3);
            FillMapWith(id);
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
        private void AddAiIntoTile(int x, int y, ClanId id) => _map.Tiles[x, y].SetAiEntity(new Headquarter(id, 100, 100));
        private void SetTerrain(int x, int y, TerrainId id) => _map.Tiles[x, y].SetTerrain(_terrainFactory.Terrain(id));


        [OneTimeSetUp]
        public void Init()
        {
            _map = new MapContainer();
            _vision = new Vision();
            _terrainFactory = new TileTerrainFactory();
        }


        [Test]
        public void When_SurroundedByAllUnoccupiedAndPassableTiles_Expect_Free8Allies0Enemies0()
        {
            //Arange
            GenerateNewMap3x3WithFill(TerrainId.Grass);
            Entity entity = new Headquarter(ClanId.Blue, 100, 100);
            _map.Tiles[1, 1].SetAiEntity(entity);

            //Act
            Result result = _vision.GetResult(entity);

            //Assert
            Assert.IsTrue((result.FreeTiles.Count == 8) && (result.Allies.Count == 0) && (result.Enemies.Count == 0));
        }

        [Test]
        public void When_SurroundedByAllImpassableTiles_Expect_Free0Allies0Enemies0()
        {
            //Arange
            GenerateNewMap3x3WithFill(TerrainId.Water);
            Entity entity = new Headquarter(ClanId.Blue, 100, 100);
            _map.Tiles[1, 1].SetAiEntity(entity);
            SetTerrain(1, 1, TerrainId.Grass);

            //Act
            Result result = _vision.GetResult(entity);

            //Assert
            Assert.IsTrue((result.FreeTiles.Count == 0) && (result.Allies.Count == 0) && (result.Enemies.Count == 0));
        }

        [Test]
        public void When_SurroundedByThreeImpassableTiles_Expect_Free5Allies0Enemies0()
        {
            //Arange
            GenerateNewMap3x3WithFill(TerrainId.Grass);
            Entity entity = new Headquarter(ClanId.Blue, 100, 100);
            _map.Tiles[1, 1].SetAiEntity(entity);
            for (int x = 0; x < 3; x++)
            {
                SetTerrain(x, 0, TerrainId.Water);
            }

            //Act
            Result result = _vision.GetResult(entity);

            //Assert
            Assert.IsTrue((result.FreeTiles.Count == 5) && (result.Allies.Count == 0) && (result.Enemies.Count == 0));
        }

        [Test]
        public void When_SurroundedByOneEnemyAndAllPassableTiles_Expect_Free7Allies0Enemies1()
        {
            //Arange
            GenerateNewMap3x3WithFill(TerrainId.Grass);
            Entity entity = new Headquarter(ClanId.Blue, 100, 100);
            _map.Tiles[1, 1].SetAiEntity(entity);
            AddAiIntoTile(0, 0, ClanId.Red);

            //Act
            Result result = _vision.GetResult(entity);

            //Assert
            Assert.IsTrue((result.FreeTiles.Count == 7) && (result.Allies.Count == 0) && (result.Enemies.Count == 1));
        }

        [Test]
        public void When_SurroundedByOneEnemyOneAllyAndAllPassableTiles_Expect_Free6Allies1Enemies1()
        {
            //Arange
            GenerateNewMap3x3WithFill(TerrainId.Grass);
            Entity entity = new Headquarter(ClanId.Blue, 100, 100);
            _map.Tiles[1, 1].SetAiEntity(entity);
            AddAiIntoTile(0, 0, ClanId.Red);
            AddAiIntoTile(0, 1, ClanId.Blue);

            //Act
            Result result = _vision.GetResult(entity);

            //Assert
            Assert.IsTrue((result.FreeTiles.Count == 6) && (result.Allies.Count == 1) && (result.Enemies.Count == 1));
        }

        public void When_SurroundedByThreeAlliesAndImpassableTiles_Expect_Free0Allies3Enemies0()
        {
            //Arange
            GenerateNewMap3x3WithFill(TerrainId.Water);
            Entity entity = new Headquarter(ClanId.Blue, 100, 100);
            _map.Tiles[1, 1].SetAiEntity(entity);
            AddAiIntoTile(0, 0, ClanId.Blue);
            AddAiIntoTile(0, 1, ClanId.Blue);
            AddAiIntoTile(0, 2, ClanId.Blue);

            //Act
            Result result = _vision.GetResult(entity);

            //Assert
            Assert.IsTrue((result.FreeTiles.Count == 6) && (result.Allies.Count == 1) && (result.Enemies.Count == 1));
        }

        [Test]
        public void When_InTopLeftCornerSurroundedByOneEnemyOneAlly_Expect_Free1Allies1Enemies1()
        {
            //Arange
            GenerateNewMap3x3WithFill(TerrainId.Grass);
            Entity entity = new Headquarter(ClanId.Blue, 100, 100);
            _map.Tiles[0, 0].SetAiEntity(entity);
            AddAiIntoTile(0, 1, ClanId.Blue);
            AddAiIntoTile(1, 0, ClanId.Red);

            //Act
            Result result = _vision.GetResult(entity);

            //Assert
            Assert.IsTrue((result.FreeTiles.Count == 1) && (result.Allies.Count == 1) && (result.Enemies.Count == 1));
        }

        [Test]
        public void When_InTopRightCornerSurroundedByOneEnemyOneAlly_Expect_Free1Allies1Enemies1()
        {
            //Arange
            GenerateNewMap3x3WithFill(TerrainId.Grass);
            Entity entity = new Headquarter(ClanId.Blue, 100, 100);
            _map.Tiles[2, 0].SetAiEntity(entity);
            AddAiIntoTile(1, 0, ClanId.Blue);
            AddAiIntoTile(2, 1, ClanId.Red);

            //Act
            Result result = _vision.GetResult(entity);

            //Assert
            Assert.IsTrue((result.FreeTiles.Count == 1) && (result.Enemies.Count == 1) && (result.Allies.Count == 1));
        }

        [Test]
        public void When_InBottomLeftCornerSurroundedByOneEnemyOneAlly_Expect_Free1Allies1Enemies1()
        {
            //Arange
            GenerateNewMap3x3WithFill(TerrainId.Grass);
            Entity entity = new Headquarter(ClanId.Blue, 100, 100);
            _map.Tiles[0, 2].SetAiEntity(entity);
            AddAiIntoTile(0, 1, ClanId.Blue);
            AddAiIntoTile(1, 2, ClanId.Red);

            //Act
            Result result = _vision.GetResult(entity);

            //Assert
            Assert.IsTrue((result.FreeTiles.Count == 1) && (result.Enemies.Count == 1) && (result.Allies.Count == 1));
        }

        [Test]
        public void When_InBottomRightCornerSurroundedByOneEnemyOneAlly_Expect_Free1Allies1Enemies1()
        {
            //Arange
            GenerateNewMap3x3WithFill(TerrainId.Grass);
            Entity entity = new Headquarter(ClanId.Blue, 100, 100);
            _map.Tiles[2, 2].SetAiEntity(entity);
            AddAiIntoTile(2, 1, ClanId.Blue);
            AddAiIntoTile(1, 2, ClanId.Red);

            //Act
            Result result = _vision.GetResult(entity);

            //Assert
            Assert.IsTrue((result.FreeTiles.Count == 1) && (result.Enemies.Count == 1) && (result.Allies.Count == 1));
        }

        [Test]
        public void When_OnLeftBorderSurroundedByOneEnemyOneAlly_Expect_Free3Allies1Enemies1()
        {
            //Arange
            GenerateNewMap3x3WithFill(TerrainId.Grass);
            Entity entity = new Headquarter(ClanId.Blue, 100, 100);
            _map.Tiles[0, 1].SetAiEntity(entity);
            AddAiIntoTile(0, 0, ClanId.Blue);
            AddAiIntoTile(0, 2, ClanId.Red);

            //Act
            Result result = _vision.GetResult(entity);

            //Assert
            Assert.IsTrue((result.FreeTiles.Count == 3) && (result.Enemies.Count == 1) && (result.Allies.Count == 1));
        }

        [Test]
        public void When_OnBottomBorderSurroundedByOneEnemyOneAlly_Expect_Free3Allies1Enemies1()
        {
            //Arange
            GenerateNewMap3x3WithFill(TerrainId.Grass);
            Entity entity = new Headquarter(ClanId.Blue, 100, 100);
            _map.Tiles[1, 2].SetAiEntity(entity);
            AddAiIntoTile(0, 2, ClanId.Blue);
            AddAiIntoTile(2, 2, ClanId.Red);

            //Act
            Result result = _vision.GetResult(entity);

            //Assert
            Assert.IsTrue((result.FreeTiles.Count == 3) && (result.Enemies.Count == 1) && (result.Allies.Count == 1));
        }

        [Test]
        public void When_OnRightBorderSurroundedByOneEnemyOneAlly_Expect_Free3Allies1Enemies1()
        {
            //Arange
            GenerateNewMap3x3WithFill(TerrainId.Grass);
            Entity entity = new Headquarter(ClanId.Blue, 100, 100);
            _map.Tiles[2, 1].SetAiEntity(entity);
            AddAiIntoTile(2, 0, ClanId.Blue);
            AddAiIntoTile(2, 2, ClanId.Red);

            //Act
            Result result = _vision.GetResult(entity);

            //Assert
            Assert.IsTrue((result.FreeTiles.Count == 3) && (result.Enemies.Count == 1) && (result.Allies.Count == 1));
        }

        [Test]
        public void When_OnTopBorderSurroundedByOneEnemyOneAlly_Expect_Free3Allies1Enemies1()
        {
            //Arange
            GenerateNewMap3x3WithFill(TerrainId.Grass);
            Entity entity = new Headquarter(ClanId.Blue, 100, 100);
            _map.Tiles[1, 0].SetAiEntity(entity);
            AddAiIntoTile(0, 0, ClanId.Blue);
            AddAiIntoTile(2, 0, ClanId.Red);

            //Act
            Result result = _vision.GetResult(entity);

            //Assert
            Assert.IsTrue((result.FreeTiles.Count == 3) && (result.Enemies.Count == 1) && (result.Allies.Count == 1));
        }
    }
}