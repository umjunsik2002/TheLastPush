using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject[,] tiles;
    private Tuple<int, int> enemyPos;
    public Tuple<int,int> initialpos = new Tuple<int, int>(5,5);

    //adjust speed
    public float movementSpeed = 5f;

    public void SetTiles(GameObject[,] tileArray)
    {
        tiles = tileArray;
    }
    void Start()
    {
        
        enemyPos = initialpos;
    }

    void Update()
    {
        Vector3 targetPosition = transform.position;
        SetTiles(GameObject.Find("LevelManager").GetComponent<LevelManager>().TileArray);
        GameObject tile = tiles[enemyPos.Item1, enemyPos.Item2];
        if (tile.GetComponent<TileScript>().tileType != TileScript.TileType.Obstacle)
        {

           // transform.position = tiles[playerPos.Item1, playerPos.Item2].transform.position;
            targetPosition = tiles[enemyPos.Item1, enemyPos.Item2].transform.position;
            transform.position = Vector3.Lerp(transform.position, targetPosition, movementSpeed * Time.deltaTime);
        }
        

    }
    public void moveEnemy(Tuple<int, int> dir){
        enemyPos = new Tuple<int, int>(enemyPos.Item1 + dir.Item1, enemyPos.Item2 + dir.Item2);
         SetTiles(GameObject.Find("LevelManager").GetComponent<LevelManager>().TileArray);
        GameObject tile = tiles[enemyPos.Item1, enemyPos.Item2];
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
            Vector3 targetPosition = tiles[enemyPos.Item1, enemyPos.Item2].transform.position;
            transform.position = Vector3.Lerp(transform.position, targetPosition, movementSpeed * Time.deltaTime);
        }

    }

    public void ResetPlayer()
    {
        enemyPos = new Tuple<int, int>(5, 5);
    }
}

