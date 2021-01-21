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
    public static int redNumber;
    int redCount;
    int rand;
    [SerializeField]
    bool turnOnAI;
    GameObject player;
    GameObject playerAI;
    Camera cameraSky;
    Camera cameraPlayer;
    Camera cameraMinimap;
    Camera[] cameras;
    GameObject miniMap;
    Camera cameraHuman;
    public static bool humanActive;
    public static bool playerReady;

    // Start is called before the first frame update
    void Start()
    {
        playerReady = false;
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
            //print(buildings[rand]);
            redCount++;

        }

        cameras = Camera.allCameras;

        foreach (Camera camera in cameras)
        {
            if (camera.name == "Camera Player AI" && !humanActive)
                cameraPlayer = camera;
            if (camera.name == "Camera Player" && humanActive)
                cameraPlayer = camera;
            if (camera.name == "Camera Sky")
                cameraSky = camera;
            if (camera.name == "Camera Minimap")
                cameraMinimap = camera;
        }

        foreach (Camera camera in cameras)
        {
            camera.enabled = false;
        }
        cameraSky.enabled = true;

        miniMap = GameObject.FindGameObjectWithTag("Minimap");

        //redBuildings.Sort();

    }

    private void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            foreach (Camera camera in cameras)
            {
                camera.enabled = false;
            }
            miniMap.SetActive(true);
            cameraPlayer.enabled = true;
            cameraMinimap.enabled = true;
        }

        if (Input.GetKeyDown("2"))
        {
            foreach (Camera camera in cameras)
            {
                camera.enabled = false;
            }
            cameraSky.enabled = true;
            miniMap.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

}
