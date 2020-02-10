using GameOfLifeClans.Generics;
using GameOfLifeClans.Map.Data;


namespace GameOfLifeClans.Ai.Senses.Vision
{
    public class Result
    {
        public Result()
        {
            FreeTiles = new ItemsContainer<Tile>();
            Allies = new ItemsContainer<Entity>();
            Enemies = new ItemsContainer<Entity>();
        }


        public ItemsContainer<Tile> FreeTiles { get; private set; }
        public ItemsContainer<Entity> Allies { get; private set; }
        public ItemsContainer<Entity> Enemies { get; private set; }
    }
}
