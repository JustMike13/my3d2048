using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    private Vector3 destination;
    [SerializeField]
    private int value;
    private int growSpeed = 3;
    const float speed = 10;
    public bool isLocked;
    [SerializeField]
    TextMeshPro textMeshPro;
    // Start is called before the first frame update
    void Start()
    {
        destination = transform.position;
        transform.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);
        isLocked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (destination != transform.position)
        {
            transform.Translate((destination - transform.position) / speed);
        }

        if (transform.localScale.x < 1)
        {
            float t = Time.deltaTime;
            transform.localScale = transform.localScale + new Vector3(t, t, t) * growSpeed;
        }
        if (transform.localScale.x > 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void SetValue(int v)
    {
        value = v;
        Material material = Resources.Load<Material>(value + "BlockMaterial");
        GetComponent<Renderer>().material = material;
        textMeshPro.text = v.ToString();
        textMeshPro.fontSize = 8;
        if (v > 100)
        {
            textMeshPro.fontSize = 5;
        }
        if (v > 1000)
        {
            textMeshPro.fontSize = 4;
        }
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

    public void Lock()
    {
        isLocked = true;
    }

    public void Unlock()
    {
        isLocked = false;
    }

    public bool IsLocked()
    {
        return isLocked;
    }
}
