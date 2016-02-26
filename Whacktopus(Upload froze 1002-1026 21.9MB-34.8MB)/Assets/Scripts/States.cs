using UnityEngine;
using System.Collections;

//The State of what the octopus is in a space
public enum OctopusState
{
	Under,
	Idle,
	Stunned,
	Jumping,
	Picked,
	Cashed
};

//The State of a space to see if that space needs to be checked for matches
public enum CheckState
{
	None,
	Moving,
	CheckMatch
}

//The type of object in the grid space

public class States : MonoBehaviour {
	public OctopusState currentOctopus = OctopusState.Under;
	//notes what the previous state was (for picking and moving)
	public OctopusState PrevState = OctopusState.Under;
	public CheckState currentCheck = CheckState.None;

	public float JumpTime = 0;
	public float IdleTime = 0;
	public float StunTime = 0;
	public float CashTime = 0;
	Animator OctoAnimator;
	Material Red_Octo, Green_Octo, Orange_Octo;

	// Use this for initialization
	void Start () {
		OctoAnimator = gameObject.GetComponentsInParent<Animator>()[0];
		Red_Octo = Resources.Load ("Materials/octomaterials/Materials/Red_Octo") as Material;
		Green_Octo = Resources.Load ("Materials/octomaterials/Materials/Green_Octo") as Material;
		Orange_Octo = Resources.Load ("Materials/octomaterials/Materials/Orange_Octo") as Material;

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (currentOctopus == OctopusState.Jumping && !OctoAnimator.GetBool("Spawned")){
			int Rand_Mat;
			Rand_Mat = Random.Range(1,4);
			switch(Rand_Mat){
			case 1: gameObject.GetComponent<Renderer>().material = Red_Octo;
				gameObject.tag = "Red_octo";
				break;
			case 2: gameObject.GetComponent<Renderer>().material = Green_Octo;
				gameObject.tag = "Green_octo";
				break;
			case 3: gameObject.GetComponent<Renderer>().material = Orange_Octo;
				gameObject.tag = "Orange_octo";
				break;
			default: gameObject.GetComponent<Renderer>().material = null;
				gameObject.tag = "default";
				break;
			};

			gameObject.transform.parent.position = new Vector3(gameObject.transform.parent.position.x,0,gameObject.transform.parent.position.z);
			OctoAnimator.SetBool("Spawned", true);


		}

		if (currentOctopus == OctopusState.Idle && !OctoAnimator.GetBool("Spawned")) {
			OctoAnimator.SetBool("Spawned", false);	
			OctoAnimator.SetBool("Time to Under", false);
		}

		if (currentOctopus == OctopusState.Stunned && !OctoAnimator.GetBool("One tap")){
			OctoAnimator.SetBool("Spawned", false);
			OctoAnimator.SetBool("One tap", true);
		}

		if (currentOctopus == OctopusState.Cashed && !OctoAnimator.GetBool("Two tap")){
			OctoAnimator.SetBool("Two tap", true);
		}

		if (currentOctopus == OctopusState.Under){
			this.transform.gameObject.GetComponent<AudioSource>().Stop();
			gameObject.transform.parent.position = new Vector3(gameObject.transform.parent.position.x,-1.5f,gameObject.transform.parent.position.z);
			ResetStatebools();
		}

	}

	public void ResetStatebools(){
		OctoAnimator.SetBool("Spawned", false);
		OctoAnimator.SetBool("One tap", false);
		OctoAnimator.SetBool("Two tap", false);
		OctoAnimator.SetBool("Time to Idle", false);
	}

	public void SetUnder(){

		currentOctopus = OctopusState.Under;
		this.gameObject.GetComponent<Renderer>().material.color = Color.white;
		ResetStatebools();
	
	}
}
