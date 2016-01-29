using UnityEngine;
using System.Collections;


public class InGameGlobals : MonoBehaviour {

	public float BASE_TIMER;
	public float GameTimer;

	public int Level = 1;
	
	public double PointsNeeded = 2000;
	
	public double TotalPoints = 0;
	
	public double LevelPoints = 0;

	// Use this for initialization
	void Start () {
		BASE_TIMER = 30.0f;
		GameTimer = BASE_TIMER;
	}
	
	// Update is called once per frame
	void Update () {
		GameTimer -= Time.deltaTime;

		if (GameTimer <= 0) {
			EndGame();
		}
	}

	void ResetGameTimer ()
	{
		GameTimer = BASE_TIMER;
	}

	void IncreaseLevel(){
		Level ++;
		IncreasePoints ();
		ResetGameTimer ();
	}

	void IncreasePoints(){

		PointsNeeded = PointsNeeded * 1.5;

		//round to 2 sig fig
			//Math.Round (PointsNeeded, 2);
		
	}

	void AddPoints(double x){
		TotalPoints += x;
		LevelPoints += x;

		if (LevelPoints > PointsNeeded) {
			LevelPoints = 0;
			IncreaseLevel();
		}
	}

	void EndGame(){

	}

}
