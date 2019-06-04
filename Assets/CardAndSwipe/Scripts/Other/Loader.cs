using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour {

	// Use this for initialization
	public Button swipeButton;
	public Button dynamicLayoutButton;
	public Button quitButton;


	void Start () 
	{

		//assiging all the button action i main menu scene
		swipeButton.onClick.RemoveAllListeners ();
		swipeButton.onClick.AddListener(()=>
				{
					LoadParticularScene("Swipe");
				});
		dynamicLayoutButton.onClick.RemoveAllListeners ();
		dynamicLayoutButton.onClick.AddListener(()=>
				{
					LoadParticularScene("DynamicLayout");
				});
		quitButton.onClick.RemoveAllListeners ();
		quitButton.onClick.AddListener(()=>
				{
					QuitGame();
				});
	}


	//Loaing a scene with its name
	void LoadParticularScene(string sceneName)
	{
		SceneManager.LoadSceneAsync (sceneName);
	}

	void QuitGame()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}

}
