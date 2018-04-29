using System.Collections.Generic;

namespace FASTX.tree
{
    public class SequenceTree<T>
    {
        /// <summary>
        /// root node of the sequence tree
        /// </summary>
        public SequenceNode<T> Root { get; private set; }

        /// <summary>
        /// init a seqnence tree ,actually init the root node ,the root node do not have VIL or Parent 
        /// </summary>
        /// <param name="NumSequences"></param>
        public SequenceTree(int NumSequences)
        {
            Root = new SequenceNode<T>(null, new Sequence<T>(), null,  NumSequences);
        }

        /// <summary>
        /// a function to add node to the sequence tree
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="sequence"></param>
        /// <param name="verticalIdList"></param>
        /// <param name="support"></param>
        /// <returns></returns>
        public SequenceNode<T> AddChild(SequenceNode<T> parent, Sequence<T> sequence, VerticalIdList verticalIdList, int support)
        {
            var newNode = new SequenceNode<T>(verticalIdList, sequence, parent, support);
            parent.GetChildren.Add(newNode);
            return newNode;
        }
        
        /// <summary>
        /// use a List to store all the sequence node with the DFS
        /// </summary>
        /// <param name="Tree"></param>
        /// <returns></returns>
        public static List<SequenceNode<T>> Visit(SequenceTree<T> Tree)
        {
            var queue = new Queue<SequenceNode<T>>();
            var result = new List<SequenceNode<T>>();
            foreach (var child in Tree.Root.GetChildren)
            {
                queue.Enqueue(child);
            }

            while (queue.Count != 0)
            {
                var currentNode = queue.Dequeue();
                result.Add(currentNode);
                foreach (var child in currentNode.GetChildren)
                {
                    queue.Enqueue(child);
                }
            }

            return result;
        }
        
    }
}