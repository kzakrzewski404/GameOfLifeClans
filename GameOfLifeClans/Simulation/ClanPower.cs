namespace GameOfLifeClans.Simulation
{
    public class ClanPower
    {
        public int ControlledTerritory {get; private set;}
        public int DamageBonus => ControlledTerritory / 10;
        public int DefenceBonus => ControlledTerritory / 25;


        public ClanPower()
        {
            ControlledTerritory = 0;
        }


        public void LoseTerritory() => ControlledTerritory--;
        public void GainTerritory() => ControlledTerritory++;
    }
}
