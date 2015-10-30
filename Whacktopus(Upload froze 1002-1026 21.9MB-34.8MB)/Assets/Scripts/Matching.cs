using UnityEngine;
using System.Collections;

public enum state
{
	Under,
	Stunned,
	Jumping,
	Cashed
};

public class Matching : MonoBehaviour {

	private int clicks=0;
	public state current=state.Under;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown(){
		clicks++;
		if (clicks == 1) {
			current = state.Stunned;
		}
		if (clicks == 2) {
			current = state.Cashed;
		}
	}

	void Cashedin(){
		Destroy(this.gameObject);
	}

	void OnTriggerEnter(Collider collision) {
		if (collision.gameObject.tag == "Blue_octopus" 
			&& this.gameObject.tag == "Blue_octopus"
			&& collision.gameObject.GetComponent<Matching> ().current == state.Cashed
			&& this.current == state.Stunned) {
			collision.gameObject.GetComponent<Matching>().Cashedin();
		}

		if (clicks == 2) {
			Cashedin ();
		}
	}
}
