using System.Runtime.CompilerServices;

namespace FASTX
{
    public class ListNode
    {
       

        public ListNode(ListNode next, int spareId)
        {
            Next = next;
            GetSpareId = spareId;
        }

        private ListNode Next { get; set; }

        private int GetSpareId { get; set; }

        public ListNode Brfore(ListNode node)
        {
            while (null != node)
            {
                if (this.GetSpareId < node.GetSpareId)
                {
                    return node;
                }

                node = node.Next;
            }
            return null;
        }

    }
}