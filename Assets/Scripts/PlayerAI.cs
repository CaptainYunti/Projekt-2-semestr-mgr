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

        buildings = GameObject.FindGameObjectsWithTag("Building");
        numberRed = 0;
        algorithmCount = 0;

        foreach(GameObject build in buildings)
        {
            if (build.GetComponent<Renderer>().sharedMaterial.name == "Red")
            {
                redBuildings.Add(build);
                numberRed++;
            }
 
        }

        print("red: " + redBuildings.ToArray().Length);

        copyRedBuildings = redBuildings;

        //print(numberRed);

        //path = SaveMatrixToFile(redBuildings, numberRed);

        //Algorithms.Program.graphPath = path;

        switchAlgorithm = false;

        transform.position = buildings[0].transform.position;

        prevBuilding = -1;
        ChangeBuilding(buildings[nextBuilding]);



        //roads = [annealingRoad, antRoad, geneticRoad, neighbourRoad];
        roads.Add(annealingRoad);
        roads.Add(antRoad);
        roads.Add(geneticRoad);
        roads.Add(neighbourRoad);

        print("Annealing" + annealingRoad.ToString());
        algorithmCount++;
        countBuildingOnRoad = 0;

    }


    // Update is called once per frame
    void Update()
    {
        if(!isBuildingReached)
        {
            Move();
 
        }

        if(isBuildingReached && prevBuilding != nextBuilding)
        {
            countBuildingOnRoad++;
            nextBuilding = roads[algorithmCount][countBuildingOnRoad];
            ChangeBuilding(buildings[nextBuilding]);
        }

        transform.LookAt(buildings[nextBuilding].transform);

        if(switchAlgorithm)
        {
            NewAlgorithm();
            switchAlgorithm = false;
        }

        switchAlgorithm = ShouldSwitchAlgorithm();

    }

    void Move()
    {
        direction = Vector3.zero;

 
        direction += transform.forward;
        transform.localPosition += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == buildings[nextBuilding])
        {
            isBuildingReached = true;
            prevBuilding = nextBuilding;
        }
  
    }

    private void ChangeBuilding(GameObject building)
    {

        isBuildingReached = false;
        print(building.name);
    }

    string SaveMatrixToFile(List<GameObject> buildings, int number)
    {
        string matrix = MakeMatrix(buildings, number);
        string name = "tsp_" + number.ToString() + "_" + Random.Range(0, 1000).ToString();
        string path = Application.dataPath + "/Algorithms/graphs/";
        path += name + ".txt";
        File.WriteAllText(path, matrix);
        print(path);

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

        transform.position = buildings[0].transform.position;
        prevBuilding = -1;
        ChangeBuilding(buildings[nextBuilding]);

        /*switch(algorithmCount)
        {
            case 1: 
                algorithms[1].LoadGraph(path);
                algorithms[1].Start();
                goodRoad = algorithms[1].GetCities();
                print("Annealing" + goodRoad.ToString());
                print("Ants" + goodRoad.ToString());
                break;
            case 2: 
                algorithms[2].LoadGraph(path);
                algorithms[2].Start();
                goodRoad = algorithms[2].GetCities();
                print("Genetic" + goodRoad.ToString());
                break;
            case 3:
                algorithms[3].LoadGraph(path);
                algorithms[3].Start();
                goodRoad = algorithms[3].GetCities();
                print("Nearest" + goodRoad.ToString());
                break;
            default:
                print("No tak się nie da");
                break;
        }
        */
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
