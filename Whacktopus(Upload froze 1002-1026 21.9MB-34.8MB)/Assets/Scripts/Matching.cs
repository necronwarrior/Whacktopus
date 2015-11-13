using UnityEngine;
using System.Collections;

public class Matching : MonoBehaviour {

	//private int clicks=0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	//removed clicks
	void OnMouseDown(){
		if (this.GetComponent<States> ().currentOctopus == OctopusState.Jumping) {
			this.GetComponent<States> ().currentOctopus = OctopusState.Stunned;
			this.gameObject.GetComponent<Renderer>().material.color = Color.red;
		}
		else if (this.GetComponent<States> ().currentOctopus == OctopusState.Stunned) {
			this.GetComponent<States> ().currentOctopus = OctopusState.Cashed;

			Cashedin ();
		}
	}

	void Cashedin(){

		Vector3 origin = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
		Collider[] Squidmatch = Physics.OverlapSphere (origin, 1f);
		
		int i = 0;

		//
		while (i < Squidmatch.Length) {
			//check if squids are the same type and ready for cashing in
			if (Squidmatch [i].gameObject.GetComponent<States> ().currentType == Squaretype.Blue_Octopus
			    && this.gameObject.GetComponent<States> ().currentType == Squaretype.Blue_Octopus
			    && Squidmatch [i].gameObject.GetComponent<States> ().currentOctopus == OctopusState.Stunned
			    && this.GetComponent<States> ().currentOctopus == OctopusState.Cashed) {
				Squidmatch [i].gameObject.GetComponent<States> ().currentOctopus = OctopusState.Cashed;
				Squidmatch [i].gameObject.GetComponent<Matching> ().Cashedin ();
			}
			/* The following was removed and changed to the avbove to accomodate the new type variable state and
			 if (Squidmatch [i].gameObject.tag == "Blue_Octopus" 
			    && this.gameObject.tag == "Blue_Octopus"
			    && Squidmatch [i].gameObject.GetComponent<States> ().currentOctopus == OctopusState.Stunned
			    && this.GetComponent<States> ().currentOctopus == OctopusState.Cashed) {
				Squidmatch [i].gameObject.GetComponent<States> ().currentOctopus = OctopusState.Cashed;
				Squidmatch [i].gameObject.GetComponent<Matching> ().Cashedin ();
			}*/
			i++;
		}

		//reset back to under
		this.GetComponent<States> ().currentOctopus = OctopusState.Under;
		this.gameObject.GetComponent<Renderer>().material.color = Color.white;

		//removed so i can reset to normal
		//Destroy(this.gameObject);
	}

	/*void OnTriggerStay(Collider collision) {
		if (collision.gameObject.tag == "Blue_octopus" 
			&& this.gameObject.tag == "Blue_octopus"
			&& collision.gameObject.GetComponent<Matching> ().current == state.Cashed
			&& this.current == state.Stunned) {
			this.gameObject.GetComponent<Matching>().Cashedin();
		}

		if (clicks == 2) {
			Cashedin ();
		}
	}*/
}
