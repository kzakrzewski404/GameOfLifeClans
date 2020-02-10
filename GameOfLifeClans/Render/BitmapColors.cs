using System.Drawing;

using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Map.Data.Enums;


namespace GameOfLifeClans.Render
{
    public class BitmapColors
    {
        private const int NUMBER_OF_ENTITIES = 2;
        private const int NUMBER_OF_CLANS = 8;
        private const int NUMBER_OF_TERRAINS = 4;

        private Color[,] _entities;
        private Color[] _terrainOwnerships;
        private Color[] _terrains;


        public BitmapColors()
        {
            InitializeEntitiesColors();
            InitializeTerrainsColors();
        }


        public Color GetEntityColor(ClanId clan, EntityId id) => _entities[(int)clan, (int)id];
        public Color GetTerrainOwnershipColor(ClanId clan) => _terrainOwnerships[(int)clan];
        public Color GetTerrainColor(TerrainId id) => _terrains[(int)id];


        private void InitializeEntitiesColors()
        {
            _entities = new Color[NUMBER_OF_CLANS, NUMBER_OF_ENTITIES];
            _terrainOwnerships = new Color[NUMBER_OF_CLANS];

            CreateEntitiesColorScheme(ClanId.Blue,      Color.FromArgb(83, 99, 148),    Color.FromArgb(0, 83, 196),     Color.FromArgb(0, 162, 255));
            CreateEntitiesColorScheme(ClanId.Red,       Color.FromArgb(148, 89, 83),    Color.FromArgb(166, 0, 0),      Color.FromArgb(255, 0, 0));
            CreateEntitiesColorScheme(ClanId.Yellow,    Color.FromArgb(138, 148, 83),   Color.FromArgb(163, 174, 0),    Color.FromArgb(239, 255, 0));
            CreateEntitiesColorScheme(ClanId.Green,     Color.FromArgb(87, 148, 83),    Color.FromArgb(0, 160, 24),     Color.FromArgb(0, 219, 33));
            CreateEntitiesColorScheme(ClanId.Purple,    Color.FromArgb(122, 83, 148),   Color.FromArgb(121, 0, 166),    Color.FromArgb(181, 13, 243));
            CreateEntitiesColorScheme(ClanId.Black,     Color.FromArgb(48, 48, 48),     Color.FromArgb(43, 43, 43),     Color.FromArgb(60, 60, 60));
            CreateEntitiesColorScheme(ClanId.White,     Color.FromArgb(190, 190, 190),  Color.FromArgb(160, 160, 160),  Color.FromArgb(240, 240, 240));
            CreateEntitiesColorScheme(ClanId.Pink,      Color.FromArgb(148, 83, 136),   Color.FromArgb(152, 93, 156),   Color.FromArgb(247, 127, 255));
        }

        private void InitializeTerrainsColors()
        {
            _terrains = new Color[NUMBER_OF_TERRAINS];

            // Passable
            _terrains[(int)TerrainId.Grass] = Color.FromArgb(76, 102, 18);
            _terrains[(int)TerrainId.Sand] = Color.FromArgb(214, 208, 164);

            // Impassable
            _terrains[(int)TerrainId.Water] = Color.FromArgb(18, 64, 102);
            _terrains[(int)TerrainId.Mountain] = Color.FromArgb(71, 55, 48);
        }

        private void CreateEntitiesColorScheme(ClanId id, Color terrainOwnership, Color headquarter, Color soldier)
        {
            _terrainOwnerships[(int)id] = terrainOwnership;
            _entities[(int)id, (int)EntityId.Headquarter] = headquarter;
            _entities[(int)id, (int)EntityId.Soldier] = soldier;
        }
    }
}
