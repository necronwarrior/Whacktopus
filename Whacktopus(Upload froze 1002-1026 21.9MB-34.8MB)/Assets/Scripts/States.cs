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

public class States : MonoBehaviour {
	public OctopusState currentOctopus = OctopusState.Under;
	//notes what the previous state was (for picking and moving)
	public OctopusState PrevState = OctopusState.Under;
	public CheckState currentCheck = CheckState.None;

	public float JumpTime = 0;
	public float IdleTime = 0;
	public float StunTime = 0;
	public float CashTime = 0;
	Animator OctoAnimator;
	Material Red_Octo, Green_Octo, Orange_Octo;
    Object Coin, Star;
    GameObject Starro;

    bool Starry, Underonce;

	// Use this for initialization
	void Start () {
		OctoAnimator = gameObject.GetComponentsInParent<Animator>()[0];
		Red_Octo = Resources.Load ("Materials/octomaterials/Materials/Red_Octo") as Material;
		Green_Octo = Resources.Load ("Materials/octomaterials/Materials/Green_Octo") as Material;
		Orange_Octo = Resources.Load ("Materials/octomaterials/Materials/Orange_Octo") as Material;
        Coin = Resources.Load("Models/Coin") as Object;
        Star = Resources.Load("Models/Star") as Object;
        Starry = true;
        Underonce = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (currentOctopus == OctopusState.Jumping && !OctoAnimator.GetBool("Spawned")){
			int Rand_Mat;
			Rand_Mat = Random.Range(1,4);
			switch(Rand_Mat){
			case 1: gameObject.GetComponent<Renderer>().material = Red_Octo;
				gameObject.tag = "Red_octo";
				break;
			case 2: gameObject.GetComponent<Renderer>().material = Green_Octo;
				gameObject.tag = "Green_octo";
				break;
			case 3: gameObject.GetComponent<Renderer>().material = Orange_Octo;
				gameObject.tag = "Orange_octo";
				break;
			default: gameObject.GetComponent<Renderer>().material = null;
				gameObject.tag = "default";
				break;
			};

			gameObject.transform.parent.position = new Vector3(gameObject.transform.parent.position.x,0,gameObject.transform.parent.position.z);
			OctoAnimator.SetBool("Spawned", true);


		}


		if (currentOctopus == OctopusState.Stunned && !OctoAnimator.GetBool("One tap")){
			OctoAnimator.SetBool("One tap", true);
            Starro = Instantiate(Star, 
                        new Vector3(gameObject.transform.parent.transform.position.x+0.15f,
                        gameObject.transform.parent.transform.position.y,
                        gameObject.transform.parent.transform.position.z-0.6f), 
                        Quaternion.Euler(new Vector3(0,0,0))) as GameObject;

            Starro.transform.parent = transform;
            Starry = false;
		}

		if (currentOctopus == OctopusState.Cashed && !OctoAnimator.GetBool("Two tap")){
			OctoAnimator.SetBool("Two tap", true);
            float CoinRand;
            int HowMany = Random.Range(1,5);
            for(int i =0; i< HowMany;i++){
            CoinRand = Random.Range(0,360);
            Instantiate(Coin, 
                        new Vector3(gameObject.transform.parent.transform.position.x,
                                         gameObject.transform.parent.transform.position.y+1.0f,
                                          gameObject.transform.parent.transform.position.z), 
                        Quaternion.Euler(new Vector3(0,CoinRand,90)));
            }
            StarroDestroy();
		}

        if (currentOctopus == OctopusState.Under && Underonce==true){
            this.GetComponent<Jumping>().spawn_number = Random.Range(1,300);
			this.transform.gameObject.GetComponent<AudioSource>().Stop();
			gameObject.transform.parent.position = new Vector3(gameObject.transform.parent.position.x,-1.5f,gameObject.transform.parent.position.z);
			ResetStatebools();
            StarroDestroy();
            Underonce=false;
		}

	}

	public void ResetStatebools(){
		OctoAnimator.SetBool("Spawned", false);
		OctoAnimator.SetBool("One tap", false);
		OctoAnimator.SetBool("Two tap", false);
		OctoAnimator.SetBool("Time to Idle", false);
		OctoAnimator.SetBool("Time to Under", false);
	}

    public void StarroDestroy()
    {
        if(Starry==false)
        {
            Destroy(Starro);
            Starry= true;
        }
    }

	public void SetUnder(){

		currentOctopus = OctopusState.Under;
		for (int cross=0; cross<4; cross++) {
			this.gameObject.GetComponent<Matching>().Match[cross] = 0;
		}
        Underonce=true;
	}
}
