using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject[,] tiles;
    private Tuple<int, int> playerPos;

    public void SetTiles(GameObject[,] tileArray)
    {
        tiles = tileArray;
    }
    void Start()
    {
        playerPos = new Tuple<int, int>(0, 0);
    }

    void Update()
    {
        SetTiles(GameObject.Find("LevelManager").GetComponent<LevelManager>().TileArray);
        if (Input.GetKeyDown(KeyCode.W) && playerPos.Item2 < tiles.GetLength(1) - 1)
        {
            playerPos = new Tuple<int, int>(playerPos.Item1, playerPos.Item2 + 1);
        }
        else if (Input.GetKeyDown(KeyCode.S) && playerPos.Item2 > 0)
        {
            playerPos = new Tuple<int, int>(playerPos.Item1, playerPos.Item2 - 1);
        }
        else if (Input.GetKeyDown(KeyCode.A) && playerPos.Item1 > 0)
        {
            playerPos = new Tuple<int, int>(playerPos.Item1 - 1, playerPos.Item2);
        }
        else if (Input.GetKeyDown(KeyCode.D) && playerPos.Item1 < tiles.GetLength(0) - 1)
        {
            playerPos = new Tuple<int, int>(playerPos.Item1 + 1, playerPos.Item2);
        }

        transform.position = tiles[playerPos.Item1, playerPos.Item2].transform.position;

    }
}
