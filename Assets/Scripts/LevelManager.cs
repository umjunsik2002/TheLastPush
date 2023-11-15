using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject TilePrefab;
    public GameObject [,] TileArray;
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

    void generateLevel(){
        float tileWidth = TilePrefab.GetComponent<Renderer>().bounds.size.x;
        for (int i = 0; i < levelWidth; i++){
            for (int j = 0; j < levelLength; j++){
                GameObject tile = Instantiate(TilePrefab, new Vector3(i*tileWidth, 0, j*tileWidth), Quaternion.identity);
                // tile.transform.parent = this.transform;
                TileArray[i,j] = tile;
            }
        }
    }
}
