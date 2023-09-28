using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private Vector3 destination;
    const float speed = 100;
    // Start is called before the first frame update
    void Start()
    {
        destination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (destination != transform.position)
        {
            transform.Translate((destination - transform.position) / speed);
        }
    }

    public void SetDestination(Vector3 dest)
    {
        destination = dest;
    }
}
