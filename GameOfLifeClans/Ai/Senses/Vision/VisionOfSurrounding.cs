using GameOfLifeClans.Ai.Entities;
using GameOfLifeClans.Map.Data;
using GameOfLifeClans.Map;
using GameOfLifeClans.Simulation.Clan;


namespace GameOfLifeClans.Ai.Senses.Vision
{
    public class VisionOfSurrounding : IVisionSense
    {
        protected MapContainer _map;
        protected int _visionRange;


        public VisionOfSurrounding(int surroundingVisionRange = 1)
        {
            _visionRange = surroundingVisionRange;
        }


        public IVisionResult GetResult(Entity visionOwner) => GenerateResult(visionOwner);


        protected bool IsNotCheckingOwner(Entity visionOwner, Tile target) =>
            ((visionOwner.LocationX != target.LocationX) || (visionOwner.LocationY != target.LocationY));
        protected bool IsTileFree(Tile target) => !target.IsOccupied && target.Terrain.IsPassable;
        protected bool IsEnemy(IClanInfo ownerClan, Tile target) => target.IsOccupied && (target.AiEntity.ClanInfo.Id != ownerClan.Id);
        protected bool IsAlly(IClanInfo ownerClan, Tile target) => target.IsOccupied && (target.AiEntity.ClanInfo.Id == ownerClan.Id);


        protected virtual IVisionResultCreating GenerateResult(Entity visionOwner)
        {
            _map = visionOwner.OccupiedTile.Map;

            IVisionResultCreating result = new VisionResult();

            int minX, maxX, minY, maxY;
            SetAlgorithmBorders(visionOwner, _visionRange, out minX, out maxX, out minY, out maxY);

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
                        else if (IsAlly(visionOwner.ClanInfo, currentTile))
                        {
                            result.AddAlly(currentTile.AiEntity);

                            if (currentTile.AiEntity.IsNeedingHealing)
                            {
                                result.AddAllyToHeal(currentTile.AiEntity);
                            }
                        }
                        else if (IsEnemy(visionOwner.ClanInfo, currentTile))
                        {
                            result.AddEnemy(currentTile.AiEntity);
                        }
                    }
                }
            }

            return result;
        }

        protected void SetAlgorithmBorders(Entity visionOwner, int range, out int minX, out int maxX, out int minY, out int maxY)
        {
            minX = (visionOwner.LocationX - range) < 0 ? 0 : (visionOwner.LocationX - range);
            maxX = (visionOwner.LocationX + range) >= _map.Width ? _map.Width - 1 : visionOwner.LocationX + range;
            minY = (visionOwner.LocationY - range) < 0 ? 0 : (visionOwner.LocationY - range);
            maxY = (visionOwner.LocationY + range) >= _map.Height ? _map.Height - 1 : visionOwner.LocationY + range;
        }
    }
}
