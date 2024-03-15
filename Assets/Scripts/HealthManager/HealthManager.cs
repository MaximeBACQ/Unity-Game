using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthManager : MonoBehaviour
{
    public float health = 100f;
    public float maxHealth = 100f;

    public Image healthBarImage;
    public TextMeshProUGUI healthText;

    void Update()
    {
        // Update the health bar's fill amount based on the current health relative to maximum health
        healthBarImage.fillAmount = health / maxHealth;
        // Update the health text to display current health and maximum health
        healthText.text = health + " / " + maxHealth;
        // Ensure health value stays within the bounds of 0 and maxHealth
        health = Mathf.Clamp(health, 0f, maxHealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Heal"))
        {
            health += 30;
        }

        if (other.gameObject.tag == "Arrow")
        {
            health -= 5;
        }
    }
}
