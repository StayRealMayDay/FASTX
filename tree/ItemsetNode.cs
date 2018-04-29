using System.Collections.Generic;

namespace FASTX.tree
{
    public class ItemsetNode<T>
    {
        

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

        
        /// <summary>
        /// store children of this node
        /// </summary>
        private List<ItemsetNode<T>> ChildrenNodes = new List<ItemsetNode<T>>();
        
        public List<ItemsetNode<T>> GetChildren => ChildrenNodes;
        
        /// <summary>
        /// store the position of this itemset in the children of its parent node
        /// </summary>
        public int Position { get; private set; }
        
        /// <summary>
        /// store the parent of this node in the tree
        /// </summary>
        public  ItemsetNode<T> Parent { get; private set; }
        
        /// <summary>
        /// store itemset of this node
        /// </summary>
        public Itemset<T> Itemset { get; private set; }

        /// <summary>
        /// Get the Itemset of this Node
        /// </summary>
        public Itemset<T> GetItemset => Itemset;
        /// <summary>
        /// store the SIL of this Itemset
        /// </summary>
        public  SparseIdList SparseIdList { get; private set; }
    }
}