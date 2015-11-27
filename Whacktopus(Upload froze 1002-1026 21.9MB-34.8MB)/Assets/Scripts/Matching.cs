﻿using UnityEngine;
using System.Collections;

public class Matching : MonoBehaviour {
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//check if a match check is needed for current object
		if (this.GetComponent<States> ().currentCheck == CheckState.CheckMatch && this.GetComponent<States> ().currentOctopus == OctopusState.Stunned) {
			//set matches for each direction to 0
			int upmatch = 0;
			int downmatch = 0;
			int leftmatch = 0;
			int rightmatch = 0;
			int totalmatch = 1;
			
			bool verticalmatch = false;
			bool horizontalmatch = false;
			
			Vector3 origin = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
			Collider[] Squidmatch = Physics.OverlapSphere (origin, 1f);
			Collider[] SecondSquidmatch = Physics.OverlapSphere (origin, 1f);
			
			
			//find number matched in each direction
			int i = 0;
			int j = 0;
			
			while (i < Squidmatch.Length) {
				
				//checkup
				//check if Octopuses are the same type and stunned
				if ((Squidmatch [i].gameObject.GetComponent<States> ().currentType == this.gameObject.GetComponent<States> ().currentType)
				    && (Squidmatch [i].gameObject.GetComponent<States> ().currentOctopus == OctopusState.Stunned)
				    && (Squidmatch [i].gameObject.transform.position.z > this.gameObject.transform.position.z) 
				    && (Squidmatch [i].gameObject.transform.position.x == this.gameObject.transform.position.x)) {
					
					//increase match counter
					upmatch++;
					
					//set second collider to new object
					Vector3 UpSquid = new Vector3 (Squidmatch [i].gameObject.transform.position.x, Squidmatch [i].gameObject.transform.position.y, Squidmatch [i].gameObject.transform.position.z );
					SecondSquidmatch = Physics.OverlapSphere (UpSquid, 1f);
					j=0;
					
					//check up one more space
					while (j < SecondSquidmatch.Length) {
						
						//checkup
						//check if Octopuses are the same type and stunned
						if ((SecondSquidmatch [j].gameObject.GetComponent<States> ().currentType == Squidmatch [i].gameObject.GetComponent<States> ().currentType)
						    && SecondSquidmatch [j].gameObject.GetComponent<States> ().currentOctopus == OctopusState.Stunned
						    && (SecondSquidmatch [j].gameObject.transform.position.z > Squidmatch [i].gameObject.transform.position.z 
						    && SecondSquidmatch [j].gameObject.transform.position.x == Squidmatch [i].gameObject.transform.position.x)) {
							
							//increase match counter
							upmatch++;
						}
						j++;
					}
				}

				//checkdown
				//check if Octopuses are the same type and stunned
				if ((Squidmatch [i].gameObject.GetComponent<States> ().currentType == this.gameObject.GetComponent<States> ().currentType)
				    && Squidmatch [i].gameObject.GetComponent<States> ().currentOctopus == OctopusState.Stunned
				    && (Squidmatch [i].gameObject.transform.position.z < this.gameObject.transform.position.z 
				    && Squidmatch [i].gameObject.transform.position.x == this.gameObject.transform.position.x)) {
					
					//increase match counter
					downmatch++;
					
					//set second collider to new object
					Vector3 DownSquid = new Vector3 (Squidmatch [i].gameObject.transform.position.x, Squidmatch [i].gameObject.transform.position.y, Squidmatch [i].gameObject.transform.position.z );
					SecondSquidmatch = Physics.OverlapSphere (DownSquid, 1f);
					j=0;
					
					//check down one more space
					while (j < SecondSquidmatch.Length) {

						//check if Octopuses are the same type and stunned
						if ((SecondSquidmatch [j].gameObject.GetComponent<States> ().currentType == Squidmatch [i].gameObject.GetComponent<States> ().currentType)
						    && SecondSquidmatch [j].gameObject.GetComponent<States> ().currentOctopus == OctopusState.Stunned
						    && (SecondSquidmatch [j].gameObject.transform.position.z < Squidmatch [i].gameObject.transform.position.z 
						    && SecondSquidmatch [j].gameObject.transform.position.x == Squidmatch [i].gameObject.transform.position.x)) {
							
							//increase match counter
							downmatch++;
						}
						j++;
					}
					
				}

				//checkright
				//check if Octopuses are the same type and stunned
				if ((Squidmatch [i].gameObject.GetComponent<States> ().currentType == this.gameObject.GetComponent<States> ().currentType)
				    && Squidmatch [i].gameObject.GetComponent<States> ().currentOctopus == OctopusState.Stunned
				    && (Squidmatch [i].gameObject.transform.position.x > this.gameObject.transform.position.x 
				    && Squidmatch [i].gameObject.transform.position.z == this.gameObject.transform.position.z)) {
					
					//increase match counter
					rightmatch++;
					
					//set second collider to new object
					Vector3 RightSquid = new Vector3 (Squidmatch [i].gameObject.transform.position.x, Squidmatch [i].gameObject.transform.position.y, Squidmatch [i].gameObject.transform.position.z );
					SecondSquidmatch = Physics.OverlapSphere (RightSquid, 1f);
					j=0;
					
					//check down one more space
					while (j < SecondSquidmatch.Length) {
						
						//checkright
						//check if Octopuses are the same type and stunned
						if ((SecondSquidmatch [j].gameObject.GetComponent<States> ().currentType == Squidmatch [i].gameObject.GetComponent<States> ().currentType)
						    && SecondSquidmatch [j].gameObject.GetComponent<States> ().currentOctopus == OctopusState.Stunned
						    && (SecondSquidmatch [j].gameObject.transform.position.x > Squidmatch [i].gameObject.transform.position.x 
						    && SecondSquidmatch [j].gameObject.transform.position.z == Squidmatch [i].gameObject.transform.position.z)) {
							
							//increase match counter
							rightmatch++;
						}
						j++;
					}
					
				}

				//checkleft
				//check if Octopuses are the same type and stunned
				if ((Squidmatch [i].gameObject.GetComponent<States> ().currentType == this.gameObject.GetComponent<States> ().currentType)
				    && Squidmatch [i].gameObject.GetComponent<States> ().currentOctopus == OctopusState.Stunned
				    && (Squidmatch [i].gameObject.transform.position.x < this.gameObject.transform.position.x 
				    && Squidmatch [i].gameObject.transform.position.z == this.gameObject.transform.position.z)) {
					
					//increase match counter
					leftmatch++;
					
					//set second collider to new object
					Vector3 LeftSquid = new Vector3 (Squidmatch [i].gameObject.transform.position.x, Squidmatch [i].gameObject.transform.position.y, Squidmatch [i].gameObject.transform.position.z );
					SecondSquidmatch = Physics.OverlapSphere (LeftSquid, 1f);
					j=0;
					
					//check one more space
					while (j < SecondSquidmatch.Length) {
						
						//checkleft
						//check if Octopuses are the same type and stunned
						if ((SecondSquidmatch [j].gameObject.GetComponent<States> ().currentType == Squidmatch [i].gameObject.GetComponent<States> ().currentType)
						    && SecondSquidmatch [j].gameObject.GetComponent<States> ().currentOctopus == OctopusState.Stunned
						    && (SecondSquidmatch [j].gameObject.transform.position.x < Squidmatch [i].gameObject.transform.position.x 
						    && SecondSquidmatch [j].gameObject.transform.position.z == Squidmatch [i].gameObject.transform.position.z)) {
							
							//increase match counter
							leftmatch++;
						}
						j++;
					}
					
				}

				i++;
			}
			
			
			//check if enough matched
			if (upmatch + downmatch >= 2){
				verticalmatch = true;
				totalmatch += upmatch + downmatch;
				
			}
			if (leftmatch + rightmatch >= 2){
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
							if ((Squidmatch [i].gameObject.transform.position.z > this.gameObject.transform.position.z) 
							    && (Squidmatch [i].gameObject.transform.position.x == this.gameObject.transform.position.x)) {
								
								//set to cashed in 
								Squidmatch [i].gameObject.GetComponent<States> ().currentOctopus = OctopusState.Cashed;

								if (upmatch == 2){
									//set second collider to new object
									Vector3 UpSquid = new Vector3 (Squidmatch [i].gameObject.transform.position.x, Squidmatch [i].gameObject.transform.position.y, Squidmatch [i].gameObject.transform.position.z );
									SecondSquidmatch = Physics.OverlapSphere (UpSquid, 1f);
									j=0;
									
									//check up one more space
									while (j < Squidmatch.Length) {
										
										//checkup
										//check if Octopuses are the same type and stunned
										if ((SecondSquidmatch [j].gameObject.transform.position.z > Squidmatch [i].gameObject.transform.position.z 
										    && SecondSquidmatch [j].gameObject.transform.position.x == Squidmatch [i].gameObject.transform.position.x)) {
											
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
							if ((Squidmatch [i].gameObject.transform.position.z < this.gameObject.transform.position.z 
							    && Squidmatch [i].gameObject.transform.position.x == this.gameObject.transform.position.x)) {
								
								//increase match counter
								Squidmatch [i].gameObject.GetComponent<States> ().currentOctopus = OctopusState.Cashed;

								if (downmatch == 2){
									//set second collider to new object
									Vector3 DownSquid = new Vector3 (Squidmatch [i].gameObject.transform.position.x, Squidmatch [i].gameObject.transform.position.y, Squidmatch [i].gameObject.transform.position.z );
									SecondSquidmatch = Physics.OverlapSphere (DownSquid, 1f);
									j=0;
									
									//check down one more space
									while (j < Squidmatch.Length) {
										
										//check if Octopuses are the same type and stunned
										if ((SecondSquidmatch [j].gameObject.transform.position.z < Squidmatch [i].gameObject.transform.position.z 
										    && SecondSquidmatch [j].gameObject.transform.position.x == Squidmatch [i].gameObject.transform.position.x)) {
											
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
							if ((Squidmatch [i].gameObject.transform.position.x > this.gameObject.transform.position.x 
							    && Squidmatch [i].gameObject.transform.position.z == this.gameObject.transform.position.z)) {
								
								//increase match counter
								Squidmatch [i].gameObject.GetComponent<States> ().currentOctopus = OctopusState.Cashed;

								if (rightmatch == 2){
									//set second collider to new object
									Vector3 RightSquid = new Vector3 (Squidmatch [i].gameObject.transform.position.x, Squidmatch [i].gameObject.transform.position.y, Squidmatch [i].gameObject.transform.position.z );
									SecondSquidmatch = Physics.OverlapSphere (RightSquid, 1f);
									j=0;
									
									//check one more space
									while (j < Squidmatch.Length) {
										
										//checkright
										//check if Octopuses are the same type and stunned
										if ((SecondSquidmatch [j].gameObject.transform.position.x > Squidmatch [i].gameObject.transform.position.x 
										    && SecondSquidmatch [j].gameObject.transform.position.z == Squidmatch [i].gameObject.transform.position.z)) {
											
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
							if ((Squidmatch [i].gameObject.transform.position.x < this.gameObject.transform.position.x 
							    && Squidmatch [i].gameObject.transform.position.z == this.gameObject.transform.position.z)) {
								
								//increase match counter
								Squidmatch [i].gameObject.GetComponent<States> ().currentOctopus = OctopusState.Cashed;

								if (leftmatch == 2){
									//set second collider to new object
									Vector3 LeftSquid = new Vector3 (Squidmatch [i].gameObject.transform.position.x, Squidmatch [i].gameObject.transform.position.y, Squidmatch [i].gameObject.transform.position.z );
									SecondSquidmatch = Physics.OverlapSphere (LeftSquid, 1f);
									j=0;
									
									//check one more space
									while (j < SecondSquidmatch.Length) {
										
										//checkup
										//check if Octopuses are the same type and stunned
										if ((SecondSquidmatch [j].gameObject.transform.position.x < Squidmatch [i].gameObject.transform.position.x 
										    && SecondSquidmatch [j].gameObject.transform.position.z == Squidmatch [i].gameObject.transform.position.z)) {
											
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
			
			//score points if match >=3
			if (totalmatch >=3){
				
			}


			//set current check to none
			this.GetComponent<States> ().currentCheck = CheckState.None;
		}		
	}

	void OnMouseUp(){
		
	}
	//removed clicks counter
	void OnMouseDown(){

	}

	//test to see if octopus can be cashed in
	public void TestCashin(){
		if (this.GetComponent<States> ().currentOctopus == OctopusState.Stunned) {
			//cash in
			this.GetComponent<States> ().currentOctopus = OctopusState.Cashed;
			
			Cashedin ();
		}
	}

	//test to see if octopus can be stunned
	public void TestStun(){
		if (this.GetComponent<States> ().currentOctopus == OctopusState.Jumping) {
			//set Octopus to stunned
			this.GetComponent<States> ().currentOctopus = OctopusState.Stunned;
			this.gameObject.GetComponent<Renderer>().material.color = Color.red;
			//set checkmatches flag
			this.GetComponent<States> ().currentCheck = CheckState.CheckMatch;
		}
	}



	void Cashedin(){
		
		Vector3 origin = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
		Collider[] Squidmatch = Physics.OverlapSphere (origin, 1f);
		
		int i = 0;
		
		var TwoCashIn = false;
		//
		while (i < Squidmatch.Length && TwoCashIn == false) {
			//check if squids are the same type and ready for cashing in
			if ((Squidmatch [i].gameObject.GetComponent<States> ().currentType == this.gameObject.GetComponent<States> ().currentType)
			    && (Squidmatch [i].gameObject.GetComponent<States> ().currentOctopus == OctopusState.Stunned)
			    && (this.GetComponent<States> ().currentOctopus == OctopusState.Cashed)) {

				//cash in
				Squidmatch [i].gameObject.GetComponent<States> ().currentOctopus = OctopusState.Cashed;
				
				TwoCashIn = true;
			}
			

			i++;
		}

		
		if (TwoCashIn == true) {
			
		} else {
			
		}
		
	}
	

}