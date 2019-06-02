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


	private IEnumerator CreateCards()
	{
		depth = -1f;

		for (int i = 0; i < initialCards; i++) 
		{

			depth -= 0.001f;
			GameObject cardClone = Instantiate (cardPrefabs, new Vector3 (0f, 8f, depth), Quaternion.AngleAxis (60f, Vector3.forward), deck);
			cardClone.name = i.ToString ();

			int cardSpriteIndex = i;
			if(cardSpriteIndex >= images.Length)
			{
				cardSpriteIndex = Random.Range (0, images.Length);
			}
			cardClone.GetComponent<SpriteRenderer>().sprite = images [cardSpriteIndex];

			LeanTween.rotateZ (cardClone, 0f, 0.1f);
			LeanTween.move (cardClone, new Vector3 (0f, 0f, depth), 0.1f);
		
			cardList.Add (cardClone);
			objectPoolList.Add (cardClone);

			yield return new WaitForSeconds (0.1f);
		}
	}
		

	public void RestartCardSpawn ()
	{
		
		StartCoroutine (ResetCardFromObjectPool());
	}

	public void RemoveCardsAndCheckTotal (bool isRightSwiped = false)
	{

		if(isRightSwiped)
		{
			UIManager.Instance ().unknownCounter++;
		}
		else
		{
			UIManager.Instance ().knownCounter++;
		}
		cardList.RemoveAt (totalCards - 1);
		totalCards--;
		UIManager.Instance ().UpdateCounter ();

		if (totalCards <= 0)
		{
			if (leftOverCardList.Count > 0) 
			{
				totalCards = leftOverCardList.Count;
				StartCoroutine(ResetPositionOfLeftOverCards ());
			} 
			else 
			{
				UIManager.Instance ().GameOverPopUp.SetActive (true);
			}
		}
	}

	IEnumerator ResetPositionOfLeftOverCards()
	{
		
		UIManager.Instance ().WaitObject.SetActive (true);
		yield return new WaitForSeconds (0.8f);
		UIManager.Instance ().UpdateCounter (true);
		UIManager.Instance ().WaitObject.SetActive (false);
		cardList.Clear ();

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
		leftOverCardList.Clear ();
	}

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
