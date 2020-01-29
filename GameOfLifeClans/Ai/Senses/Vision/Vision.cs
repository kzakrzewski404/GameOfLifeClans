using GameOfLifeClans.Map.Data;
using GameOfLifeClans.Map;


namespace GameOfLifeClans.Ai.Senses.Vision
{
    public class Vision
    {
        private MapContainer map;


        public Result GetResult(Entity visionOwner) => GenerateResult(visionOwner);


        private bool IsNotCheckingOwner(Entity visionOwner, Tile target) =>
            ((visionOwner.LocationX != target.LocationX) || (visionOwner.LocationY != target.LocationY));
        private bool IsTileFree(Tile target) => !target.IsOccupied && target.Terrain.IsPassable;
        private bool IsEnemy(Entity visionOwner, Tile target) => target.IsOccupied && (target.AiEntity.Clan != visionOwner.Clan);
        private bool IsAlly(Entity visionOwner, Tile target) => !IsEnemy(visionOwner, target);


        private Result GenerateResult(Entity visionOwner)
        {
            map = visionOwner.OccupiedTile.Map;

            Result result = new Result();

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
                            result.FreeTiles.Add(currentTile);
                        }
                        else if (IsAlly(visionOwner, currentTile))
                        {
                            result.Allies.Add(currentTile.AiEntity);
                        }
                        else if (IsEnemy(visionOwner, currentTile))
                        {
                            result.Enemies.Add(currentTile.AiEntity);
                        }
                    }
                }
            }
            return result;
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
