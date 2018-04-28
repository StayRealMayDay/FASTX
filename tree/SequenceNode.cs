using System.Collections.Generic;

namespace FASTX.tree
{
    public class SequenceNode<T>
    {

        /// <summary>
        /// to point to the father node of this node
        /// </summary>
        public SequenceNode<T> Parent { get; private set; }

        /// <summary>
        /// each frequence correspond to a sequenceNode in the sequence Tree
        /// </summary>
        public Sequence<T> Sequence { get; private set; }

        /// <summary>
        /// store the support of this sequence
        /// </summary>
        public int Support { get; private set; }

        /// <summary>
        /// store the VIL of this sequence
        /// </summary>
        public VerticalIdList VerticalIdList { get; private set; }
        
        /// <summary>
        /// store the children of this Sequence Node
        /// </summary>
        private List<SequenceNode<T>> Children = new List<SequenceNode<T>>();

        /// <summary>
        /// init a sequence Node
        /// </summary>
        /// <param name="verticalIdList"></param>
        /// <param name="sequence"></param>
        /// <param name="parent"></param>
        /// <param name="support"></param>
        public SequenceNode(VerticalIdList verticalIdList, Sequence<T> sequence, SequenceNode<T> parent, int support)
        {
            VerticalIdList = verticalIdList;
            Sequence = sequence;
            Parent = parent;
            Support = support;
        }

        /// <summary>
        /// A function to get the Children of this sequence Node
        /// </summary>
        public List<SequenceNode<T>> GetChildren => Children;
    }
}