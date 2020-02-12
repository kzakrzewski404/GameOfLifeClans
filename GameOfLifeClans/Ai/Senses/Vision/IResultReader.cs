using GameOfLifeClans.Map.Data;


namespace GameOfLifeClans.Ai.Senses.Vision
{
    public interface IResultReader
    {
        bool IsEnemyFound { get; }
        bool IsAllyFound { get; }
        bool IsFreeTileFound { get; }

        Entity GetRandomEnemy();
        Entity GetRandomAlly();
        Tile GetRandomFreeTile();
    }
}
