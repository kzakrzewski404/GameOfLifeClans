using GameOfLifeClans.Simulation.Config;


namespace GameOfLifeClans.Simulation.Clan
{
    public class ClanStrength : IClanStrengthController
    {
        public int ControlledTerritory {get; private set;}

        public float DamageBonusMultiplier => 1.0f + (ControlledTerritory * Multipliers.DAMAGE_MULTIPLIER_BONUS);
        public float DefenceBonusMultiplier => 1.0f + (ControlledTerritory * Multipliers.DEFENCE_MULTIPLIER_BONUS);

        public ClanStrength()
        {
            ControlledTerritory = 0;
        }


        public void LoseTerritory() => ControlledTerritory--;
        public void GainTerritory() => ControlledTerritory++;
    }
}
