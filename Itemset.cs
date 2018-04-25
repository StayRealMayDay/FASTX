﻿using System.Collections;
using System.Collections.Generic;

namespace FASTX
{
    public class Itemset<T> : IList<T>
    {
        public void AddItems(params T[] items)
        {
            foreach (var item in items)
            {
                Element.Add(item);
            }
        }

        public IEnumerator<T> GetEnumerator() => Element.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(T item) => Element.Add(item);

        public void Clear() => Element.Clear();

        public bool Contains(T item) => Element.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => Element.CopyTo(array, arrayIndex);

        public bool Remove(T item) => Element.Remove(item);

        public int Count => Element.Count;
        
        public bool IsReadOnly => false;
        
        public int IndexOf(T item) =>  Element.IndexOf(item);
        

        public void Insert(int index, T item) => Element.Insert(index, item);

        public void RemoveAt(int index) => Element.RemoveAt(index);

        private List<T> Element = new List<T>();
        
        public T this[int index]
        {
            get => Element[index];
            set => Element[index] = value;
        }
    }
}