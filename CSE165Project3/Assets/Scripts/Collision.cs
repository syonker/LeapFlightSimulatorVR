using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour {


    public Vector3 lastPos;
    public Quaternion lastRot;

    // Use this for initialization
    void Start () {

        Debug.Log("Start");
        lastPos = this.transform.position;
        lastRot = this.transform.rotation;
        
	}
	

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Collision");


        //if we hit something on campus
        if (col.transform.parent.gameObject.CompareTag("campus"))
        {
            Debug.Log("Collision with campus");
            GetComponent<Gameplay>().ResetCheckpoint();
          

        } else if (col.transform.parent.gameObject.CompareTag("checkpoint"))
        {
            //check if we hit the right checkpoint
            if (col.gameObject == GetComponent<Gameplay>().checkPoint.transform.parent.GetChild(GetComponent<Gameplay>().currIndex+1).gameObject)
            {
                Debug.Log("Hit goal");
                GetComponent<Gameplay>().NewCheckPoint();

            }


        }
    }
}
