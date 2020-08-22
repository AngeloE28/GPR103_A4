using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle_Spawner : MonoBehaviour
{
    public GameObject vehiclePrefab; // The prefab being used for this script
    public GameObject recentVehicleSpawned; // Clones the Prefab being used

    public bool canSpawn = true;    // Can the Prefab be spawned

    public int spawnCount; // Number of prefabs being spawned
    public float spawnTimer = 0f;   // Timer between each spawn
    public float resetTimer;    // Resets the spawnTimer

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;   // Starts to countdown for the next spawn

        // If Prefab can be spawned, the spawn timer has reached 0 and hasnt reached 0 spawncount yet
        if (canSpawn == true && spawnTimer <= 0 && spawnCount > 0)
        {   // Clones the Prefab
            recentVehicleSpawned = Instantiate(vehiclePrefab, transform.position, Quaternion.identity) as GameObject;
            spawnTimer = resetTimer;    // Resets the timer when Prefab is cloned
            spawnCount--;   // Takes away one from the spawncount
        }
        
    }
}
