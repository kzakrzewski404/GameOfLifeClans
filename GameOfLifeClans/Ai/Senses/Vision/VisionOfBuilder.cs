using GameOfLifeClans.Ai.Entities;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Map.Data;
using GameOfLifeClans.Simulation.Clan;


namespace GameOfLifeClans.Ai.Senses.Vision
{
    public class VisionOfBuilder : VisionOfSurrounding, IBuilderVisionSense
    {
        public VisionOfBuilder(int surroundingVisionRange = 1) : base(surroundingVisionRange)
        {
            // Empty
        }


        public IBuilderVisionResult GetResult(Entity visionOwner, int minimalDistanceFromHeadquarter)
        {
            IVisionResultCreating result = GenerateResult(visionOwner);
            result.SetIsAwayFromClosestHeadquarter(CheckIfIsAwayFromClosestHeadquarter(visionOwner, minimalDistanceFromHeadquarter));

            return result;
        }


        private bool IsAlliedHeadquarter(IClanInfo ownerClan, Tile targetTile) => (targetTile.AiEntity.Id == EntityId.Headquarter) && IsAlly(ownerClan, targetTile);

        private bool CheckIfIsAwayFromClosestHeadquarter(Entity visionOwner, int headquarterCheckRange)
        {
            int minX, maxX, minY, maxY;
            SetAlgorithmBorders(visionOwner, headquarterCheckRange, out minX, out maxX, out minY, out maxY);

            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    Tile currentTile = _map.Tiles[x, y];
                    if (IsAlliedHeadquarter(visionOwner.ClanInfo, currentTile))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
