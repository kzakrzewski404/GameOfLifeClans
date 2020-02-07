using System.Windows.Media.Imaging;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using GameOfLifeClans.Map;
using GameOfLifeClans.Map.Data.Enums;
using GameOfLifeClans.Ai.Enums;


namespace GameOfLifeClans.Render
{
    public class BitmapRenderer : Renderer
    {
        private Bitmap _canvas;
        private BitmapColors _colors = new BitmapColors();
        private System.Windows.Controls.Image _renderOutput;


        public void TestCreateCanvas() => _canvas = new Bitmap(100, 100);
        public void SetRenderOutput(System.Windows.Controls.Image uiImage) => _renderOutput = uiImage;


        public override void LinkMapContainer(MapContainer map)
        {
            base.LinkMapContainer(map);
            _canvas = new Bitmap(map.Width, map.Height);
        }

        public override void Render()
        {
            for (int x = 0; x < _map.Width; x++)
            {
                for (int y = 0; y < _map.Height; y++)
                {
                    if (_map.Tiles[x, y].IsOccupied)
                    {
                        _canvas.SetPixel(x, y, _colors.GetEntityColor(_map.Tiles[x, y].AiEntity.Clan, _map.Tiles[x, y].AiEntity.Id));
                    }
                    else
                    {
                        _canvas.SetPixel(x, y, _colors.GetTerrainColor(_map.Tiles[x, y].Terrain.Id));
                    }
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
                case TerrainId.Sand: return Color.Wheat;

                //impassable
                case TerrainId.Water: return Color.RoyalBlue;
                case TerrainId.Mountain: return Color.Gray;

                default: return Color.Magenta;
            };
        }
    }
}
