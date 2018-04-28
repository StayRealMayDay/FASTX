using System.Collections.Generic;
using System.Linq;
using FASTX.tree;

namespace FASTX
{
    public class FAST<T>
    {


        private DataSet DataSet { get; set; }

        private SequenceTree<string> SequenceTree { get; set; }

        private void ItemsetExtension()
        {
            var itemsetTree = new ItemsetTree<string>();
            var root = itemsetTree.GetRoot;
            var queue = new Queue<ItemsetNode<string>>();
            var position = 0;
            ItemsetNode<string> node;
            foreach (var item in DataSet.GetItemSILDic())
            {
                var tempNode = itemsetTree.AddChild(root, new Itemset<string>(item.Key), item.Value, position++);
                queue.Enqueue(tempNode);
            }

            while (queue.Count != 0)
            {
                var tempNode = queue.Dequeue();
                ItemsetExtension(itemsetTree, tempNode);
                foreach (var child in tempNode.GetChildren)
                {
                    queue.Enqueue(child);
                }
            }
        }

        private void ItemsetExtension(ItemsetTree<string> itemsetTree, ItemsetNode<string> node)
        {
            var position = 0;
            var children = node.Parent.GetChildren;
            for (int i = node.Position + 1; i < children.Count; i++)
            {
                var rightBrother = children[i];
                var SIL = SparseIdList.IStep(node.SparseIdList, rightBrother.SparseIdList);
                if (SIL.Support > DataSet.MinSupport)
                {
                    var newItemset = node.Itemset.Clone();
                    newItemset.AddItems(rightBrother.Itemset.Last());
                    DataSet.GetItemSILDic().Add(newItemset.Display(), SIL);
                    itemsetTree.AddChild(node, newItemset, SIL, position);
                    position++;
                }
            }
            
        }
    }
}