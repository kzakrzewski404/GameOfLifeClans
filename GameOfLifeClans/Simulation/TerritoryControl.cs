namespace GameOfLifeClans.Simulation
{
    public class TerritoryControl
    {
        public int ConqueredTerritories { get; private set; }


        public TerritoryControl()
        {
            ConqueredTerritories = 0;
        }


        public void Gain() => ConqueredTerritories++;
        public void Lose() => ConqueredTerritories--;
    }
}
