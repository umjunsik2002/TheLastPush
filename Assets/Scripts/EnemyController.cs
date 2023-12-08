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

    EnemyBehavior enemyBehavior;

    public List<Tuple<int, int>> enemyTiles = new List<Tuple<int, int>>();

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
            targetPosition = tiles[enemyPos.Item1, enemyPos.Item2].transform.position + new Vector3(2.5f,2.5f,-2.5f);
            transform.position = Vector3.Lerp(transform.position, targetPosition, movementSpeed * Time.deltaTime);
        }
        

    }
    public void moveEnemy(Tuple<int, int> dir){
        enemyPos = new Tuple<int, int>(enemyPos.Item1 + dir.Item1, enemyPos.Item2 + dir.Item2);
        SetTiles(GameObject.Find("LevelManager").GetComponent<LevelManager>().TileArray);
        GameObject tile = tiles[enemyPos.Item1, enemyPos.Item2];
        
        if (tile.GetComponent<TileScript>().tileType != TileScript.TileType.Obstacle)
        {

           // transform.position = tiles[playerPos.Item1, playerPos.Item2].transform.position;
            Vector3 targetPosition = tiles[enemyPos.Item1, enemyPos.Item2].transform.position + new Vector3(-3,-3,0);
            transform.position = Vector3.Lerp(transform.position, targetPosition, movementSpeed * Time.deltaTime);
        }

    }

    public void ResetPlayer()
    {
        enemyPos = new Tuple<int, int>(5, 5);
    }
    public List<Tuple<int, int>> GetEnemyTiles()
    {

        enemyTiles.Clear(); // Clear the list before populating it again

        enemyTiles.Add(enemyPos);


        return enemyTiles;
        }
}

