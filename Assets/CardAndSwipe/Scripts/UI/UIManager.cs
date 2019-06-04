using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager  : MonoBehaviour
{

	//creating singleton of this class
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

	//Text element for known and unknown counter UI
	public Text knownText ;
	public Text unknownText;

	//Gameover popup This will show when all the cards swipped correctly
	public GameObject GameOverPopUp;

	//waiting screen shows when some cards brining back to screen
	public GameObject WaitObject;

	//Restart button when game is fininshed it will popup
	public Button restartButton;
	public Button quiteButton;


	// counter variables for known and unknown counter
	public int knownCounter;
	public int unknownCounter;


	void Start () 
	{
		//default kepping these screen disable
		GameOverPopUp.SetActive (false);
		WaitObject.SetActive (false);


		//assingning the button actions
		restartButton.onClick.RemoveAllListeners ();
		restartButton.onClick.AddListener(()=>
				{
					RestartButtonAction();
				});

		quiteButton.onClick.RemoveAllListeners ();
		quiteButton.onClick.AddListener(()=>
				{
					UnityEngine.SceneManagement.SceneManager.LoadSceneAsync ("Loader");
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

	//Giving the chance to restart the Game
	void RestartButtonAction()
	{
		GameOverPopUp.SetActive(false);
		cardCOntroller.RestartCardSpawn();
		//resetting the counter to 0
		UpdateCounter(true);
	}
}
