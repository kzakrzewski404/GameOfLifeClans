namespace GameOfLifeClans.Simulation.Clan
{
    public interface IClanStrengthChanger : IClanStrength
    {
        void LoseTerritory();
        void GainTerritory(int numberOfGainedTerritory = 1);
    }
}
