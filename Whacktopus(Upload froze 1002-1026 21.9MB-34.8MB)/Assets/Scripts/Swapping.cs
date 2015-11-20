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
				Ray ray = new Ray(new Vector3 (gameObject.transform.position.x, (gameObject.transform.position.y-1.0f), (gameObject.transform.position.z+1.0f)), new Vector3(0.0f,-2.0f,+0.5f));

				if(Physics.Raycast(ray, out hit))
				{
					if (hit.collider != null)
					{
						bool fool = ((click_reset.z-1.5f <= hit.collider.gameObject.transform.position.z)
						             || (hit.collider.gameObject.transform.position.z >= click_reset.z+1.5f)
						             && hit.collider.gameObject.transform.position.x == click_reset.x);
						bool rule =((click_reset.x-1.5f <= hit.collider.gameObject.transform.position.x)
							         ||(hit.collider.gameObject.transform.position.x >= click_reset.x+1.5f)
							&& hit.collider.gameObject.transform.position.z == click_reset.z);

						if (fool ==true || rule ==true){
						this.gameObject.transform.position = new Vector3 (hit.collider.transform.position.x, hit.collider.transform.position.y, hit.collider.transform.position.z);
						hit.collider.transform.position = new Vector3 (click_reset.x,click_reset.y,click_reset.z);


						clicked = !clicked;
						gameObject.GetComponent<States> ().currentOctopus = OctopusState.Stunned;
						}
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
