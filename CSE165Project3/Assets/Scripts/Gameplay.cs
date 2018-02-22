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

    public GameObject arrow;

    public Text gameTimer;
    public Text countdownTimer;
    public Text distance;

    public Material nextMaterial;
    public Material hitMaterial;

    private LineRenderer line;

    public bool countdown;

    private float timeLeft = 3;

    private float gameTime = 0;

    // Use this for initialization
    void Start () {

        line = GetComponent<LineRenderer>();

        line.enabled = true;

        line.material.color = Color.green;

        Parse();

        countdown = true;
        Countdown();

	}

    void Parse()
    {

        float x, y, z;
        int i;

        string path = "Assets/file.txt";

        StreamReader reader = new StreamReader(path);

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

        numCheckpoints = i;
        Debug.Log(numCheckpoints);

        this.transform.position = checkPoint.transform.parent.GetChild(1).transform.position;
        
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


        //countdown = true;

        timeLeft -= Time.deltaTime;

        countdownTimer.text = "Begin in: " + timeLeft.ToString();

        if (timeLeft < 0)
        {

            countdown = false;
            timeLeft = 3;
            countdownTimer.text = "";

        }



    }

    public void ResetCheckpoint()
    {

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

    public void NewCheckPoint()
    {

        if (currIndex+1 == numCheckpoints)
        {
            line.enabled = false;

            Debug.Log("WIN GAME");

            return;

        }

        currIndex++;
        currCheckpoint = checkPoint.transform.parent.GetChild(currIndex).gameObject;
        currCheckpoint.GetComponent<MeshRenderer>().material = hitMaterial;

        checkPoint.transform.parent.GetChild(currIndex+1).gameObject.GetComponent<MeshRenderer>().material = nextMaterial;

        line.SetPosition(1, checkPoint.transform.parent.GetChild(currIndex + 1).gameObject.transform.position);


    }

    // Update is called once per frame
    void Update () {

        gameTime += Time.deltaTime;

        gameTimer.text = "Time: \n" + gameTime.ToString() + "s";

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
