﻿using UnityEngine;
using System.Collections;

public class Matching : MonoBehaviour {

	GameObject Points;

	public AudioClip Hit, Cash;

	ClickManager ClickManagerParent;
	States stater;
    //4 rays at 90 degrees starting at forward
    Ray[] FourDirections;
    RaycastHit[] FourHits;

    public struct Matches {
        public int upmatch;
        public int downmatch;
        public int leftmatch;
        public int rightmatch;

        public Matches( int upmatch,
                       int downmatch,
                       int leftmatch,
                       int rightmatch )
        {
            this.upmatch =0;
            this.downmatch=0;
            this.leftmatch=0;
            this.rightmatch=0;
        }
    }

    Matches MatchesThis;
   
	// Use this for initialization
	void Start () {
		ClickManagerParent = gameObject.transform.parent.transform.parent.GetComponent<ClickManager> ();
		Points = GameObject.Find ("Scripts");
		Hit = Resources.Load ("Sounds/Squid-sounds/hit-2") as AudioClip;
		Cash = Resources.Load ("Sounds/new folder/shillings") as AudioClip;
        FourDirections = new Ray[4];
        FourHits = new RaycastHit[4];
	}



	/*void FourDirectionsCheck(Matches matches, Collider[] Squidmatch, Collider[] SecondSquidmatch , int i, int j )
    {
        //checkup
        //check if Octopuses are the same type and stunned
        if((Squidmatch[i].gameObject.transform.parent.position.z > this.gameObject.transform.parent.position.z)
            && (Squidmatch[i].gameObject.transform.parent.position.x == this.gameObject.transform.parent.position.x))
        {

            //increase match counter
            matches++;

            //set second collider to new object
            Vector3 UpSquid = new Vector3(Squidmatch[i].gameObject.transform.parent.position.x, Squidmatch[i].gameObject.transform.parent.position.y, Squidmatch[i].gameObject.transform.parent.position.z);
            SecondSquidmatch = Physics.OverlapSphere(UpSquid, 1f);
            j = 0;
        }
    }*/

