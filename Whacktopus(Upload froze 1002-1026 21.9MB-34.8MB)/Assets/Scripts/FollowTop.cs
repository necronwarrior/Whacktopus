using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FollowTop : MonoBehaviour {

	public GameObject Coin_limit;
	public float coinsdsad;
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		coinsdsad = Coin_limit.GetComponent<RectTransform>().localPosition.y + ((Coin_limit.GetComponent<RectTransform>().rect.height/2) *Coin_limit.GetComponent<RectTransform>().localScale.y) ;
		GetComponent<RectTransform>().localPosition = new Vector3 (GetComponent<RectTransform>().localPosition.x,
			coinsdsad,
			GetComponent<RectTransform>().localPosition.z);
	}
}
