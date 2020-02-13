using GameOfLifeClans.Ai.Entities;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai.Senses.Vision;
using GameOfLifeClans.Map.Data;
using GameOfLifeClans.Simulation.Clan;


namespace GameOfLifeClans.UnitTests.TestsTools
{
    public class VisionResultTestTools
    {
        private VisionResult _result;
        private EntityFactory _factory = new EntityFactory();
        private IClanInfo _fakedAllyClanInfo = new ClanInfoFaker(0);
        private IClanInfo _fakedEnemyClanInfo = new ClanInfoFaker(1);


        public VisionResultTestTools(int startingFreeTiles = 0, int startingAllies = 0, int startingEnemies = 0)
        {
            _result = new VisionResult();

            for (int i = 0; i < startingFreeTiles; i++)
            {
                _result.FreeTiles.Add(new Tile(0, 0, null, null));
            }
            for (int i = 0; i < startingAllies; i++)
            {
                _result.Allies.Add(_factory.Create(EntityId.Headquarter, _fakedAllyClanInfo));
            }
            for (int i = 0; i < startingEnemies; i++)
            {
                _result.Enemies.Add(_factory.Create(EntityId.Headquarter, _fakedEnemyClanInfo));
            }
        }


        public IVisionResultCreating GetAsIVisionResultCreating() => _result;

        public IVisionResult GetAsIVisionResult() => _result;
    }
}
