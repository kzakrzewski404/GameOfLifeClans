namespace GameOfLifeClans.Simulation
{
    public interface IClanStrengthController : IClanStrength
    {
        void LoseTerritory();
        void GainTerritory();
    }
}
