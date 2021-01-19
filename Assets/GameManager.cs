using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    Renderer rend;
    public static GameObject[] buildings;
    public static List<GameObject> redBuildings;
    public Material materialRed;
    [SerializeField]
    int redNumber;
    int redCount;
    int rand;

    // Start is called before the first frame update
    void Start()
    {
        redCount = 0;
        buildings = GameObject.FindGameObjectsWithTag("Building");
        int numberBuildings = buildings.Length-1;
        redBuildings = new List<GameObject>();

        while(redCount < redNumber)
        {
            rand = Random.Range(0, numberBuildings);

            //print(buildings[rand]);
            if (redBuildings.Contains(buildings[rand]))
                continue;

            rend = buildings[rand].GetComponent<Renderer>();
            rend.sharedMaterial = materialRed;
            redBuildings.Add(buildings[rand]);
            redCount++;

        }

    }

}
