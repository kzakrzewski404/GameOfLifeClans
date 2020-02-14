using GameOfLifeClans.Simulation.Clan;


namespace GameOfLifeClans.UnitTests.TestsTools
{
    public class ClanInfoFaker : IClanInfo
    {
        public int Id { get; private set; }
        private IClanStrength _str = new ClanStrength();

        public IClanStrength Strength => _str;


        public ClanInfoFaker(int id)
        {
            Id = id;
        }
    }
}
