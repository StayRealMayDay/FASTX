using System.Collections.Generic;
using System.IO;

namespace FASTX
{
    public class DataSet
    {
        /// <summary>
        /// the itemset separator
        /// </summary>
        public static string ITEMSET_SEPARATOR = "-1";

        /// <summary>
        /// the sequence separator
        /// </summary>
        public static string SEQUENCE_SEPARATOR = "-2";

        /// <summary>
        /// map each sequens to its SparseIdList
        /// </summary>
        public Dictionary<string, SparseIdList> ItemSILDic { get; private set; }

        /// <summary>
        /// Number of roes (sequences)
        /// </summary>
        public int NumberOfRows { get; private set; }

        /// <summary>
        /// store the min Support
        /// </summary>
        public int MinSupport { get; private set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="numberOfRows"></param>
        /// <param name="minSupport"></param>
        public DataSet(int numberOfRows, int minSupport)
        {
            NumberOfRows = numberOfRows;
            MinSupport = minSupport;
            ItemSILDic = new Dictionary<string, SparseIdList>();
        }

        public DataSet ReadDate(string path, int support)
        {
            var data = File.ReadAllLines(path);
            foreach (var line in data)
            {
                if (line.Length == 0)
                {
                   continue;
                }

                var transactionId = 1;
                var itemList = line.Split(' ');
                foreach (var item in itemList)
                {
                    if (item == ITEMSET_SEPARATOR)
                    {
                        transactionId++;
                        continue;
                    }

                    if (item == SEQUENCE_SEPARATOR)
                    {
                        break;;
                    }
                   // SparseIdList inserted = 
                }
            }
        }

    }
}