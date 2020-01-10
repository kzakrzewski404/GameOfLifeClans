using System.Windows.Media.Imaging;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using GameOfLifeClans.Map;
using GameOfLifeClans.Map.Enums;
using GameOfLifeClans.Ai.Enums;


namespace GameOfLifeClans.Render
{
    public class BitmapRenderer : Renderer
    {
        private Bitmap _canvas;
        private System.Windows.Controls.Image _renderOutput;


        public void TestCreateCanvas() => _canvas = new Bitmap(100, 100);
        public void SetRenderOutput(System.Windows.Controls.Image uiImage) => _renderOutput = uiImage;


        public override void LinkMap(MapContainer map)
        {
            base.LinkMap(map);
            _canvas = new Bitmap(map.Width, map.Height);
        }

        public override void Render()
        {
            for (int x = 0; x < 100; x++)
            {
                for (int y = 0; y < 100; y++)
                {
                    //TODO - recognize tiles in map
                    //TODO - change this func to map.width, map.height
                    _canvas.SetPixel(x, y, Color.Magenta);
                }
            }

            using (MemoryStream stream = new MemoryStream())
            {
                _canvas.Save(stream, ImageFormat.Bmp);
                stream.Seek(0, SeekOrigin.Begin);

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = stream;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                _renderOutput.Source = bitmap;
            }
        }


        Color GetOccupiedColor(ClanId id)
        {
            switch (id)
            {
                case ClanId.Blue: return Color.DeepSkyBlue;
                case ClanId.Red: return Color.Red;

                default: return Color.White;
            }
        }

        Color GetTerrainColor(TerrainId id)
        {
            switch (id)
            {
                //passable
                case TerrainId.Grass: return Color.YellowGreen;
                case TerrainId.Desert: return Color.Wheat;

                //impassable
                case TerrainId.Water: return Color.RoyalBlue;
                case TerrainId.Mountain: return Color.Gray;

                default: return Color.Magenta;
            };
        }
    }
}
