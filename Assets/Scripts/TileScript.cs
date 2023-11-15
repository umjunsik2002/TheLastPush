using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[InitializeOnLoad]
public class TileScript : MonoBehaviour
{
    public enum TileType
    {
        Empty,
        Walkable,
        Obstacle,
        Hazard,
        Goal
        
    }
 
    public TileType tileType;
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
         if (tileType == TileType.Hazard)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        //if the tile is a goal, set the color to green
        else if (tileType == TileType.Goal)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else if(tileType == TileType.Obstacle){
            GetComponent<Renderer>().material.color = Color.gray;
        }
        else if (tileType == TileType.Walkable)
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
        else if (tileType == TileType.Empty)
        {
            GetComponent<Renderer>().material.color = Color.black;
        }
        //if the tile is a hazard, set the color to red
    }
}
