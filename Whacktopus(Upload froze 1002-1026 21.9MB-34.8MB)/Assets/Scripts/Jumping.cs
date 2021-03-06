﻿using UnityEngine;
using System.Collections;

public class Jumping : MonoBehaviour {

	Animator OctoAnimator;
	AudioClip Spawn, Stun;
	bool OneSpawn;
    float CompoundTime;
    public float spawn_number;
	// Use this for initialization
	void Start () {
		OctoAnimator = gameObject.GetComponentsInParent<Animator>()[0];
		
		Spawn = Resources.Load ("Sounds/Octo_sounds/Spawn/Water_spawn_1") as AudioClip;
		OneSpawn = true;
        CompoundTime = 0;
        //spawn_number = Random.Range(1,300);
	}
	
	// Update is called once per frame
	void Update()
	{

		//clear cashed
		if (this.GetComponent<States> ().currentOctopus == OctopusState.Cashed) //currently stunned
		{
			//set current time cashing in
			this.gameObject.GetComponent<States> ().CashTime += Time.deltaTime;
			
			//check if cashtime has ended
			if (this.gameObject.GetComponent<States> ().CashTime >= 1)
			{
				//set back to being under and change color
				this.GetComponent<States> ().SetUnder();
			}
			
		}

		if(this.GetComponent<States> ().currentOctopus == OctopusState.Under)//if in a position to jump
		{
			//reset timers
			this.gameObject.GetComponent<States> ().JumpTime = 0;
			this.gameObject.GetComponent<States> ().StunTime = 0;
			this.gameObject.GetComponent<States> ().CashTime = 0;
			this.gameObject.GetComponent<States> ().IdleTime = 0;

			//get random number

            CompoundTime += (Time.deltaTime*50.0f);

			//check for jumping
            if( spawn_number < CompoundTime)
			{
				//set type
				//this.GetComponent<States> ().currentType = Squaretype.Blue_Octopus;

				//set to jumping and change color
				this.gameObject.GetComponent<States> ().currentOctopus = OctopusState.Jumping;
                CompoundTime=0;
			}
		}

		if (this.GetComponent<States> ().currentOctopus == OctopusState.Jumping) //currently jumping
		{
			//set current time jumping
			this.gameObject.GetComponent<States> ().JumpTime += Time.deltaTime;


			gameObject.transform.parent.position = new Vector3(gameObject.transform.parent.position.x,(float)(-1.5 + (1.5 * this.gameObject.GetComponent<States> ().JumpTime/1.4)),gameObject.transform.parent.position.z);

			if(GetComponent<States> ().JumpTime >= 0.6 &&
			   GetComponent<States> ().currentOctopus == OctopusState.Jumping &&
			   OneSpawn == true)
			{
				GetComponent<AudioSource>().PlayOneShot(Spawn);
				OneSpawn =false;
                gameObject.transform.GetComponent<SphereCollider>().enabled = true;
			}


			//check if jumptime has ended
			if (this.gameObject.GetComponent<States> ().JumpTime >= 1.4 &&
			    this.GetComponent<States> ().currentOctopus == OctopusState.Jumping)
			{
				OneSpawn =true;
				this.GetComponent<States> ().currentOctopus = OctopusState.Idle;
				gameObject.transform.parent.position = new Vector3(gameObject.transform.parent.position.x,(float)0.0f,gameObject.transform.parent.position.z);
			}

		}

		if (this.GetComponent<States> ().currentOctopus == OctopusState.Idle) //currently idle
		{
			//set current time jumping
			this.gameObject.GetComponent<States> ().IdleTime += Time.deltaTime;

			//animate sinking
			/*if (this.gameObject.GetComponent<States> ().IdleTime >= 8.0)
			{
				gameObject.transform.parent.position = new Vector3(gameObject.transform.parent.position.x,(float)(0 - (1.5 * (this.gameObject.GetComponent<States> ().IdleTime - 2)/0.6)),gameObject.transform.parent.position.z);
                gameObject.transform.GetComponent<SphereCollider>().enabled = false;
            }*/

			//check if jumptime has ended 
			if (this.gameObject.GetComponent<States> ().IdleTime >= 9.6 &&
			    this.GetComponent<States> ().currentOctopus == OctopusState.Idle)
			{
				//set back to being under and change color
				OctoAnimator.SetBool("Time to Under", true);
				this.GetComponent<States> ().SetUnder();
			}
			
		}

		if (GetComponent<States> ().currentOctopus == OctopusState.Stunned) //currently stunned
		{
			//set current time jumping
			this.gameObject.GetComponent<States> ().StunTime += Time.deltaTime;
			gameObject.transform.parent.position = new Vector3(gameObject.transform.parent.position.x,(float)0.0f,gameObject.transform.parent.position.z);
			
			//check if jumptime has ended
			if (this.gameObject.GetComponent<States> ().StunTime >= 10 &&
			    this.GetComponent<States> ().currentOctopus == OctopusState.Stunned)
			{
				//set back to being under and change color
                this.GetComponent<States> ().currentOctopus = OctopusState.Idle;
                this.GetComponent<States> ().StarroDestroy();
                OctoAnimator.SetBool("Time to Idle", true);
                OctoAnimator.SetBool("One tap", false);
			}
			
		}

	}

}
