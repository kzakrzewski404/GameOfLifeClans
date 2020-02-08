using System.Collections.Generic;
using System;


namespace GameOfLifeClans.Generics
{
    public class ItemsContainer<T>
    {
        public List<T> Results { get; private set; }
        private static Random rnd = new Random();

        public bool IsNotEmpty => Results.Count > 0;
        public int Count => Results.Count;
        public T PickRandom => Results[rnd.Next(0, Results.Count)];
        
        
        public T PickRandomAndRemoveFromList()
        {
            T item = PickRandom;
            Results.Remove(item);
            return item;
        }

        public void Add(T item) => Results.Add(item);


        public ItemsContainer()
        {
            Results = new List<T>();
        }
    }
}
