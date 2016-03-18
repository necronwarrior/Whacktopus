using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveTowardsBar : MonoBehaviour {

    float step, speed;
    float Movetimer;
    public GameObject Bar;
	// Use this for initialization
	void Start () {
        Bar = GameObject.Find("Coins_limit_topper");
        Movetimer = 1.0f;
        speed = 5.0f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Movetimer -= Time.deltaTime;

        if (Movetimer < 0.0f)
        {
            step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, Bar.transform.position,step);
            if(gameObject.transform.position == Bar.transform.position)
            {
                Destroy(gameObject);
            }
        }

	}
}
