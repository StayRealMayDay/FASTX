using System;
using System.Collections.Generic;
using System.Linq;
using FASTX.tree;

namespace FASTX
{
    public class FAST<T>
    {
        /// <summary>
        /// store all the frequent itemset SIL in a dictionary
        /// </summary>
        private DataSet DataSet { get; set; }

        /// <summary>
        /// itemset tree
        /// </summary>
        private ItemsetTree<string> ItemsetTree { get; set; }

        /// <summary>
        /// sequence tree
        /// </summary>
        private SequenceTree<string> SequenceTree { get; set; }

        /// <summary>
        /// Get the sequence Tree
        /// </summary>
        public SequenceTree<string> GetSequenceTree => SequenceTree;

        /// <summary>
        /// Get Itemset tree
        /// </summary>
        public ItemsetTree<string> GeItemsetTree => ItemsetTree;

        /// <summary>
        /// use a queue to pick up all the itemset node and do extension
        /// </summary>
        private void ItemsetExtension()
        {
            ItemsetTree = new ItemsetTree<string>();
            var root = ItemsetTree.GetRoot;
            var queue = new Queue<ItemsetNode<string>>();
            var position = 0;
            ItemsetNode<string> node;
            // first insert all the item in the itemset tree as the children of the root's children
            foreach (var item in DataSet.GetItemSILDic())
            {
                node = ItemsetTree.AddChild(root, new Itemset<string>(item.Key), item.Value, position++);
                queue.Enqueue(node);
            }

            // then pick up every node of the root node'children do the extension
            while (queue.Count != 0)
            {
                node = queue.Dequeue();
                ItemsetExtension(ItemsetTree, node);
                foreach (var child in node.GetChildren)
                {
                    queue.Enqueue(child);
                }
            }
        }

        /// <summary>
        /// extension a itemset, we pick up the node's SIL, and its right borther's SIL, we do IStep and if its support bigger than minSupport
        /// we neet to merge the itemset of the node whit the item in the last position of the node's right brother's itemset
        /// then we insert the new itemset into the itemset tree with the position
        /// we store all the itemset in the DataSet's ItemSILDic
        /// </summary>
        /// <param name="itemsetTree"></param>
        /// <param name="node"></param>
        private void ItemsetExtension(ItemsetTree<string> itemsetTree, ItemsetNode<string> node)
        {
            var position = 0;
            var children = node.Parent.GetChildren;
            for (int i = node.Position + 1; i < children.Count; i++)
            {
                var rightBrother = children[i];
                var SIL = SparseIdList.IStep(node.SparseIdList, rightBrother.SparseIdList);
                if (SIL.Support >= DataSet.MinSupport)
                {
                    var newItemset = node.Itemset.Clone();
                    newItemset.AddItems(rightBrother.Itemset.Last());
                    DataSet.GetItemSILDic().Add(newItemset.Display(), SIL);
                    itemsetTree.AddChild(node, newItemset, SIL, position);
                    position++;
                }
            }
        }

        /// <summary>
        /// the same with the itemset extension ,picku all the sequence node and do sequence extension
        /// </summary>
        /// <returns></returns>
        private SequenceTree<string> SequenceExtension()
        {
            SequenceTree = new SequenceTree<string>(DataSet.NumberOfRows);

            var queue = new Queue<SequenceNode<string>>();
            Sequence<string> s;
            SequenceNode<string> node;
            // first we insert all the itemset into the sequence tree as the children of the root node
            foreach (var frequentItemset in DataSet.GetItemSILDic())
            {
                s = new Sequence<string>(new Itemset<string>(frequentItemset.Key.Split(' ')));
                var VIL = frequentItemset.Value.GetStartingVIL();
                node = SequenceTree.AddChild(SequenceTree.Root, s, VIL, frequentItemset.Value.Support);
                queue.Enqueue(node);
            }

            // then we pick every node of the root's children then do the the sequence extension
            while (queue.Count != 0)
            {
                node = queue.Dequeue();
                SequenceExtension(SequenceTree, node);
                foreach (var child in node.GetChildren)
                {
                    queue.Enqueue(child);
                }
            }

            return SequenceTree;
        }

