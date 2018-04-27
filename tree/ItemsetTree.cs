namespace FASTX.tree
{
    public class ItemsetTree<T>
    {
        /// <summary>
        /// Root node of the Itemset tree
        /// </summary>
        private ItemsetNode<T> Root = new ItemsetNode<T>();

        public ItemsetNode<T> AddChild(ItemsetNode<T> parent, Itemset<T> itemset, SparseIdList sparseIdList,
            int position)
        {
            var newNode = new ItemsetNode<T>(itemset, parent, sparseIdList, position);
            parent.GetChildren.Add(newNode);
            return newNode;
        }

        public ItemsetNode<T> GetRoot => Root;
    }
}