using GameOfLifeClans.Ai;


namespace GameOfLifeClans.Map.Data
{
    public interface IOccupiable
    {
        void MoveHere(Entity invoker);
    }
}
