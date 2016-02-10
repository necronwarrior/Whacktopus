using UnityEngine;
using System.Collections;

//The State of what the octopus is in a space
public enum OctopusState
{
	Under,
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
	public float StunTime = 0;
	public float CashTime = 0;
	
	Animator OctoAnimator;

	// Use this for initialization
	void Start () {
		OctoAnimator = gameObject.GetComponentsInParent<Animator>()[0];
	}
	
	// Update is called once per frame
	void Update () {
	
		if (currentOctopus == OctopusState.Jumping){
			OctoAnimator.SetBool("Spawned", true);
		}

		if (currentOctopus == OctopusState.Stunned){
			OctoAnimator.SetBool("One tap", true);
		}

		if (currentOctopus == OctopusState.Cashed){
			OctoAnimator.SetBool("Two tap", true);
		}

		/*if (currentOctopus == OctopusState.Under){
			OctoAnimator.SetBool("Spawned", false);
			OctoAnimator.SetBool("One tap", false);
			OctoAnimator.SetBool("Two tap", false);
			OctoAnimator.SetBool("Time to Under", false);
			OctoAnimator.SetBool("Time to Idle", false);

		}*/

	}
}
