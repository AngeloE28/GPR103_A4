using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;

/// <summary>
/// This script must be used as the core Player script for managing the player character in the game.
/// </summary>
public class Player : MonoBehaviour
{
    public string playerName = ""; //The players name for the purpose of storing the high score
   
    public int playerTotalLives; //Players total possible lives.
    public int playerLivesRemaining; //PLayers actual lives remaining.
   
    public bool playerIsAlive = true; //Is the player currently alive?
    public bool playerCanMove = false; //Can the player currently move?

    public bool isPlayerMoving = false;

    public AudioSource myAudioSource;
    public AudioClip jumpSound;
    public AudioClip deathSound;

    public GameObject deathFxPrefab;

    public bool onPlatform = false;

    public KeyCode up = KeyCode.W;
    public KeyCode down = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;

    private GameManager myGameManager; //A reference to the GameManager in the scene.

    // Start is called before the first frame update
    void Start()
    {
        myGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myGameManager.isGameRunning == true)
        {
            if (playerCanMove && playerIsAlive)
            {
                Move();

                if (isPlayerMoving)
                {
                    myAudioSource.PlayOneShot(jumpSound);

                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerIsAlive == true)
        {
            if (collision.transform.GetComponent<Vehicle>() != null)
            {
                print("Hit");
                playerIsAlive = false;
                playerCanMove = false;
                myAudioSource.PlayOneShot(deathSound);
                Instantiate(deathFxPrefab, transform.position, Quaternion.identity);
                GetComponent<SpriteRenderer>().enabled = false;
            }
            else if(collision.transform.GetComponent<Platform>()!= null)
            {
                print("ran into log");
                transform.SetParent(collision.transform);
                onPlatform = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    // Moves the player and rotates the sprite according to the direction player is moving
    // Also determines if player is moving or not to play the jumpSound for myAudioSource
    void Move()
    {
        if (Input.GetKeyUp(up) && transform.position.y < myGameManager.levelConstraintTop)
        {
            transform.position = transform.position + new Vector3(0, 1, 0);

            GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 0);

            isPlayerMoving = true;
        }
        else if (Input.GetKeyUp(down) && transform.position.y > myGameManager.levelConstraintBottom)
        {
            transform.position = transform.position + new Vector3(0, -1, 0);

            GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 180);

            isPlayerMoving = true;
        }
        else if (Input.GetKeyUp(left) && transform.position.x > myGameManager.levelConstraintLeft)
        {
            transform.position = transform.position + new Vector3(-1, 0, 0);

            GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 90);

            isPlayerMoving = true;
        }
        else if (Input.GetKeyUp(right) && transform.position.x < myGameManager.levelConstraintRight)
        {
            transform.position = transform.position + new Vector3(1, 0, 0);

            GetComponent<Transform>().eulerAngles = new Vector3(0, 0, -90);

            isPlayerMoving = true;
        }
        else
        {
            isPlayerMoving = false;
        }

    }
}
