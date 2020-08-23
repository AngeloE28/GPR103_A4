using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This script is to be attached to a GameObject called GameManager in the scene. It is to be used to manager the settings and overarching gameplay loop.
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("Scoring")]
    public int currentScore = 0; //The current score in this round.
    public int highScore = 0; //The highest score achieved either in this session or over the lifetime of the game.
    public int whichScene = 0;  // Which scene is being played

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
    public GameObject uiGameOverWindow; // Displays the Game over window with the uiGameOverMsg
    public TMP_Text uiGameOverMsg;  // Displays the game over message depending on the outcome of game win/lose
    public TMP_Text uiScore;    // Displays the current score
    public TMP_Text uiHighScore;    // Displays the high score


    // Start is called before the first frame update
    void Start()
    {
        gameTimeRemaining = totalGameTime;  // Sets remaining time to total time
        isGameRunning = true;   // Game is running 
        showHighScore(); // Displays the high score
    }

    // Update is called once per frame
    void Update()
    {
        // Starts counting down the time remaining
        if (gameTimeRemaining > 0)
        {
            gameTimeRemaining -= Time.deltaTime;
        }
        else
        {   // Ends the game if time remaining reaches 0
            isGameRunning = false;
        }
        // Checks to see if a new highscore is made depending on the scene
        if(currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("High Score" + whichScene, highScore);
        }
    }

    // Restarts the game
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Goes back to the main menu
    public void Quit()
    {
        SceneManager.LoadScene(0);  // Man menu index is 0 in the build settings
    }

    // Updates the score of the player
    public void updateScore(int amount)
    {
        currentScore += amount;
        uiScore.text = "Score: " + currentScore.ToString();
    }

    // Updates the HighScore of the player
    public void showHighScore()
    {
        uiHighScore.text = "High Score: " + PlayerPrefs.GetInt("High Score" + whichScene).ToString();
    }

    // Control to end the game
    public void GameOver(bool isWin)
    {
        if (isWin == true)
        {
            uiGameOverMsg.text = "YOU WIN PANDA THANKS YOU";
        }
        else
        {
            uiGameOverMsg.text = "YOU KILLED PANDA";
        }
        uiGameOverWindow.SetActive(true);
    }
}
