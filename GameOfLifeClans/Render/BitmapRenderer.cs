using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Map;


namespace GameOfLifeClans.Render
{
    public class BitmapRenderer : Renderer
    {
        private Bitmap _canvas;
        private BitmapColors _colors = new BitmapColors();
        private System.Windows.Controls.Image _renderOutput;


        public void SetRenderOutput(System.Windows.Controls.Image uiImage) => _renderOutput = uiImage;


        public override void LinkMapContainer(MapContainer map, int numberOfClans)
        {
            base.LinkMapContainer(map, numberOfClans);
            _colors.PrepareClanColors(numberOfClans);
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
                        _canvas.SetPixel(x, y, _colors.GetEntityColor(_map.Tiles[x, y].AiEntity.ClanId, _map.Tiles[x, y].AiEntity.Id));
                    }
                    else if (_map.Tiles[x, y].ClanOwnership != ClanId._Neutral)
                    {
                        _canvas.SetPixel(x, y, _colors.GetTerrainOwnershipColor(_map.Tiles[x, y].ClanOwnership));
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
    }
}
