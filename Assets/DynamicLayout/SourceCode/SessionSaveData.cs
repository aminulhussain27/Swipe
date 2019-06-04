using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SessionSaveData
{

	//making the class is singleton for use inother class and making sure
	//only one object is created
    private static SessionSaveData gameSessionData = null;
    public static SessionSaveData Instance()
    {
        if (gameSessionData == null)
        {
            gameSessionData = new SessionSaveData();
        }

        return gameSessionData;
    }


	public Dictionary<string, CountryData> allDataDict = new Dictionary<string, CountryData>();
	public AllDataClass allDataClassObject;
	//defining the file path where a file will create and the data will store
	string filePath = Application.persistentDataPath + "SessionData.json";

    public void Save()
    {
//        Debug.Log("SAVE");

        List<string> dataClassKeys = new List<string>(allDataDict.Keys);//List of All Keys of Dictionary

        List<CountryData> tempDataClassList = new List<CountryData>();

		//adding all the keys into the temp list
        foreach (string key in dataClassKeys)
        {

            tempDataClassList.Add(allDataDict[key]);
        }

        AllDataClass listClassObj = new AllDataClass() { structList = tempDataClassList };

		//creating a file for saving purpose
        FileStream file = File.Create(filePath);

		//creating JSON from customised object(allDataClass
        string json = JsonUtility.ToJson(listClassObj);

        file.Dispose();
		//String the JSON data to the file accessing by file path
        File.WriteAllText(filePath, json);
        file.Close();
    }



    public void Load()
    {
//		Debug.Log("LOAD_  " + filePath);

		//alldataClass is list of objects which I stored
		//If not initiated creating a new object
        if (allDataClassObject == null)
        {
            allDataClassObject = new AllDataClass();
        }
//        else
//        {
//			//clearing the dataClass list for reuse
//            if (allDataClassObject.structList != null)
//            {
//                allDataClassObject.structList.Clear();
//            }
//        }

		//clearing the dictionary for fresh use
        allDataDict.Clear();

		//Checking the existance for previuos file
        if (!File.Exists(filePath))
        {
            Debug.Log("No Previous session");
            return;
        }

		//reading the data from JSON file in string format
        string json = File.ReadAllText(filePath);

        allDataClassObject = (AllDataClass)JsonUtility.FromJson(json, typeof(AllDataClass));

		//putting the dictionary data to newly created list
        foreach (CountryData cd in allDataClassObject.structList)
        {

            allDataDict.Add(cd._Cntname, cd);// using title as key
        }
    }

}
