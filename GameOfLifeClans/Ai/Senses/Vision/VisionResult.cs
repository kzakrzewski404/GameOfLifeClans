using GameOfLifeClans.Ai.Entities;
using GameOfLifeClans.Generics;
using GameOfLifeClans.Map.Data;


namespace GameOfLifeClans.Ai.Senses.Vision
{
    public class VisionResult : IVisionResultCreating
    {
        public ItemsContainer<Tile> FreeTiles { get; private set; }
        public ItemsContainer<Entity> Allies { get; private set; }
        public ItemsContainer<Entity> Enemies { get; private set; }


        public bool IsEnemyFound => Enemies.IsNotEmpty;
        public bool IsAllyFound => Allies.IsNotEmpty;
        public bool IsFreeTileFound => FreeTiles.IsNotEmpty;
        public int NumberOfEnemies => Enemies.Count;
        public int NumberOfAllies => Allies.Count;
        public int NumberOfFreeTiles => FreeTiles.Count;


        public VisionResult()
        {
            FreeTiles = new ItemsContainer<Tile>();
            Allies = new ItemsContainer<Entity>();
            Enemies = new ItemsContainer<Entity>();
        }



        public void AddFreeTile(Tile tile) => FreeTiles.Add(tile);

        public void AddAlly(Entity ally) => Allies.Add(ally);

        public void AddEnemy(Entity enemy) => Enemies.Add(enemy);

        public IAttackable GetRandomEnemy() => Enemies.PickRandom;

        public Entity GetRandomAlly() => Allies.PickRandom;

        public Tile GetRandomFreeTile() => FreeTiles.PickRandom;
    }
}