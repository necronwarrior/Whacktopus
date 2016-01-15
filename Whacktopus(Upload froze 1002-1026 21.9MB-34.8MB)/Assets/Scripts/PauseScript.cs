using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
		this.transform.gameObject.tag = "NotPaused";
	}
	
	// Update is called once per frame
	public void OnClick () {
		if (this.transform.gameObject.tag == "Paused") {
			Time.timeScale = 1;
			this.transform.gameObject.tag = "NotPaused";
		} else {
			Time.timeScale = 0;
			this.transform.gameObject.tag = "Paused";
		}
	}
}

//for matching.cs after two if statements where totalmatch is tallyied
/*
 switch (totalmatch)
{
case 3:
	points += 600;
	break;
case 4:
	points += 1200;
	break;
case 5:
	points += 2000;
	break;
case 6:
	points += 3000;
	break;
case 7:
	points += 4200;
	break;
case 8:
	points += 5600;
	break;
case 9:
	points += 9001;
	break;
}
*/

//for matching.cs under appropriate if staement
/*
if (TwoCashIn == true) {
	points += 100;
} else {
	points += 300;
}
*/