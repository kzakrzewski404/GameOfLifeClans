using GameOfLifeClans.Ai.Entities;
using GameOfLifeClans.Map.Data;


namespace GameOfLifeClans.Ai.Senses.Vision
{
    public interface IVisionResultCreating : IVisionResult
    {
        void AddFreeTile(Tile tile);
        void AddAlly(Entity ally);
        void AddAllyToHeal(Entity allyToHeal);
        void AddEnemy(Entity enemy);

        void SetIsAwayFromClosestHeadquarter(bool result);
    }
}
