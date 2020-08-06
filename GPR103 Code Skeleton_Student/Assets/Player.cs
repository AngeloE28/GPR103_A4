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

    public AudioSource myAudioSource;
    public AudioClip jumpSound;
    public AudioClip deathSound;

    public GameObject deathFxPrefab;

    public bool onPlatform = false;

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
       if(playerCanMove && playerIsAlive)
        {
            if (Input.GetKeyUp(KeyCode.UpArrow) && transform.position.y < myGameManager.levelConstraintTop)
            {
                transform.Translate(new Vector2(0, 1));
                // same thing as the ones in left and right
                myAudioSource.PlayOneShot(jumpSound);
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow) && transform.position.y > myGameManager.levelConstraintBottom)
            {
                transform.Translate(new Vector2(0, -1));
                // same thing as the ones in left and right
                myAudioSource.PlayOneShot(jumpSound);
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow) && transform.position.x > myGameManager.levelConstraintLeft)
            {
                transform.Translate(new Vector2(-1, 0));

                myAudioSource.clip = jumpSound;
                myAudioSource.pitch = Random.Range(0.7f, 1.2f);
                myAudioSource.Play();
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow) && transform.position.x < myGameManager.levelConstraintRight)
            {
                transform.Translate(new Vector2(1, 0));

                myAudioSource.clip = jumpSound;
                myAudioSource.Play();
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
}
