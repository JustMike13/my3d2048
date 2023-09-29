using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    GameObject BlockObject;
    const float zPos = -480;
    const int matrixSize = 4;
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
        GenerateNewBlock();
        GenerateNewBlock();
        GenerateNewBlock();
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
            AfterMove();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
            AfterMove();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveUp();
            AfterMove();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDown();
            AfterMove();
        }
    }

    void AfterMove()
    {
        GenerateNewBlock();
        UnlockBlocks();
    }

    private void UnlockBlocks()
    {
        foreach (GameObject block in Blocks)
        {
            if (block == null)
            {
                continue;
            }
            block.GetComponent<Block>().Unlock();
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
        else
        {
            IsGameOver();
        }
    }

    private void IsGameOver()
    {
        bool gameOver = true;
        for (int i = 0; i < 16; i++)
        {
            int x = (int)MatrixPosition(i)[0], y = (int)MatrixPosition(i)[1];
            if (x > 0)
            {
                if (Blocks[i].GetComponent<Block>().GetValue() == Blocks[MatrixPosition(x - 1, y)].GetComponent<Block>().GetValue())
                {
                    gameOver = false;
                    break;
                }
            }
            if (x < matrixSize - 1)
            {
                if (Blocks[i].GetComponent<Block>().GetValue() == Blocks[MatrixPosition(x + 1, y)].GetComponent<Block>().GetValue())
                {
                    gameOver = false;
                    break;
                }
            }
            if (y > 0)
            {
                if (Blocks[i].GetComponent<Block>().GetValue() == Blocks[MatrixPosition(x, y - 1)].GetComponent<Block>().GetValue())
                {
                    gameOver = false;
                    break;
                }
            }
            if (y < matrixSize - 1)
            {
                if (Blocks[i].GetComponent<Block>().GetValue() == Blocks[MatrixPosition(x, y + 1)].GetComponent<Block>().GetValue())
                {
                    gameOver = false;
                    break;
                }
            }
        }

        if (gameOver)
        {
            Debug.LogError("Game Over");
        }
    }

    private void MergeBlocks(int i, int j, int x, int y)
    {
        Destroy(Blocks[MatrixPosition(x, y)]);
        Blocks[MatrixPosition(x, y)] = null;
        MoveBlock(i, j, x, y);
        Blocks[MatrixPosition(x, y)].GetComponent<Block>().IncreaseValue();
        Blocks[MatrixPosition(x, y)].GetComponent<Block>().Lock();
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
                    else if (Blocks[MatrixPosition(i, j)].GetComponent<Block>().GetValue() == Blocks[MatrixPosition(i, k)].GetComponent<Block>().GetValue()
                        && !Blocks[MatrixPosition(i, k)].GetComponent<Block>().IsLocked())
                    {
                        MergeBlocks(i, j, i, k);
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
                    else if (Blocks[MatrixPosition(i, j)].GetComponent<Block>().GetValue() == Blocks[MatrixPosition(i, k)].GetComponent<Block>().GetValue()
                        && !Blocks[MatrixPosition(i, k)].GetComponent<Block>().IsLocked())
                    {
                        MergeBlocks(i, j, i, k);
                    }
                    else
                    {
                        MoveBlock(i, j, i, k - 1);
                    }
                }
            }
        }
    }

    void MoveUp()
    {
        for (int i = 0; i < matrixSize; i++)
        {
            for (int j = 1; j < matrixSize; j++)
            {
                if (Blocks[MatrixPosition(j, i)] != null)
                {
                    int k = j - 1;
                    while (Blocks[MatrixPosition(k, i)] == null && k != -1)
                    {
                        k--;
                        if (k == -1)
                        {
                            break;
                        }
                    }
                    if (k == -1)
                    {
                        MoveBlock(j, i, 0, i);
                    }
                    else if (Blocks[MatrixPosition(j, i)].GetComponent<Block>().GetValue() == Blocks[MatrixPosition(k, i)].GetComponent<Block>().GetValue()
                        && !Blocks[MatrixPosition(k, i)].GetComponent<Block>().IsLocked())
                    {
                        MergeBlocks(j, i, k, i);
                    }
                    else
                    {
                        MoveBlock(j, i, k + 1, i);
                    }
                }
            }
        }
    }

    void MoveDown()
    {
        for (int i = 0; i < matrixSize; i++)
        {
            for (int j = matrixSize - 2; j >= 0; j--)
            {
                if (Blocks[MatrixPosition(j, i)] != null)
                {
                    int k = j + 1;
                    while (Blocks[MatrixPosition(k, i)] == null)
                    {
                        k++;
                        if (k == 4)
                        {
                            break;
                        }
                    }
                    if (k == 4)
                    {
                        MoveBlock(j, i, 3, i);
                    }
                    else if (Blocks[MatrixPosition(j, i)].GetComponent<Block>().GetValue() == Blocks[MatrixPosition(k, i)].GetComponent<Block>().GetValue()
                        && !Blocks[MatrixPosition(k, i)].GetComponent<Block>().IsLocked())
                    {
                        MergeBlocks(j, i, k, i);
                    }
                    else
                    {
                        MoveBlock(j, i, k - 1, i);
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
