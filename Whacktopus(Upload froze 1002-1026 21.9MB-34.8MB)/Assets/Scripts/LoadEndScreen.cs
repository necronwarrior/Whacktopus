using UnityEngine;
using System.Collections;

public class LoadEndScreen : MonoBehaviour {
    public ParticleSystem MainPart;
	public GameObject Foam;
	GameObject MainCam;
	bool Foamy, Foamystart;
    // Use this for initialization
    void Start () {
		Foamy = false;
		Foamystart = true;
		MainCam = GameObject.Find("Main Camera");
    }
    
    public void GameEnd () {
        StartCoroutine(LoadLVL());
    }
    
    // Update is called once per frame
    public void OnClick () {
        StartCoroutine(LoadLVL());
    }
    
    IEnumerator LoadLVL() {
		Foamy = true;
        yield return new WaitForSeconds(3);
        Application.LoadLevel("EndScreen"); //load the instructions screen
        
    }
	
	void Update()
	{
		if (Foamystart == true) {
			Foam.transform.position = 		Vector3.MoveTowards (Foam.transform.position, 
			                                                 new Vector3 (MainCam.transform.position.x , MainCam.transform.position.y-10.0f, MainCam.transform.position.z-10.0f),
			                                                 0.3f);
			if (Foam.transform.position ==  new Vector3 (MainCam.transform.position.x , MainCam.transform.position.y-10.0f, MainCam.transform.position.z-10.0f))
			{
				Foamystart=false;
			}

		}
		if (Foamy == true) {
			Foam.transform.position = 		Vector3.MoveTowards (Foam.transform.position, 
			                                                 new Vector3 (MainCam.transform.position.x , MainCam.transform.position.y-1.0f, MainCam.transform.position.z+1.0f),
			                                                 0.1f);
		}
	}
}
