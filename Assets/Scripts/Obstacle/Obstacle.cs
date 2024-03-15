using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public HealthManager healthManager;

    public int damage;

    private void Awake()
    {
        healthManager = GameObject.FindObjectOfType<HealthManager>();
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            // Checks if the player is currently invincible by accessing the Invincibility
            if (Invincibility.instance.isInvincible == false)
            {
                // If the player is not invincible, reduces health by the specified damage amount
                healthManager.health -= damage;
            }
        }
    }
}
