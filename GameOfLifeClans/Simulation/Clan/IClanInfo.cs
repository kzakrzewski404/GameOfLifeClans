namespace GameOfLifeClans.Simulation.Clan
{
    public interface IClanInfo
    {
        int Id { get; }
        IClanStrength Strength { get; }
    }
}
