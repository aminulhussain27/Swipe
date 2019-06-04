using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(BoxCollider))]
public class SwipeController : MonoBehaviour {

	private Vector3 screenPoint;
	private Vector3 offset;
	private Vector3 curPosition;


	public SpriteRenderer tickMark;
	public SpriteRenderer crossMark;

	private CardController cardController;

	void Start () 
	{
		//Taking the cardcontroller object
		cardController = FindObjectOfType (typeof (CardController)) as CardController;
	}
	

	void Update () {
		LeanTween.rotate (this.gameObject, new Vector3 (0f, 0f, this.gameObject.transform.position.x * -2.5f), 0.0001f);


		transform.position = new Vector3 (this.gameObject.transform.position.x,
			Mathf.Clamp (this.gameObject.transform.position.y, 0f, 0f), 
			this.gameObject.transform.position.z);
	
	}

	void OnMouseDown () 
	{
		//whe the touch started
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}
		

	void OnMouseUp () 
	{
		//when touch ended resetting the cross mark and tick mark
		crossMark.color = new Color32 (0, 0, 0, 0);
		tickMark.color = new Color32 (0, 0, 0, 0);

		//checking the current position of card when touch ended
		if (curPosition.x > 0) 
		{
			//If card position is on right end of screen removing the card and considering as right swipped
			if (curPosition.x > 2f) 
			{
				cardController.RemoveCardsAndCheckTotal ();

				//moving the card to right end of screen
				LeanTween.move (this.gameObject, new Vector2 (50f, 0f), 0.5f);
				StartCoroutine (DisableAfterFullSwipe ());
			}
			else 
			{
				//if card is not ully swipped to right end Restting the posion to mid of the screen
				LeanTween.move (this.gameObject, Vector2.zero, 0.2f);
			}
		} 

		if (curPosition.x < 0)
		{
			//If card position is on left end of screen removing the card and considering as left swipped
			if (curPosition.x < -2f) 
			{
				//as this card is left swipped Kepping this in different list for showing again
				cardController.leftOverCardList.Add (this.gameObject);

				// removing the card from card list and marked as swipped
				cardController.RemoveCardsAndCheckTotal (true);

				//moving the card to left end of screen
				LeanTween.move (this.gameObject, new Vector2 (-50f, 0f), 0.5f);
				StartCoroutine (DisableAfterFullSwipe ());
			} 
			else 
			{
				//if card is not ully swipped to right end Restting the posion to mid of the screen
				LeanTween.move (this.gameObject, Vector2.zero, 0.2f);
			}
		}
	}


	void OnMouseDrag () 
	{
		//kepping the current position of touch
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

		//assigning the current touch position to card position
		transform.position = curPosition;



		//Showing one Tick and Cross mard mark to showing whether its right/wrong
		float darkness =120 * Mathf.Abs(curPosition.x);

//		Debug.Log ("Darkness:  " + darkness + "    Pos:  " + curPosition.x);
		if(darkness > 255)
		{
			darkness = 255;
		}

		if(curPosition.x > 0f)
		{
			//assigning the sprite color
			tickMark.color = new Color32 (255,255,255, (byte)darkness);//darkness ,darkness,darkness,darkness);
		}
		else if(curPosition.x < 0f)
		{
			//asigning the sprite color when swipped left
			crossMark.color =new Color32 (255,255,255, (byte)darkness);
		}
		else if(curPosition.x == 0)
		{
			//resetting the color to 0 alpha when the card is in mid of the screen
			crossMark.color = new Color32 (0, 0, 0, 0);
			tickMark.color = new Color32 (0, 0, 0, 0);
		}
	}

	IEnumerator DisableAfterFullSwipe()
	{
		//diabling the card Not destroying for reuse
		yield return new WaitForSeconds (0.5f);
		gameObject.SetActive (false);
	}
}
