using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using System.IO;

public class HandControls : MonoBehaviour {

    Controller controller;
    float HandPalmPitch;
    float HandPalmYaw;
    float HandPalmRoll;
    float HandWristRot;

    private Hand leftHand, rightHand;


    public GameObject checkPoint;


	// Use this for initialization
	void Start () {

        Parse();

	}


    void Parse()
    {

        float x, y, z;

        string path = "Assets/file2.txt";

        StreamReader reader = new StreamReader(path);

        for (int i = 0; i < 100; i++)
        {

            string line = reader.ReadLine();

            if (line != null)
            {

                string[] nums;

                char[] temp = null;

                nums = line.Split(temp, 3);

                //Debug.Log(nums[2]);
                x = float.Parse(nums[0]);
                y = float.Parse(nums[1]);
                z = float.Parse(nums[2]);

                x /= 39.3701f;
                y /= 39.3701f;
                z /= 39.3701f;



                GameObject newObj = Instantiate(checkPoint, checkPoint.transform.parent, true);

                newObj.transform.position = new Vector3(x,y,z);
                newObj.SetActive(true);



            } else
            {

                break;
            }


        }

    }
	
	// Update is called once per frame
	void Update () {

        

        controller = new Controller();
        Frame frame = controller.Frame();
        List<Hand> hands = frame.Hands;

        if (frame.Hands.Count > 0)
        {

            if (hands[0].IsRight) {

                rightHand = hands[0];

            } else if (hands[0].IsLeft)
            {
                leftHand = hands[0];
            }

            if ((frame.Hands.Count > 1) && (hands[1].IsRight))
            {
                rightHand = hands[1];

            } else if ((frame.Hands.Count > 1) && (hands[1].IsLeft))
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
                //y
                HandPalmRoll = rightHand.PalmNormal.Roll;
                //z
                HandPalmYaw = rightHand.PalmNormal.Yaw;
                HandWristRot = rightHand.WristPosition.Pitch;




                //turn left/right
                if (HandPalmPitch > 1)
                {
                    transform.Rotate(-20 * Time.deltaTime, 0, 0);
                }
                else if (HandPalmPitch < -1)
                {
                    transform.Rotate(20 * Time.deltaTime, 0, 0);
                }

                //turn up/down
                if (HandPalmRoll > 1.5)
                {
                    transform.Rotate(0, 20 * Time.deltaTime, 0);
                }
                else if (HandPalmRoll < -0.5)
                {
                    transform.Rotate(0, -20 * Time.deltaTime, 0);
                }



            }

            





        }





        




    }









}
