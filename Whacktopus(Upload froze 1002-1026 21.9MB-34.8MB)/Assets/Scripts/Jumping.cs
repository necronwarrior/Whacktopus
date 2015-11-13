using UnityEngine;
using System.Collections;

public class Jumping : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update()
	{
		if(this.GetComponent<States> ().currentOctopus == OctopusState.Under)//if in a position to jump
		{
			//reset timers
			this.gameObject.GetComponent<States> ().JumpTime = 0;
			this.gameObject.GetComponent<States> ().StunTime = 0;

			//get random number
			float number = Random.Range(0,2000);


			//check for jumping
			if( number < Time.deltaTime/20000)
			{
				//set type
				this.GetComponent<States> ().currentType = Squaretype.Blue_Octopus;

				//set to jumping and change color
				this.gameObject.GetComponent<States> ().currentOctopus = OctopusState.Jumping;
				this.gameObject.GetComponent<Renderer>().material.color = Color.blue;

			}
		}

		if (this.GetComponent<States> ().currentOctopus == OctopusState.Jumping) //currently jumping
		{
			//set current time jumping
			this.gameObject.GetComponent<States> ().JumpTime += Time.deltaTime;

			//check if jumptime has ended
			if (this.gameObject.GetComponent<States> ().JumpTime >= 4)
			{
				//set back to being under and change color
				this.GetComponent<States> ().currentOctopus = OctopusState.Under;
				this.gameObject.GetComponent<Renderer>().material.color = Color.white;
			}

		}

		if (this.GetComponent<States> ().currentOctopus == OctopusState.Stunned) //currently stunned
		{
			//set current time jumping
			this.gameObject.GetComponent<States> ().StunTime += Time.deltaTime;
			
			//check if jumptime has ended
			if (this.gameObject.GetComponent<States> ().StunTime >= 10)
			{
				//set back to being under and change color
				this.GetComponent<States> ().currentOctopus = OctopusState.Under;
				this.GetComponent<States> ().currentType = Squaretype.Empty;
				this.gameObject.GetComponent<Renderer>().material.color = Color.white;
			}
			
		}

	}
	
	/*IEnumerator Example() {
		this.GetComponent<States> ().currentOctopus = OctopusState.Under;
		yield return new WaitForSeconds(Random.Range(5,20));
		this.GetComponent<States> ().currentOctopus = OctopusState.Jumping;
	}*/

}
