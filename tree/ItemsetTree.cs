using System;

namespace FASTX.tree
{
    public class ItemsetTree<T>
    {
        /// <summary>
        /// Root node of the Itemset tree
        /// </summary>
        private ItemsetNode<T> Root = new ItemsetNode<T>();

        /// <summary>
        /// add a node to the Itemset Tree and you neet provide the father node of this insert node
        /// and the itemset of this node and its SIL and the position of this insert node in its father node's
        /// children node
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="itemset"></param>
        /// <param name="sparseIdList"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public ItemsetNode<T> AddChild(ItemsetNode<T> parent, Itemset<T> itemset, SparseIdList sparseIdList,
            int position)
        {
            var newNode = new ItemsetNode<T>(itemset, parent, sparseIdList, position);
            parent.GetChildren.Add(newNode);
            return newNode;
        }

        public ItemsetNode<T> GetRoot => Root;

        /// <summary>
        /// print the itemset tree
        /// </summary>
        public void PrintTheTree()
        {
            Display(Root, "--");
        }

        /// <summary>
        /// recusive print every node of the tree
        /// </summary>
        /// <param name="itemsetNode"></param>
        /// <param name="gap"></param>
        private void Display(ItemsetNode<T> itemsetNode, string gap)
        {
            if (itemsetNode.GetChildren.Count == 0) return;
            foreach (var node in itemsetNode.GetChildren)
            {
                var newGap = gap;
                Console.Write(gap);
                foreach (var item in node.GetItemset)
                {
                    newGap += "--";
                    Console.Write(" " + item);
                }
                Console.WriteLine();
                Display(node, newGap);
            }
        }
    }
}