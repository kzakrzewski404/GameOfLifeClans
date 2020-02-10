using System;
using System.Collections.Generic;


namespace GameOfLifeClans.Generics
{
    public class ItemsContainer<T>
    {
        private static Random _rnd = new Random();


        public ItemsContainer()
        {
            Items = new List<T>();
        }


        public List<T> Items { get; private set; }
        public bool IsNotEmpty => Items.Count > 0;
        public int Count => Items.Count;
        public T PickRandom => Items[_rnd.Next(0, Items.Count)];


        public void Add(T item) => Items.Add(item);

        public T PickRandomAndRemoveFromList()
        {
            T item = PickRandom;
            Items.Remove(item);
            return item;
        }
    }
}
