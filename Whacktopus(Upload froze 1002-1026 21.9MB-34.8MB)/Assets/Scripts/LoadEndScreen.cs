using UnityEngine;
using System.Collections;

public class LoadEndScreen : MonoBehaviour {
    public ParticleSystem MainPart;
    
    // Use this for initialization
    void Start () {
        
    }
    
    public void GameEnd () {
        StartCoroutine(LoadLVL());
    }
    
    // Update is called once per frame
    public void OnClick () {
        StartCoroutine(LoadLVL());
    }
    
    IEnumerator LoadLVL() {
        MainPart.Play (); //Cascade bubbles
        yield return new WaitForSeconds(1);
        
        yield return new WaitForSeconds(1);
        MainPart.Stop (); //Prevent looping of particles
        yield return new WaitForSeconds(1);
        Application.LoadLevel("EndScreen"); //load the instructions screen
        
    }
}
