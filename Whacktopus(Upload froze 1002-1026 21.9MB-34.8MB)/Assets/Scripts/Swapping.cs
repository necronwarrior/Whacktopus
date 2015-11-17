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
			FollowDrag();
		}
	}

	void OnMouseDown() {
		clicked = !clicked;
		if (Input.GetMouseButton(0)) 
		{
			if (gameObject.GetComponent<States> ().currentOctopus == OctopusState.Stunned) 
			{
				if (clicked == true)
				{
					gameObject.GetComponent<States> ().currentOctopus = OctopusState.Picked;
					click_reset = gameObject.transform.position;
				}
			}
		}
		if (Input.GetMouseButton (0)) 
		{
			if (clicked == false) {
				gameObject.GetComponent<States> ().currentOctopus = OctopusState.Stunned;
				gameObject.transform.position = new Vector3 (click_reset.x, click_reset.y, click_reset.z);
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
