using System.Runtime.CompilerServices;

namespace FASTX
{
    public class ListNode
    {
        public ListNode(ListNode next, int sparesId)
        {
            Next = next;
            SparseId = sparesId;
        }

        public ListNode(int sparseId)
        {
            SparseId = sparseId;
        }

        /// <summary>
        /// point to the next ListNode in the same sequence
        /// </summary>
        private ListNode Next { get; set; }

        /// <summary>
        /// store the position of the item
        /// </summary>
        private int SparseId { get; set; }

        /// <summary>
        /// get the position
        /// </summary>
        public int GetSparseId => SparseId;

        /// <summary>
        /// get the next ListNode
        /// </summary>
        public ListNode GetNext => Next;

        /// <summary>
        /// set the next ListNode
        /// </summary>
        /// <param name="node"></param>
        public void SetNext(ListNode node)
        {
            Next = node;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public ListNode Brfore(ListNode node)
        {
            while (null != node)
            {
                if (this.SparseId < node.SparseId)
                {
                    return node;
                }

                node = node.Next;
            }

            return null;
        }
    }
}