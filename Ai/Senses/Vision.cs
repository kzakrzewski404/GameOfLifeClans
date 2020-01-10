using System.Collections.Generic;
using GameOfLifeClans.Map.Data;
using GameOfLifeClans.Map;


namespace GameOfLifeClans.Ai.Senses
{
    public class Vision
    {
        private delegate bool SearchCriteria(Tile tile);


        public List<Tile> GetUnoccupiedTiles(Tile origin) => Search(origin, IsUnoccupied);


        private List<Tile> Search(Tile origin, SearchCriteria searchCriteria)
        {
            List<Tile> resultList = new List<Tile>();
            MapContainer map = origin.Map;

            int minX = (origin.LocationX - 1) < 0 ? 0 : (origin.LocationX - 1);
            int maxX = (origin.LocationX + 1) >= map.Width ? map.Width - 1 : origin.LocationX + 1;
            int minY = (origin.LocationY - 1) < 0 ? 0 : (origin.LocationY - 1);
            int maxY = (origin.LocationY + 1) >= map.Height ? map.Height - 1 : origin.LocationY + 1;

            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    if ((origin.LocationX != map.Tiles[x, y].LocationX) &&
                         origin.LocationY != map.Tiles[x, y].LocationY)
                    {
                        if (searchCriteria(map.Tiles[x, y]))
                        {
                            resultList.Add(map.Tiles[x, y]);
                        }
                    }
                }
            }

            return resultList;
        }

        private bool IsUnoccupied(Tile tile) => !tile.IsOccupied;
    }
}
