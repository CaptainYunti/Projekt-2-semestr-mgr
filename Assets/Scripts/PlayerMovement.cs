using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    [SerializeField]
    float speed; // szyblość poruszanie
    [SerializeField]
    float rotationSpeed; //szybkość obrotu
    List<GameObject> redBuildings;
    [SerializeField]
    Material green;

    Vector3 direction;
    Vector3 rotation;

    // Start is called before the first frame update
    void Start()
    {
        redBuildings = GameManager.redBuildings;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        direction = Vector3.zero;


        if(Input.GetKey("w"))
        {
            direction += transform.forward;
        }

        if(Input.GetKey("s"))
        {
            direction -= transform.forward;
        }

        if(Input.GetKey("a"))
        {
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        }

        if (Input.GetKey("d"))
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }


        transform.localPosition += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(redBuildings.Contains(other.gameObject))
        {
            other.gameObject.GetComponent<Renderer>().sharedMaterial = green;
        }
    }
}
