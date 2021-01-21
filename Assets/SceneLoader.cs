using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update

    Scene game;
    Scene loadGame;

    public static bool ready;
    public static int redNumber;


    void Start()
    {
        ready = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(ready)
        {
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
            GameManager.redNumber = redNumber;
        }
    }
}
