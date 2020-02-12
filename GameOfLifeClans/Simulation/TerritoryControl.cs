namespace GameOfLifeClans.Simulation
{
    public class TerritoryControl
    {
        public int ConqueredTerritories { get; private set; }


        public TerritoryControl()
        {
            ConqueredTerritories = 0;
        }


        public void GainTerritory() => ConqueredTerritories++;
        public void LoseTerritory() => ConqueredTerritories--;
    }
}
