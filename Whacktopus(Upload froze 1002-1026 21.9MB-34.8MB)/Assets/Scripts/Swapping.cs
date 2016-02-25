using UnityEngine;
using System.Collections;

public class Swapping : MonoBehaviour {

	//public float testin_1, testin_2, testin_3;
	public float new_y;
	public Vector3 world_pos, click_reset;

	public bool clicked;
	public bool SetToClicked;

	public Ray ray;

	public int numclicks;
	public float MouseTimer;
	public float ClickTimer; 
	public bool MouseClick;
	public bool MouseHeld;
	public bool MouseHeldDrop;


	// Use this for initialization
	void Start () {
		clicked = false;
		MouseTimer = 0;
		ClickTimer = 0.25f; 
		MouseClick = false;
		MouseHeld = false;
		MouseHeldDrop = false;
		SetToClicked = false;
		numclicks = 0;


		click_reset = gameObject.transform.parent.position;

	} 
	
	// Update is called once per frame
	void FixedUpdate () {

		//reset mouseclick
		MouseClick = false;
		MouseHeldDrop = false;

		//test for misplaced cubes
		/*GameObject[] objects = GameObject.FindGameObjectsWithTag("Blue_Octopus");
		foreach (var obj in objects) {
			if (obj.gameObject.GetComponent<Swapping> ().clicked == false && obj.gameObject.transform.position.y != 0){
				//reset object position
				obj.gameObject.transform.position = obj.gameObject.GetComponent<Swapping> ().click_reset;
			}
		}*/

		//ray test for current object
		Ray ray2 = Camera.main.ScreenPointToRay( Input.mousePosition );
		RaycastHit hit2;

		if( Physics.Raycast( ray2, out hit2, 100 ) && clicked == true ){

			if (Input.GetMouseButton (0)) {


				//update current mouse down time
				hit2.transform.gameObject.GetComponent<Matching> ().TestStun();
				MouseTimer += Time.deltaTime;
				if (MouseTimer > ClickTimer) {
					MouseHeld = true;
					if (clicked == true){
 						hit2.transform.gameObject.GetComponent<Matching> ().TestStun();
					}
				}
			} else {
				//check if previously held
				if (MouseTimer > 0){
					if (MouseTimer < ClickTimer) {
						MouseClick = true;
						if (clicked == true){
							if (numclicks ==1){
								hit2.transform.gameObject.GetComponent<Matching> ().TestCashin();
							}
							hit2.transform.gameObject.GetComponent<Matching> ().TestStun();
						}
					}
					else {
						MouseHeld = false;
						MouseHeldDrop = true;
					}
				}
				//reset mouse timer
				MouseTimer = 0;
			}

			if (clicked == true) {
				if (Input.GetMouseButtonUp(0)){
					clicked = false;
				}
				if (MouseHeld == true) {
					FollowDrag ();
				}
				if (MouseHeldDrop == true && Input.GetMouseButtonUp (0)) {
					RaycastHit hit;
					// ray shoting our of octo-object
					ray = new Ray (new Vector3 (gameObject.transform.parent.position.x, (gameObject.transform.parent.position.y - 1.0f), (gameObject.transform.parent.position.z + 1.5f)),
					               new Vector3 (0.0f, -1.0f, 0.0f));

					if (Physics.Raycast (ray, out hit)) {
						Debug.DrawLine(ray.origin, hit.point, Color.black, 5.0f);
						if (hit.collider != null) {
							bool fool = (((click_reset.z - 1.5f == hit.collider.gameObject.transform.parent.position.z)
							              || (hit.collider.gameObject.transform.parent.position.z == click_reset.z + 1.5f))
							             && hit.collider.gameObject.transform.parent.position.x == click_reset.x);
							bool rule = (((click_reset.x - 1.5f == hit.collider.gameObject.transform.parent.position.x)
							              || (hit.collider.gameObject.transform.parent.position.x == click_reset.x + 1.5f))
							             && hit.collider.gameObject.transform.parent.position.z == click_reset.z);

							if (fool == true || rule == true) {
								hit2.transform.gameObject.transform.parent.position = new Vector3 (hit.collider.transform.parent.position.x, hit.collider.transform.parent.position.y, hit.collider.transform.parent.position.z);
								hit.collider.transform.parent.position = new Vector3 (click_reset.x, click_reset.y, click_reset.z);

								//set new click resets
								hit2.transform.gameObject.GetComponent<Swapping> ().click_reset = hit2.transform.gameObject.transform.parent.position;
								hit.collider.gameObject.GetComponent<Swapping> ().click_reset = hit.collider.transform.parent.position;

								//set checkstates
								hit2.transform.gameObject.GetComponent<States> ().currentCheck = CheckState.CheckMatch;
								hit.collider.gameObject.GetComponent<States> ().currentCheck = CheckState.CheckMatch;

								hit2.transform.gameObject.GetComponent<States> ().currentOctopus = hit2.transform.gameObject.GetComponent<States> ().PrevState;
							}
						}
					} else {

						hit2.transform.gameObject.transform.parent.position = new Vector3 (click_reset.x, click_reset.y, click_reset.z);
						hit2.transform.gameObject.GetComponent<States> ().currentOctopus = hit2.transform.gameObject.GetComponent<States> ().PrevState;
					}
				}
			}
		}
		if (!Input.GetMouseButton(0)) {
			clicked = false;
			gameObject.transform.parent.position = new Vector3 (click_reset.x, click_reset.y, click_reset.z);
		}
		if(clicked == false) {
				if (gameObject.GetComponent<States> ().currentOctopus == OctopusState.Picked && gameObject.GetComponent<Swapping> ().clicked == false) {
					gameObject.GetComponent<States> ().currentOctopus = gameObject.GetComponent<States> ().PrevState;

			}
		}

	}

