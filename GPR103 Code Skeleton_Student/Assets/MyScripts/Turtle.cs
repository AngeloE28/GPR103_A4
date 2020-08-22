using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script must be utlised as the core component on the 'vehicle' obstacle in the frogger game.
/// </summary>
public class Turtle : MonoBehaviour
{
    /// <summary>
    /// -1 = left, 1 = right
    /// </summary>
    public int moveDirection = 0; //This variabe is to be used to indicate the direction the vehicle is moving in.
    public float speed; //This variable is to be used to control the speed of the vehicle.
    public Vector2 startingPosition; //This variable is to be used to indicate where on the map the vehicle starts (or spawns)
    public Vector2 endPosition; //This variablle is to be used to indicate the final destination of the vehicle.

    public float sinkTimer;    // Total time before turtle starts submerging
    private float submergeTimer ;    // Total Time before goes completely under
    public float resetSinkTimer;  // Utility refence to reset the sinkTimer

    public Animator turtleAnim;    // Plays the animations of the turtle

    public bool canSink = false;    // Can the turtle sink?

    // Start is called before the first frame update
    void Start()
    {
        transform.position = startingPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed * moveDirection);

        // When you multiply the inequaltiy on both siddes by -1 ( or divide by -1), the inequality changes direction
        if ((transform.position.x * moveDirection) > (endPosition.x * moveDirection))
        {
            transform.position = startingPosition;
        }
        Submerge(canSink);  // If canSink is true, the turtle can submerge
       
    }

    private void Submerge(bool turtleSink)
    {
        if(turtleSink == true)
        {
            // Timer for when turtle begins to sink
            if (sinkTimer > 0)
            {
                GetComponent<BoxCollider2D>().enabled = true;   // Sets the boxcollider2d to true
                sinkTimer -= Time.deltaTime;    // Starts the countdown
                submergeTimer = 2f; // Sets the time for the second timer
            }
            else
            {  // Second timer starts when sinkTimer has reached 0
                submergeTimer -= Time.deltaTime;    // Starts the second timer
                turtleAnim.SetTrigger("Submerge"); // Starts to play Submerge to warn player turtle is going underwater
                turtleAnim.SetBool("Sinking", true);   // Sets boolean to true to fill condition for water to play the Submerge

            }
            // Checks if the submergeTimer has reached 0
            if (submergeTimer < 0)
            {
                GetComponent<BoxCollider2D>().enabled = false;  // Turns off boxcollider2d off for 1 frame
                turtleAnim.SetTrigger("Under");   // Plays the animation which makes turtle go completely underwater
                turtleAnim.SetBool("Sinking", false); // Sets the boolean for animator to false to prevent animation looping
                sinkTimer = resetSinkTimer; // Resets the timer for sinkTimer
            }
        }
    }
}
