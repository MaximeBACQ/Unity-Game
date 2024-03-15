using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectile;
    public Transform spawnLocation;
    public Quaternion spawnRotation;

    public DetectionZone detectionZone;
    public float spawnTime = 0.5f;
    private float timeSinceSpawned = 0.5f;

    void Start()
    {
        spawnRotation = Quaternion.Euler(0, 0, 90);
    }
    
    void Update()
    {
        // Checks if there are any objects detected by the detection zone
        if (detectionZone.detectedObjs.Count > 0)
        {
            timeSinceSpawned += Time.deltaTime;

            // Checks if enough time has passed to spawn another projectile
            if (timeSinceSpawned >= spawnTime)
            {
                // Spawns a projectile at the specified location and rotation
                Instantiate(projectile, spawnLocation.position, spawnRotation);
                timeSinceSpawned = 0;
            }
        } 
        else 
        {
            // Resets the timer if no objects are detected
            timeSinceSpawned = 0.5f;
        }
    }
}
