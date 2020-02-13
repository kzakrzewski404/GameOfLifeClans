namespace GameOfLifeClans.Simulation.Clan
{
    public interface IClanStrengthController : IClanStrength
    {
        void LoseTerritory();
        void GainTerritory();
    }
}
