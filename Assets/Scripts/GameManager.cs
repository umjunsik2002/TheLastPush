using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerInputMovement;
using static EnemyBehavior;
using PlayerNoteDir = PlayerInputMovement.NoteDir;
using EnemyNoteDir = EnemyBehavior.NoteDir;

public class GameManager : MonoBehaviour
{
    public bool gameOver;
    public bool gameWon;

    PlayerNoteDir[] playerMoves;
    EnemyNoteDir[] enemyMoves;
    [SerializeField] private PlayerController player;
    [SerializeField] private PlayerInputMovement playerInput;
    [SerializeField] private EnemyController enemy;
    [SerializeField] private EnemyBehavior enemyBehavior;
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
        enemy.moveEnemy(enemyMove.dir);
    }
    IEnumerator CustomUpdate(){
        int idx = 0;
        int enemyIdx = 0;
        Debug.Log("playerMoves.Length: " + playerMoves.Length);
        while(idx < playerMoves.Length && idx < enemyMoves.Length){
            if(playerMoves[idx].note == 0) yield break;

            for(int i = 0; i< playerMoves[idx].note; i++){
                move(playerMoves[idx], enemyMoves[enemyIdx]);
                enemyIdx++;
                if(enemyIdx >= enemyMoves.Length) enemyIdx = 0;
                yield return new WaitForSeconds(0.5f);
            }
            idx++;

        }
    }
    public void OnButtonClick()
    {
        playerMoves = playerInput.GetNoteDirectionPairs(); //get the player's move queue
        enemyMoves = GameObject.Find("Enemy").GetComponent<EnemyBehavior>().GetNoteDirectionPairs();

        StartCoroutine(CustomUpdate());

    }
}
