using GameOfLifeClans.Map.Data;


namespace GameOfLifeClans.Ai.Senses
{
    public interface IReadableVisionResult
    {
        bool IsEnemyFound { get; }
        bool IsAllyFound { get; }
        bool IsFreeTileFound { get; }
        int NumberOfEnemies { get; }
        int NumberOfAllies { get; }
        int NumberOfFreeTiles { get; }

        IAttackable GetRandomEnemy();
        Entity GetRandomAlly();
        Tile GetRandomFreeTile();
    }
}
