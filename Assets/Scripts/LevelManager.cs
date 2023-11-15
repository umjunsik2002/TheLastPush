using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject TilePrefab;
    public GameObject[,] TileArray;
    public float levelWidth;
    public float levelLength;
    // Start is called before the first frame update
    void Start()
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
        float tileWidth = TilePrefab.GetComponent<Renderer>().bounds.size.x;
        for (int i = 0; i < levelWidth; i++)
        {
            for (int j = 0; j < levelLength; j++)
            {

                GameObject tile = Instantiate(TilePrefab, new Vector3(i * tileWidth, 0, j * tileWidth), Quaternion.identity);
                tile.SetActive(true); 
                if (i == 0 && j == 0)
                {
                    tile.GetComponent<TileScript>().tileType = TileScript.TileType.Walkable;
                    TileArray[i, j] = tile;
                    continue;
                }
                if (i == levelWidth - 1 && j == levelLength - 1)
                {
                    tile.GetComponent<TileScript>().tileType = TileScript.TileType.Goal;
                    TileArray[i, j] = tile;
                    continue;
                }
                //this generates a random tile type
                // Array values = Enum.GetValues(typeof(TileScript.TileType));
                // System.Random random = new System.Random();
                // tile.GetComponent<TileScript>().tileType = (TileScript.TileType)values.GetValue(random.Next(values.Length));

                float random = UnityEngine.Random.Range(0f, 1f);
                if (random < 0.1f)
                {
                    tile.GetComponent<TileScript>().tileType = TileScript.TileType.Hazard;
                }
                else
                {
                    tile.GetComponent<TileScript>().tileType = TileScript.TileType.Walkable;
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
}
