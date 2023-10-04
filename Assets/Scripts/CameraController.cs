using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    const int IN_GAME = 0;
    const int IN_MENU = 3;
    Vector3 destination;
    Quaternion rotation;
    float speed;
    Vector3 inGameCameraPosition = new Vector3(3, 5, -490);
    Vector3 inMenuCameraPosition = new Vector3(-2, 5, -495);

    // Start is called before the first frame update
    void Start()
    {
        transform.position = inMenuCameraPosition;
        transform.rotation = Quaternion.Euler(0, 10, 0);
        destination = transform.position;
        rotation = transform.rotation;
        speed = 8;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != destination)
        {
            float dist = Vector3.Distance(transform.position, destination);
            if ( -0.01 < dist && dist < 0.01)
            {
                transform.position = destination;
            }
            else
            {
                transform.position = transform.position + (destination - transform.position) * Time.deltaTime * speed;
            }
            
        }
        if (!(transform.rotation.y == rotation.y))
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * speed * speed);
        }
    }

    public void SetCamera(int mode)
    {
        if (mode == IN_GAME)
        {
            destination = inGameCameraPosition;
            rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (mode == IN_MENU)
        {
            destination = inMenuCameraPosition;
            rotation = Quaternion.Euler(0, 10, 0);
        }
    }
}
