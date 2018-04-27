using System.Collections.Generic;
using System.Linq;

namespace FASTX
{
    public class SparseIdList
    {
        private TransactionIds[] Vector;

        public SparseIdList(int rows)
        {
            Vector = new TransactionIds[rows];
        }

        public int GetLength()
        {
            return Vector.Length;
        }

        public void AddElement(int row, int value)
        {
            if (Vector[row] == null)
            {
                Vector[row] = new TransactionIds();

                Support++;
            }

            Vector[row].Add(new ListNode(value));
        }

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
                    }

                    if (aNode.GetSparseId > bNode.GetSparseId)
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

        public int Support { get; private set; }


        private class TransactionIds
        {
            private List<ListNode> Transactions = new List<ListNode>();

            public void Add(ListNode node)
            {
                if (Transactions.Count != 0)
                {
                    ListNode temp = Transactions.Last();
                    temp.SetNext(node);
                }

                Transactions.Add(node);
            }

            public ListNode GetNode(int position)
            {
                return Transactions[position];
            }

            public int GetLength()
            {
                return Transactions.Count;
            }
        }
    }
}