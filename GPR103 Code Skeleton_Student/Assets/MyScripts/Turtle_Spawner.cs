using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Turtle_Spawner : MonoBehaviour
{
    public GameObject TurtlePrefab; // The prefab being used for this script
    public GameObject recentVehicleSpawned; // Clones the Prefab being used

    public bool canSpawn = true;    // Can the Prefab be spawned

    public int spawnCount;  // Number of prefabs being spawned
    public int turtleToSubmerge;    // Which turtle should submerge
    public float spawnTimer = 0f;   // Timer between each spawn
    public float resetTimer;   // Resets the spawnTimer

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime; // Starts to countdown for the next spawn
        
        // If Prefab can be spawned, the spawn timer has reached 0 and hasnt reached 0 spawncount yet
        if(canSpawn == true && spawnTimer <= 0 && spawnCount > 0)
        {   // Clones the Prefab
            recentVehicleSpawned = Instantiate(TurtlePrefab, transform.position, Quaternion.identity) as GameObject;
            spawnTimer = resetTimer;    // Resets the timer when Prefab is cloned
            spawnCount--;   // Takes away one from the spawncount
        }
        TurtleSink(turtleToSubmerge);   // Clone to submerge
        
    }

    // Utility function to determine which clone of the Prefab Gets to submerge
    private void TurtleSink(int SpawnNumber)
    {
        if (spawnCount == SpawnNumber)    // Creates a special condition for a sumberging turtle
        {
            // Gets all the children in the Prefab
            Turtle[] turtles = TurtlePrefab.GetComponentsInChildren<Turtle>();
            foreach (Turtle turtle in turtles)
            {
                turtle.canSink = true;  // Sets canSink to true for all children of the Prefab
            }
        }
        else // Check to prevent the other Prefab being cloned to follow the special condition
        {   
            Turtle[] turtles = TurtlePrefab.GetComponentsInChildren<Turtle>();
            foreach (Turtle turtle in turtles)
            {
                turtle.canSink = false;  // Sets canSink to false for all children of the Prefab
            }
        }
    }
}
