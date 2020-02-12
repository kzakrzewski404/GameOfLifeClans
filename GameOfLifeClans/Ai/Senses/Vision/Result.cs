using GameOfLifeClans.Generics;
using GameOfLifeClans.Map.Data;


namespace GameOfLifeClans.Ai.Senses.Vision
{
    public class Result : IResultCreator, IResultReader
    {
        private ItemsContainer<Tile> _freeTiles;
        private ItemsContainer<Entity> _allies;
        private ItemsContainer<Entity> _enemies;


        public bool IsEnemyFound => _enemies.IsNotEmpty;
        public bool IsAllyFound => _allies.IsNotEmpty;
        public bool IsFreeTileFound => _freeTiles.IsNotEmpty;


        public Result()
        {
            _freeTiles = new ItemsContainer<Tile>();
            _allies = new ItemsContainer<Entity>();
            _enemies = new ItemsContainer<Entity>();
        }


        public void AddFreeTile(Tile tile) => _freeTiles.Add(tile);

        public void AddAlly(Entity ally) => _allies.Add(ally);

        public void AddEnemy(Entity enemy) => _enemies.Add(enemy);

        public Entity GetRandomEnemy() => _enemies.PickRandom;

        public Entity GetRandomAlly() => _allies.PickRandom;

        public Tile GetRandomFreeTile() => _freeTiles.PickRandom;
    }
}