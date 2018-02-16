using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

public class HandControls : MonoBehaviour {

    Controller controller;
    float HandPalmPitch;
    float HandPalmYaw;
    float HandPalmRoll;
    float HandWristRot; 


	// Use this for initialization
	void Start () {
       
        
		
	}
	
	// Update is called once per frame
	void Update () {

        

        controller = new Controller();
        Frame frame = controller.Frame();
        List<Hand> hands = frame.Hands;

        if (frame.Hands.Count > 0)
        {
            //Hand firstHand = hands[0];


            //x
            HandPalmPitch = hands[0].PalmNormal.Pitch;
            //y
            HandPalmRoll = hands[0].PalmNormal.Roll;
            //z
            HandPalmYaw = hands[0].PalmNormal.Yaw;
            HandWristRot = hands[0].WristPosition.Pitch;

           

            if (HandPalmYaw > 0)
            {
                //transform.Translate(new Vector3(Time.deltaTime, 0, 0));

                //transform.Rotate(0,0,-20*Time.deltaTime);
            }
            else if (HandPalmYaw < 0)
            {
                //transform.Translate(new Vector3(Time.deltaTime*-1, 0, 0));

                //transform.Rotate(0, 0, 20*Time.deltaTime);
            }

            if (HandPalmPitch > 1)
            {
                //transform.Translate(new Vector3(0, Time.deltaTime, 0));
                transform.Rotate(-20 * Time.deltaTime, 0, 0);
            }
            else if (HandPalmPitch < -1)
            {
                transform.Rotate(20 * Time.deltaTime, 0, 0);
                //transform.Translate(new Vector3(0, Time.deltaTime * -1, 0));
            }

            
            if (HandPalmRoll > 1)
            {
                //transform.Translate(new Vector3(Time.deltaTime, 0, 0));

                transform.Rotate(0, 20 * Time.deltaTime, 0);
            }
            else if (HandPalmRoll < -1)
            {
                //transform.Translate(new Vector3(Time.deltaTime * -1, 0, 0));

                transform.Rotate(0, -20 * Time.deltaTime, 0);
            }
            

        }





        




    }









}
