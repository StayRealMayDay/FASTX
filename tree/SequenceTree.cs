using System.Collections.Generic;

namespace FASTX.tree
{
    public class SequenceTree<T>
    {

        public SequenceNode<T> Root { get; private set; }

        public SequenceTree(int NumSequences)
        {
            Root = new SequenceNode<T>(null, new Sequence<T>(), null,  NumSequences);
        }

        public SequenceNode<T> AddChild(SequenceNode<T> parent, Sequence<T> sequence, VerticalIdList verticalIdList, int support)
        {
            var newNode = new SequenceNode<T>(verticalIdList, sequence, parent, support);
            parent.GetChildren.Add(newNode);
            return newNode;
        }

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