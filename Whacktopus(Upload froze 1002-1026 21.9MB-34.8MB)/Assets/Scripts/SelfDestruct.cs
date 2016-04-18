using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {
	float step;
	// Use this for initialization
	void Start () {
		step = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		step += Time.deltaTime;
		if (step > 1.0f) {
			Destroy(gameObject);
		}
	}
}
