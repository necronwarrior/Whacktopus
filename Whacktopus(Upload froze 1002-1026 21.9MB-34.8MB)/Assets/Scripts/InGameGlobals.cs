using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class InGameGlobals : MonoBehaviour {

	public float BASE_TIMER;
	public float GameTimer;
	private float timechange, pointadditive, nextlevel;

	public int Level = 1;
	
	public double PointsNeeded = 2000;
	
	public double TotalPoints = 0;
	
	public double LevelPoints = 0;

	public Image Time_limit, Coin_limit;

	// Use this for initialization
	void Start () {
		BASE_TIMER = 30.0f;
		GameTimer = BASE_TIMER;
		pointadditive = 0.0f;
		nextlevel = 15;
	}
	
	// Update is called once per frame
	void Update () {
		GameTimer -= Time.deltaTime;

		if (GameTimer <= 0) {
			EndGame ();
		} else {
			timechange = (float)(GameTimer * 4.2);

			Time_limit.rectTransform.anchoredPosition = new Vector3 (300.0f, (timechange - 128), 0.0f);
			Time_limit.transform.localScale = new Vector3 (0.5f, (GameTimer / 10), 0);
			if (LevelPoints > 0) {
				if (Level==1){
					pointadditive = (float)(LevelPoints / 15);
				}else{
					pointadditive = (float)(LevelPoints / nextlevel);
				}

				Coin_limit.rectTransform.anchoredPosition = new Vector3 (300.0f, (pointadditive - 128), 0.0f);
				Coin_limit.transform.localScale = new Vector3 (0.5f, (pointadditive / 48), 0);
		
			}
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

		nextlevel *= 1.5f;
		Coin_limit.rectTransform.anchoredPosition = new Vector3 (300.0f, (1 - 128), 0.0f);
		Coin_limit.transform.localScale = new Vector3 (0.5f, (1 / 40), 0);
	}

	void IncreasePoints(){

		PointsNeeded = PointsNeeded * 1.5;

		//round to 2 sig fig
			//Math.Round (PointsNeeded, 2);
		
	}

	public void AddPoints(double x){
		TotalPoints += x;
		LevelPoints += x;

		if (LevelPoints > PointsNeeded) {
			LevelPoints = 0;
			IncreaseLevel();
		}
	}

	void EndGame(){

		this.gameObject.GetComponent<LoadMenuScreen> ().GameEnd ();
	}

}
