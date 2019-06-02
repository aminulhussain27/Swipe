using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script controls the swipe.
/// </summary>

[RequireComponent(typeof(BoxCollider))]
public class SwipeController : MonoBehaviour {

	private Vector3 screenPoint;
	private Vector3 offset;
	private Vector3 curPosition;

	private int random;

	private CardController cardController;

	void Start () 
	{
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
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}
		

	void OnMouseUp () 
	{
		if (curPosition.x > 0) 
		{
			if (curPosition.x > 3f) 
			{
				cardController.leftOverCardList.Add (this.gameObject);
				cardController.RemoveCardsAndCheckTotal (true);

				LeanTween.move (this.gameObject, new Vector2 (50f, 0f), 0.5f);
				StartCoroutine (DisableAfterFullSwipe ());
			}
			else 
			{
				LeanTween.move (this.gameObject, Vector2.zero, 0.2f);
			}
		} 

		if (curPosition.x < 0)
		{
			if (curPosition.x < -3f) 
			{
				cardController.RemoveCardsAndCheckTotal ();
				LeanTween.move (this.gameObject, new Vector2 (-50f, 0f), 0.5f);
				StartCoroutine (DisableAfterFullSwipe ());
			} 
			else 
			{
				LeanTween.move (this.gameObject, Vector2.zero, 0.2f);
			}
		}
	}


	void OnMouseDrag () 
	{
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		transform.position = curPosition;
	}

	IEnumerator DisableAfterFullSwipe()
	{
		yield return new WaitForSeconds (0.5f);
		gameObject.SetActive (false);
	}
}
