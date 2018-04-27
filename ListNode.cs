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

        private ListNode Next { get; set; }

        private int SparseId { get; set; }

        public int GetSparseId => SparseId;

        public ListNode GetNext => Next;
        
        public void SetNext(ListNode node)
        {
            Next = node;
        }

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