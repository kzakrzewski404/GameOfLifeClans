using GameOfLifeClans.Ai.Entities;
using GameOfLifeClans.Map.Data;


namespace GameOfLifeClans.Ai.Senses.Vision
{
    public interface IVisionResultCreating : IVisionResult, IBuilderVisionResult
    {
        void AddFreeTile(Tile tile);
        void AddAlly(Entity ally);
        void AddEnemy(Entity enemy);

        void SetIsAwayFromClosestHeadquarter(bool result);
    }
}
