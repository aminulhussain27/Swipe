using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CardController : MonoBehaviour {


	public GameObject cardPrefabs;

	public Sprite[] images;

	public Transform deck;

	public int initialCards;

	private int randomCardChoose;

	public int totalCards;

	private List<GameObject> objectPoolList = new List<GameObject> ();
	private List<GameObject> cardList = new List<GameObject>();
	public List<GameObject> leftOverCardList = new List<GameObject>();

	private float depth;


	void Start () 
	{
		totalCards = initialCards;
		StartCoroutine (CreateCards ());

		UIManager.Instance ().UpdateCounter (true);
	}


	//creating the cards 
	private IEnumerator CreateCards()
	{
		depth = -1f;

		//creating a limit of cards 
		//Max limit is : initialCards varible
		for (int i = 0; i < initialCards; i++) 
		{

			depth -= 0.001f;

			//Instantiating the card gameobject from a card prefab
			GameObject cardClone = Instantiate (cardPrefabs, new Vector3 (0f, 8f, depth), Quaternion.AngleAxis (60f, Vector3.forward), deck);
			//putting the object name as index
			cardClone.name = i.ToString ();

			//ssigning some sprite on the card gameobject
			int cardSpriteIndex = i;
			if(cardSpriteIndex >= images.Length)
			{
				cardSpriteIndex = Random.Range (0, images.Length);
			}
			cardClone.GetComponent<SpriteRenderer>().sprite = images [cardSpriteIndex];

			//rotate the card object 
			LeanTween.rotateZ (cardClone, 0f, 0.1f);

			LeanTween.move (cardClone, new Vector3 (0f, 0f, depth), 0.1f);
		

			//adding the card to all card list
			cardList.Add (cardClone);

			//created one poolObject list from where cards will be reused
			objectPoolList.Add (cardClone);

			//making some delay in creating so that the card will get time for little animate
			yield return new WaitForSeconds (0.1f);

			//for swipe handling one script ia added 
			cardClone.GetComponent<SwipeController> ().enabled = true;
		}
	}
		

	//In case of the restart the game I'm resetting the card objects from card ObjectPool list
	public void RestartCardSpawn ()
	{
		
		StartCoroutine (ResetCardFromObjectPool());
	}


	//Once a card is swiped Left or Right its being removed from remaining card list
	public void RemoveCardsAndCheckTotal (bool isRightSwiped = false)
	{
		//Checking wheter the card is righr swiped
		if(isRightSwiped)
		{//is right swiped incrementing the unknown counter
			UIManager.Instance ().unknownCounter++;
		}
		else
		{
			UIManager.Instance ().knownCounter++;
		}

		//removing the card from all card List
		cardList.RemoveAt (totalCards - 1);

		//decreasing the card count
		totalCards--;

		//Updating the card counter UI
		UIManager.Instance ().UpdateCounter ();


		//Is no remaining card to swipe
		if (totalCards <= 0)
		{

			//If some cards are in the unknown list
			if (leftOverCardList.Count > 0) 
			{
				totalCards = leftOverCardList.Count;

				//adding the unknown card into play for another chance
				StartCoroutine(ResetPositionOfLeftOverCards ());
			} 
			else 
			{
				
				//If all the cards are swiped left GAMEOVER is showing
				UIManager.Instance ().GameOverPopUp.SetActive (true);
			}
		}
	}


	//Redisplay the cards which matked as unknown with initial position and rotation
	IEnumerator ResetPositionOfLeftOverCards()
	{

		//putting a banner showing WAIT for a littlte moment just before bringing back the unknown cards
		UIManager.Instance ().WaitObject.SetActive (true);
		yield return new WaitForSeconds (0.8f);

		//reseting the counter now
		UIManager.Instance ().UpdateCounter (true);
		UIManager.Instance ().WaitObject.SetActive (false);

		//clearing the total card list for reuse
		cardList.Clear ();

		//resetting the position and rotation of cards which marked as unknown
		for (int i = 0; i < leftOverCardList.Count; i++) 
		{
			leftOverCardList [i].transform.position = new Vector3 (0f, 8f, depth);
			leftOverCardList [i].SetActive (true);
			depth -= 0.001f;
			LeanTween.rotateZ (leftOverCardList[i], 0f, 0.1f);
			LeanTween.move (leftOverCardList[i], new Vector3 (0f, 0f, depth), 0.1f);

			cardList.Add (leftOverCardList[i]);

			yield return new WaitForSeconds (0.1f);
		}
		yield return new WaitForSeconds (0.6f);

		//clearing the list where all the unknown cards kept
		leftOverCardList.Clear ();
	}



	//reseting the card Incase of restart game
	IEnumerator ResetCardFromObjectPool()
	{
		totalCards = objectPoolList.Count;

		for (int i = 0; i < objectPoolList.Count; i++) 
		{
			depth -= 0.001f;
			GameObject cardClone = objectPoolList [i];
			objectPoolList [i].transform.position = new Vector3 (0, 8, -1);
			objectPoolList [i].transform.rotation = Quaternion.AngleAxis (60f, Vector3.forward);
			objectPoolList [i].SetActive (true);
			LeanTween.rotateZ (cardClone, 0f, 0.1f);
			LeanTween.move (cardClone, new Vector3 (0f, 0f, depth), 0.1f);
			cardList.Add (cardClone);

			yield return new WaitForSeconds (0.1f);
		}
	}
}
