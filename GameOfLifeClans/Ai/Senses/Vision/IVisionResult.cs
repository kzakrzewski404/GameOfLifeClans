using GameOfLifeClans.Ai.Entities;
using GameOfLifeClans.Map.Data;


namespace GameOfLifeClans.Ai.Senses.Vision
{
    public interface IVisionResult
    {
        bool IsEnemyFound { get; }
        bool IsAllyFound { get; }
        bool IsAllyToHealFound { get; }
        bool IsFreeTileFound { get; }
        int NumberOfEnemies { get; }
        int NumberOfAllies { get; }
        int NumberOfAlliesToHeal { get; }
        int NumberOfFreeTiles { get; }


        IAttackable GetRandomEnemy();
        Entity GetRandomAlly();
        IHealable GetRandomAllyWhoRequireHealing();
        Tile GetRandomFreeTile();
    }
}
