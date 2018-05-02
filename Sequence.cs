using System;
using System.Collections.Generic;
using System.Linq;

namespace FASTX
{
    public class Sequence<T>
    {

        /// <summary>
        /// constructor you can add some itemsets into the sequence one time. you pass the parameter like ( 1, 2, 3)
        /// </summary>
        /// <param name="itemsets"></param>
        public Sequence(params Itemset<T>[] itemsets)
        {
            foreach (var itemset in itemsets)
            {
                Elements.Add(itemset);
            }
        }

        /// <summary>
        /// constructor you can add an item with a init relative position
        /// </summary>
        /// <param name="itemset"></param>
        /// <param name="relativePositions"></param>
        public Sequence(Itemset<T> itemset,int relativePositions)
        {
            Elements.Add(itemset);
            RelativePosition.Add(relativePositions);
        }

        /// <summary>
        /// add an itemset at the end of the sequence
        /// </summary>
        /// <param name="itemset"></param>
        public void AddItemset(Itemset<T> itemset)
        {
            Elements.Add(itemset);
        }

        /// <summary>
        /// add an itemset at the end of the sequence and with its relative position
        /// </summary>
        /// <param name="itemset"></param>
        /// <param name="relativePosition"></param>
        public void AddItemsetWithRelativePosition(Itemset<T> itemset, int relativePosition)
        {
            Elements.Add(itemset);
            RelativePosition.Add(relativePosition);
        }

        /// <summary>
        /// get the last itemset in the sequence
        /// </summary>
        /// <returns></returns>
        public Itemset<T> GetLastItemset()
        {
            return Elements.Last();
        }

        /// <summary>
        /// get the last item of the last itemset in the sequence 
        /// </summary>
        /// <returns></returns>
        public T GetLastItem()
        {
           return GetLastItemset().Last();
        }

        /// <summary>
        /// get the length of the sequence
        /// </summary>
        /// <returns></returns>
        public int GetLength()
        {
            return Elements.Count;
        }

        /// <summary>
        /// clone a new same sequence, usually used in sequence extension
        /// </summary>
        /// <returns></returns>
        public Sequence<T> Clone()
        {
            var other = new Sequence<T>();
//            foreach (var element in Elements)
//            {
//                other.AddItemset(element.Clone());
//            }

            for (int i = 0; i < Elements.Count; i++)
            {
                other.AddItemsetWithRelativePosition(Elements[i].Clone(), RelativePosition[i]);
            }

            return other;
        }

        /// <summary>
        /// sequence is a list of Itemset
        /// </summary>
        private List<Itemset<T>> Elements = new List<Itemset<T>>();

        /// <summary>
        /// store the relative position of each itemset in the sequence
        /// </summary>
        private List<int> RelativePosition = new List<int>();
        
        public List<Itemset<T>> GetElements => Elements;

    }
}