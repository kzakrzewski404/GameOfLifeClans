using GameOfLifeClans.Simulation.Clan;


namespace GameOfLifeClans.UnitTests.TestsTools
{
    public class ClanInfoFaker : IClanInfo
    {
        public int Id { get; private set; }

        public IClanStrength Strength => throw new System.NotImplementedException();


        public ClanInfoFaker(int id)
        {
            Id = id;
        }
    }
}
