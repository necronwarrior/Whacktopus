using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
		this.transform.gameObject.tag = "NotPaused";
	}
	
	// Update is called once per frame
	public void OnClick () {
		if (this.transform.gameObject.tag == "Paused") {
			Time.timeScale = 1;
			this.transform.gameObject.tag = "NotPaused";
		} else {
			Time.timeScale = 0;
			this.transform.gameObject.tag = "Paused";
		}
	}
}