namespace GameOfLifeClans.Simulation
{
    public interface IClanStrength
    {
        int ControlledTerritory { get; }
        float DamageBonusMultiplier { get; }
        float DefenceBonusMultiplier { get; }
    }
}
