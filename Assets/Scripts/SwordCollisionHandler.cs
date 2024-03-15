using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has a HealthManager component
        HealthManager healthManager = other.gameObject.GetComponent<HealthManager>();
        if (healthManager != null)
        {
            // Deduct 75 HP from the target
            healthManager.health -= 75;
        }
    }
}
