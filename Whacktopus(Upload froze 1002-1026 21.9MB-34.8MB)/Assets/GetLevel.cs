using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GetLevel : MonoBehaviour {

	GameObject level;

	// Use this for initialization
	void Start () {
		level = GameObject.Find ("Scripts");
	}

	// Update is called once per frame
	void Update () {
		this.gameObject.GetComponent<Text> ().text = (level.GetComponent<InGameGlobals> ().Level).ToString ();
	}
}
