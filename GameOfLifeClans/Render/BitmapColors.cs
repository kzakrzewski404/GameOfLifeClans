using System.Drawing;

using GameOfLifeClans.Map.Data.Enums;
using GameOfLifeClans.Ai.Enums;


namespace GameOfLifeClans.Render
{
    public class BitmapColors
    {
        private Color[,] entities;
        private Color[] terrains;

        private const int NUMBER_OF_ENTITIES = 2;
        private const int NUMBER_OF_CLANS = 8;
        private const int NUMBER_OF_TERRAINS = 4;


        public Color GetEntityColor(ClanId clan, EntityId id) => entities[(int)(clan), (int)(id)];
        public Color GetTerrainColor(TerrainId id) => terrains[(int)(id)];


        public BitmapColors()
        {
            InitializeEntitiesColors();
            InitializeTerrainsColors();
        }


        private void InitializeEntitiesColors()
        {
            entities = new Color[NUMBER_OF_CLANS, NUMBER_OF_ENTITIES];

            CreateEntitiesColorScheme(ClanId.Blue, Color.FromArgb(0, 83, 196), Color.FromArgb(0, 162, 255));
            CreateEntitiesColorScheme(ClanId.Red, Color.FromArgb(166, 0, 0), Color.FromArgb(255, 0, 0));
            CreateEntitiesColorScheme(ClanId.Yellow, Color.FromArgb(163, 174, 0), Color.FromArgb(239, 255, 0));
            CreateEntitiesColorScheme(ClanId.Green, Color.FromArgb(0, 160, 24), Color.FromArgb(0, 219, 33));
            CreateEntitiesColorScheme(ClanId.Purple, Color.FromArgb(121, 0, 166), Color.FromArgb(181, 13, 243));
            CreateEntitiesColorScheme(ClanId.Black, Color.FromArgb(43, 43, 43), Color.FromArgb(98, 98, 98));
            CreateEntitiesColorScheme(ClanId.White, Color.FromArgb(225, 225, 225), Color.FromArgb(240, 240, 240));
            CreateEntitiesColorScheme(ClanId.Pink, Color.FromArgb(152, 93, 156), Color.FromArgb(247, 127, 255));
        }

        private void InitializeTerrainsColors()
        {
            terrains = new Color[NUMBER_OF_TERRAINS];

            //Passable
            terrains[(int)TerrainId.Grass] = Color.FromArgb(76, 102, 18);
            terrains[(int)TerrainId.Sand] = Color.FromArgb(214, 208, 164);

            //Impassable
            terrains[(int)TerrainId.Water] = Color.FromArgb(18, 64, 102);
            terrains[(int)TerrainId.Mountain] = Color.FromArgb(71, 55, 48);
        }

        private void CreateEntitiesColorScheme(ClanId id, Color headquarter, Color soldier)
        {
            entities[(int)id, (int)EntityId.Headquarter] = headquarter;
            entities[(int)id, (int)EntityId.Soldier] = soldier;
        }
    }
}
