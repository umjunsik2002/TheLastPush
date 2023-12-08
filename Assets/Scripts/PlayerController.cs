using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject[,] tiles;
    private Tuple<int, int> playerPos;

    //adjust speed
    public float movementSpeed = 5f;

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
        Vector3 targetPosition = transform.position;
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
        GameObject tile = tiles[playerPos.Item1, playerPos.Item2];
        if (tile.GetComponent<TileScript>().tileType == TileScript.TileType.Hazard)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().gameOver = true;
        }
        else if (tile.GetComponent<TileScript>().tileType == TileScript.TileType.Goal)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().gameOver = true;
            GameObject.Find("GameManager").GetComponent<GameManager>().gameWon = true;
        }
        if (tile.GetComponent<TileScript>().tileType != TileScript.TileType.Obstacle)
        {

           // transform.position = tiles[playerPos.Item1, playerPos.Item2].transform.position;
            targetPosition = tiles[playerPos.Item1, playerPos.Item2].transform.position;
            transform.position = Vector3.Lerp(transform.position, targetPosition, movementSpeed * Time.deltaTime);
        }

    }
    public void movePlayer(Tuple<int, int> dir){
        playerPos = new Tuple<int, int>(playerPos.Item1 + dir.Item1, playerPos.Item2 + dir.Item2);
         SetTiles(GameObject.Find("LevelManager").GetComponent<LevelManager>().TileArray);
        GameObject tile = tiles[playerPos.Item1, playerPos.Item2];
        if (tile.GetComponent<TileScript>().tileType == TileScript.TileType.Hazard)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().gameOver = true;
        }
        else if (tile.GetComponent<TileScript>().tileType == TileScript.TileType.Goal)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().gameOver = true;
            GameObject.Find("GameManager").GetComponent<GameManager>().gameWon = true;
        }
        if (tile.GetComponent<TileScript>().tileType != TileScript.TileType.Obstacle)
        {

           // transform.position = tiles[playerPos.Item1, playerPos.Item2].transform.position;
            Vector3 targetPosition = tiles[playerPos.Item1, playerPos.Item2].transform.position;
            transform.position = Vector3.Lerp(transform.position, targetPosition, movementSpeed * Time.deltaTime);
        }

    }

    public void ResetPlayer()
    {
        playerPos = new Tuple<int, int>(0, 0);
    }
}
