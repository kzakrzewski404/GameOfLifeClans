using System.Collections.Generic;

using GameOfLifeClans.Map.Data;
using GameOfLifeClans.Map;
using GameOfLifeClans.Ai.Enums;


namespace GameOfLifeClans.Ai.Senses
{
    public class Vision
    {
        private delegate bool SearchCriteria(Tile tileToCheck, ClanId ownerId);


        public VisionResult GetNearbyFreeTiles(Tile visionOwner) => Search(visionOwner, IsUnoccupiedAndPassable);
        public VisionResult GetNearbyEnemies(Tile visionOwner) => Search(visionOwner, IsOccupiedByEnemy);
        public VisionResult GetNearbyAllies(Tile visionOwner) => Search(visionOwner, IsOccupiedByAlly);


        private VisionResult Search(Tile visionOwner, SearchCriteria searchPattern)
        {
            VisionResult results = new VisionResult();
            MapContainer map = visionOwner.Map;

            int minX = (visionOwner.LocationX - 1) < 0 ? 0 : (visionOwner.LocationX - 1);
            int maxX = (visionOwner.LocationX + 1) >= map.Width ? map.Width - 1 : visionOwner.LocationX + 1;
            int minY = (visionOwner.LocationY - 1) < 0 ? 0 : (visionOwner.LocationY - 1);
            int maxY = (visionOwner.LocationY + 1) >= map.Height ? map.Height - 1 : visionOwner.LocationY + 1;

            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    if ((visionOwner.LocationX != map.Tiles[x, y].LocationX) ||
                         visionOwner.LocationY != map.Tiles[x, y].LocationY)
                    {
                        if (searchPattern(map.Tiles[x, y], visionOwner.AiEntity.Id))
                        {
                            results.Add(map.Tiles[x, y]);
                        }
                    }
                }
            }

            return results;
        }

        private bool IsUnoccupiedAndPassable(Tile tile, ClanId ownerId) => !tile.IsOccupied && tile.Terrain.IsPassable;
        private bool IsOccupiedByEnemy(Tile tile, ClanId ownerId) => tile.IsOccupied && (tile.AiEntity.Id != ownerId);
        private bool IsOccupiedByAlly(Tile tile, ClanId ownerId) => tile.IsOccupied && (tile.AiEntity.Id == ownerId);
    }
}
