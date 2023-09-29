using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    GameObject BlockObject;
    const float zPos = -480;
    const int matrixSize = 4;
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
        CreateBlock(2, 2);
        CreateBlock(2, 3);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    MoveBlock(test3, test1, test3, test2);
        //    int aux = test1;
        //    test1 = test2;
        //    test2 = aux;
        //}
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
            GenerateNewBlock();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
            GenerateNewBlock();
        }
    }

    void GenerateNewBlock()
    {
        int[] availablePositions = new int[16];
        int x = 0;
        for(int i = 0; i < 16; i++)
        {
            if (Blocks[i] == null)
            {
                availablePositions[x] = i;
                x++;
            }
        }
        if(x != 0)
        {
            int test1 = Random.Range(0, x);
            Vector2 position = MatrixPosition(availablePositions[test1]);
            CreateBlock((int)position[0], (int)position[1]);
        }
    }

    void MoveLeft()
    {
        for(int i = 0; i < matrixSize; i++)
        {
            for (int j = 1; j < matrixSize; j++)
            {
                if (Blocks[MatrixPosition(i, j)] != null)
                {
                    int k = j - 1;
                    while (Blocks[MatrixPosition(i, k)] == null && k != -1)
                    {
                        k--;
                        if(k == -1)
                        {
                            break;
                        }
                    }
                    if (k == -1)
                    {
                         MoveBlock(i, j, i, 0);
                    }
                    else if (Blocks[MatrixPosition(i, j)].GetComponent<Block>().GetValue() == Blocks[MatrixPosition(i, k)].GetComponent<Block>().GetValue())
                    {
                        Destroy(Blocks[MatrixPosition(i, k)]);
                        Blocks[MatrixPosition(i, k)] = null;
                        MoveBlock(i, j, i, k);
                        Blocks[MatrixPosition(i, k)].GetComponent<Block>().IncreaseValue();
                    }
                    else
                    {
                        MoveBlock(i, j, i, k + 1);
                    }
                }
            }
        }
    }

    void MoveRight()
    {
        for (int i = 0; i < matrixSize; i++)
        {
            for (int j = matrixSize - 2; j >= 0 ; j--)
            {
                if (Blocks[MatrixPosition(i, j)] != null)
                {
                    int k = j + 1;
                    while (Blocks[MatrixPosition(i, k)] == null)
                    {
                        k++;
                        if (k == 4)
                        {
                            break;
                        }
                    }
                    if (k == 4)
                    {
                        MoveBlock(i, j, i, 3);
                    }
                    else if (Blocks[MatrixPosition(i, j)].GetComponent<Block>().GetValue() == Blocks[MatrixPosition(i, k)].GetComponent<Block>().GetValue())
                    {
                        Destroy(Blocks[MatrixPosition(i, k)]);
                        Blocks[MatrixPosition(i, k)] = null;
                        MoveBlock(i, j, i, k);
                        Blocks[MatrixPosition(i, k)].GetComponent<Block>().IncreaseValue();
                    }
                    else
                    {
                        MoveBlock(i, j, i, k - 1);
                    }
                }
            }
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

        int value = Random.Range(0, 100) < 50 ? 2 : 4;

        Blocks[positionIndex] = Instantiate(BlockObject, Positions[positionIndex], Quaternion.identity);
        Blocks[positionIndex].GetComponent<Block>().SetValue(value);
        return true;
    }

    bool MoveBlock(int i, int j, int x, int y)
    {
        if (i == x && j == y)
        {
            return true;
        }
        int fromPosition = MatrixPosition(i, j);
        int toPosition   = MatrixPosition(x, y);
        if (Blocks[fromPosition] == null)
        {
            Debug.Log("No block to move from position " + fromPosition);
            return false;
        }
        if (Blocks[toPosition] != null)
        {
            Debug.Log("Can not move block to occupied position " + toPosition);
            return false;
        }

        Blocks[toPosition] = Blocks[fromPosition];
        Blocks[fromPosition] = null;

        Blocks[toPosition].GetComponent<Block>().SetDestination(Positions[toPosition]);

        return true;
    }

    Vector2 MatrixPosition(int i)
    {
        return new Vector2(i / 4, i % 4);
    }

    int MatrixPosition(int i, int j)
    {
        return (i * 4 + j);
    }
}
