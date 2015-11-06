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

	IEnumerator JumpRoutine(){
		this.GetComponent<Rigidbody> ().AddForce(new Vector3(0.0f, 5.0f, 0.0f));
		yield return new WaitForSeconds (1);//Random.Range(5,20));
		this.GetComponent<States> ().current = state.Jumping;
		this.GetComponent<Rigidbody> ().AddForce(new Vector3(0.0f, -5.0f, 0.0f));
		this.GetComponent<Rigidbody> ().AddForce(new Vector3(0.0f, -5.0f, 0.0f));
		yield return new WaitForSeconds (1);//Random.Range(5,20));
		this.GetComponent<Rigidbody> ().AddForce(new Vector3(0.0f, 5.0f, 0.0f));
		yield return new WaitForSeconds (Random.Range(5,20));
		this.GetComponent<States> ().current = state.Under;
	}
}
