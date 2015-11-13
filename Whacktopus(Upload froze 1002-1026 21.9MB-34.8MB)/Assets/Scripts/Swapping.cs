using UnityEngine;
using System.Collections;

public class Swapping : MonoBehaviour {

	public float testin, testin2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 mouseInp = Input.mousePosition;
		Vector3 worldpos = Camera.main.ScreenToWorldPoint(new Vector3(mouseInp.x, mouseInp.y, 0.0f));
		gameObject.transform.position = (worldpos);
		testin = worldpos.x;
		testin2 = gameObject.transform.position.x;
	}

	void OnMouseClick() {
	
	}
}
