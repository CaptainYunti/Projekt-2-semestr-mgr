using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBuildingColor : MonoBehaviour
{
    //[SerializeField]
    //int probabilityRed;

    public Material[] material;
    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        //int rand = Random.Range(0, probabilityRed);

        rend = GetComponent<Renderer>();
        rend.enabled = true;

        rend.sharedMaterial = material[0];

        /*if(rand != 0)
        {
            rend.sharedMaterial = material[0];
            
        }
        else
        {
            rend.sharedMaterial = material[1];
        }*/

    }

    void OnTriggerEnter(Collider other)
    {/*
        if (rend.sharedMaterial == material[1])
        {
            rend.sharedMaterial = material[2];
        }*/
    }

    public void SetRed()
    {
        rend.sharedMaterial = material[1];
    }

}
