using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;


public class HandControls : MonoBehaviour {

    Controller controller;
    float HandPalmPitch;
    float HandPalmYaw;
    float HandPalmRoll;
    //float HandWristRot;

    private Hand leftHand, rightHand;


	// Use this for initialization
	void Start () {


	}

	
	// Update is called once per frame
	void Update () {

        if (!GetComponent<Gameplay>().countdown)
        {

            controller = new Controller();
            Frame frame = controller.Frame();
            List<Hand> hands = frame.Hands;

            if (frame.Hands.Count > 0)
            {

                if (hands[0].IsRight)
                {

                    rightHand = hands[0];

                }
                else if (hands[0].IsLeft)
                {
                    leftHand = hands[0];
                }

                if ((frame.Hands.Count > 1) && (hands[1].IsRight))
                {
                    rightHand = hands[1];

                }
                else if ((frame.Hands.Count > 1) && (hands[1].IsLeft))
                {
                    leftHand = hands[1];
                }


                if (leftHand != null)
                {


                    //between 0 and 1
                    float speed = leftHand.GrabAngle;
                    speed = speed / (Mathf.PI);

                    transform.Translate(new Vector3(0, 0, speed / 10));

                }







                if (rightHand != null)
                {



                    //check for closed fist
                    if (rightHand.GrabAngle > ((Mathf.PI) * 0.75f))
                    {


                        return;
                    }


                    //x
                    HandPalmPitch = rightHand.PalmNormal.Pitch;
                    //z
                    HandPalmRoll = rightHand.PalmNormal.Roll;
                    //y
                    HandPalmYaw = rightHand.PalmNormal.Yaw;
                    //HandWristRot = rightHand.WristPosition.Pitch;



                    /*
                    //turn left/right
                    if (HandPalmPitch > 1)
                    {
                        transform.Rotate(-20 * Time.deltaTime, 0, 0);
                    }
                    else if (HandPalmPitch < -1)
                    {
                        transform.Rotate(20 * Time.deltaTime, 0, 0);
                    }
                    */

                    if (rightHand.PinchStrength < 0.9f)
                    {
                        
                            //turn up/down
                            if (HandPalmPitch > 1.5)
                            {
                                transform.Rotate(-20 * Time.deltaTime, 0, 0);
                            }
                            else if (HandPalmPitch < -0.5)
                            {
                                transform.Rotate(20 * Time.deltaTime, 0, 0);
                            }
                            

                    }
                    else
                    {


                        if (HandPalmYaw > 1.5)
                        {
                            transform.Rotate(0, 20 * Time.deltaTime, 0);
                        }
                        else if (HandPalmYaw < -0.5)
                        {
                            transform.Rotate(0, -20 * Time.deltaTime, 0);
                        }

                    }
                    
                




                    /*
                    if (Mathf.Abs(HandPalmRoll) > Mathf.Abs(HandPalmPitch))
                    {

                        //turn left/right
                        if (HandPalmRoll > 1)
                        {
                            transform.Rotate(0, 20 * Time.deltaTime, 0);
                        }
                        else if (HandPalmRoll < -1)
                        {
                            transform.Rotate(0, -20 * Time.deltaTime, 0);
                        }

                    }
                    */
                    /* else
                     {

                         //turn up/down
                         if (HandPalmPitch > 1.5)
                         {
                             transform.Rotate(20 * Time.deltaTime, 0, 0);
                         }
                         else if (HandPalmPitch < -0.5)
                         {
                             transform.Rotate(-20 * Time.deltaTime, 0, 0);
                         }

                     }*/


                }







            }








        }

    }









}
