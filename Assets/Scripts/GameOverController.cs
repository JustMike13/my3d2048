using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    Block[] blocks;
    // Start is called before the first frame update
    void Start()
    {
        blocks = gameObject.GetComponentsInChildren<Block>();
        GenerateRandomBlocks();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void GenerateRandomBlocks()
    {
        foreach(Block block in blocks)
        {
            int r = UnityEngine.Random.Range(0, 12);
            block.SetLevel(r);
        }
    }
}
