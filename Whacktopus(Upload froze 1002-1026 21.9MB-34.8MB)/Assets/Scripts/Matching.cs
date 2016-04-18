using UnityEngine;
using System.Collections;

public class Matching : MonoBehaviour {

	GameObject Points;
	GameObject MainCam;

	AudioClip Hit, Nice1, Nice2, Great1, Great2, Awesome1, Awesome2, Fantastic1, Fantastic2, Wonderful1, Wonderful2, Amazing1, Amazing2;
	AudioClip[] Cash;
	ClickManager ClickManagerParent;
	States stater;
    //4 rays at 90 degrees starting at north
    Ray[] FourDirections;
    RaycastHit[] FourHits;
    Vector3 RealOrigin;
	int totalmatch, word_choice;
    public int[] Match;

    public bool verticalmatch, horizontalmatch, roundcheck;

    public GameObject[] ObjectsHits;
    /*
    0 = down
    1 = left
    2 = up
    3 = right
    */

	Object Nice, Great, Awesome, Fantastic, Wonderful, Amazing;
	GameObject Word;
   
	// Use this for initialization
	void Start () {
		MainCam = GameObject.Find("Main Camera");

		//load word files
		Nice = Resources.Load("Art assets/UI/In_Game/Excitement words/Nice") as Object;
		Great = Resources.Load("Art assets/UI/In_Game/Excitement words/Great") as Object;
		Awesome = Resources.Load("Art assets/UI/In_Game/Excitement words/Awesome") as Object;
		Fantastic = Resources.Load("Art assets/UI/In_Game/Excitement words/Fantastic") as Object;
		Wonderful = Resources.Load("Art assets/UI/In_Game/Excitement words/Wonderful") as Object;
		Amazing = Resources.Load("Art assets/UI/In_Game/Excitement words/Amazing") as Object;

		//load sound word files
		Nice1 = Resources.Load ("Sounds/Excitement_words/Nice1") as AudioClip;
		Nice2 = Resources.Load ("Sounds/Excitement_words/Nice2") as AudioClip;
		Great1 = Resources.Load ("Sounds/Excitement_words/Great1") as AudioClip;
		Great2 = Resources.Load ("Sounds/Excitement_words/Great2") as AudioClip;
		Awesome1 = Resources.Load ("Sounds/Excitement_words/Awesome1") as AudioClip;
		Awesome2 = Resources.Load ("Sounds/Excitement_words/Awesome2") as AudioClip;
		Fantastic1 = Resources.Load ("Sounds/Excitement_words/Fantastic1") as AudioClip;
		Fantastic2 = Resources.Load ("Sounds/Excitement_words/Fantastic2") as AudioClip;
		Wonderful1 = Resources.Load ("Sounds/Excitement_words/Wonderful1") as AudioClip;
		Wonderful2 = Resources.Load ("Sounds/Excitement_words/Wonderful2") as AudioClip;
		Amazing1 = Resources.Load ("Sounds/Excitement_words/Amazing1") as AudioClip;
		Amazing2 = Resources.Load ("Sounds/Excitement_words/Amazing2") as AudioClip;


        totalmatch = 0;
		ClickManagerParent = gameObject.transform.parent.transform.parent.GetComponent<ClickManager> ();
		Points = GameObject.Find ("Scripts");
		Hit = Resources.Load ("Sounds/Squid-sounds/hit-2") as AudioClip;
		Cash = new AudioClip[5];
		Cash[0] = Resources.Load ("Sounds/Octo_sounds/Coins/coin-drop-1") as AudioClip;
		Cash[1] = Resources.Load ("Sounds/Octo_sounds/Coins/coin-drop-2") as AudioClip;
		Cash[2] = Resources.Load ("Sounds/Octo_sounds/Coins/coin-drop-3") as AudioClip;
		Cash[3] = Resources.Load ("Sounds/Octo_sounds/Coins/coin-drop-4") as AudioClip;
		Cash[4] = Resources.Load ("Sounds/Octo_sounds/Coins/coin-drop-5") as AudioClip;
        FourDirections = new Ray[4];
        FourHits = new RaycastHit[4];
        ObjectsHits = new GameObject[4];
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
            if(Physics.Raycast(FourDirections[cross],out FourHits[cross], 2.0F))
            {
                if( FourHits [cross].collider.gameObject!=null)
                {
                    
                    ObjectsHits [cross] = FourHits [cross].collider.gameObject;
                }
            }
        }
        //gameObject.GetComponent<SphereCollider>().enabled = true;


	}

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
            if (ObjectsHits[0]!=null)
            {
               ObjectsHits[0].GetComponent<Matching>().Cashing();
            }

            if (ObjectsHits[2]!=null)
            {
                ObjectsHits[2].GetComponent<Matching>().Cashing();
            }
            verticalmatch = false;
        }

        if (horizontalmatch)
        {
            totalmatch++;
            if (ObjectsHits[1]!=null)
            {
                ObjectsHits[1].GetComponent<Matching>().Cashing();
            }

            if (ObjectsHits[3]!=null)
            {
                ObjectsHits[3].GetComponent<Matching>().Cashing();
            }
            horizontalmatch = false;
        }

        if((Match[0]>=1
            && Match[2]>=1))
        {
            verticalmatch =true;
        }
        
        if((Match[1]>=1
            && Match[3]>=1))
        {
            horizontalmatch =true;
        }

		//check if a match check is needed for current object
        if (this.GetComponent<States>().currentOctopus == OctopusState.Stunned 
            && ClickManagerParent.GetClickHold() == 3
		    //&& roundcheck == false
         )
        {
			//set matches for each direction to 0

            for(int cross=0;cross<4;cross++)
            {
                if(ObjectsHits[cross]!=null)
                {
                    if(ObjectsHits[cross].GetComponent<States>().currentOctopus == OctopusState.Stunned
                       && (ObjectsHits[cross].tag == gameObject.tag))
                    {
                        Match[cross]++;
						if(cross ==0 || cross==1)
						{
                            ObjectsHits[cross].GetComponent<Matching>().Match[cross+2]++;
						}

						if(cross ==2 || cross==3)
						{
                            ObjectsHits[cross].GetComponent<Matching>().Match[cross-2]++;
						}
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
            if (ObjectsHits[1]!=null)
            {
                if(ObjectsHits[1].GetComponent<Matching>().ObjectsHits[1]!= null)
                {
                    totalmatch++;
                }
            }
            
            if (ObjectsHits[3]!=null)
            {
                if(ObjectsHits[3].GetComponent<Matching>().ObjectsHits[3]!= null)
                {
                    totalmatch++;
                }
            }
        }

        if (verticalmatch)
        {
            if (ObjectsHits[0]!=null)
            {
                if(ObjectsHits[0].GetComponent<Matching>().ObjectsHits[0]!= null)
                {
                    totalmatch++;
                }
            }
            
            if (ObjectsHits[2]!=null)
            {
                if(ObjectsHits[2].GetComponent<Matching>().ObjectsHits[2]!= null)
                {
                    totalmatch++;
                }
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
		int rand_coin = Random.Range (0, 5);
		transform.gameObject.GetComponent<AudioSource> ().PlayOneShot (Cash[rand_coin]);
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
                            ObjectsHits[last].GetComponent<Matching>().Cashing();
                        }
                    }
                    Cashing();
				}

				if ((this.gameObject.GetComponent<States> ().currentOctopus == OctopusState.Jumping 
				     || this.gameObject.GetComponent<States> ().currentOctopus == OctopusState.Idle)) {
					//set Octopus to stunned
					this.gameObject.GetComponent<States> ().currentOctopus = OctopusState.Stunned;
					//set checkmatches flag
					this.gameObject.GetComponent<States> ().currentCheck = CheckState.CheckMatch;
				
					this.gameObject.GetComponent<AudioSource> ().PlayOneShot (Hit);
					//set numclicks to 0
					//this.GetComponent<Swapping>().numclicks = 0;
				}
			}
		}
	}

	void Cashedin(){
		
//		Vector3 origin = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
		
		int i = 0;
		//
		for(int Twos= 0; Twos<4;Twos++)
		{

		if (Match[Twos]>0){
				i++;
			}
		}
		//score points
		//attach to global

		if (i == 1) {
			Points.GetComponent<InGameGlobals>().AddPoints(300);

		} else {
			Points.GetComponent<InGameGlobals>().AddPoints(100);
		}
		//score points if match >=3
		if (totalmatch >=2){
			//attach to global
			switch (totalmatch)
			{
			case 2:
				Points.GetComponent<InGameGlobals>().AddPoints(300);
				Word = Instantiate(Nice,
				                  	new Vector3(transform.position.x,
				            transform.position.y+1.0f,
				            transform.position.z),
				                   Quaternion.Euler(new Vector3(0,0,0))) as GameObject;
				word_choice = Random.Range(0,2);
				if(word_choice ==0){
					GetComponent<AudioSource>().PlayOneShot(Nice1);
				}else{
					if(word_choice ==1){
						GetComponent<AudioSource>().PlayOneShot(Nice2);
					}
				}
				Word.transform.LookAt((MainCam.transform.position*-1.0f));
				break;
			case 3:
				Points.GetComponent<InGameGlobals>().AddPoints(600);
				Word = Instantiate(Great,
				                   new Vector3(transform.position.x,
				            transform.position.y+1.0f,
				            transform.position.z),
				                   Quaternion.Euler(new Vector3(0,0,0))) as GameObject;
				word_choice = Random.Range(0,2);
				if(word_choice ==0){
					GetComponent<AudioSource>().PlayOneShot(Great1);
				}else{
					if(word_choice ==1){
						GetComponent<AudioSource>().PlayOneShot(Great2);
					}
				}
				Word.transform.LookAt((MainCam.transform.position*-1.0f));
				break;
			case 4:
				Points.GetComponent<InGameGlobals>().AddPoints(1200);
				Word = Instantiate(Awesome,
				                   new Vector3(transform.position.x,
				            transform.position.y+1.0f,
				            transform.position.z),
				                   Quaternion.Euler(new Vector3(0,0,0))) as GameObject;
				word_choice = Random.Range(0,2);
				if(word_choice ==0){
					GetComponent<AudioSource>().PlayOneShot(Awesome1);
				}else{
					if(word_choice ==1){
						GetComponent<AudioSource>().PlayOneShot(Awesome2);
					}
				}
				Word.transform.LookAt((MainCam.transform.position*-1.0f));
				break;
			case 5:
				Points.GetComponent<InGameGlobals>().AddPoints(2000);
				Word = Instantiate(Fantastic,
				                   new Vector3(transform.position.x,
				            transform.position.y+1.0f,
				            transform.position.z),
				                   Quaternion.Euler(new Vector3(0,0,0))) as GameObject;
				word_choice = Random.Range(0,2);
				if(word_choice ==0){
					GetComponent<AudioSource>().PlayOneShot(Fantastic1);
				}else{
					if(word_choice ==1){
						GetComponent<AudioSource>().PlayOneShot(Fantastic2);
					}
				}
				Word.transform.LookAt((MainCam.transform.position*-1.0f));
				break;
			case 6:
				Points.GetComponent<InGameGlobals>().AddPoints(3000);
				Word = Instantiate(Wonderful,
				                   new Vector3(transform.position.x,
				            transform.position.y+1.0f,
				            transform.position.z),
				                   Quaternion.Euler(new Vector3(0,0,0))) as GameObject;
				word_choice = Random.Range(0,2);
				if(word_choice ==0){
					GetComponent<AudioSource>().PlayOneShot(Wonderful1);
				}else{
					if(word_choice ==1){
						GetComponent<AudioSource>().PlayOneShot(Wonderful2);
					}
				}
				Word.transform.LookAt((MainCam.transform.position*-1.0f));
				break;
			case 7:
				Points.GetComponent<InGameGlobals>().AddPoints(4200);
				Word = Instantiate(Amazing,
				                   new Vector3(transform.position.x,
				            transform.position.y+1.0f,
				            transform.position.z),
				                   Quaternion.Euler(new Vector3(0,0,0))) as GameObject;
				word_choice = Random.Range(0,2);
				if(word_choice ==0){
					GetComponent<AudioSource>().PlayOneShot(Amazing1);
				}else{
					if(word_choice ==1){
						GetComponent<AudioSource>().PlayOneShot(Amazing2);
					}
				}
				Word.transform.LookAt((MainCam.transform.position*-1.0f));
				break;
			case 8:
				Points.GetComponent<InGameGlobals>().AddPoints(5600);
				Word = Instantiate(Amazing,
				                   new Vector3(transform.position.x,
				            transform.position.y+1.0f,
				            transform.position.z),
				                   Quaternion.Euler(new Vector3(0,0,0))) as GameObject;
				word_choice = Random.Range(0,2);
				if(word_choice ==0){
					GetComponent<AudioSource>().PlayOneShot(Amazing1);
				}else{
					if(word_choice ==1){
						GetComponent<AudioSource>().PlayOneShot(Amazing2);
					}
				}
				Word.transform.LookAt((MainCam.transform.position*-1.0f));
				break;
			case 9:
				Points.GetComponent<InGameGlobals>().AddPoints(9001);
				Word = Instantiate(Amazing,
				                   new Vector3(transform.position.x,
				            transform.position.y+1.0f,
				            transform.position.z),
				                   Quaternion.Euler(new Vector3(0,0,0))) as GameObject;
				word_choice = Random.Range(0,2);
				if(word_choice ==0){
					GetComponent<AudioSource>().PlayOneShot(Amazing1);
				}else{
					if(word_choice ==1){
						GetComponent<AudioSource>().PlayOneShot(Amazing2);
					}
				}
				Word.transform.LookAt((MainCam.transform.position*-1.0f));
				break;   
			default: 
				Points.GetComponent<InGameGlobals>().AddPoints(300);
				Word = Instantiate(Nice,
				                   new Vector3(transform.position.x,
				            transform.position.y+1.0f,
				            transform.position.z),
				                   Quaternion.Euler(new Vector3(0,0,0))) as GameObject;
				word_choice = Random.Range(0,2);
				if(word_choice ==0){
					GetComponent<AudioSource>().PlayOneShot(Nice1);
				}else{
					if(word_choice ==1){
						GetComponent<AudioSource>().PlayOneShot(Nice2);
					}
				}
				Word.transform.LookAt((MainCam.transform.position*-1.0f));
				break;
			} 
		}
	}
}