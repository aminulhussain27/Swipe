using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loader : MonoBehaviour {

	// Use this for initialization
	public Button swipeButton;
	public Button dataBaseButton;
	public Button quitButton;


	void Start () {
		swipeButton.onClick.RemoveAllListeners ();
		swipeButton.onClick.AddListener(()=>
				{
					LoadParticularScene("Swipe");
				});
		dataBaseButton.onClick.RemoveAllListeners ();
		dataBaseButton.onClick.AddListener(()=>
				{
					LoadParticularScene("Database");
				});
		quitButton.onClick.RemoveAllListeners ();
		quitButton.onClick.AddListener(()=>
				{

				});
	}

	void LoadParticularScene(string sceneName)
	{
		UnityEditor.SceneManagement.EditorSceneManager.LoadSceneAsync (sceneName);
	}
	

}
