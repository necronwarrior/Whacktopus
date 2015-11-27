using UnityEngine;
using System.Collections;

public class Swapping : MonoBehaviour {

	public float testin_1, testin_2, testin_3;
	public float new_y;
	public Vector3 world_pos, click_reset;

	public bool clicked;

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
	} 
	
	// Update is called once per frame
	void FixedUpdate () {

		//reset mouseclick
		MouseClick = false;
		MouseHeldDrop = false;

		if (Input.GetMouseButton (0)) {
			//update current mouse down time
			MouseTimer += Time.deltaTime;
			if (MouseTimer > ClickTimer) {
				MouseHeld = true;
			}
		} else {
			//check if previously held
			if (MouseTimer > 0){
				if (MouseTimer < ClickTimer) {
					MouseClick = true;
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
				Ray ray = new Ray (new Vector3 (this.gameObject.transform.position.x, (this.gameObject.transform.position.y - 1.0f), (this.gameObject.transform.position.z + 1.0f)), new Vector3 (0.0f, -2.0f, +0.5f));

				if (Physics.Raycast (ray, out hit)) {
					if (hit.collider != null) {
						bool fool = ((click_reset.z - 1.5f <= hit.collider.gameObject.transform.position.z)
							|| (hit.collider.gameObject.transform.position.z >= click_reset.z + 1.5f)
							&& hit.collider.gameObject.transform.position.x == click_reset.x);
						bool rule = ((click_reset.x - 1.5f <= hit.collider.gameObject.transform.position.x)
							|| (hit.collider.gameObject.transform.position.x >= click_reset.x + 1.5f)
							&& hit.collider.gameObject.transform.position.z == click_reset.z);

						if (fool == true || rule == true) {
							this.gameObject.transform.position = new Vector3 (hit.collider.transform.position.x, hit.collider.transform.position.y, hit.collider.transform.position.z);
							hit.collider.transform.position = new Vector3 (click_reset.x, click_reset.y, click_reset.z);

							//set checkstates
							this.gameObject.GetComponent<States> ().currentCheck = CheckState.CheckMatch;
							hit.collider.gameObject.GetComponent<States> ().currentCheck = CheckState.CheckMatch;

							this.gameObject.GetComponent<States> ().currentOctopus = OctopusState.Stunned;
						}
					}
				} else {
					this.gameObject.GetComponent<States> ().currentOctopus = OctopusState.Stunned;
					this.gameObject.transform.position = new Vector3 (click_reset.x, click_reset.y, click_reset.z);
				}
				if (clicked == false && this.gameObject.GetComponent<States> ().currentOctopus != OctopusState.Stunned || this.gameObject.GetComponent<States> ().currentOctopus != OctopusState.Under){
					this.gameObject.GetComponent<States> ().currentOctopus = OctopusState.Stunned;
				}
			}
		}
		if (clicked == false && this.gameObject.GetComponent<States> ().currentOctopus == OctopusState.Picked){
			gameObject.GetComponent<States> ().currentOctopus = OctopusState.Stunned;
		}
	}

	void OnMouseDown() {
		
		if (gameObject.GetComponent<States> ().currentOctopus == OctopusState.Stunned) 
		{
			//set current object as being clicked
			clicked = true;
			this.gameObject.GetComponent<States> ().currentOctopus = OctopusState.Picked;
			this.gameObject.GetComponent<States> ().currentCheck = CheckState.CheckMatch;
			click_reset = this.gameObject.transform.position;

		}
	}

	void FollowDrag() 
	{
		Vector3 mouseInp = Input.mousePosition;
			
		world_pos = Camera.main.ScreenToWorldPoint(new Vector3(mouseInp.x, mouseInp.y, (6.8f+(gameObject.transform.position.z/2.5f))));
		gameObject.transform.position = (world_pos);
			
		new_y = (gameObject.transform.position.y * ((float)System.Math.Sin (0))+3.0f);
		gameObject.transform.position = new Vector3(gameObject.transform.position.x , new_y, gameObject.transform.position.z);
	}
}
