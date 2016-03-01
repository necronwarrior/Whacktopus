using UnityEngine;
using System.Collections;

public class Matching : MonoBehaviour {

	GameObject Points;

	public AudioClip Hit, Cash;

	ClickManager ClickManagerParent;
	States stater;
    //4 rays at 90 degrees starting at forward
    Ray[] FourDirections;
    RaycastHit[] FourHits;
    Vector3 RealOrigin;
    int totalmatch;
    public int[] Match;

    public bool verticalmatch, horizontalmatch, roundcheck;

    public GameObject[] debughits;
    /*
    0 = down
    1 = left
    2 = up
    3 = right
    */
   
	// Use this for initialization
	void Awake () {
        totalmatch = 0;
		ClickManagerParent = gameObject.transform.parent.transform.parent.GetComponent<ClickManager> ();
		Points = GameObject.Find ("Scripts");
		Hit = Resources.Load ("Sounds/Squid-sounds/hit-2") as AudioClip;
		Cash = Resources.Load ("Sounds/new folder/shillings") as AudioClip;
        FourDirections = new Ray[4];
        FourHits = new RaycastHit[4];
        debughits = new GameObject[4];
        RealOrigin = new Vector3(gameObject.transform.parent.position.x + 0.05f, 
                                 gameObject.transform.parent.position.y + 0.04f, 
                                 gameObject.transform.parent.position.z - 0.3f);
        Match = new int[4];
        roundcheck = false;

        FourDirections[0] = new Ray((RealOrigin),
                                    (2*gameObject.transform.parent.forward));
        FourDirections[1] = new Ray((RealOrigin),
                                    (2*gameObject.transform.parent.right));
        FourDirections[2] = new Ray((RealOrigin),
                                    (-2*(gameObject.transform.parent.forward)));
        FourDirections[3] = new Ray((RealOrigin),
                                    (-2*(gameObject.transform.parent.right)));
        for (int i=0; i<4; i++)
        {
            Debug.DrawRay(FourDirections[i].origin,FourDirections[i].direction, Color.blue, 5.0f);
        }

        //gameObject.GetComponent<SphereCollider>().enabled = false;
        for(int cross=0;cross<4;cross++)
        {
            if(Physics.Raycast(FourDirections[cross],out FourHits[cross]))
            {
                debughits [cross] = FourHits [cross].collider.gameObject.transform.parent.gameObject;
            }
        }
        //gameObject.GetComponent<SphereCollider>().enabled = true;


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

        if (horizontalmatch || verticalmatch)
        {
           MegaCashing();
        }

        if (verticalmatch)
        {
            totalmatch++;
            if (FourHits[0].collider!=null)
            {
                FourHits[0].collider.gameObject.GetComponent<Matching>().Cashing();
            }

            if (FourHits[2].collider!=null)
            {
                FourHits[2].collider.gameObject.GetComponent<Matching>().Cashing();
            }
            verticalmatch = false;
        }

        if (horizontalmatch)
        {
            totalmatch++;
            if (FourHits[1].collider!=null)
            {
                FourHits[1].collider.gameObject.GetComponent<Matching>().Cashing();
            }

            if (FourHits[3].collider!=null)
            {
                FourHits[3].collider.gameObject.GetComponent<Matching>().Cashing();
            }
            horizontalmatch = false;
        }

        if((Match[0]>=1
            && Match[2]>=1)
           ||Match[0] >=2
           ||Match[2]>=2)
        {
            verticalmatch =true;
        }
        
        if((Match[1]>=1
            && Match[3]>=1)
           ||Match[1] >=2
           ||Match[3]>=2)
        {
            horizontalmatch =true;
        }

		//check if a match check is needed for current object
        if (this.GetComponent<States>().currentOctopus == OctopusState.Stunned 
            && ClickManagerParent.GetClickHold() == 3
            && roundcheck == false)
        {
			//set matches for each direction to 0

			int totalmatch = 1;

            for(int cross=0;cross<4;cross++)
            {
                if(FourHits[cross].collider!=null)
                {
                    if(FourHits[cross].collider.gameObject.GetComponent<States>().currentOctopus == OctopusState.Stunned
                       && (FourHits[cross].collider.gameObject.tag == gameObject.tag))
                    {
                        Match[cross]++;
                        FourHits[cross].collider.gameObject.GetComponent<Matching>().Match[cross]++;
                    }
                }
            }
            roundcheck = true;
			//set current check to none
			this.GetComponent<States> ().currentCheck = CheckState.None;
		}		
	}

    void MegaCashing()
    {


        if (horizontalmatch)
        {
            if (FourHits[1].collider!=null)
            {
                if(FourHits[1].collider.gameObject.GetComponent<Matching>().FourHits[1].collider!= null)
                {
                    totalmatch++;
                }
            }
            
            if (FourHits[3].collider!=null)
            {
                if(FourHits[3].collider.gameObject.GetComponent<Matching>().FourHits[3].collider!= null)
                {
                    totalmatch++;
                }
            }
        }

        if (verticalmatch)
        {
            if (FourHits[0].collider!=null)
            {
                if(FourHits[0].collider.gameObject.GetComponent<Matching>().FourHits[0].collider!= null)
                {
                    totalmatch++;
                }
            }
            
            if (FourHits[2].collider!=null)
            {
                if(FourHits[2].collider.gameObject.GetComponent<Matching>().FourHits[2].collider!= null)
                {
                    totalmatch++;
                }
            }
        }

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
        Cashing();
    }

    public void Cashing(){
        //cash in
        roundcheck = false;
        Match[0] = 0;
        Match[1] = 0;
        Match[2] = 0;
        Match[3] = 0;
        transform.gameObject.GetComponent<AudioSource> ().PlayOneShot (Cash);
        GetComponent<States> ().currentOctopus = OctopusState.Cashed;
        
        Cashedin ();
    }

	public void ClickCheck(){
		if (ClickManagerParent.GetClickHold () == 2) 
		{
			if (ClickManagerParent.CurrentOctoObject == this.gameObject) 
			{
				if (this.GetComponent<States> ().currentOctopus == OctopusState.Stunned) {
                    for (int last=0; last<4; last++)
                    {
                        if(Match[last]!=0)
                        {
                            FourHits [last].collider.gameObject.GetComponent<Matching>().Cashing();
                        }
                    }
                    Cashing();
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