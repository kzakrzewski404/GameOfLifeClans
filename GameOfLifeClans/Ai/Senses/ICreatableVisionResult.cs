using GameOfLifeClans.Map.Data;


namespace GameOfLifeClans.Ai.Senses
{
    public interface ICreatableVisionResult
    {
        void AddFreeTile(Tile tile);
        void AddAlly(Entity ally);
        void AddEnemy(Entity enemy);
    }
}
