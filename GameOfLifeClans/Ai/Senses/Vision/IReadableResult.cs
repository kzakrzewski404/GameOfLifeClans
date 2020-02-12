using GameOfLifeClans.Map.Data;


namespace GameOfLifeClans.Ai.Senses.Vision
{
    public interface IReadableResult
    {
        bool IsEnemyFound { get; }
        bool IsAllyFound { get; }
        bool IsFreeTileFound { get; }
        int NumberOfEnemies { get; }
        int NumberOfAllies { get; }
        int NumberOfFreeTiles { get; }

        Entity GetRandomEnemy();
        Entity GetRandomAlly();
        Tile GetRandomFreeTile();
    }
}
