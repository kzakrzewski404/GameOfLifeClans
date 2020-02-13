﻿using GameOfLifeClans.Map.Data;
using GameOfLifeClans.Map;


namespace GameOfLifeClans.Ai.Senses
{
    public class Vision
    {
        private MapContainer map;


        public IReadableVisionResult GetResult(Entity visionOwner) => GenerateResult(visionOwner);


        private bool IsNotCheckingOwner(Entity visionOwner, Tile target) =>
            ((visionOwner.LocationX != target.LocationX) || (visionOwner.LocationY != target.LocationY));
        private bool IsTileFree(Tile target) => !target.IsOccupied && target.Terrain.IsPassable;
        private bool IsEnemy(Entity visionOwner, Tile target) => target.IsOccupied && (target.AiEntity.ClanId != visionOwner.ClanId);
        private bool IsAlly(Entity visionOwner, Tile target) => target.IsOccupied && (target.AiEntity.ClanId == visionOwner.ClanId);


        private IReadableVisionResult GenerateResult(Entity visionOwner)
        {
            map = visionOwner.OccupiedTile.Map;

            ICreatableVisionResult result = new VisionResult();

            int minX, maxX, minY, maxY;
            SetAlgorithmBorders(visionOwner, out minX, out maxX, out minY, out maxY);

            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    Tile currentTile = map.Tiles[x, y];
                    if (IsNotCheckingOwner(visionOwner, currentTile))
                    {
                        if (IsTileFree(currentTile))
                        {
                            result.AddFreeTile(currentTile);
                        }
                        else if (IsAlly(visionOwner, currentTile))
                        {
                            result.AddAlly(currentTile.AiEntity);
                        }
                        else if (IsEnemy(visionOwner, currentTile))
                        {
                            result.AddEnemy(currentTile.AiEntity);
                        }
                    }
                }
            }
            return result as IReadableVisionResult;
        }

        private void SetAlgorithmBorders(Entity visionOwner, out int minX, out int maxX, out int minY, out int maxY)
        {
            minX = (visionOwner.LocationX - 1) < 0 ? 0 : (visionOwner.LocationX - 1);
            maxX = (visionOwner.LocationX + 1) >= map.Width ? map.Width - 1 : visionOwner.LocationX + 1;
            minY = (visionOwner.LocationY - 1) < 0 ? 0 : (visionOwner.LocationY - 1);
            maxY = (visionOwner.LocationY + 1) >= map.Height ? map.Height - 1 : visionOwner.LocationY + 1;
        }
    }
}