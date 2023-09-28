using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    GameObject BlockObject;
    const float zPos = -480;
    int test1, test2, test3;
    Vector3[] Positions =
    {
        new Vector3(0, 8, zPos),
        new Vector3(2, 8, zPos),
        new Vector3(4, 8, zPos),
        new Vector3(6, 8, zPos),
        new Vector3(0, 6, zPos),
        new Vector3(2, 6, zPos),
        new Vector3(4, 6, zPos),
        new Vector3(6, 6, zPos),
        new Vector3(0, 4, zPos),
        new Vector3(2, 4, zPos),
        new Vector3(4, 4, zPos),
        new Vector3(6, 4, zPos),
        new Vector3(0, 2, zPos),
        new Vector3(2, 2, zPos),
        new Vector3(4, 2, zPos),
        new Vector3(6, 2, zPos),
    };
    GameObject[] Blocks = new GameObject[16];
    // Start is called before the first frame update
    void Start()
    {
        test1 = 1;
        test2 = 2;
        test3 = 3;
        CreateBlock(test3, test1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            MoveBlock(test3, test1, test3, test2);
            int aux = test1;
            test1 = test2;
            test2 = aux;
        }
    }

    bool CreateBlock(int i, int j)
    {
        int positionIndex = MatrixPosition(i, j);
        if (Blocks[positionIndex] != null)
        {
            Debug.Log("Failed to create block on position " + positionIndex);
            return false;
        }

        Blocks[positionIndex] = Instantiate(BlockObject, Positions[positionIndex], Quaternion.identity);
        return true;
    }

    bool MoveBlock(int i, int j, int x, int y)
    {
        int fromPosition = MatrixPosition(i, j);
        int toPosition   = MatrixPosition(x, y);
        if (Blocks[fromPosition] == null)
        {
            Debug.Log("No block to move from position " + fromPosition);
            return false;
        }
        if (Blocks[toPosition] != null)
        {
            Debug.Log("Can not ove block to occupied position " + toPosition);
            return false;
        }

        Blocks[toPosition] = Blocks[fromPosition];
        Blocks[fromPosition] = null;

        Blocks[toPosition].GetComponent<Block>().SetDestination(Positions[toPosition]);

        return true;
    }

    Vector2 MatrixPosition(int i)
    {
        return new Vector2(i / 4, i & 4);
    }

    int MatrixPosition(int i, int j)
    {
        return (i * 4 + j);
    }
}
