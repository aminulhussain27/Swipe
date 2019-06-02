using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager  : MonoBehaviour
{

	private static UIManager instance;

	public static UIManager Instance ()
	{
		if (instance == null) 
		{
			instance = GameObject.FindObjectOfType (typeof(UIManager)) as UIManager;
		}

		return instance;
	}

	public CardController cardCOntroller;

	public Text knownText ;
	public Text unknownText;

	public GameObject GameOverPopUp;
	public GameObject WaitObject;

	public Button restartButton;
	public Button quiteButton;

	public int knownCounter;
	public int unknownCounter;


	void Start () 
	{
		GameOverPopUp.SetActive (false);
		WaitObject.SetActive (false);

		restartButton.onClick.RemoveAllListeners ();
		restartButton.onClick.AddListener(()=>
				{
					RestartButtonAction();
				});

		quiteButton.onClick.RemoveAllListeners ();
		quiteButton.onClick.AddListener(()=>
				{
					UnityEditor.SceneManagement.EditorSceneManager.LoadSceneAsync ("Loader");
				});
	}
	
	public void UpdateCounter(bool needRest = false)
	{
		if(needRest)
		{
			knownCounter = unknownCounter = 0;
		}

		knownText.text = knownCounter.ToString ();
		unknownText.text = unknownCounter.ToString ();
	}

	void RestartButtonAction()
	{
		GameOverPopUp.SetActive(false);
		cardCOntroller.RestartCardSpawn();
		UpdateCounter(true);
	}
}
