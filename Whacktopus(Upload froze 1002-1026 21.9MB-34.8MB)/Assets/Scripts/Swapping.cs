using UnityEngine;
using System.Collections;

public class Swapping : MonoBehaviour {

	public float testin_1, testin_2, testin_3;
	public float new_y;
	public Vector3 world_pos, click_reset;

	int clicks;

	public bool clicked;

	// Use this for initialization
	void Start () {
		clicked = false;
		clicks = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if (clicked == true) {
			if (Input.GetMouseButton (0)) {
				FollowDrag ();
			}
		
			if (Input.GetMouseButtonUp (0)) {
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

				// Save current object layer
				int oldLayer = gameObject.layer;
				
				//Change object layer to a layer it will be alone
				gameObject.layer = 8;
				
				int layerToIgnore = gameObject.layer;
				//layerToIgnore = ~layerToIgnore;

				if(Physics.Raycast(ray, out hit, layerToIgnore))
				{
					if (hit.collider != null){
						this.gameObject.transform.position = new Vector3 (hit.collider.transform.position.x, hit.collider.transform.position.y, hit.collider.transform.position.z);
						hit.collider.transform.position = new Vector3 (click_reset.x,click_reset.y,click_reset.z);


						clicked = !clicked;
						gameObject.GetComponent<States> ().currentOctopus = OctopusState.Stunned;
						gameObject.layer = oldLayer;
					}
				}
				else{
				clicked = !clicked;
				if (clicked == false) {
					gameObject.GetComponent<States> ().currentOctopus = OctopusState.Stunned;
					gameObject.transform.position = new Vector3 (click_reset.x, click_reset.y, click_reset.z);
				}
				}
			}
		}
	}

	void OnMouseDown() {
		
		if (gameObject.GetComponent<States> ().currentOctopus == OctopusState.Stunned) 
		{
			clicked = !clicked;
			if (clicked == true)
			{
				gameObject.GetComponent<States> ().currentOctopus = OctopusState.Picked;
				click_reset = gameObject.transform.position;
			}
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
