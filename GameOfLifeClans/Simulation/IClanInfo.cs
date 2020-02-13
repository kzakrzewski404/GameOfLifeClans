namespace GameOfLifeClans.Simulation
{
    public interface IClanInfo
    {
        int Id { get; }
        IClanStrength Strength { get; }
    }
}
