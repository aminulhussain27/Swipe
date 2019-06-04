using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CountryData//Country
{
	//country name
    public string _Cntname;
	//list of states present in this country
    public List<StateData> sTATEs;

}

[System.Serializable]
public class StateData
{
	//Name of the states
    public string _stateName;
	//list of cities in this state
    public List<string> cityList;
}


