using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;

/// <summary>
/// This script must be used as the core Player script for managing the player character in the game.
/// </summary>
public class Player : MonoBehaviour
{
    public string playerName = ""; //The players name for the purpose of storing the high score

    private int playerTotalLives = 3; //Players total possible lives.
    public int playerLivesRemaining; //PLayers actual lives remaining.

    public bool playerIsAlive = true; //Is the player currently alive?
    public bool playerCanMove = false; //Can the player currently move?

    public bool isPlayerMoving = false; // Is player moving?

    // Booleans to control the water section of the game
    public bool inRiver = false;    // Is player in the river?
    public bool onPlatform = false; // Is player on the platform?
    public bool inEndZone = false;
    public bool safeInEndZone = false;
    public bool inKillZone = false;

    // Gets the initial position used for respawning player
    private Vector2 initialPosition;

    public AudioSource myAudioSource; // Reference to play the different sounds
    public AudioClip jumpSound;     // Jump sound
    public AudioClip deathSound;    // Sound for dying on the road
    public AudioClip drownSound;    // Sound for drowning

    public Animator myAnimator; // Plays idle animation

    // Prefabs to play death animations
    public GameObject roadkillPrefab;
    public GameObject drownPrefab;

    private float deathCooldown = 0; // Creates a cooldown to allow animation to play
    private int bonusPoints; // Bonus points
    private int endZone = 0; // Counts how many endzones is reached

    // To easily change the the keycodes in inspector and the code
    public KeyCode up = KeyCode.W;
    public KeyCode down = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;


    private GameManager myGameManager; //A reference to the GameManager in the scene.
    public GameObject life1;    // Reference for the first sprite representing amount of life left
    public GameObject life2;    // Reference for the second sprite representing amount of life left
    public GameObject pointSystem;  // Reference to empty game object parenting for the many boxcolliders for the forward point system

