using System.Collections.Generic;

namespace FASTX.tree
{
    public class ItemsetNode<T>
    {
        private List<ItemsetNode<T>> ChildrenNodes = new List<ItemsetNode<T>>();

        public ItemsetNode()
        {
            Position = -1;
        }

        public ItemsetNode(Itemset<T> itemset, ItemsetNode<T> parent, SparseIdList sparseIdList, int position)
        {
            Itemset = itemset;
            Parent = parent;
            SparseIdList = sparseIdList;
            Position = position;
        }

        public List<ItemsetNode<T>> GetChildren => ChildrenNodes;
        public int Position { get; private set; }
        
        public  ItemsetNode<T> Parent { get; private set; }
        
        public Itemset<T> Itemset { get; private set; }
        
        public  SparseIdList SparseIdList { get; private set; }
    }
}