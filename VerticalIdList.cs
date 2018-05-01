namespace FASTX
{
    public class VerticalIdList
    {
        public VerticalIdList(ListNode[] elements, int support)
        {
            Elements = elements;
            Support = support;
        }

        /// <summary>
        /// store the support of this VIL
        /// </summary>
        public int Support { get; private set; }

        /// <summary>
        /// a array to store the VIL's ListNodes
        /// </summary>
        public ListNode[] Elements { get; private set; }
        
        
    }
}