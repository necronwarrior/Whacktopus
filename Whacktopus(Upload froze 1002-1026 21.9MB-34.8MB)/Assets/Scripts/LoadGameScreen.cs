using UnityEngine;
using System.Collections;

public class LoadGameScreen : MonoBehaviour {
	public ParticleSystem MainPart;
	public GameObject Foam;
	GameObject MainCam;
	bool Foamy;

	AudioClip Click;
	
	void Start()
	{	
		Foamy = false;
		Click = Resources.Load ("Sounds/moving/Button-Click-2") as AudioClip;
		MainCam = GameObject.Find("Main Camera");
	}
	
	// Update is called once per frame
	public void OnClick () {
		Foamy = true;
	this.transform.gameObject.GetComponent<AudioSource>().PlayOneShot(Click);
		StartCoroutine(Example());
	}

	IEnumerator Example() {


		yield return new WaitForSeconds(3);
		Application.LoadLevel("GameScreen"); //load the instructions screen

	}

	void Update()
	{
		if (Foamy == true) {
			Foam.transform.position = 		Vector3.MoveTowards (Foam.transform.position, 
			                                                 new Vector3 (MainCam.transform.position.x , MainCam.transform.position.y, MainCam.transform.position.z+1.0f),
			                                                 3.0f);
		}
	}
}