        /// <summary>
        /// extension a sequence,pick all the VIL of the sequence's brothers(include itself) one by one to do the VIL merge
        /// we need a ListNode[] array to store the new sequence's VIL
        /// after the merge operation, if the new sequence is ferquent, we need to insert the new sequence node(the brother node) to the sequence tree as the child of this sequence node 
        /// </summary>
        /// <param name="sequenceTree"></param>
        /// <param name="node"></param>
        private void SequenceExtension(SequenceTree<string> sequenceTree, SequenceNode<string> node)
        {
            // to stiore the support
            var count = 0;
            //to store the new sequence VIL
            ListNode[] newPositionList;
            ListNode listNode, listBrotherNode;
            // pick up the VIL of this Node
            var nodeVIL = node.VerticalIdList;
            //store those ListNode which have the same relative position  ,first int is the relative position, and the second int is the sequenceID
            //used to create the VIL of the new sequence.
            var positionDic = new Dictionary<int, Dictionary<int, ListNode>>();
            // used to store the VIL of its brothers
            VerticalIdList brotherVIL;
            // in this way we get all the node of its brother and itself 
            var brothers = node.Parent.GetChildren;
            foreach (var brotherNode in brothers)
            {
                //init it
                newPositionList = new ListNode[nodeVIL.Elements.Length];
                //clear the positionDic
                positionDic.Clear();
                //pick up the VIL of the brother Node
                brotherVIL = brotherNode.VerticalIdList;
                for (int i = 0; i < nodeVIL.Elements.Length; i++)
                {
                    listNode = nodeVIL.Elements[i];
                    listBrotherNode = brotherVIL.Elements[i];
                    // if this List node is null or its brother is null just ignore it
                    if ((listNode == null) || (listBrotherNode == null))
                    {
                        continue;
                        ;
                    }

                    // find the node which listBrotherNode SID bigger than listNode SID
                    while ((listBrotherNode != null) && (listNode.GetSparseId >= listBrotherNode.GetSparseId))
                    {
                        listBrotherNode = listBrotherNode.GetNext;
                    }

                    // store all the back node with the relative position in a dictionary(relativeposition, dictionary(sequenceID, ListNode))
                    while (listBrotherNode != null)
                    {
                        var relativePosition = listBrotherNode.GetSparseId - listNode.GetSparseId;
                        if (positionDic.ContainsKey(relativePosition))
                        {
                            positionDic[relativePosition].Add(i, listBrotherNode);
                        }
                        else
                        {
                            positionDic.Add(relativePosition, new Dictionary<int, ListNode>());
                            positionDic[relativePosition].Add(i, listBrotherNode);
                        }

                        listBrotherNode = listBrotherNode.GetNext;
                    }
                }
                // if the have the relative position sequence if frequent, create its VIL and then insert in to the tree with its VIL
                foreach (var keyValue in positionDic)
                {
                    if (keyValue.Value.Count >= DataSet.MinSupport)
                    {
                        foreach (var sequenceIdWithListNode in keyValue.Value)
                        {
                            newPositionList[sequenceIdWithListNode.Key] = sequenceIdWithListNode.Value;
                        }
                        var sequence = node.Sequence.Clone();
                        sequence.AddItemset(brotherNode.Sequence.GetLastItemset());
                        sequenceTree.AddChild(node, sequence, new VerticalIdList(newPositionList, keyValue.Value.Count),
                            keyValue.Value.Count);
                    }
                }
            }
        }
        
//         private void SequenceExtension(SequenceTree<string> sequenceTree, SequenceNode<string> node)
//        {
//            // to stiore the support
//            var count = 0;
//            //to store the new sequence VIL
//            ListNode[] newPositionList;
//            ListNode listNode, listBrotherNode;
//            // pick up the VIL of this Node
//            var nodeVIL = node.VerticalIdList;
//            
//            // used to store the VIL of its brothers
//            VerticalIdList brotherVIL;
//            // in this way we get all the node of its brother and itself 
//            var brothers = node.Parent.GetChildren;
//            foreach (var brotherNode in brothers)
//            {
//                //init it
//                newPositionList = new ListNode[nodeVIL.Elements.Length];
//                //pick up the VIL of the brother Node
//                brotherVIL = brotherNode.VerticalIdList;
//                for (int i = 0; i < nodeVIL.Elements.Length; i++)
//                {
//                    listNode = nodeVIL.Elements[i];
//                    listBrotherNode = brotherVIL.Elements[i];
//                    // if this List node is null or its brother is null just ignore it
//                    if ((listNode == null) || (listBrotherNode == null))
//                    {
//                        continue;
//                        ;
//                    }                    
//                    if (listNode.GetSparseId < listBrotherNode.GetSparseId)
//                    {
//                        newPositionList[i] = listBrotherNode;
//                        count++;
//                    }
//
//                    // find the brother node which the sequence position is behind this node
//                    if (listNode.GetSparseId >= listBrotherNode.GetSparseId)
//                    {
//                        while ((listBrotherNode != null) && (listNode.GetSparseId >= listBrotherNode.GetSparseId))
//                        {
//                            listBrotherNode = listBrotherNode.GetNext;
//                        }
//
//                        if (listBrotherNode != null && listNode.GetSparseId < listBrotherNode.GetSparseId)
//                        {
//                            newPositionList[i] = listBrotherNode;
//                            count++;
//                        }
//                    }
//                }
//
//                //if the borthe node exist we insert it in to the sequence tree whih the VIL
//                if (count > DataSet.MinSupport)
//                {
//                    var sequence = node.Sequence.Clone();
//                    sequence.AddItemset(brotherNode.Sequence.GetLastItemset());
//                    sequenceTree.AddChild(node, sequence, new VerticalIdList(newPositionList, count), count);
//                }
//
//                count = 0;
//            }
//        }

        /// <summary>
        /// run this algorithm
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="support"></param>
        public void RunAlgorithm(string filePath, int support)
        {
            DataSet = DataSet.ReadData(filePath, support);
            ItemsetExtension();
            SequenceTree = SequenceExtension();
        }
    }
}