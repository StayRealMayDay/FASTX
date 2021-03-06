﻿using System.Collections.Generic;
using System.IO;
using System.Linq;

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
        /// first string is the item
        /// second string is the extension item
        /// third int is the support of this pattern
        /// </summary>
        public Dictionary<string, Dictionary<string, List<int>>> CMapIExtension { get; private set; }

        /// <summary>
        /// Store the CMap information for S-Extension
        /// the first string is the item
        /// second string is the extension item
        /// third int is the support of this two item pattern
        /// </summary>
        public Dictionary<string, Dictionary<string, List<int>>> CMapSExtension { get; private set; }

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
            CMapIExtension = new Dictionary<string, Dictionary<string, List<int>>>();
            CMapSExtension = new Dictionary<string, Dictionary<string, List<int>>>();
        }

        /// <summary>
        /// read data from file and return a dictionary(string, SIL)
        /// </summary>
        /// <param name="path"></param>
        /// <param name="support"></param>
        /// <returns></returns>
//        public static DataSet ReadData(string path, int support)
//        {
//            var data = File.ReadAllLines(path);
//            var dataSet = new DataSet(data.Length, support);
//            int lineNumber = 0;
//            foreach (var line in data)
//            {
//                if (line.Length == 0)
//                {
//                   continue;
//                }
//
//                var transactionId = 1;
//                var itemList = line.Split(' ');
//
//                for (int i = 0; i < itemList.Length; i++)
//                {
//                    if (itemList[i] == ITEMSET_SEPARATOR)
//                    {
//                        transactionId++;
//                        continue;
//                    }
//                    if (itemList[i] == SEQUENCE_SEPARATOR)
//                    {
//                        break;
//                    }
//                    // used to judge whether need to do CMapIextension operation(indicate whether the two item in the same itemset)
//                    var samteItemset = true;
//                    for (int j = i + 1; j < itemList.Length; j++)
//                    {
//                        if (itemList[j] == ITEMSET_SEPARATOR)
//                        {
//                            // when item is -1 change the value
//                            samteItemset = false;
//                            continue;
//                        }
//
//                        if (itemList[j] == SEQUENCE_SEPARATOR)
//                        {
//                            break;
//                        }
//                        // extension the IExtension CMap
//                        if (samteItemset)
//                        {   // if it contain the item[i]
//                            if (dataSet.CMapIExtension.ContainsKey(itemList[i]))
//                            {    //whether contain the item[j]
//                                if (dataSet.CMapIExtension[itemList[i]].ContainsKey(itemList[j]))
//                                {    // if not contain this line then add it to the dictionay
//                                    if (!dataSet.CMapIExtension[itemList[i]][itemList[j]].Contains(lineNumber))
//                                    {
//                                        dataSet.CMapIExtension[itemList[i]][itemList[j]].Add(lineNumber);
//                                    }
//                                }
//                                else
//                                {
//                                    dataSet.CMapIExtension[itemList[i]].Add(itemList[j], new List<int>(){lineNumber});
//                                }
//                            }
//                            else // if not we should init it and add the item[j] whit lineNumber to it
//                            {
//                                dataSet.CMapIExtension.Add(itemList[i], new Dictionary<string, List<int>>());
//                                dataSet.CMapIExtension[itemList[i]].Add(itemList[j], new List<int>(){lineNumber});
//                            }
//                        }
//                        else
//                        {   // the same with CMapIExtension
//                            if (dataSet.CMapSExtension.ContainsKey(itemList[i]))
//                            {
//                                if (dataSet.CMapSExtension[itemList[i]].ContainsKey(itemList[j]))
//                                {
//                                    if (!dataSet.CMapSExtension[itemList[i]][itemList[j]].Contains(lineNumber))
//                                    {
//                                        dataSet.CMapSExtension[itemList[i]][itemList[j]].Add(lineNumber);
//                                    }
//                                }
//                                else
//                                {
//                                    dataSet.CMapSExtension[itemList[i]].Add(itemList[j], new List<int>(){lineNumber});
//                                }
//                            }
//                            else
//                            {
//                                dataSet.CMapSExtension.Add(itemList[i], new Dictionary<string, List<int>>());
//                                dataSet.CMapSExtension[itemList[i]].Add(itemList[j], new List<int>(){lineNumber});
//                            }
//                        }
//                        
//                    }
//                    
//
//                    if (!dataSet.ItemSILDic.ContainsKey(itemList[i]))
//                    {
//                        dataSet.ItemSILDic.Add(itemList[i], new SparseIdList(data.Length));
//                    }
//                    dataSet.ItemSILDic[itemList[i]].AddElement(lineNumber, transactionId);
//                }
//                lineNumber++;
//            }
//
//            dataSet.ComputeFrequentItems();
//            dataSet.CunputeFrequentCMap();
//            return dataSet;
//        }
        
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

                    if (!dataSet.ItemSILDic.ContainsKey(item))
                    {
                        dataSet.ItemSILDic.Add(item, new SparseIdList(data.Length));
                    }
                    dataSet.ItemSILDic[item].AddElement(lineNumber, transactionId);
                }
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

        /// <summary>
        /// delete the patterns which not satisfy the support condition
        /// </summary>
        public void CunputeFrequentCMap()
        {
            // can not use foreach statement if we widd do Add or Remore operation 
            // because Collection was modified ,enumeration operation can not exexute
            var cMapIExtensionKeys = CMapIExtension.Keys.ToArray();
            var cMapSExtensionKeys = CMapSExtension.Keys.ToArray();
            for (int i = 0; i < cMapIExtensionKeys.Length; i++)
            {
                // if
                var cMapIExtensionValuesKeys = CMapIExtension[cMapIExtensionKeys[i]].Keys.ToArray();
                for (int j = 0; j < cMapIExtensionValuesKeys.Length; j++)
                {
                    if (CMapIExtension[cMapIExtensionKeys[i]][cMapIExtensionValuesKeys[j]].Count < MinSupport)
                    {
                        CMapIExtension[cMapIExtensionKeys[i]].Remove(cMapIExtensionValuesKeys[j]);
                    }
                }

                if (CMapIExtension[cMapIExtensionKeys[i]].Count == 0)
                {
                    CMapIExtension.Remove(cMapIExtensionKeys[i]);
                }

            }
            for (int i = 0; i < cMapSExtensionKeys.Length; i++)
            {
                var cMapSExtensionValuesKeys = CMapSExtension[cMapSExtensionKeys[i]].Keys.ToArray();
                for (int j = 0; j < cMapSExtensionValuesKeys.Length; j++)
                {
                    if (CMapSExtension[cMapSExtensionKeys[i]][cMapSExtensionValuesKeys[j]].Count < MinSupport)
                    {
                        CMapSExtension[cMapSExtensionKeys[i]].Remove(cMapSExtensionValuesKeys[j]);
                    }
                }

                if (CMapSExtension[cMapSExtensionKeys[i]].Count == 0)
                {
                    CMapSExtension.Remove(cMapSExtensionKeys[i]);
                }

            }
//            foreach (var keyValue in CMapIExtension)
//            {
//                foreach (var iExtension in keyValue.Value)
//                {
//                    if (iExtension.Value.Count < MinSupport)
//                    {
//                        CMapIExtension[keyValue.Key].Remove(iExtension.Key);
//                    }
//                }
//
//                if (keyValue.Value.Count == 0)
//                {
//                    CMapIExtension.Remove(keyValue.Key);
//                }
//            }
//            foreach (var keyValue in CMapSExtension)
//            {
//                foreach (var iExtension in keyValue.Value)
//                {
//                    if (iExtension.Value.Count < MinSupport)
//                    {
//                        CMapSExtension[keyValue.Key].Remove(iExtension.Key);
//                    }
//                }
//
//                if (keyValue.Value.Count == 0)
//                {
//                    CMapSExtension.Remove(keyValue.Key);
//                }
//            }
        }

        public Dictionary<string, SparseIdList> GetItemSILDic()
        {
            return ItemSILDic;
        }

    }
}