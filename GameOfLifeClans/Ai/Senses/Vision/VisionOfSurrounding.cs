using GameOfLifeClans.Ai.Entities;
using GameOfLifeClans.Map.Data;
using GameOfLifeClans.Map;


namespace GameOfLifeClans.Ai.Senses.Vision
{
    public class VisionOfSurrounding : IVisionSense
    {
        private MapContainer _map;
        private int _visionRange;


        public VisionOfSurrounding(int visionRange = 1)
        {
            _visionRange = visionRange;
        }


        public IVisionResult GetResult(Entity visionOwner) => GenerateResult(visionOwner);


        private bool IsNotCheckingOwner(Entity visionOwner, Tile target) =>
            ((visionOwner.LocationX != target.LocationX) || (visionOwner.LocationY != target.LocationY));
        private bool IsTileFree(Tile target) => !target.IsOccupied && target.Terrain.IsPassable;
        private bool IsEnemy(Entity visionOwner, Tile target) => target.IsOccupied && (target.AiEntity.ClanInfo.Id != visionOwner.ClanInfo.Id);
        private bool IsAlly(Entity visionOwner, Tile target) => target.IsOccupied && (target.AiEntity.ClanInfo.Id == visionOwner.ClanInfo.Id);


        private IVisionResultCreating GenerateResult(Entity visionOwner)
        {
            _map = visionOwner.OccupiedTile.Map;

            IVisionResultCreating result = new VisionResult();

            int minX, maxX, minY, maxY;
            SetAlgorithmBorders(visionOwner, out minX, out maxX, out minY, out maxY);

            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    Tile currentTile = _map.Tiles[x, y];
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

            return result;
        }

        private void SetAlgorithmBorders(Entity visionOwner, out int minX, out int maxX, out int minY, out int maxY)
        {
            minX = (visionOwner.LocationX - _visionRange) < 0 ? 0 : (visionOwner.LocationX - _visionRange);
            maxX = (visionOwner.LocationX + _visionRange) >= _map.Width ? _map.Width - _visionRange : visionOwner.LocationX + _visionRange;
            minY = (visionOwner.LocationY - _visionRange) < 0 ? 0 : (visionOwner.LocationY - _visionRange);
            maxY = (visionOwner.LocationY + _visionRange) >= _map.Height ? _map.Height - _visionRange : visionOwner.LocationY + _visionRange;
        }
    }
}
