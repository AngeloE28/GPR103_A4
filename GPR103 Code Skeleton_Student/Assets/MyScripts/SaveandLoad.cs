using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveandLoad : MonoBehaviour
{
    public GameManager myGameManager;

    public int playerHighScore = 0;
    public int playerCurrentScore = 0;

    public float gameTimer = 5f;
    public bool isGameOver = false;

    public static string PLAYERNAMESAVE = "PlayerName";

    // Start is called before the first frame update
    void Start()
    {
        playerHighScore = PlayerPrefs.GetInt("HighScore");

        print("High Score: " + playerHighScore);
    }

    // Update is called once per frame
    void Update()
    {

        if (gameTimer <= 0 && isGameOver == false)
        {
            isGameOver = true;

            if (playerCurrentScore > playerHighScore)
            {
                playerHighScore = playerCurrentScore;
                PlayerPrefs.SetInt("HighScore", playerHighScore);
            }
        }
    }
}
