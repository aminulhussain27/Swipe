//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;
//
//
//public class HeroUpgradeSessionData 
//{
//
//
//    public Dictionary<string, HeroData> allHerodataDict = new Dictionary<string, HeroData>();
//    public AllHeroClass allHeroClassObject;
//    string filePath = Application.persistentDataPath + "SessionData/HeroData.json";
//
//
//    private static HeroUpgradeSessionData gameSessionData = null;
//    public static HeroUpgradeSessionData Instance()
//    {
//        if (gameSessionData == null)
//        {
//            gameSessionData = new HeroUpgradeSessionData();
//        }
//
//        return gameSessionData;
//    }
//
//    HeroUpgradeSessionData()
//    {
//        if(allHerodataDict == null)
//        {
//            allHerodataDict = new Dictionary<string, HeroData>();
//        }
//    }
//
//    public Dictionary<string, HeroData> GetAllHeroDataDict()
//    {
//        if(allHerodataDict == null)
//        {
//            allHerodataDict = new Dictionary<string, HeroData>();
//        }
//        return allHerodataDict;
//    }
//
//
//    public void UpdateHeroDataDict(HeroData heroData)
//    {
//        allHerodataDict[heroData.title] = heroData;
//    }
//
//
//
//    public void Save()
//    {
//        Debug.Log("SAVE");
//
//        List<string> dataClassKeys = new List<string>(allHerodataDict.Keys);//List of All Keys of Dictionary
//
//        List<HeroData> tempDataClassList = new List<HeroData>();
//
//
//        foreach (string key in dataClassKeys)
//        {
//
//            tempDataClassList.Add(allHerodataDict[key]);
//        }
//
//         AllHeroClass listClassObj = new  AllHeroClass() { allHeroDataList = tempDataClassList };
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
//        Debug.Log("LOAD");
//
//        if (allHeroClassObject == null)
//        {
//            allHeroClassObject = new AllHeroClass();
//        }
//        else
//        {
//            if (allHeroClassObject.allHeroDataList != null)
//            {
//                allHeroClassObject.allHeroDataList.Clear();
//            }
//        }
//
//        allHerodataDict.Clear();
//
//        if (!File.Exists(filePath))
//        {
//            Debug.Log("No Previous session for Hero");
//            return;
//        }
//        string json = File.ReadAllText(filePath);
//
//        allHeroClassObject = (AllHeroClass)JsonUtility.FromJson(json, typeof(AllHeroClass));
//       
//        foreach (HeroData heroData in allHeroClassObject.allHeroDataList)
//        {
//
//            allHerodataDict.Add(heroData.title, heroData);// using title as key
//        }
//    }
//
//}
