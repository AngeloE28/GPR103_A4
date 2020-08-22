using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZone_Spawner : MonoBehaviour
{
    public float maxTime;   // Gets the maximum time range
    public float minTime;   // Gets the minimum time range

    public Animator EndZone;    // Gets its animator

    public float delayTime; // Creates a delay before the gameObject disappears
    public float spawnTime; // Time before gameObject spawns
    public float candyTime; // Gives a value to determine if gameObject should be a candy
    public float evilOctoTime;  // Gives a value to determine if gameObject shoudl be the evil octo

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = candyTime;  // Sets the inital spawn time to the candy time
        randTime(); // Gives a random number for the spawntime
    }

    // Update is called once per frame
    void Update()
    {

        if (candyTime > evilOctoTime)
        {
            EndZone.SetTrigger("Treat");    // Shows the candy sprite
            transform.tag = "Candy";    // Changes tag to candy
        }
        else
        {
            EndZone.SetTrigger("Trick");    // Shows the evil octo sprite
            transform.tag = "Kill"; // Changes tag to Kill
        }
        if (spawnTime <= 0)
        {
            // Enables the sprite and the boxcollider, "spawning" it
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            // Disables the sprite and the boxcollider
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
        }
        timer();     
    }
    
    // Utility function to set a random time to spawn the evil octo or the candy
    private void randTime()
    {
        spawnTime = Random.Range(minTime, maxTime); // Gives a random number for the spawnTime timer
        candyTime = Random.Range(minTime, maxTime); // Gives a random value for candyTime
        evilOctoTime = Random.Range(minTime, maxTime);  // Gives a random value for evilOctoTimer
    }

    // Utility function to begin the timer for spawnTimer and delayTimer
    private void timer()
    {
        // Timer for when turtle begins to sink
        if (spawnTime > 0)
        {
            //GetComponent<BoxCollider2D>().enabled = false;   // Sets the boxcollider2d to false
            spawnTime -= Time.deltaTime;    // Starts the countdown
            delayTime = 4f; // Sets the time for the second timer
        }
        else
        {  // Second timer starts when sinkTimer has reached 0
            delayTime -= Time.deltaTime;    // Starts the second timer
        }
        // Checks if the submergeTimer has reached 0
        if (delayTime < 0)
        {
            //GetComponent<BoxCollider2D>().enabled = true;  // Turns on boxcollider2d until delayTime reaches 0
            randTime(); // Sets a new timer
        }
    }
}
