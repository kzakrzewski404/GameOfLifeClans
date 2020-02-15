using GameOfLifeClans.Ai.Entities;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Map.Data;
using GameOfLifeClans.Simulation.Clan;


namespace GameOfLifeClans.Ai.Senses.Vision
{
    public class VisionOfBuilder : VisionOfSurrounding
    {
        private int _headquarterCheckRange;


        public VisionOfBuilder(int headquarterCheckRange, int surroundingVisionRange = 1) : base(surroundingVisionRange)
        {
            _headquarterCheckRange = headquarterCheckRange;
        }


        protected override IVisionResultCreating GenerateResult(Entity visionOwner)
        {
            IVisionResultCreating result = base.GenerateResult(visionOwner);
            result.SetIsAwayFromClosestHeadquarter(CheckIfIsAwayFromClosestHeadquarter(visionOwner));
            return result;
        }


        private bool IsAlliedHeadquarter(IClanInfo ownerClan, Tile targetTile) => IsAlly(ownerClan, targetTile) && (targetTile.AiEntity.Id == EntityId.Headquarter);

        private bool CheckIfIsAwayFromClosestHeadquarter(Entity visionOwner)
        {
            int minX, maxX, minY, maxY;
            SetAlgorithmBorders(visionOwner, _headquarterCheckRange, out minX, out maxX, out minY, out maxY);

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