	// Update is called once per frame
	void Update () {

		ClickCheck();

		//check if a match check is needed for current object
        if (this.GetComponent<States>().currentOctopus == OctopusState.Stunned 
            && ClickManagerParent.GetClickHold() == 3)
        {
			//set matches for each direction to 0

			int totalmatch = 1;

            FourDirections[0] = new Ray((gameObject.transform.parent.position),
                                        (gameObject.transform.parent.forward));
            FourDirections[1] = new Ray((gameObject.transform.parent.position),
                                        (gameObject.transform.parent.right));
            FourDirections[2] = new Ray((gameObject.transform.parent.position),
                                        (-1*(gameObject.transform.parent.forward)));
            FourDirections[3] = new Ray((gameObject.transform.parent.position),
                                        (-1*(gameObject.transform.parent.right)));

            Debug.DrawRay(FourDirections[0].origin, FourDirections[0].direction, Color.magenta);

            for(int cross=0;cross<4;cross++)
            {
                if(Physics.Raycast(FourDirections[cross],out FourHits[cross], 0.6f)){
                    if(FourHits[cross].collider.gameObject.GetComponent<States>().currentOctopus == OctopusState.Stunned)
                    {

                    }
                
                
                }
            }
			bool verticalmatch = false;
			bool horizontalmatch = false;
			
			Vector3 origin = new Vector3(gameObject.transform.parent.position.x, 
			                             gameObject.transform.parent.position.y, 
			                             gameObject.transform.parent.position.z);

   

			Collider[] Squidmatch = Physics.OverlapSphere (origin, 1f);
			Collider[] SecondSquidmatch = Physics.OverlapSphere (origin, 1f);
			
			
			//find number matched in each direction
			int i = 0;
			int j = 0;
			
           // if ((Squidmatch[i].gameObject.tag == this.gameObject.tag)
           // && (Squidmatch[i].gameObject.GetComponent<States>().currentOctopus == OctopusState.Stunned)
			while (i < Squidmatch.Length) {

				//FourDirectionsCheck(upmatch, Squidmatch,SecondSquidmatch, i, j);
				//FourDirectionsCheck(downmatch, Squidmatch,SecondSquidmatch, i, j);
				//FourDirectionsCheck(leftmatch, Squidmatch,SecondSquidmatch, i, j);
				//FourDirectionsCheck(rightmatch, Squidmatch,SecondSquidmatch, i, j);

				i++;
			}
			/*
			
			//check if enough matched
			if ((upmatch + downmatch) >= 2){
				verticalmatch = true;
				totalmatch += upmatch + downmatch;
				
			}

			if ((leftmatch + rightmatch) >= 2){
				horizontalmatch = true;
				totalmatch += leftmatch + rightmatch;
			}
			
			// if a direction matched then set this object (the origin of the match) and the other matched tiles to cashed in
			if (verticalmatch == true || horizontalmatch == true){
				
				this.GetComponent<States> ().currentOctopus = OctopusState.Cashed;
				
				
				
				//clean up the matched squares
				i = 0;
				while (i < Squidmatch.Length) {

					//check if there is a vertical match
					if (verticalmatch == true){
						//checkup
						if (upmatch > 0){
							//get up octopus
							if ((Squidmatch [i].gameObject.transform.parent.position.z > this.gameObject.transform.parent.position.z) 
							    && (Squidmatch [i].gameObject.transform.parent.position.x == this.gameObject.transform.parent.position.x)) {
								
								//set to cashed in 
								Squidmatch [i].gameObject.GetComponent<States> ().currentOctopus = OctopusState.Cashed;

								if (upmatch == 2){
									//set second collider to new object
									Vector3 UpSquid = new Vector3 (Squidmatch [i].gameObject.transform.parent.position.x, Squidmatch [i].gameObject.transform.parent.position.y, Squidmatch [i].gameObject.transform.parent.position.z );
									SecondSquidmatch = Physics.OverlapSphere (UpSquid, 1f);
									j=0;
									
									//check up one more space
									while (j < SecondSquidmatch.Length) {
										
										//checkup
										//check if Octopuses are the same type and stunned
										if ((SecondSquidmatch [j].gameObject.transform.parent.position.z > Squidmatch [i].gameObject.transform.parent.position.z 
										    && SecondSquidmatch [j].gameObject.transform.parent.position.x == Squidmatch [i].gameObject.transform.parent.position.x)) {
											
											//increase match counter
											SecondSquidmatch [j].gameObject.GetComponent<States> ().currentOctopus = OctopusState.Cashed;
										}
										j++;
									}
								}
							}
						}
						
						//checkdown
						if (downmatch > 0){
							//check if Octopuses are the same type and stunned
							if ((Squidmatch [i].gameObject.transform.parent.position.z < this.gameObject.transform.parent.position.z 
							    && Squidmatch [i].gameObject.transform.parent.position.x == this.gameObject.transform.parent.position.x)) {
								
								//increase match counter
								Squidmatch [i].gameObject.GetComponent<States> ().currentOctopus = OctopusState.Cashed;

								if (downmatch == 2){
									//set second collider to new object
									Vector3 DownSquid = new Vector3 (Squidmatch [i].gameObject.transform.parent.position.x, Squidmatch [i].gameObject.transform.parent.position.y, Squidmatch [i].gameObject.transform.parent.position.z );
									SecondSquidmatch = Physics.OverlapSphere (DownSquid, 1f);
									j=0;
									
									//check down one more space
									while (j < SecondSquidmatch.Length) {
										
										//check if Octopuses are the same type and stunned
										if ((SecondSquidmatch [j].gameObject.transform.parent.position.z < Squidmatch [i].gameObject.transform.parent.position.z) 
										    && (SecondSquidmatch [j].gameObject.transform.parent.position.x == Squidmatch [i].gameObject.transform.parent.position.x)) {
											
											//increase match counter
											SecondSquidmatch [j].gameObject.GetComponent<States> ().currentOctopus = OctopusState.Cashed;
										}
										j++;
									}
								}
							}
						}
					}


					//check if there is a horizontal match
					if(horizontalmatch == true){
						//checkright
						if (rightmatch > 0){
							//check if Octopuses are the same type and stunned
							if ((Squidmatch [i].gameObject.transform.parent.position.x > this.gameObject.transform.parent.position.x 
							    && Squidmatch [i].gameObject.transform.parent.position.z == this.gameObject.transform.parent.position.z)) {
								
								//increase match counter
								Squidmatch [i].gameObject.GetComponent<States> ().currentOctopus = OctopusState.Cashed;

								if (rightmatch == 2){
									//set second collider to new object
									Vector3 RightSquid = new Vector3 (Squidmatch [i].gameObject.transform.parent.position.x, Squidmatch [i].gameObject.transform.parent.position.y, Squidmatch [i].gameObject.transform.parent.position.z );
									SecondSquidmatch = Physics.OverlapSphere (RightSquid, 1f);
									j=0;
									
									//check one more space
									while (j < SecondSquidmatch.Length) {
										
										//checkright
										//check if Octopuses are the same type and stunned
										if ((SecondSquidmatch [j].gameObject.transform.parent.position.x > Squidmatch [i].gameObject.transform.parent.position.x) 
										    && (SecondSquidmatch [j].gameObject.transform.parent.position.z == Squidmatch [i].gameObject.transform.parent.position.z)) {
											
											//increase match counter
											SecondSquidmatch [j].gameObject.GetComponent<States> ().currentOctopus = OctopusState.Cashed;
										}
										j++;
									}
								}
							}
							
						}
						
						//checkleft
						if (leftmatch > 0){
							//check if Octopuses are the same type and stunned
							if ((Squidmatch [i].gameObject.transform.parent.position.x < this.gameObject.transform.parent.position.x 
							    && Squidmatch [i].gameObject.transform.parent.position.z == this.gameObject.transform.parent.position.z)) {
								
								//increase match counter
								Squidmatch [i].gameObject.GetComponent<States> ().currentOctopus = OctopusState.Cashed;

								if (leftmatch == 2){
									//set second collider to new object
									Vector3 LeftSquid = new Vector3 (Squidmatch [i].gameObject.transform.parent.position.x, Squidmatch [i].gameObject.transform.parent.position.y, Squidmatch [i].gameObject.transform.parent.position.z );
									SecondSquidmatch = Physics.OverlapSphere (LeftSquid, 1f);
									j=0;
									
									//check one more space
									while (j < SecondSquidmatch.Length) {
										
										//checkup
										//check if Octopuses are the same type and stunned
										if ((SecondSquidmatch [j].gameObject.transform.parent.position.x < Squidmatch [i].gameObject.transform.parent.position.x 
										    && SecondSquidmatch [j].gameObject.transform.parent.position.z == Squidmatch [i].gameObject.transform.parent.position.z)) {
											
											//increase match counter
											SecondSquidmatch [j].gameObject.GetComponent<States> ().currentOctopus = OctopusState.Cashed;
										}
										j++;
									}
								}
							}
						}
					}
						
					i++;
				}
				
			}
			*/
			//score points if match >=3
			if (totalmatch >=3){
				//attach to global
				switch (totalmatch)
				{
				case 3:
					Points.GetComponent<InGameGlobals>().AddPoints(600);
					break;
				case 4:
					Points.GetComponent<InGameGlobals>().AddPoints(1200);
					break;
				case 5:
					Points.GetComponent<InGameGlobals>().AddPoints(2000);
					break;
				case 6:
					Points.GetComponent<InGameGlobals>().AddPoints(3000);
					break;
				case 7:
					Points.GetComponent<InGameGlobals>().AddPoints(4200);
					break;
				case 8:
					Points.GetComponent<InGameGlobals>().AddPoints(5600);
					break;
				case 9:
					Points.GetComponent<InGameGlobals>().AddPoints(9001);
					break;

				}

			}


			//set current check to none
			this.GetComponent<States> ().currentCheck = CheckState.None;
		}		
	}

