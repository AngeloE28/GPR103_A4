using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script is to be attached to a GameObject called GameManager in the scene. It is to be used to manager the settings and overarching gameplay loop.
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("Scoring")]
    public int currentScore = 0; //The current score in this round.
    public int highScore = 0; //The highest score achieved either in this session or over the lifetime of the game.

    [Header("Playable Area")]
    public float levelConstraintTop; //The maximum positive Y value of the playable space.
    public float levelConstraintBottom; //The maximum negative Y value of the playable space.
    public float levelConstraintLeft; //The maximum negative X value of the playable space.
    public float levelConstraintRight; //The maximum positive X value of the playablle space.

    [Header("Gameplay Loop")]
    public bool isGameRunning; //Is the gameplay part of the game current active?
    public float totalGameTime; //The maximum amount of time or the total time avilable to the player.
    public float gameTimeRemaining; //The current elapsed time


    [Header("UI")]
    public GameObject uiGameOverWindow;
    public TMP_Text uiGameOverMsg;
    public TMP_Text uiScore;
    public TMP_Text uiTime;
    public TMP_Text uiHighScore;

    // Start is called before the first frame update
    void Start()
    {
        gameTimeRemaining = totalGameTime;
        isGameRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameTimeRemaining > 0)
        {
            gameTimeRemaining -= Time.deltaTime;
        }
        else
        {
            isGameRunning = false;
        }
        // Checks to see if a new highscore is made
        if(currentScore > highScore)
        {
            highScore = currentScore;
        }
    }

    public void Restart()
    {
        print("restart the scene poofy");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // ^^ is the same as the bottom one due to index being 0 we know this
        //SceneManager.LoadScene(0);
    }

    // Updates the score of the player
    public void updateScore(int amount)
    {
        currentScore += amount;
        uiScore.text = "Score: " + currentScore.ToString();
    }

    public void showHighScore()
    {
        uiHighScore.text = "High Score: " + highScore.ToString();
    }

    // Shows the time available
    public void updateTime()
    {
        uiTime.text = "Time left: " + Mathf.Round(gameTimeRemaining).ToString();
    }

    // Control to end the game
    public void GameOver(bool isWin)
    {
        if (isWin == true)
        {
            uiGameOverMsg.text = "Winner is u";
        }
        else
        {
            uiGameOverMsg.text = "loser is u";
        }
        uiGameOverWindow.SetActive(true);
    }
}
