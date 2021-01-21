using Algorithms;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GenerateMatrix : MonoBehaviour
{

    GameObject[] buildings;
    public List<GameObject> redBuildings;
    int numberRed;
    string path;


    // Start is called before the first frame update
    void Start()
    {
        buildings = GameManager.buildings;
        numberRed = 0;

        redBuildings = GameManager.redBuildings;

        foreach (GameObject build in buildings)
        {
            if (build.GetComponent<Renderer>().sharedMaterial.name == "Red")
            {
                //redBuildings.Add(build);
                numberRed++;
            }

        }

        path = SaveMatrixToFile(redBuildings, numberRed);

        Algorithms.Program.graphPath = path;



    }




    string SaveMatrixToFile(List<GameObject> buildings, int number)
    {
        string matrix = MakeMatrix(buildings, number);
        string name = "tsp_" + number.ToString() + "_" + Random.Range(0, 1000).ToString();
        string path = Application.dataPath;
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


        for (int i = 0; i < number; i++)
        {
            for (int j = 0; j < number; j++)
            {
                if (i == j)
                {
                    matrix += "-1 ";
                    continue;
                }

                distance = (int)Vector3.Distance(buildings[i].transform.position, buildings[j].transform.position);

                matrix += distance.ToString() + " ";
            }

            matrix += "\n";
            print(buildings[i].name);
        }



        return matrix;
    }
}
