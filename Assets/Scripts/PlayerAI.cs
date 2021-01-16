using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : MonoBehaviour
{

    GameObject[] buildings;
    public int nextBuilding;
    int prevBuilding;
    public float speed;
    Vector3 direction;
    bool isBuildingReached;
 

    // Start is called before the first frame update
    void Start()
    {
        buildings = GameObject.FindGameObjectsWithTag("Building");
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

}
