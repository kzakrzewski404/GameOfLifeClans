using GameOfLifeClans.Ai.Entities;
using GameOfLifeClans.Map.Data;


namespace GameOfLifeClans.Ai.Senses.Vision
{
    public interface IVisionResult
    {
        bool IsEnemyFound { get; }
        bool IsAllyFound { get; }
        bool IsFreeTileFound { get; }
        int NumberOfEnemies { get; }
        int NumberOfAllies { get; }
        int NumberOfFreeTiles { get; }


        IAttackable GetRandomEnemy();
        IHealable GetRandomAlly();
        Tile GetRandomFreeTile();
    }
}