	void OnMouseDown() {

		//set numclicks to 1
		numclicks = 1;

		//set thgis object to clicked
		SetToClicked = true;
		clicked = true;

		if (gameObject.GetComponent<States> ().currentOctopus == OctopusState.Stunned) {

			//set current object as being clicked
			if(MouseHeld == true){
				if (gameObject.GetComponent<States> ().currentOctopus != OctopusState.Picked) {
					//store prev stae
					gameObject.GetComponent<States> ().PrevState = gameObject.GetComponent<States> ().currentOctopus;
				   	gameObject.GetComponent<States> ().currentOctopus = OctopusState.Picked;
				}
			}
			gameObject.GetComponent<States> ().currentCheck = CheckState.CheckMatch;
		}

		//set all other objects clicked to false
		/*var objects = GameObject.FindGameObjectsWithTag("Blue_Octopus");
		foreach (var obj in objects) {
			if (obj.gameObject.GetComponent<Swapping> ().SetToClicked == false){
				obj.gameObject.transform.position = obj.gameObject.GetComponent<Swapping> ().click_reset;
				obj.gameObject.GetComponent<Swapping> ().clicked = false;
			}
		}*/

		SetToClicked = false;

	}

	void FollowDrag() 
	{
		//set object to being picked up
		if (gameObject.GetComponent<States> ().currentOctopus != OctopusState.Picked) {
			gameObject.GetComponent<States> ().PrevState = gameObject.GetComponent<States> ().currentOctopus;
			gameObject.GetComponent<States> ().currentOctopus = OctopusState.Picked;
		}

		Vector3 mouseInp = Input.mousePosition;
			
		world_pos = Camera.main.ScreenToWorldPoint(new Vector3(mouseInp.x, mouseInp.y, (6.8f+(gameObject.transform.parent.position.z/2.5f))));
		gameObject.transform.parent.position = (world_pos);
			
		new_y = (gameObject.transform.parent.position.y * ((float)System.Math.Sin (0))+3.0f);
		gameObject.transform.parent.position = new Vector3(gameObject.transform.parent.position.x , new_y, gameObject.transform.parent.position.z);
	}
}
