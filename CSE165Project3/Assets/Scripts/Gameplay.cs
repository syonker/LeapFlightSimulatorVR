using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour {

    public GameObject checkPoint;
    public int numCheckpoints;
    public int currIndex;
    public GameObject currCheckpoint;

    public GameObject countDownCanvas;
    public GameObject distanceCanvas;
    public GameObject timerCanvas;
    public GameObject planeCanvas;

    public GameObject countdownAudio;
    public GameObject cheerAudio;
    public GameObject checkpointAudio;
    public GameObject engineAudio;
    public GameObject explosionAudio;

    private int currView = 0;

    public GameObject rig;

    public GameObject plane;

    public GameObject displays;

    public GameObject arrow;

    public Text gameTimer;
    public Text countdownTimer;
    public Text distance;

    public Material nextMaterial;
    public Material hitMaterial;

    public bool gameOver = false;

    private LineRenderer line;

    private LineRenderer newLine;

    public bool countdown;

    //change back to 10
    private float timeLeft = 10;

    private float gameTime = -10;

    // Use this for initialization
    void Start () {

        line = GetComponent<LineRenderer>();

        line.enabled = true;

        line.material.color = Color.green;

        Parse();

        countdown = true;
        Countdown();

        //inlucde this to set starting view
        ChangeView();

	}

    void Parse()
    {

        float x, y, z;
        int i;

        string path = "Assets/file2.txt";

        StreamReader reader = new StreamReader(path);

        newLine = checkPoint.GetComponent<LineRenderer>();

        //newLine.SetVertexCount(100);

        newLine.positionCount = 100;

        newLine.enabled = true;

        //Color orange = new Color(255, 50, 0);

        newLine.material.color = Color.blue;

        for (i = 0; i < 100; i++)
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

                newObj.transform.position = new Vector3(x, y, z);
                newObj.SetActive(true);

                newLine.SetPosition(i, newObj.transform.position);

                newObj.GetComponent<LineRenderer>().enabled = false;


            }
            else
            {

                break;
            }


        }

        currIndex = 1;
        currCheckpoint = checkPoint.transform.parent.GetChild(1).gameObject;
        currCheckpoint.GetComponent<MeshRenderer>().material = hitMaterial;
        checkPoint.transform.parent.GetChild(2).gameObject.GetComponent<MeshRenderer>().material = nextMaterial;

        //checkPoint.transform.parent.GetChild(2).gameObject.GetComponent<AudioSource>().mute = false;

        numCheckpoints = i;
        Debug.Log(numCheckpoints);

        this.transform.position = checkPoint.transform.parent.GetChild(1).transform.position;


        plane.SetActive(false);
        //add offset for 3rd person
        //Vector3 offset = new Vector3(0,5.0f,-20.0f);


        //rig.transform.position = rig.transform.position + offset;
        //displays.transform.position = displays.transform.position + offset;










        Vector3 child1Pos = checkPoint.transform.parent.GetChild(1).transform.position;
        Vector3 child2Pos = checkPoint.transform.parent.GetChild(2).transform.position;

        Vector3 heading = child2Pos - child1Pos;

        heading.Normalize();

        heading.y = 0;

        this.transform.forward = heading;

        line.SetPosition(0, this.transform.position);
        line.SetPosition(1, checkPoint.transform.parent.GetChild(2).gameObject.transform.position);





    }

    public void Countdown ()
    {

        engineAudio.GetComponent<AudioSource>().volume = 0;

        //countdown = true;

        countdownAudio.GetComponent<AudioSource>().mute = false;

        timeLeft -= Time.deltaTime;

        countdownTimer.text = "Begin in: " + timeLeft.ToString();

        if (timeLeft < 0)
        {

            countdown = false;
            countdownAudio.GetComponent<AudioSource>().mute = true;
            timeLeft = 3;
            countdownTimer.text = "";

        }



    }

    public void ResetCheckpoint()
    {

        explosionAudio.GetComponent<AudioSource>().Play();

        this.transform.position = currCheckpoint.transform.position;

        Vector3 heading = this.transform.forward;
        heading.y = 0;

        this.transform.forward = heading;

        //draw the line from the player to the next checkpoint
        line.SetPosition(0, this.transform.position);

        //adjust arrow direction
        Vector3 goal = line.GetPosition(1) - line.GetPosition(0);

        //rotate so that z is goal
        arrow.transform.forward = goal;


        float dist = (checkPoint.transform.parent.GetChild(currIndex + 1).gameObject.transform.position - this.transform.position).magnitude;

        distance.text = "Distance: \n" + dist + "m";


        countdown = true;
        Countdown();

    }


    public void ChangeView()
    {

        if (currView < 3)
        {
            currView++;
        }
        else
        {
            currView = 0;
        }


        //nothing
        if (currView == 0)
        {
            /*
            //add offset for 3rd person
            Vector3 offset = new Vector3(0.0f, 0.0f, 0.0f);

            Vector3 shiftBack = -plane.transform.forward;
            Vector3 shiftUp = plane.transform.up;

            offset = 20.0f * shiftBack + 5.0f * shiftUp;

            rig.transform.position = rig.transform.position - offset;
            displays.transform.position = displays.transform.position - offset;
            */
            plane.SetActive(false);

            //displays.SetActive(true);

            planeCanvas.SetActive(true);
            distanceCanvas.SetActive(true);



            newLine.enabled = true;
            line.enabled = true;

            engineAudio.GetComponent<AudioSource>().mute = false;

            checkPoint.transform.parent.GetChild(currIndex + 1).gameObject.GetComponent<AudioSource>().mute = true;



        }
        //cockpit
        else if (currView == 1)
        {
            plane.SetActive(true);


        }
        //3rd person
        else if (currView == 2)
        {

            //add offset for 3rd person
            //Vector3 offset = new Vector3(0,5.0f,-20.0f);
            Vector3 offset = new Vector3(0.0f, 0.0f, 0.0f);

            Vector3 shiftBack = -plane.transform.forward;
            Vector3 shiftUp = plane.transform.up;

            offset = 20.0f * shiftBack + 5.0f * shiftUp;

            rig.transform.position = rig.transform.position + offset;
            displays.transform.position = displays.transform.position + offset;


        }
        //just sound
        else if (currView == 3)
        {

            //add offset for 3rd person
            Vector3 offset = new Vector3(0.0f, 0.0f, 0.0f);

            Vector3 shiftBack = -plane.transform.forward;
            Vector3 shiftUp = plane.transform.up;

            offset = 20.0f * shiftBack + 5.0f * shiftUp;

            rig.transform.position = rig.transform.position - offset;
            displays.transform.position = displays.transform.position - offset;

            //displays.SetActive(false);

            planeCanvas.SetActive(false);
            distanceCanvas.SetActive(false);


            //countDownCanvas.SetActive(true);

            newLine.enabled = false;
            line.enabled = false;

            engineAudio.GetComponent<AudioSource>().mute = true;

            checkPoint.transform.parent.GetChild(currIndex + 1).gameObject.GetComponent<AudioSource>().mute = false;


        }
        else
        {

            Debug.Log("ERRRROORR");
        }



    }


    public void NewCheckPoint()
    {

        if (currIndex+1 == numCheckpoints)
        {

            WinGame();

            return;

        }


        checkpointAudio.GetComponent<AudioSource>().Play();

        currIndex++;
        currCheckpoint = checkPoint.transform.parent.GetChild(currIndex).gameObject;
        currCheckpoint.GetComponent<MeshRenderer>().material = hitMaterial;
        currCheckpoint.GetComponent<AudioSource>().mute = true;

        checkPoint.transform.parent.GetChild(currIndex+1).gameObject.GetComponent<MeshRenderer>().material = nextMaterial;

        if (currView == 3)
        {

            checkPoint.transform.parent.GetChild(currIndex + 1).gameObject.GetComponent<AudioSource>().mute = false;

        }
        line.SetPosition(1, checkPoint.transform.parent.GetChild(currIndex + 1).gameObject.transform.position);


    }

    public void WinGame()
    {
        gameOver = true;

        line.enabled = false;

        //Debug.Log("WIN GAME");

        if (gameTime <= 300)
        {

            checkPoint.transform.parent.GetChild(currIndex + 1).gameObject.GetComponent<AudioSource>().mute = true;

            countdownTimer.text = "You Win!";

            cheerAudio.GetComponent<AudioSource>().Play();

            engineAudio.GetComponent<AudioSource>().mute = true;

        } else
        {

            countdownTimer.text = "You Lose!";
        }


    }

    // Update is called once per frame
    void Update () {

        if (!gameOver)
        {

            Quaternion temp = this.transform.localRotation;

            Vector3 tempp = temp.eulerAngles;

            tempp.z = 0;


            //fix roll
            this.transform.localRotation = Quaternion.Euler(tempp);


            if (this.transform.position.y < 0)
            {

                ResetCheckpoint();

            }



            if (gameTime > 300)
            {
                WinGame();

                return;
            }

            gameTime += Time.deltaTime;

            if (gameTime >= 0) {

                gameTimer.text = "Time: \n" + gameTime.ToString() + "s";

            }

            if (countdown)
            {
                Countdown();

                return;
            }


            //draw the line from the player to the next checkpoint
            line.SetPosition(0, this.transform.position);

            //adjust arrow direction
            Vector3 goal = line.GetPosition(1) - line.GetPosition(0);

            //rotate so that z is goal
            arrow.transform.forward = goal;


            float dist = (checkPoint.transform.parent.GetChild(currIndex + 1).gameObject.transform.position - this.transform.position).magnitude;

            distance.text = "Distance: \n" + dist + "m";

        }


	}
}
