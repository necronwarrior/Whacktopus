using UnityEngine;
using System.Collections;

public enum state
{
	Under,
	Stunned,
	Jumping,
	Cashed
};

public class States : MonoBehaviour {
	public state current=state.Under;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
