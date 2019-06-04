using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	//variables for country states and cities count
	int dynamicCountryCount;
	int dynamicStateCount;
	int dynamicCityCount;

    void Awake()
    {
		//deciding length of country state and cities Which can be changed anytime
		dynamicCountryCount = 3;
		dynamicStateCount = 9;
		dynamicCityCount = 9;

		//Creating some default data if there is not JSON data for country state and cities
//		if (SessionSaveData.Instance ().allDataDict.Count == 0) 
//		{
//			CreatData ();
//			SessionSaveData.Instance ().Save ();
//		}
    
		//Loading the JSON file to game from storage
//		SessionSaveData.Instance ().Load ();
		if(SessionSaveData.Instance().allDataDict.Count == 0)
		{
			CreatData ();
		}

		SessionSaveData.Instance ().Save ();
	}
	void Start()
	{

	}


	//Creating some default data
    public void CreatData()
    {
//        Debug.Log("Create");
		for (int countryCount = 0; countryCount < dynamicCountryCount; countryCount++)//country
        {

			//creating a country data class where all state and cities data will be there
			CountryData countryDataClass = new CountryData();

			//Directly putting the country name as per indexing
			if(countryCount == 0) 
			{
				countryDataClass._Cntname = "INDIA";
			}
			if(countryCount == 1) 
			{
				countryDataClass._Cntname = "GERMANY";
			}
			if(countryCount == 2) 
			{
				countryDataClass._Cntname = "JAPAN";
			}

			//creating a list of states in country object
            countryDataClass.sTATEs = new List<StateData>();

			for (int stateCount = 0; stateCount <= dynamicStateCount; stateCount++)//state
            {
				//Creating a state object in this country object 
                StateData sTATE = new StateData();

				//Name on the basis of state index
				sTATE._stateName = "STATE_" + stateCount.ToString();

				//creating the object of the list of city
                sTATE.cityList = new List<string>();

                countryDataClass.sTATEs.Add(sTATE);


				//Creating a list of cities for this state
				for (int cityCount = 0; cityCount <= dynamicCityCount; cityCount++)//City
                {
					string cityName = "CITY_" + cityCount.ToString();
                    sTATE.cityList.Add(cityName);
                }
            }

			//adding the country data to a dictionary as countryName is key
            SessionSaveData.Instance().allDataDict.Add(countryDataClass._Cntname, countryDataClass);
        }
    }

}


//This class will store in json file
[System.Serializable]
public class AllDataClass 
{
    public List<CountryData> structList = new List<CountryData>();
}
