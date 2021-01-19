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
 

    // Start is called before the first frame update
    void Start()
    {

        buildings = GameManager.buildings;

        algorithmCount = 0;

        redBuildings = GameManager.redBuildings;
        lenRed = redBuildings.Count;
        copyRedBuildings = redBuildings;
        copyRedBuildings = redBuildings;

        switchAlgorithm = false;

        goodRoad = Algorithms.Program.NearestNeighbour();
        //goodRoad = Algorithms.Program.SimulatedAnnealing();

        length = 0;
        for(int i = 0; i < goodRoad.Count-1; i++)
        {
            length += (int)Vector3.Distance(redBuildings[goodRoad[i]].transform.position, redBuildings[goodRoad[i+1]].transform.position);
        }
        print("Czy zjebalismy: " + length);

        countRoad = 0;
        nextBuilding = goodRoad[countRoad++];

        //nextBuilding = countRoad++;

        transform.position = redBuildings[nextBuilding].transform.position;
        redBuildings[nextBuilding].GetComponent<Renderer>().sharedMaterial = buildingGreen;


        nextBuilding = goodRoad[countRoad++];
        //nextBuilding = countRoad++;
        
        
        ChangeBuilding(redBuildings[nextBuilding]);
        GetComponent<TrailRenderer>().enabled = true;

        algorithmCount++;

    }


    // Update is called once per frame
    void Update()
    {

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

        if(isBuildingReached && countRoad < lenRed)
        {
            //print(redBuildings[nextBuilding]);
            nextBuilding = goodRoad[countRoad++];
            //nextBuilding = countRoad++;


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

        redBuildings = copyRedBuildings;

        foreach(GameObject build in redBuildings)
        {
            build.GetComponent<Renderer>().sharedMaterial = buildingRed;
        }



        /*switch (algorithmCount)
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
        */
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
        nextBuilding = goodRoad[countRoad++];
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
