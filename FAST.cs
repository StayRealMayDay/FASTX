using System.Collections.Generic;
using FASTX.tree;

namespace FASTX
{
    public class FAST<T>
    {


        private void ItemsetExtension()
        {
            var itemsetTree = new ItemsetTree<T>();
            var root = itemsetTree.GetRoot;
            var queue = new List<ItemsetNode<T>>();
            var position = 0;
            ItemsetNode<T> node;
            
        }
    }
}