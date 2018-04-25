using System.Collections.Generic;

namespace FASTX.tree
{
    public class ItemsetNode<T>
    {
        private int position;
        private List<ItemsetNode<T>> childrenNodes = new List<ItemsetNode<T>>();
        private ItemsetNode<T> parent;
        private Itemset<T> itemset;
    }
}