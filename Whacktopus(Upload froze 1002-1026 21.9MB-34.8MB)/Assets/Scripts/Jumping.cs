using UnityEngine;
using System.Collections;

public class Jumping : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update()
	{
		if(this.GetComponent<States> ().currentOctopus == OctopusState.Under)//(jumpButtonDown && !jumping)
		{
		
		}
	}
	
	IEnumerator Example() {
		this.GetComponent<States> ().currentOctopus = OctopusState.Under;
		yield return new WaitForSeconds(Random.Range(5,20));
		this.GetComponent<States> ().currentOctopus = OctopusState.Jumping;
	}

}
