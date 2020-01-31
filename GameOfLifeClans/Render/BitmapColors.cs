using System.Drawing;

using GameOfLifeClans.Map.Enums;
using GameOfLifeClans.Ai.Enums;


namespace GameOfLifeClans.Render
{
    public class BitmapColors
    {
        private Color[,] entities;
        private Color[] terrains;


        public Color GetEntityColor(EntityId id, ClanId clan) => entities[(int)(id), (int)(clan)];
        public Color GetTerrainColor(TerrainId id) => terrains[(int)(id)];


        public BitmapColors()
        {
            InitializeEntitiesColors();
            InitializeTerrainsColors();
        }


        private void InitializeEntitiesColors()
        {
            entities = new Color[2, 2];

            entities[(int)(EntityId.Headquarter), (int)(ClanId.Blue)] = Color.Blue;
            entities[(int)(EntityId.Headquarter), (int)(ClanId.Red)] = Color.DarkRed;

            entities[(int)(EntityId.Soldier), (int)(ClanId.Blue)] = Color.DeepSkyBlue;
            entities[(int)(EntityId.Soldier), (int)(ClanId.Red)] = Color.Red;
        }

        private void InitializeTerrainsColors()
        {
            terrains = new Color[4];

            //Passable
            terrains[(int)TerrainId.Grass] = Color.YellowGreen;
            terrains[(int)TerrainId.Sand] = Color.Wheat;

            //Impassable
            terrains[(int)TerrainId.Water] = Color.RoyalBlue;
            terrains[(int)TerrainId.Mountain] = Color.Gray;
        }
    }
}
