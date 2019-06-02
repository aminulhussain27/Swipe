//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;
//
//
//public class PlayerProgressSessionData 
//{
//
//    string filePath = Application.persistentDataPath + "SessionData/PlayerProgressData.json";
//
//
//
//    public Dictionary<string, ArenaData> allArenaDataDict = new Dictionary<string, ArenaData>();
//    public AllAreanaDataClass AllAreanaSessionData;
//
//    private static PlayerProgressSessionData gameSessionData = null;
//    public static PlayerProgressSessionData Instance()
//    {
//        if (gameSessionData == null)
//        {
//            var folder = Directory.CreateDirectory(Application.persistentDataPath+ "SessionData");
//            gameSessionData = new PlayerProgressSessionData();
//        }
//
//        return gameSessionData;
//    }
//
//    PlayerProgressSessionData()
//    {
//        if (allArenaDataDict == null)
//        {
//            allArenaDataDict = new Dictionary<string, ArenaData>();
//        }
//    }
//
//    public ArenaData GetArenaData(string key)
//    {
//        ArenaData arenaData = null;
//        if(allArenaDataDict.ContainsKey(key))
//        {
//            arenaData = allArenaDataDict[key];
//        }
//
//        return arenaData;
//
//    }
//
//    public void SetArenaData(ArenaData data)
//    {
//        if (allArenaDataDict.ContainsKey(data.arenaName))
//        {
//            allArenaDataDict[data.arenaName] = data;
//        }
//
//        Save();
//
//    }
//
//
//    public void Save()
//    {
//        Debug.Log("SAVE_PROGRESS");
//
//        List<string> dataClassKeys = new List<string>(allArenaDataDict.Keys);//List of All Keys of Dictionary
//
//        List<ArenaData> tempDataClassList = new List<ArenaData>();
//
//
//        foreach (string key in dataClassKeys)
//        {
//
//            tempDataClassList.Add(allArenaDataDict[key]);
//        }
//
//        AllAreanaDataClass listClassObj = new AllAreanaDataClass() { allArenaSessionDataList = tempDataClassList };
//
//
//        FileStream file = File.Create(filePath);
//
//        string json = JsonUtility.ToJson(listClassObj);
//
//        file.Dispose();
//        File.WriteAllText(filePath, json);
//        file.Close();
//    }
//
//    public void Load()
//    {
//        Debug.Log("LOAD_PROGRESS");
//
//        if (AllAreanaSessionData == null)
//        {
//            AllAreanaSessionData = new AllAreanaDataClass();
//        }
//        else
//        {
//            allArenaDataDict.Clear();
//            if (AllAreanaSessionData.allArenaSessionDataList != null)
//            {
//                AllAreanaSessionData.allArenaSessionDataList.Clear();
//            }
//        }
//
//        if(!File.Exists(filePath))
//        {
//            Debug.Log("No Previous session for Player");
//            return;
//        }
//        string json = File.ReadAllText(filePath);
//
//        AllAreanaSessionData = (AllAreanaDataClass)JsonUtility.FromJson(json, typeof(AllAreanaDataClass));
//       
//        foreach (ArenaData arenaData in AllAreanaSessionData.allArenaSessionDataList)
//        {
//
//            allArenaDataDict.Add(arenaData.arenaName, arenaData);// using title as key
//        }
//    }
//
//}
//
