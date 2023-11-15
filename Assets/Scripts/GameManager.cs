using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver;
    public bool gameWon;
    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        gameWon = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameWon){
            GameObject.Find("LevelManager").GetComponent<LevelManager>().greenify();
        } else if(gameOver && !gameWon){
            GameObject.Find("LevelManager").GetComponent<LevelManager>().redify();
        }
    }
}
