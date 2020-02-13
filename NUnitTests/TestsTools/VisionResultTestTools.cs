using GameOfLifeClans.Ai;
using GameOfLifeClans.Ai.Enums;
using GameOfLifeClans.Ai.Senses;
using GameOfLifeClans.Map.Data;


namespace GameOfLifeClans.UnitTests.TestsTools
{
    public class VisionResultTestTools
    {
        private VisionResult _result;
        private EntityFactory _factory = new EntityFactory();


        public VisionResultTestTools(int startingFreeTiles = 0, int startingAllies = 0, int startingEnemies = 0)
        {
            _result = new VisionResult();

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


        public ICreatableVisionResult GetAsCreatableResult() => _result as ICreatableVisionResult;

        public IReadableVisionResult GetAsReadableResult() => _result as IReadableVisionResult;

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
