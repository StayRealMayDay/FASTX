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
                Elements.AddLast(itemset);
            }
        }

        /// <summary>
        /// add an itemset at the last of the sequence
        /// </summary>
        /// <param name="itemset"></param>
        public void AddItemset(Itemset<T> itemset)
        {
            Elements.AddLast(itemset);
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

        public Sequence<T> Clone()
        {
            var other = new Sequence<T>();
            foreach (var element in Elements)
            {
                other.AddItemset(element.Clone());
            }

            return other;
        }

        /// <summary>
        /// sequence is a list of Itemset
        /// </summary>
        private LinkedList<Itemset<T>> Elements = new LinkedList<Itemset<T>>();
        
    }
}