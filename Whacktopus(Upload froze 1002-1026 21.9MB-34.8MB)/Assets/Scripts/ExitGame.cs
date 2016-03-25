using UnityEngine;
using System.Collections;

public class ExitGame : MonoBehaviour 
{

	AudioClip Click;
	
	void Start()
	{	
		Click = Resources.Load ("Sounds/moving/Button-Click-2") as AudioClip;
	}

	public void OnClick() 
	{
		PlayerPrefs.DeleteAll ();
	this.transform.gameObject.GetComponent<AudioSource>().PlayOneShot(Click);
			Application.Quit(); 
	}
}
