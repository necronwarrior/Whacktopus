using UnityEngine;
using System.Collections;

public class ClickManager : MonoBehaviour {

	float Timing;
	public int Held;
	//held 0 off
	//held 1 Click
	//held 2 Hold
	//held 
	public bool OneClick;

	public GameObject CurrentOctoObject;

	// Use this for initialization
	void Start () {
		Timing = 0.0f;
		OneClick = true;
		Held = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//start holding timer

		if (Input.GetMouseButtonDown (0)) {
			OneClick=false;
			CollisionTest();
			Timing = 0.0f;
		}


		if (Input.GetMouseButton (0))
		{
			Timing += Time.deltaTime;
		}

		//reset timer for next touch
		if(Held == 1 || Held == 2){
			Held= 3;
		}

		//determine if there is any holding
		if (Timing > 0.25f && Input.GetMouseButton (0)) {
			Held = 1;
		}
		if(Timing <= 0.25f && Input.GetMouseButtonUp (0) && OneClick==false){
			Held = 2;
			OneClick = true;
		}



	}

	//return state of click or holds 
	public int GetClickHold(){
		return Held;
	}

	public bool CollisionTest(){
		Ray Mouseinput;
		Mouseinput = Camera.main.ScreenPointToRay( Input.mousePosition );

		RaycastHit OctoHit;

		Debug.DrawRay(Mouseinput.origin,
		              (Mouseinput.direction*10.0f),
		              Color.black,
		              0.5f);
		if(Physics.Raycast( Mouseinput, out OctoHit, 100 )){
			if (OctoHit.collider.gameObject.transform.parent.tag== "Green_octo" ||
			    OctoHit.collider.gameObject.transform.parent.tag== "Orange_octo" ||
			    OctoHit.collider.gameObject.transform.parent.tag== "Red_octo" )
			{
				CurrentOctoObject = OctoHit.collider.gameObject;
				//if(this.gameObject== CurrentOctoObject.transform.parent.gameObject)
				//{
				return true;
				//}
			}
		}
		return false;
	}
}
