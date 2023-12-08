using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PlayerInputMovement;
using static EnemyBehavior;
using PlayerNoteDir = PlayerInputMovement.NoteDir;
using EnemyNoteDir = EnemyBehavior.NoteDir;

public class GameManager : MonoBehaviour
{
    public bool gameOver;
    public bool gameWon;

    public List<Tuple<int, int>> enemyTiles = new List<Tuple<int, int>>();

    PlayerNoteDir[] playerMoves;
    EnemyNoteDir[] enemyMoves;
    [SerializeField] private PlayerController player;
    [SerializeField] private PlayerInputMovement playerInput;
    [SerializeField] private EnemyController enemy;
    [SerializeField] private EnemyBehavior enemyBehavior;
    public Button uiButton;
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

        if(Input.GetKeyDown(KeyCode.R)){

            GameObject.Find("Player").GetComponent<PlayerController>().ResetPlayer();
            GameObject.Find("LevelManager").GetComponent<LevelManager>().ResetLevel();
            gameOver = false;
            gameWon = false;
        }
        
    }
    void move(PlayerNoteDir move, EnemyNoteDir enemyMove){
        
        player.movePlayer(move.dir);
        StartCoroutine(enemyBehavior.RotateEnemySmoothly(new Vector3(enemyMove.dir.Item1, 0, enemyMove.dir.Item2)));
        enemy.moveEnemy(enemyMove.dir);
        //check collision
        //if collision, game over
        CheckCollisionWithEnemy();
        
    }
    void CheckCollisionWithEnemy()
    {
        
        
        // Check if the player is on an enemy tile or within its vision tile
        Tuple<int,int> playerpos = player.getPlayerPos();

        enemyTiles = enemy.GetEnemyTiles();
        
        foreach (var enemyTile in enemyTiles)
        {
            
            if (playerpos.Equals(enemyTile))
            {
                Debug.Log("detected");
                // Pause or reset the game as needed
                gameOver=true;
                gameWon=false;

                return; // Exit the loop if collision detected
            }
        }
    }

   
    IEnumerator CustomUpdate(){
        
        int idx = 0;
        int enemyIdx = 0;
        Debug.Log("playerMoves.Length: " + playerMoves.Length);
        //TODO: && !gameOver
        while(idx < playerMoves.Length && idx < enemyMoves.Length){
            
            if(playerMoves[idx].note == 0){
                uiButton.gameObject.SetActive(true);
                yield break;
            } 
            
            for(int i = 0; i< playerMoves[idx].note; i++){
                move(playerMoves[idx], enemyMoves[enemyIdx]);
                enemyIdx++;
                if(enemyIdx >= enemyMoves.Length) enemyIdx = 0;
                yield return new WaitForSeconds(0.5f);
            }
            uiButton.gameObject.SetActive(false);
            idx++;

        }
        
        
    }
    public void OnButtonClick()
    {
        uiButton.gameObject.SetActive(false);
        Debug.Log("enemytiles added");
        playerMoves = playerInput.GetNoteDirectionPairs(); //get the player's move queue
        enemyMoves = GameObject.Find("Enemy").GetComponent<EnemyBehavior>().GetNoteDirectionPairs();
        // GameObject[] EnemyList = GameObject.FindGameObjectsWithTag("Enemy");
        // foreach (var enemy in EnemyList){
                       
        // }
        
        StartCoroutine(CustomUpdate());
        
        
        
        
    }
}
