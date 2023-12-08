using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject TilePrefab;
    public GameObject[,] TileArray;
    public float levelWidth;
    public float levelLength;

    List<Tuple<int, int>> hazardTiles = new List<Tuple<int, int>>();
    // Start is called before the first frame update
    public void Start()
    {
        TileArray = new GameObject[(int)levelWidth, (int)levelLength];
        generateLevel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void generateLevel()
    {
        GameManager myGameManager = FindObjectOfType<GameManager>();
        int level = myGameManager.getLevel();
        if(level == 1){
            levelWidth = 7;
            levelLength = 4;
        }
        else if(level == 2){
            levelWidth = 4;
            levelLength = 12;
            TileArray = new GameObject[(int)levelWidth, (int)levelLength];
            hazardTiles.Add(new Tuple<int, int>(0, 11));
            hazardTiles.Add(new Tuple<int, int>(0, 10));
            hazardTiles.Add(new Tuple<int, int>(0, 9));
            hazardTiles.Add(new Tuple<int, int>(3, 9));
            hazardTiles.Add(new Tuple<int, int>(3, 10));
            hazardTiles.Add(new Tuple<int, int>(3, 11));
        }
        float tileWidth = TilePrefab.GetComponent<Renderer>().bounds.size.x;
        for (int i = 0; i < levelWidth; i++)
        {
            for (int j = 0; j < levelLength; j++)
            {


                GameObject tile = Instantiate(TilePrefab, new Vector3(i * tileWidth, 0, j * tileWidth), Quaternion.identity * Quaternion.Euler(0, 0, -90));
                tile.SetActive(true);


                if (i == levelWidth - 1 && j == levelLength - 1)
                {
                    tile.GetComponent<TileScript>().tileType = TileScript.TileType.Goal;
                    TileArray[i, j] = tile;
                    continue;
                }

                tile.GetComponent<TileScript>().tileType = TileScript.TileType.Walkable;
                if(hazardTiles.Contains(new Tuple<int, int>(i,j))){
                    tile.GetComponent<TileScript>().tileType = TileScript.TileType.Obstacle;
                }
                TileArray[i, j] = tile;
            }
        }


    }



    //these are just to test game over and game won
    public void greenify()
    {
        for (int i = 0; i < levelWidth; i++)
        {
            for (int j = 0; j < levelLength; j++)
            {
                Renderer tileRenderer = TileArray[i, j].GetComponent<Renderer>();
                if (tileRenderer != null)
                {
                    MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
                    tileRenderer.GetPropertyBlock(propBlock);
                    propBlock.SetColor("_Color", Color.green);
                    tileRenderer.SetPropertyBlock(propBlock);
                }


            }
        }
    }

    public void redify()
    {
        for (int i = 0; i < levelWidth; i++)
        {
            for (int j = 0; j < levelLength; j++)
            {
                Renderer tileRenderer = TileArray[i, j].GetComponent<Renderer>();
                if (tileRenderer != null)
                {
                    MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
                    tileRenderer.GetPropertyBlock(propBlock);
                    propBlock.SetColor("_Color", Color.red);
                    tileRenderer.SetPropertyBlock(propBlock);
                }
            }
        }
    }

    public void ResetLevel()
    {
        //destroy all tiles
        for (int i = 0; i < levelWidth; i++)
        {
            for (int j = 0; j < levelLength; j++)
            {
                Destroy(TileArray[i, j]);
            }
        }
        //reset the array   
        TileArray = new GameObject[(int)levelWidth, (int)levelLength];
        //generate a new level
        generateLevel();
    }
}
