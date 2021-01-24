using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Algorithms;

public class PlayerAI : MonoBehaviour
{

    GameObject[] buildings;
    public List<GameObject> redBuildings;
    List<GameObject> copyRedBuildings;
    List<int> goodRoad;
    public int nextBuilding;
    public float speed;
    Vector3 direction;
    bool isBuildingReached;
    [SerializeField]
    Material buildingRed;
    [SerializeField]
    Material buildingGreen;
    public bool switchAlgorithm;
    int algorithmCount;
    int countRoad;
    int lenRed;
    int length;
    GameObject[] textScoresTime;
    GameObject[] textAlgorithm;
    Text actualTextScore;
    bool countTime;
    string path;
    string[] picturesNames;
    bool pictureTaken;
 

    // Start is called before the first frame update
    void Start()
    {

        buildings = GameManager.buildings;

        //textScoresTime = GameObject.FindGameObjectsWithTag("Text Score");
        textScoresTime = new GameObject[4];
        textScoresTime[0] = GameObject.Find("Text Score Annealing");
        textScoresTime[1] = GameObject.Find("Text Score Ants");
        textScoresTime[2] = GameObject.Find("Text Score Genetic");
        textScoresTime[3] = GameObject.Find("Text Score Neighbour");
        textAlgorithm = GameObject.FindGameObjectsWithTag("Algorithm Name");

        algorithmCount = 0;

        redBuildings = GameManager.redBuildings;
        lenRed = redBuildings.Count;
        copyRedBuildings = redBuildings;
        copyRedBuildings = redBuildings;

        switchAlgorithm = true;

        path = GenerateMatrix.partialPath;
        picturesNames = new string[] { "wyzarzanie.png", "mrowki.png", "genetyk.png", "sasiad.png"};

        NewAlgorithm();
        countTime = true;

        pictureTaken = false;

    }


    // Update is called once per frame
    void Update()
    {


        switchAlgorithm = ShouldSwitchAlgorithm();

        if (switchAlgorithm)
        {
 
            NewAlgorithm();
            switchAlgorithm = false;
            pictureTaken = false;
        }



        if (!isBuildingReached)
        {
            Move();
 
        }

        if(isBuildingReached && countRoad < lenRed)
        {
            //print(redBuildings[nextBuilding]);
            nextBuilding = goodRoad[countRoad++];
            //nextBuilding = countRoad++;


            ChangeBuilding(redBuildings[nextBuilding]);
        }

        transform.LookAt(redBuildings[nextBuilding].transform);

        if(countTime)
            actualTextScore.text = Timer.GetTime();

    }

    private void LateUpdate()
    {
        if (algorithmCount - 1 < picturesNames.Length && !pictureTaken && nextBuilding == goodRoad[goodRoad.Count - 1])
        {
            int distance = (int)Vector3.Distance(transform.position, redBuildings[goodRoad[goodRoad.Count - 1]].transform.position);
            if(distance < 10)
            {
                ScreenCapture.CaptureScreenshot(path + picturesNames[algorithmCount - 1]);
                pictureTaken = true;
            }


        }

    }

    void Move()
    {
        direction = Vector3.zero;

 
        direction += transform.forward;
        transform.localPosition += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == redBuildings[nextBuilding])
        {
            isBuildingReached = true;
            //print(redBuildings[nextBuilding]);
            redBuildings[nextBuilding].GetComponent<Renderer>().sharedMaterial = buildingGreen;



        }
  
    }

    private void ChangeBuilding(GameObject building)
    {

        isBuildingReached = false;
        //print(building.name);
        //prevBuilding = nextBuilding;

    }

    public void NewAlgorithm()
    {

        Timer.ResetTime();

        redBuildings = copyRedBuildings;

        foreach(GameObject build in redBuildings)
        {
            build.GetComponent<Renderer>().sharedMaterial = buildingRed;
        }



        switch (algorithmCount)
        {
            case 0:
                goodRoad = Algorithms.Program.SimulatedAnnealing();
                actualTextScore = textScoresTime[0].GetComponent<Text>();
                break;
            case 1:
                goodRoad = Algorithms.Program.AntColony();
                actualTextScore = textScoresTime[1].GetComponent<Text>();
                break;
            case 2:
                goodRoad = Algorithms.Program.GeneticAlgorithm();
                actualTextScore = textScoresTime[2].GetComponent<Text>();
                //print("Genetic" + goodRoad.ToString());
                break;
            case 3:
                goodRoad = Algorithms.Program.NearestNeighbour();
                actualTextScore = textScoresTime[3].GetComponent<Text>();
                //print("Nearest" + goodRoad.ToString());
                break;
            default:
                //print("No tak się nie da");
                break;
        }
        
        length = 0;
        for (int i = 0; i < goodRoad.Count - 1; i++)
        {
            length += (int)Vector3.Distance(redBuildings[goodRoad[i]].transform.position, redBuildings[goodRoad[i + 1]].transform.position);
        }
        print("Czy zjebalismy: " + length);

        countRoad = 0;
        countRoad = 0;
        nextBuilding = goodRoad[countRoad++];
        transform.position = redBuildings[nextBuilding].transform.position;
        redBuildings[nextBuilding].GetComponent<Renderer>().sharedMaterial = buildingGreen;
        GetComponent<TrailRenderer>().Clear();
        GetComponent<TrailRenderer>().enabled = true;
        nextBuilding = goodRoad[countRoad++];
        if(algorithmCount > textScoresTime.Length-1)
        {
            countTime = false;
        }


        ChangeBuilding(redBuildings[nextBuilding]);
        algorithmCount++;
        
    }

    bool ShouldSwitchAlgorithm()
    {
        foreach (GameObject build in buildings)
        {
            if (build.GetComponent<Renderer>().sharedMaterial.name == "Red")
                return false;
        }


        return true;
    }


}
