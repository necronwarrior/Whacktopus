using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadFinalScore : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Text>().text = PlayerPrefs.GetInt ("FinalScore").ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
