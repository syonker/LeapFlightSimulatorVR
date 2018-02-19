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
            this.transform.position = lastPos;
            this.transform.rotation = lastRot;
        }
    }
}
