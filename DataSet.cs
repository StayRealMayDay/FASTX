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
        /// map each sequens to its SparseIdList,
        /// </summary>
        public Dictionary<string, SparseIdList> ItemSILDic { get; private set; }

        /// <summary>
        /// Store the CMap information for I-Extension
        /// </summary>
        public Dictionary<int, Dictionary<int, int>> CMapIExtension { get; private set; }

        /// <summary>
        /// Store the CMap information for S-Extension
        /// </summary>
        public Dictionary<int, Dictionary<int, int>> CMapSExtension { get; private set; }

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
            CMapIExtension = new Dictionary<int, Dictionary<int, int>>();
            CMapSExtension = new Dictionary<int, Dictionary<int, int>>();
        }

        /// <summary>
        /// read data from file and return a dictionary(string, SIL)
        /// </summary>
        /// <param name="path"></param>
        /// <param name="support"></param>
        /// <returns></returns>
        public static DataSet ReadData(string path, int support)
        {
            var data = File.ReadAllLines(path);
            var dataSet = new DataSet(data.Length, support);
            int lineNumber = 0;
            foreach (var line in data)
            {
                if (line.Length == 0)
                {
                   continue;
                }

                var transactionId = 1;
                var itemList = line.Split(' ');

                for (int i = 0; i < itemList.Length; i++)
                {
                    if (itemList[i] == ITEMSET_SEPARATOR)
                    {
                        transactionId++;
                        continue;
                    }

                    var samteItemset = true;
                    for (int j = i + 1; j < itemList.Length; j++)
                    {
                        if (itemList[j] == ITEMSET_SEPARATOR)
                        {
                            samteItemset = false;
                            continue;
                        }

                        if (itemList[j] == SEQUENCE_SEPARATOR)
                        {
                            break;
                        }

                        if (samteItemset)
                        {
                            
                        }
                        
                    }
                    if (itemList[i] == SEQUENCE_SEPARATOR)
                    {
                        break;;
                    }

                    if (!dataSet.ItemSILDic.ContainsKey(itemList[i]))
                    {
                        dataSet.ItemSILDic.Add(itemList[i], new SparseIdList(data.Length));
                    }
                    dataSet.ItemSILDic[itemList[i]].AddElement(lineNumber, transactionId);
                }
//                foreach (var item in itemList)
//                {
//                    if (item == ITEMSET_SEPARATOR)
//                    {
//                        transactionId++;
//                        continue;
//                    }
//                    
//                    if (item == SEQUENCE_SEPARATOR)
//                    {
//                        break;;
//                    }
//
//                    if (!dataSet.ItemSILDic.ContainsKey(item))
//                    {
//                        dataSet.ItemSILDic.Add(item, new SparseIdList(data.Length));
//                    }
//                    dataSet.ItemSILDic[item].AddElement(lineNumber, transactionId);
//                }
                lineNumber++;
            }

            dataSet.ComputeFrequentItems();
            return dataSet;
        }

        /// <summary>
        /// delete those item which not satisfy the support condition
        /// </summary>
        public void ComputeFrequentItems()
        {
            var newDic = new Dictionary<string, SparseIdList>();
            foreach (var KeyValue in ItemSILDic)
            {
                if (KeyValue.Value.Support > MinSupport)
                {
                    newDic.Add(KeyValue.Key, KeyValue.Value);
                }
            }

            ItemSILDic = newDic;
        }

        public Dictionary<string, SparseIdList> GetItemSILDic()
        {
            return ItemSILDic;
        }

    }
}