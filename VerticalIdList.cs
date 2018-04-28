namespace FASTX
{
    public class VerticalIdList
    {
        public VerticalIdList(ListNode[] elements, int support)
        {
            Elements = elements;
            Support = support;
        }

        public int Support { get; private set; }

        public ListNode[] Elements { get; private set; }
        
        
    }
}