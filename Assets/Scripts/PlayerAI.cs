using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Algorithms;

public class PlayerAI : MonoBehaviour
{

    GameObject[] buildings;
    public List<GameObject> redBuildings;
    List<GameObject> copyRedBuildings;
    public static List<int> annealingRoad;
    public static List<int> neighbourRoad;
    public static List<int> antRoad;
    public static List<int> geneticRoad;
    public static List<List<int>> roads;
    List<int> goodRoad;
    public int nextBuilding;
    int numberRed;
    int prevBuilding;
    public float speed;
    Vector3 direction;
    bool isBuildingReached;
    [SerializeField]
    Material buildingRed;
    public bool switchAlgorithm;
    int algorithmCount;
    int countBuildingOnRoad;
    //List <Algorithms.IAlgorithm> algorithms;
    //string path;
    public static bool areAlgorithmsCompute;
 

    // Start is called before the first frame update
    void Start()
    {

        areAlgorithmsCompute = true;

        buildings = GameManager.buildings;
        //buildings = GameObject.FindGameObjectsWithTag("Building");
        numberRed = 0;
        algorithmCount = 0;

        /*foreach(GameObject build in buildings)
        {
            if (build.GetComponent<Renderer>().sharedMaterial.name == "Red")
            {
                redBuildings.Add(build);
                numberRed++;
            }
 
        }*/

        redBuildings = GameManager.redBuildings;

        //print("red: " + redBuildings.ToArray().Length);

        copyRedBuildings = redBuildings;

        //print(numberRed);

        //path = SaveMatrixToFile(redBuildings, numberRed);

        //Algorithms.Program.graphPath = path;

        switchAlgorithm = false;

        transform.position = redBuildings[0].transform.position;

        prevBuilding = -1;



        goodRoad = Algorithms.Program.SimulatedAnnealing();
        ChangeBuilding(buildings[nextBuilding]);

        /* roads = new List<List<int>>
         {
             annealingRoad,
             antRoad,
             geneticRoad,
             neighbourRoad
         };*/

        //print("Annealing" + annealingRoad.ToString());
        algorithmCount++;
        countBuildingOnRoad = 0;

    }


    // Update is called once per frame
    void Update()
    {
        //print("is reached = " + isBuildingReached);
        //print("next = " + nextBuilding);
        //print("prev = " + prevBuilding);

        switchAlgorithm = ShouldSwitchAlgorithm();

        if (switchAlgorithm)
        {
            NewAlgorithm();
            switchAlgorithm = false;
        }



        if (!isBuildingReached)
        {
            Move();
 
        }

        if(isBuildingReached)
        {
            //countBuildingOnRoad++;
            nextBuilding = goodRoad[countBuildingOnRoad++];
            ChangeBuilding(redBuildings[nextBuilding]);
        }

        transform.LookAt(redBuildings[nextBuilding].transform);



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


        }
  
    }

    private void ChangeBuilding(GameObject building)
    {

        isBuildingReached = false;
        print(building.name);
        //prevBuilding = nextBuilding;

    }

    string SaveMatrixToFile(List<GameObject> buildings, int number)
    {
        string matrix = MakeMatrix(buildings, number);
        string name = "tsp_" + number.ToString() + "_" + Random.Range(0, 1000).ToString();
        string path = Application.dataPath + "/Algorithms/graphs/";
        path += name + ".txt";
        File.WriteAllText(path, matrix);
        //print(path);

        Program.graphPath = path;

        return path;

    }

    string MakeMatrix(List<GameObject> buildings, int number)
    {
        string matrix = number.ToString() + '\n';
        int distance;


        for(int i = 0; i < number; i++)
        {
            for(int j = 0; j < number; j++)
            {
                if(i == j)
                {
                    matrix += "-1 ";
                    continue;
                }

                distance = (int)Vector3.Distance(buildings[i].transform.position, buildings[j].transform.position);

                matrix += distance.ToString() + " ";
            }

            matrix += "\n";
        }


        return matrix;
    }

    public void NewAlgorithm()
    {
        redBuildings = copyRedBuildings;

        foreach(GameObject build in redBuildings)
        {
            build.GetComponent<Renderer>().sharedMaterial = buildingRed;
        }

        nextBuilding = 0;
        countBuildingOnRoad = 0;
        transform.position = buildings[0].transform.position;
        prevBuilding = -1;
        ChangeBuilding(buildings[nextBuilding]);

        switch(algorithmCount)
        {
            case 1:
                goodRoad = Algorithms.Program.AntColony();
                break;
            case 2:
                goodRoad = Algorithms.Program.GeneticAlgorithm();
                //print("Genetic" + goodRoad.ToString());
                break;
            case 3:
                goodRoad = Algorithms.Program.NearestNeighbour();
                //print("Nearest" + goodRoad.ToString());
                break;
            default:
                //print("No tak się nie da");
                break;
        }
        
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

    IEnumerator HaltStart()
    {
        yield return null;
    }


}
