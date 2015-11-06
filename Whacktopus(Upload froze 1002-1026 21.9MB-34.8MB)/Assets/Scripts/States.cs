using UnityEngine;
using System.Collections;

//The State of what the octopus is in a space
public enum OctopusState
{
	Under,
	Stunned,
	Jumping,
	Cashed
};

//The State of a space to see if that space needs to be checked for matches
public enum CheckState
{
	None,
	Moving,
	CheckMatch
}

public class States : MonoBehaviour {
	public OctopusState currentOctopus = OctopusState.Under;
	public CheckState currentCheck = CheckState.None;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