    // Start is called before the first frame update
    void Start()
    {
        playerCanMove = true;   // Sets player can move at the start of the game
        playerLivesRemaining = playerTotalLives;    // Sets remaining lives to total lives
        initialPosition = transform.position;   // Sets the position of the player to initial position
        myGameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); // Gets the GameManager script
        myAudioSource = GetComponent<AudioSource>();    // Gets its audiosource to play different sounds
    }

    // Update is called once per frame
    void Update()
    {
        myGameManager.updateTime(); // Shows the time and updates it

        // Checks to see if game is running
        if (myGameManager.isGameRunning == true)
        {
            Play(); // Plays game
        }
        else
        {   // Game is not running and player has lost the game
            myGameManager.GameOver(false);
        }
    }

    void LateUpdate()
    {
        // Check if player is alive
        if (playerLivesRemaining > 0)
        {
            // Checks for water death
            if (inRiver == true && onPlatform == false)
            {
                KillPanda(drownPrefab, drownSound); // Kills player
            }
            // Checks for killzone death
            if (inKillZone == true && inEndZone == true)
            {
                KillPanda(drownPrefab, drownSound); // Kills player
            }
            // Checks if killzone isdeactivated
            else if(inKillZone == false && inEndZone == true)
            {
                safeInEndZone = true;   // Endzone is safe
            }
            if (playerIsAlive == false && playerLivesRemaining > 0)
            {
                transform.position = initialPosition;   // Resets the players position
                playerLivesRemaining -= 1;  // Takes away one player life
                                            // Resets the booleans so player can move and is alive
                playerCanMove = true;   // Player can move again
                playerIsAlive = true;   // Player is alive again
            }
        }
    }

    // Utility function to play the game
    private void Play()
    {
        // Creates a timer to play animations
        if (deathCooldown > 0)
        {
            deathCooldown = deathCooldown - Time.deltaTime;
        }
        // When timer is finished re-enables the player sprite
        else
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
        // Checks to see if the timer has not run out
        if (myGameManager.gameTimeRemaining > 0)
        {
            // Checks to see if player still has lives
            // Did two nested if statements to prevent bug
            // which prevented player to reset when all lives were gone
            if (playerLivesRemaining > 0)
            {
                LifeSprite();
                // Check to see if player can move and is alive
                if (playerCanMove && playerIsAlive)
                {
                    // Allows movement
                    Move();

                    // Plays the jumpSound and the jump animation
                    if (isPlayerMoving)
                    {
                        myAudioSource.PlayOneShot(jumpSound);
                        myAnimator.SetTrigger("Jump");
                    }
                } // If all endzones were reached player has won
                if (endZone == 6)
                {
                    myGameManager.GameOver(true);   // Shows game over window with a win statement
                }
            } // If player ran out of lives, player lost the game
            else
            {
                myGameManager.isGameRunning = false;
            }
        }   // If gameTimeRemaining has reached zero, player has lost the game
        else
        {
            myGameManager.isGameRunning = false;
        }
    }

    // Saves tings when quitting
    public void Quit()
    {
        Application.Quit();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks to see if player is alive
        if (playerIsAlive == true)
        {   // Checks for the vehicle type enemies
            if (collision.transform.GetComponent<Vehicle>() != null)
            {
                KillPanda(roadkillPrefab, deathSound);  // Kills panda and plays its death sound
            }
            // Checks if player player is on a platform
            else if (collision.transform.tag == "Platform")
            {
                onPlatform = true;
                transform.SetParent(collision.transform);
            }
            // Checks if player is on a turtle
            else if (collision.transform.tag == "turtle")
            {
                onPlatform = true;
                transform.SetParent(collision.transform);
            }
            // Checks for when player reaches an endzone
            else if (collision.transform.tag == "EndZone")
            {
                inEndZone = true;   // Player is in the endzone
                if(inKillZone == false||safeInEndZone == true)
                {
                    /************Updates Score************/
                    myGameManager.updateScore(50);    // Adds Score
                    bonusPoints = Mathf.FloorToInt(myGameManager.gameTimeRemaining * 20);   // Converts float to int and calculates bonus points
                    myGameManager.updateScore(bonusPoints); // Updates points by adding bonus points
                    /*************************************/
                    myGameManager.gameTimeRemaining = myGameManager.totalGameTime;  // Resets the timer
                    collision.GetComponent<SpriteRenderer>().enabled = true;    // Enables the winning sprite
                    collision.transform.tag = "Kill";   // Changes the tag to "Kill" to kill player if player collides with it again
                    transform.position = initialPosition;   // Repositions the player, effectively respawning them
                    endZone++;  // Adds a count to the amount of endzones reached

                    // Collider function retrieved from https://answers.unity.com/questions/1352119/how-to-access-childrens-colliders.html
                    // By Patrick2607
                    Collider2D[] childColliders = pointSystem.GetComponentsInChildren<BoxCollider2D>();
                    foreach (Collider2D collider in childColliders)
                    {
                        collider.enabled = true; // Resets the boxcolliders for the points systems
                    }
                }
            }
            // Checks for danger spots
            else if (collision.transform.tag == "Kill")
            {
                inKillZone = true;  // Player is in killzone
            }
            // Checks if player moves forward and adds 10 points
            else if (collision.transform.tag == "Points")
            {
                myGameManager.updateScore(10);  // Adds 10 points
                collision.GetComponent<BoxCollider2D>().enabled = false; // Disables the box collider to prevent further addition of points
            }
            // Checks for River
            else if (collision.transform.tag == "River")
            {
                inRiver = true;
            }
            // Checks for a candy
            else if (collision.transform.tag == "Candy")
            {
                myGameManager.updateScore(400); // Adds 400 points as bonus
                Destroy(collision.gameObject);
                safeInEndZone = true;
            }
            
            if (endZone == 6)
            {
                myGameManager.updateScore(1000);  // Bonus 1000 points for finishing the game
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check to see if player is alive
        if (playerIsAlive == true)
        {
            // Take care of leaving platform
            if (collision.transform.tag == "Platform")
            {
                onPlatform = false;
                transform.SetParent(null);
            }
            // take care of leaving the turtle
            else if (collision.transform.tag == "turtle")
            {
                onPlatform = false;
                transform.SetParent(null);
            }
            // Leave the water
            else if (collision.transform.tag == "River")
            {
                inRiver = false;
            }
            // Forced to leave the killzone
            else if (collision.transform.tag == "Kill")
            {
                inKillZone = false;
            }
            // Forced to leave the Endzone
            else if (collision.transform.tag == "EndZone")
            {
                inEndZone = false;
            }
        }
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

    // Utility function to execute the statements to kill the player
    private void KillPanda(GameObject preFab, AudioClip clip)
    {
        myAudioSource.PlayOneShot(clip);    // Plays death sound
        GameObject death = Instantiate(preFab, transform.position, Quaternion.identity);  // Clones player and plays an animation
        Destroy(death, 1f); // Destroys cloned player
        playerIsAlive = false; // Player can't move
        playerCanMove = false; // Sets player alive to false
        GetComponent<SpriteRenderer>().enabled = false; // Disables the player sprite
        deathCooldown = 1f; // Sets the cooldown to 1sec
        myGameManager.gameTimeRemaining = myGameManager.totalGameTime;  // Resets the timer 
    }

    // Utility function to get rid of the life of the player
    private void LifeSprite()
    {   // As the playerLivesRemaining gets subtracted the one life sprite gets disabled per life
        if (playerLivesRemaining == 2)
        {
            life2.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (playerLivesRemaining == 1)
        {
            life1.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}