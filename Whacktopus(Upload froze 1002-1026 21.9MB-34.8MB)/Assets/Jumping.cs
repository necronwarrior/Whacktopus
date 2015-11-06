using UnityEngine;
using System.Collections;

public class Jumping : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update()
	{
		if(this.GetComponent<States> ().current == state.Under)//(jumpButtonDown && !jumping)
		{
			//jumping = true;
			StartCoroutine(JumpRoutine());
		}
	}
	
	IEnumerator Example() {
		this.GetComponent<States> ().current = state.Under;
		yield return new WaitForSeconds(Random.Range(5,20));
		this.GetComponent<States> ().current = state.Jumping;
	}

	IEnumerator JumpRoutine()
	{

	}
}
