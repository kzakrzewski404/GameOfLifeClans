using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

using GameOfLifeClans.Map;
using GameOfLifeClans.Map.Data;


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

        public override void Render(bool renderConqueredTerritory)
        {
            for (int x = 0; x < _map.Width; x++)
            {
                for (int y = 0; y < _map.Height; y++)
                {
                    Tile rendered = _map.Tiles[x, y];
                    if (rendered.IsOccupied)
                    {
                        _canvas.SetPixel(x, y, _colors.GetEntityColor(rendered.AiEntity.ClanInfo.Id));
                    }
                    else if (renderConqueredTerritory && rendered.ClanOwnershipId != -1)
                    {
                        _canvas.SetPixel(x, y, _colors.GetClanTerritoryColor(rendered.ClanOwnershipId));
                    }
                    else
                    {
                        _canvas.SetPixel(x, y, _colors.GetTerrainColor(rendered.Terrain.Id));
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
