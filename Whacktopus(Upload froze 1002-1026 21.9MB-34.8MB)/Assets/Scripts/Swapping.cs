using UnityEngine;
using System.Collections;

public class Swapping : MonoBehaviour {

	//public float testin_1, testin_2, testin_3;

	public Vector3 world_pos, click_reset;
	public Vector3 test1;
	bool MouseHeld, HasBeenHeld;

    GameObject Passover;

	float new_y;

	private ClickManager ClickManagerParent;

	// Use this for initialization
	void Start () {
		ClickManagerParent = GetComponent<ClickManager> ();

		HasBeenHeld = false;
	} 

	// Update is called once per frame
	void Update () {

		//reset mouseclick
        if (ClickManagerParent.CurrentOctoObject != null)
        {
            if (ClickManagerParent.GetClickHold() == 1 
                && ClickManagerParent.CurrentOctoObject.GetComponent<States>().currentOctopus != OctopusState.Under)
            {

                if (ClickManagerParent.CollisionTest() == true)
                {
                    if (HasBeenHeld == false)
                    {
                        click_reset = ClickManagerParent.CurrentOctoObject.transform.parent.position;
                        HasBeenHeld = true;
                    }
                    FollowDrag();


                }

            }

            if (ClickManagerParent.GetClickHold() == 3 &&
                HasBeenHeld == true)
            {
                //ClickManagerParent.Held = 3;
				
                // ray shoting our of octo-object
                Ray RayFromHeldOcto = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit HitFromHeldOcto;
                ClickManagerParent.CurrentOctoObject.GetComponent<SphereCollider>().enabled = false;
                //Debug.DrawLine(ray.origin, ray.direction, Color.black, 5.0f);
                if (Physics.Raycast(RayFromHeldOcto, out HitFromHeldOcto, 100.0f))
                {
                    Debug.DrawLine(RayFromHeldOcto.origin, HitFromHeldOcto.point, Color.cyan, 5.0f);
                    Vector3 HitFromHeldOctoPos = HitFromHeldOcto.collider.gameObject.transform.parent.position;

                    ClickManagerParent.CurrentOctoObject.GetComponent<SphereCollider>().enabled = true;
                    if (HitFromHeldOcto.collider != null)
                    {

                        int roundz, roundx, roundclickz, roundclickx;
                        roundx = (int)Mathf.Round(HitFromHeldOctoPos.x * 100.0f) ;
                        roundz = (int)Mathf.Round(HitFromHeldOctoPos.z * 100.0f) ;

                        roundclickz = (int)Mathf.Round(click_reset.z * 100.0f);
                        roundclickx = (int)Mathf.Round(click_reset.x * 100.0f) ;

                        bool fool = ((((roundclickz - 150) == roundz)
                            || (roundz == (roundclickz + 150)))
                            && roundx == roundclickx);
                        bool rule = ((((roundclickx - 150) == roundx)
                            || (roundx == (roundclickx + 150)))
                            && roundz == roundclickz);

                        if (fool == true || rule == true)
                        {
                            ClickManagerParent.CurrentOctoObject.transform.parent.position = HitFromHeldOctoPos;
                            HitFromHeldOctoPos = click_reset;

                            //swap object neighbours


                            
                            for (int swappee=0; swappee<4; swappee++)
                            {
                                
                                
                                if (swappee == 0 || swappee == 1)
                                {
                                    if(ClickManagerParent.CurrentOctoObject.transform.gameObject.GetComponent<Matching>().ObjectsHits [swappee] == null)
                                    {
                                     //   HitFromHeldOcto.collider.gameObject.GetComponent<Matching>().ObjectsHits [swappee].gameObject.GetComponent<Matching>().ObjectsHits[swappee+2] = null;
                                    }else{
                                        if(HitFromHeldOcto.collider.gameObject.GetComponent<Matching>().ObjectsHits [swappee] == null)
                                        {
                                          //  ClickManagerParent.CurrentOctoObject.transform.gameObject.GetComponent<Matching>().ObjectsHits [swappee].gameObject.GetComponent<Matching>().ObjectsHits[swappee+2] = null;
                                        }else{

                                            ClickManagerParent.CurrentOctoObject.transform.gameObject.GetComponent<Matching>().ObjectsHits [swappee].gameObject.GetComponent<Matching>().ObjectsHits[swappee+2] =  HitFromHeldOcto.collider.gameObject;
                                    
                                            HitFromHeldOcto.collider.gameObject.GetComponent<Matching>().ObjectsHits [swappee].gameObject.GetComponent<Matching>().ObjectsHits[swappee+2] = ClickManagerParent.CurrentOctoObject.transform.gameObject;
                                   
                                        }
                                    }
                                }
                                
                                if (swappee == 2 || swappee == 3)
                                {
                                        if(ClickManagerParent.CurrentOctoObject.transform.gameObject.GetComponent<Matching>().ObjectsHits [swappee] == null)
                                        {
                                       //     HitFromHeldOcto.collider.gameObject.GetComponent<Matching>().ObjectsHits [swappee].gameObject.GetComponent<Matching>().ObjectsHits[swappee-2] = null;
                                        }else{
                                            if(HitFromHeldOcto.collider.gameObject.GetComponent<Matching>().ObjectsHits [swappee] == null)
                                            {
                                              //  ClickManagerParent.CurrentOctoObject.transform.gameObject.GetComponent<Matching>().ObjectsHits [swappee].gameObject.GetComponent<Matching>().ObjectsHits[swappee-2] = null;
                                            }else{

                                    
                                            ClickManagerParent.CurrentOctoObject.transform.gameObject.GetComponent<Matching>().ObjectsHits [swappee].gameObject.GetComponent<Matching>().ObjectsHits[swappee-2] =  HitFromHeldOcto.collider.gameObject;
                                    
                                            HitFromHeldOcto.collider.gameObject.GetComponent<Matching>().ObjectsHits [swappee].gameObject.GetComponent<Matching>().ObjectsHits[swappee-2] = ClickManagerParent.CurrentOctoObject.transform.gameObject;
                                
                                            }
                                        }
                                }
                                
                                Passover = ClickManagerParent.CurrentOctoObject.transform.gameObject.GetComponent<Matching>().ObjectsHits [swappee];
                                
                                ClickManagerParent.CurrentOctoObject.transform.gameObject.GetComponent<Matching>().ObjectsHits [swappee] = HitFromHeldOcto.collider.gameObject.GetComponent<Matching>().ObjectsHits [swappee];
                                HitFromHeldOcto.collider.gameObject.GetComponent<Matching>().ObjectsHits [swappee] = Passover;
                              
                            }


                            //set new click resets
                            ClickManagerParent.CurrentOctoObject.transform.gameObject.GetComponent<Swapping>().click_reset = ClickManagerParent.CurrentOctoObject.transform.gameObject.transform.parent.position;
                            HitFromHeldOcto.collider.gameObject.GetComponent<Swapping>().click_reset = HitFromHeldOctoPos;

                            //set checkstates
                            ClickManagerParent.CurrentOctoObject.transform.gameObject.GetComponent<States>().currentCheck = CheckState.CheckMatch;
                            HitFromHeldOcto.collider.gameObject.GetComponent<States>().currentCheck = CheckState.CheckMatch;

                            ClickManagerParent.CurrentOctoObject.transform.gameObject.GetComponent<States>().currentOctopus = ClickManagerParent.CurrentOctoObject.transform.gameObject.GetComponent<States>().PrevState;
                            HitFromHeldOcto.collider.gameObject.transform.parent.position = HitFromHeldOctoPos;
                        } else
                        {

                            ClickManagerParent.CurrentOctoObject.transform.parent.position = click_reset;
                            ClickManagerParent.CurrentOctoObject.GetComponent<States>().currentOctopus = ClickManagerParent.CurrentOctoObject.GetComponent<States>().PrevState;

                        }
                    }
                } else
                {
                    ClickManagerParent.CurrentOctoObject.GetComponent<SphereCollider>().enabled = true;
                    ClickManagerParent.CurrentOctoObject.transform.parent.position = click_reset;
                    ClickManagerParent.CurrentOctoObject.GetComponent<States>().currentOctopus = ClickManagerParent.CurrentOctoObject.GetComponent<States>().PrevState;
                }
                ClickManagerParent.CurrentOctoObject.transform.parent.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);
                HasBeenHeld = false;
            }
        }

	}

	void FollowDrag() 
	{
		//set object to being picked up
		if (ClickManagerParent.CurrentOctoObject.GetComponent<States> ().currentOctopus != OctopusState.Picked) {
			ClickManagerParent.CurrentOctoObject.GetComponent<States> ().PrevState = ClickManagerParent.CurrentOctoObject.GetComponent<States> ().currentOctopus;
			ClickManagerParent.CurrentOctoObject.GetComponent<States> ().currentOctopus = OctopusState.Picked;
			ClickManagerParent.CurrentOctoObject.transform.parent.transform.localScale +=new Vector3(0.5f,0.5f,0.5f);
		}

		Vector3 mouseInp = Input.mousePosition;
		/*	
		world_pos = Camera.main.ScreenToWorldPoint(new Vector3(mouseInp.x, mouseInp.y, (6.8f+(gameObject.transform.parent.position.z/2.5f))));
		//gameObject.transform.parent.position = (world_pos);
			
		new_y = (world_pos.y * ((float)System.Math.Sin (0))+3.0f);
		gameObject.transform.parent.position = new Vector3(gameObject.transform.parent.position.x , new_y, gameObject.transform.parent.position.z);
		test1 = gameObject.transform.parent.position;*/

		ClickManagerParent.CurrentOctoObject.transform.parent.position = Camera.main.ScreenToWorldPoint(new Vector3(mouseInp.x, mouseInp.y, 7.0f));
		ClickManagerParent.CurrentOctoObject.transform.parent.position = Vector3.MoveTowards(ClickManagerParent.CurrentOctoObject.transform.parent.position, Camera.main.ScreenToWorldPoint(Input.mousePosition),0.0001f);
		test1 = ClickManagerParent.CurrentOctoObject.transform.parent.position;
	}
}
