using GameOfLifeClans.Ai;


namespace GameOfLifeClans.Map.Data
{
    public interface IOccupiable
    {
        int ClanOwnershipId { get; }

        void MoveHere(Entity invoker);
    }
}
