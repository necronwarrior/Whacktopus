using UnityEngine;
using System.Collections;

public class LoadInstructionScreen : MonoBehaviour {
	public ParticleSystem MainPart;

	// Use this for initialization
		AudioClip Click;
	
	void Start()
	{	
		Click = Resources.Load ("Sounds/moving/Button-Click-2") as AudioClip;
	}
	
	// Update is called once per frame
	public void OnClick () {
	this.transform.gameObject.GetComponent<AudioSource>().PlayOneShot(Click);
		StartCoroutine(Example());
	}

	IEnumerator Example() {
		MainPart.Play (); //Cascade bubbles
		yield return new WaitForSeconds(1);
		
		yield return new WaitForSeconds(1);
		MainPart.Stop (); //Prevent looping of particles
		yield return new WaitForSeconds(1);
		Application.LoadLevel("InstructionScreen"); //load the instructions screen

	}
}