using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    public static SpeedUp instance;
    public Transform associatedCheckPoint;
    private Vector3 potionStartPosition;
    private bool isCollected = false;

    public float speedMultiplier = 2f;
    public float speedDuration = 3f;
    private bool isBoosted = false;
    private FPSController playerController;

    private void Awake() {
        if(instance != null)
        {
            return;
        }

        instance = this;
    }

    private void Start()
    {
        potionStartPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            // Get the FPSController component from the player
            playerController = other.GetComponent<FPSController>();
            ApplySpeedBoost();
            gameObject.SetActive(false);
            isCollected = true;
        }
    }

    public void ResetSpeedUpPosition()
    {
        // Reset the potion's position if it has been collected
        if (isCollected)
        {
            transform.position = potionStartPosition;
            isCollected = false; 
            gameObject.SetActive(true);
        }
    }

    private void ApplySpeedBoost()
    {
        if (!isBoosted && playerController != null)
        {
            isBoosted = true;
            // Multiply the player's walk speed by the speed multiplier
            playerController.walkSpeed *= speedMultiplier;
            // Schedule the speed boost to be disabled after its duration
            Invoke("DisableSpeedUp", speedDuration);
        }
    }

    private void DisableSpeedUp()
    {
        if(!isBoosted)
        {
            return;
        }
        // Divide the player's walk speed by the speed multiplier to return to original speed
        playerController.walkSpeed /= speedMultiplier;
        isBoosted = false;
    }
    public void CancelSpeedBoost()
    {
        if (isBoosted)
        {
            DisableSpeedUp();
        }
    }
}