	public void ClickCheck(){
		if (ClickManagerParent.GetClickHold () == 2) 
		{
			if (ClickManagerParent.CurrentOctoObject == this.gameObject) 
			{
				if (this.GetComponent<States> ().currentOctopus == OctopusState.Stunned) {
					//cash in
					this.transform.gameObject.GetComponent<AudioSource> ().PlayOneShot (Cash);
					this.GetComponent<States> ().currentOctopus = OctopusState.Cashed;
				
					Cashedin ();
				}

				if ((this.gameObject.GetComponent<States> ().currentOctopus == OctopusState.Jumping 
				     || this.gameObject.GetComponent<States> ().currentOctopus == OctopusState.Idle)) {
					//set Octopus to stunned
					this.gameObject.GetComponent<States> ().currentOctopus = OctopusState.Stunned;
					this.gameObject.GetComponent<Renderer> ().material.color = Color.red;
					//set checkmatches flag
					this.gameObject.GetComponent<States> ().currentCheck = CheckState.CheckMatch;
				
					this.gameObject.GetComponent<AudioSource> ().PlayOneShot (Hit);
					//set numclicks to 0
					//this.GetComponent<Swapping>().numclicks = 0;
				}
			}
		}
	}

	/*//test to see if octopus can be cashed in
	public void TestCashin(){
		if (this.GetComponent<States> ().currentOctopus == OctopusState.Stunned) {
			//cash in

			
			this.transform.gameObject.GetComponent<AudioSource>().PlayOneShot(Cash);
			this.GetComponent<States> ().currentOctopus = OctopusState.Cashed;
			
			Cashedin ();
		}
	}

	//test to see if octopus can be stunned
	public void TestStun(){
		if ((this.GetComponent<States> ().currentOctopus == OctopusState.Jumping || this.GetComponent<States> ().currentOctopus == OctopusState.Idle)) {
			//set Octopus to stunned
			this.GetComponent<States> ().currentOctopus = OctopusState.Stunned;
			this.gameObject.GetComponent<Renderer>().material.color = Color.red;
			//set checkmatches flag
			this.GetComponent<States> ().currentCheck = CheckState.CheckMatch;
			
			this.transform.gameObject.GetComponent<AudioSource>().PlayOneShot(Hit);
			//set numclicks to 0
			//this.GetComponent<Swapping>().numclicks = 0;
		}
	}*/



	void Cashedin(){
		
		Vector3 origin = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
		Collider[] Squidmatch = Physics.OverlapSphere (origin, 1f);
		
		int i = 0;
		
		var TwoCashIn = false;
		//
		while (i < Squidmatch.Length && TwoCashIn == false) {
			//check if squids are the same type and ready for cashing in
			if ((Squidmatch [i].gameObject.tag == this.gameObject.tag)
			    && (Squidmatch [i].gameObject.GetComponent<States> ().currentOctopus == OctopusState.Stunned)
			    && (this.GetComponent<States> ().currentOctopus == OctopusState.Cashed)) {

				//cash in
				Squidmatch [i].gameObject.GetComponent<States> ().currentOctopus = OctopusState.Cashed;
				
				TwoCashIn = true;
			}
			

			i++;
		}

		//score points
		//attach to global

		if (TwoCashIn == true) {
			Points.GetComponent<InGameGlobals>().AddPoints(300);
		} else {
			Points.GetComponent<InGameGlobals>().AddPoints(100);
		}

	}
	

}