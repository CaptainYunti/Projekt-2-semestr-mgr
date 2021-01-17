using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerAI : MonoBehaviour
{

    GameObject[] buildings;
    public List<GameObject> redBuildings;
    List<GameObject> copyRedBuildings;
    public int nextBuilding;
    int numberRed;
    int prevBuilding;
    public float speed;
    Vector3 direction;
    bool isBuildingReached;
    [SerializeField]
    Material buildingRed;
    public bool switchAlgorithm;
 

    // Start is called before the first frame update
    void Start()
    {
        buildings = GameObject.FindGameObjectsWithTag("Building");
        numberRed = 0;

        foreach(GameObject build in buildings)
        {
            if (build.GetComponent<Renderer>().sharedMaterial.name == "Red")
            {
                redBuildings.Add(build);
                numberRed++;
            }
 
        }

        copyRedBuildings = redBuildings;

        print(numberRed);
        SaveMatrixToFile(redBuildings, numberRed);

        switchAlgorithm = false;

        transform.position = buildings[0].transform.position;

        prevBuilding = -1;
        ChangeBuilding(buildings[nextBuilding]);
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
            ChangeBuilding(buildings[nextBuilding]);
        }

        transform.LookAt(buildings[nextBuilding].transform);

        if(switchAlgorithm)
        {
            NewAlgorithm();
            switchAlgorithm = false;
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

    void SaveMatrixToFile(List<GameObject> buildings, int number)
    {
        string matrix = MakeMatrix(buildings, number);
        string name = "tsp_" + number.ToString() + "_" + Random.Range(0, 1000).ToString();
        string path = Application.dataPath + "/Algorithms/graphs/";
        path += name + ".txt";
        File.WriteAllText(path, matrix);
        print(path);

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
    }

}
