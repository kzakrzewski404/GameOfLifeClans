using GameOfLifeClans.Ai;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai.Senses.Vision;
using GameOfLifeClans.Map.Data;


namespace GameOfLifeClans.UnitTests.TestsTools
{
    public class ResultTestTools
    {
        private Result _result;
        private EntityFactory _factory = new EntityFactory();


        public ResultTestTools(int startingFreeTiles = 0, int startingAllies = 0, int startingEnemies = 0)
        {
            _result = new Result();

            for (int i = 0; i < startingFreeTiles; i++)
            {
                _result.FreeTiles.Add(new Tile(0, 0, null, null));
            }
            for (int i = 0; i < startingAllies; i++)
            {
                _result.Allies.Add(_factory.Create(EntityId.Headquarter, 0));
            }
            for (int i = 0; i < startingEnemies; i++)
            {
                _result.Enemies.Add(_factory.Create(EntityId.Headquarter, 1));
            }
        }


        public ICreatableResult GetAsCreatableResult() => _result as ICreatableResult;

        public IReadableResult GetAsReadableResult() => _result as IReadableResult;

        public Entity CreateAndAddEnemyToResult()
        {
            Entity enemy = CreateEntity(true);
            _result.AddEnemy(enemy);
            return enemy;
        }

        public Entity CreateAndAddAllyToResult()
        {
            Entity ally = CreateEntity(false);
            _result.AddAlly(ally);
            return ally;
        }

        public Tile CreateTileAndAddToResult()
        {
            Tile tile = new Tile(0, 0, null, null);
            _result.AddFreeTile(tile);
            return tile;
        }

        private Entity CreateEntity(bool isEnemy)
        {
            return _factory.Create(EntityId.Headquarter, isEnemy ? 1 : 0);
        }
    }
}
