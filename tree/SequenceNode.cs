using System.Collections.Generic;

namespace FASTX.tree
{
    public class SequenceNode<T>
    {

        public SequenceNode<T> Parent { get; private set; }

        public Sequence<T> Sequence { get; private set; }

        public int Support { get; private set; }

        public VerticalIdList VerticalIdList { get; private set; }
        
        private List<SequenceNode<T>> Children = new List<SequenceNode<T>>();

        public SequenceNode(VerticalIdList verticalIdList, Sequence<T> sequence, SequenceNode<T> parent, int support)
        {
            VerticalIdList = verticalIdList;
            Sequence = sequence;
            Parent = parent;
            Support = support;
        }

        public List<SequenceNode<T>> GetChildren => Children;
    }
}