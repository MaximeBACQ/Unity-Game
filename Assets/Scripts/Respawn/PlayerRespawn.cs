using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public GameObject player;
    public GameObject endLevelCanvas;
    public GameObject endLevelText;
    public GameObject key;

    public Transform respawnPoint;
    public FadeOutImage fadeOutImage;
    public HealthManager healthManager;

    public Coin[] coins;
    public Potion[] potions;
    public Invincibility[] invincibilities;
    public SpeedUp[] speedUps;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    private float nbStar; // Number of stars earned based on coins collected

    private void Awake()
    {
        healthManager = GameObject.FindObjectOfType<HealthManager>();
        fadeOutImage.gameObject.SetActive(false);
    }

    public void UpdateRespawnPoint(Transform newRespawnPoint)
    {
        // Updates the current respawn point. If it's an end point, trigger level completion
        respawnPoint = newRespawnPoint;
        if (respawnPoint.CompareTag("EndPoint"))
        {
            EndLevel();
        }
    }

    public void EndLevel()
    {
        // Handles level completion: displays end level UI and calculates stars based on coins collected
        InputController.Instance.RemoveFromDontDestroyOnLoad();
        endLevelCanvas.SetActive(true);
        endLevelText.SetActive(true);
        Cursor.lockState = CursorLockMode.None;

        // Calculate the number of stars based on coins collected.
        nbStar = Coin.instance.HowManyCoin() / coins.Length;
        if (nbStar >= 0) star1.SetActive(true);
        if (nbStar >= 0.50) star2.SetActive(true);
        if (nbStar == 1) star3.SetActive(true);
    }

    private void FixedUpdate()
    {
        // Check if the player's health is depleted and handle respawn
        if (healthManager.health <= 0)
        {
            fadeOutImage.StartFade();
            player.transform.position = respawnPoint.position;
            healthManager.health = healthManager.maxHealth;
            key.SetActive(true);

            // Reset positions and states of collectibles associated with the current checkpoint
            foreach (Coin coin in coins)
            {
                if (coin.associatedCheckPoint == respawnPoint)
                {
                    coin.ResetCoinPosition();
                }
            }

            foreach (Potion potion in potions)
            {
                if (potion.associatedCheckPoint == respawnPoint)
                {
                    potion.ResetPotionPosition();
                }
            }

            foreach (Invincibility invincibility in invincibilities)
            {
                if (invincibility.associatedCheckPoint == respawnPoint)
                {
                    invincibility.ResetInvincibilityPosition();
                }
            }

            foreach (SpeedUp speedUp in speedUps)
            {
                if (speedUp.associatedCheckPoint == respawnPoint)
                {
                    speedUp.ResetSpeedUpPosition();
                    speedUp.CancelSpeedBoost();
                }
            }
        }
    }
}