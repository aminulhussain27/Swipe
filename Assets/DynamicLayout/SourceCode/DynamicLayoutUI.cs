using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicLayoutUI : MonoBehaviour
{

	//Transform of all panel
    public Transform countryPanel;
    public Transform statePanel;
    public Transform cityPanel;

	//List of all objects like country states and cities
    public List<GameObject> stateObjectList = new List<GameObject>();
    public List<GameObject> cityObjectList = new List<GameObject>();

	public ScrollRect scrollRect;

	//current panel
	public PANEL currentPanel = PANEL.COUNTRY;

	//Panel enum
	public PANEL panelEnum;
	public enum PANEL
	{
		COUNTRY,
		STATE,
		CITY,
	}

    private void Start()
    {
//		Debug.Log ("START in UI");
		//loading the data from session data
		SessionSaveData.Instance ().Load ();

		//Assigning the button in all country button
        for (int i = 0; i < countryPanel.childCount; i++ )//country button panel
        {
            int index = i;
			Debug.Log(index +"  i= " +i +"  list  " + SessionSaveData.Instance().allDataClassObject.structList.Count);
            Button button = countryPanel.GetChild(index).GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() =>
            {
				//Updatng the states data according to country selected
                UpdateStateData(SessionSaveData.Instance().allDataClassObject.structList[index], index);
            });
        }
    }


	//populates states objects
    public void UpdateStateData(CountryData a, int indexBt)
    {
        SwitchPanel(PANEL.STATE);
//		Debug.Log(indexBt +"  " + a._Cntname);

        for (int i = 0; i < a.sTATEs.Count; i++)
        {
            stateObjectList[i].GetComponent<Text>().text = a._Cntname + "_" + a.sTATEs[i]._stateName;

            int index = i;
            Button button =  statePanel.GetChild(index).GetComponent<Button>();//states button panel
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() =>
            {
                UpdateCityData(a, index);
            });
        }
    }

	//populates city objects
    public void UpdateCityData(CountryData a, int index)
    {
        SwitchPanel(PANEL.CITY);

//        Debug.LogError( a._Cntname +"_" + a.sTATEs[index]._stateName);

        for (int i = 0; i < a.sTATEs[index].cityList.Count; i++)
        {
			cityObjectList[i].GetComponent<Text>().text = a.sTATEs[index]._stateName +"_"+a.sTATEs[index].cityList[i];

        }
    }


	//Switching the panels of country, states, cities
    public void SwitchPanel(PANEL pANEL)
    {
        switch(pANEL)
        {
            case PANEL.COUNTRY:
                currentPanel = PANEL.COUNTRY;
                countryPanel.gameObject.SetActive(true);
                statePanel.gameObject.SetActive(false);
                cityPanel.gameObject.SetActive(false);
                break;
			case PANEL.STATE:
				currentPanel = PANEL.STATE;
				scrollRect.content = statePanel.GetComponent<RectTransform>();

                countryPanel.gameObject.SetActive(false);
                statePanel.gameObject.SetActive(true);
                cityPanel.gameObject.SetActive(false);
                break;

			case PANEL.CITY:
				currentPanel = PANEL.CITY;
				scrollRect.content = cityPanel.GetComponent<RectTransform>();

                countryPanel.gameObject.SetActive(false);
                statePanel.gameObject.SetActive(false);
                cityPanel.gameObject.SetActive(true);
                break;
        }
    }

	//Back button button action
    public void BackButtonPressed()
    {
        if(currentPanel == PANEL.STATE)
        {
            SwitchPanel(PANEL.COUNTRY);
        }
        else if(currentPanel == PANEL.CITY)
        {
            SwitchPanel(PANEL.STATE);
        }
		else
		{
			//Loading the main menu scene
			UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Loader");
		}
    }
}
