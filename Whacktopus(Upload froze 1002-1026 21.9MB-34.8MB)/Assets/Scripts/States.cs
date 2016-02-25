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
public enum Squaretype
{
	Empty,
	Blue_Octopus
}


public class States : MonoBehaviour {
	public OctopusState currentOctopus = OctopusState.Under;
	//notes what the previous state was (for picking and moving)
	public OctopusState PrevState = OctopusState.Under;
	public CheckState currentCheck = CheckState.None;

	//TESTING setting all blocks at srtart to blue octtopuses
	//public Squaretype currentType = Squaretype.Empty;
	public Squaretype currentType = Squaretype.Blue_Octopus;



	public float JumpTime = 0;
	public float IdleTime = 0;
	public float StunTime = 0;
	public float CashTime = 0;
	Animator OctoAnimator;

	// Use this for initialization
	void Start () {
		OctoAnimator = gameObject.GetComponentsInParent<Animator>()[0];
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (currentOctopus == OctopusState.Jumping && !OctoAnimator.GetBool("Spawned")){
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
		currentType = Squaretype.Empty;
		this.gameObject.GetComponent<Renderer>().material.color = Color.white;
		ResetStatebools();
	
	}
}
