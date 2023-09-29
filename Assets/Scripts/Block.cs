using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private Vector3 destination;
    private int value;
    const float speed = 100;
    [SerializeField]
    GameObject[] Materials = new GameObject[12];
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

    public void SetValue(int v)
    {
        value = v;
        Material material = Resources.Load<Material>(value + "BlockMaterial");
        GetComponent<Renderer>().material = material;
    }

    public int GetValue()
    {
        return value;
    }

    public void SetDestination(Vector3 dest)
    {
        destination = dest;
    }

    public void IncreaseValue()
    {
        SetValue(2 * value);
    }
}
