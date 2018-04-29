using System.Collections.Generic;
using System.Linq;

namespace FASTX
{
    public class SparseIdList
    {
        /// <summary>
        /// use an array of the Transaction to store the SIL of an item, 
        /// </summary>
        private TransactionIds[] Vector;

        /// <summary>
        /// rows meas the number of the sequence , it indicates how long the vector should be
        /// </summary>
        /// <param name="rows"></param>
        public SparseIdList(int rows)
        {
            Vector = new TransactionIds[rows];
        }

        /// <summary>
        /// get the length of the SIL
        /// </summary>
        /// <returns></returns>
        public int GetLength()
        {
            return Vector.Length;
        }

        /// <summary>
        /// if we want to add an element to the SIL, we should provide the row (means which sequence) and the value(the position of the item in the sequence) information
        /// </summary>
        /// <param name="row"></param>
        /// <param name="value"></param>
        public void AddElement(int row, int value)
        {
            if (Vector[row] == null)
            {
                Vector[row] = new TransactionIds();

                Support++;
            }

            Vector[row].Add(new ListNode(value));
        }
        
        /// <summary>
        /// if we want to get an element of the SIL ,we neet to provide the row and col information to show which sequence (row) and which position(col) you want
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public ListNode GetElement(int row, int col)
        {
            if (Vector[row] != null)
            {
                if (col < Vector[row].GetLength())
                {
                    return Vector[row].GetNode(col);
                }
            }

            return null;
        }

        /// <summary>
        /// after we stored all the element of the item into the SIL ,we need to connect the ListNode which in the same sequence one by one
        /// </summary>
        public void SetNextElement()
        {
            for (int i = 0; i < Vector.Length; i++)
            {
                if (Vector[i] != null)
                {
                    for (int j = 0; j < Vector[i].GetLength(); j++)
                    {
                        if (j + 1 < Vector[i].GetLength())
                        {
                            Vector[i].GetNode(j).SetNext(Vector[i].GetNode(j + 1));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// this function used to do item extension , you provide two SIL about item a and b ,it return a SIL about (ab)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static SparseIdList IStep(SparseIdList a, SparseIdList b)
        {
            SparseIdList sparseIdList = new SparseIdList(a.GetLength());
            ListNode aNode, bNode;
            for (int i = 0; i < a.GetLength(); i++)
            {
                aNode = a.GetElement(i, 0);
                bNode = b.GetElement(i, 0);
                while ((aNode != null) && (bNode != null))
                {
                    if (aNode.GetSparseId == bNode.GetSparseId)
                    {
                        sparseIdList.AddElement(i, aNode.GetSparseId);
                        aNode = aNode.GetNext;
                        bNode = bNode.GetNext;
                    }else if (aNode.GetSparseId > bNode.GetSparseId)
                    {
                        bNode = bNode.GetNext;
                    }
                    else
                    {
                        aNode = aNode.GetNext;
                    }
                }
            }

            return sparseIdList;
        }

        /// <summary>
        /// pick the first element of each transaction in the SIL to generate the VIL
        /// </summary>
        /// <returns></returns>
        public VerticalIdList GetStartingVIL()
        {
            ListNode[] VILElements = new ListNode[GetLength()];

            for (int i = 0; i < VILElements.Length; i++)
            {
                VILElements[i] = GetElement(i, 0);
            }
            
            return new VerticalIdList(VILElements, Support);
        }

        public int Support { get; private set; }

        /// <summary>
        /// one transaction means one SIL, means all the exist position of the item in one sequence
        /// </summary>
        private class TransactionIds
        {
            /// <summary>
            /// use a list of ListNode to store the exist information of the item
            /// </summary>
            private List<ListNode> Transactions = new List<ListNode>();
            
            /// <summary>
            /// add one node at the last of the list , means there is a new exist position in current sequence
            /// </summary>
            /// <param name="node"></param>
            public void Add(ListNode node)
            {
                if (Transactions.Count != 0)
                {
                    ListNode temp = Transactions.Last();
                    temp.SetNext(node);
                }

                Transactions.Add(node);
            }

            /// <summary>
            /// get the node in one specific position
            /// </summary>
            /// <param name="position"></param>
            /// <returns></returns>
            public ListNode GetNode(int position)
            {
                return Transactions[position];
            }

            /// <summary>
            /// get the length of this SIL
            /// </summary>
            /// <returns></returns>
            public int GetLength()
            {
                return Transactions.Count;
            }
        }
    }
}