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
	public CheckState currentCheck = CheckState.None;

	//TESTING setting all blocks at srtart to blue octtopuses
	//public Squaretype currentType = Squaretype.Empty;
	public Squaretype currentType = Squaretype.Blue_Octopus;


	public float JumpTime = 0;
	public float StunTime = 0;
	public float CashTime = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
