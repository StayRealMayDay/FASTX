using System.Collections.Generic;
using System.Linq;

namespace FASTX
{
    public class Sequence<T>
    {

        public Sequence(params Itemset<T>[] itemsets)
        {
            foreach (var itemset in itemsets)
            {
                Elements.AddLast(itemset);
            }
        }

        public void AddItemset(Itemset<T> itemset)
        {
            Elements.AddLast(itemset);
        }

        public Itemset<T> GetLastItemset()
        {
            return Elements.Last();
        }

        public T GetLastItem()
        {
           return GetLastItemset().Last();
        }

        public int GetLength()
        {
            return Elements.Count;
        }

        private LinkedList<Itemset<T>> Elements = new LinkedList<Itemset<T>>();
        
    }
}