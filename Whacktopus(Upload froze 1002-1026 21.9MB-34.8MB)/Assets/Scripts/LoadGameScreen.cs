using UnityEngine;
using System.Collections;

public class LoadGameScreen : MonoBehaviour {
	public ParticleSystem MainPart;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void OnClick () {
		StartCoroutine(Example());
	}

	IEnumerator Example() {
		MainPart.Play (); //Cascade bubbles
		yield return new WaitForSeconds(1);
		
		yield return new WaitForSeconds(1);
		MainPart.Stop (); //Prevent looping of particles
		yield return new WaitForSeconds(1);
		Application.LoadLevel("GameScreen"); //load the instructions screen

	}
}