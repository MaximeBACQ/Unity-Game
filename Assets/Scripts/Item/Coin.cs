using UnityEngine;
using TMPro;

public class Coin : MonoBehaviour
{
    public static Coin instance;
    private static int coin = 0;

    public Transform associatedCheckPoint;

    private Vector3 coinStartPosition;
    private bool isCollected = false;

    public TextMeshProUGUI coinText;

    private void Awake() {
        if(instance != null)
        {
            return;
        }

        instance = this;
    }

    private void Start()
    {
        // Cache the starting position of the coin
        coinStartPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other) 
    {
        // Increments coin count and updates UI when a player collects the coin
        if (other.gameObject.CompareTag("Player"))
        {
            coin++;
            coinText.text = "Coin : " + coin.ToString();
            gameObject.SetActive(false);
            isCollected = true;
        }
    }

    public void ResetCoinPosition()
    {
        // Resets the coin position if it has been collected, and decrements the coin count
        if (isCollected)
        {
            coin--;
            coinText.text = "Coin : " + coin.ToString(); // Update coin count in UI
            transform.position = coinStartPosition;
            isCollected = false;
            gameObject.SetActive(true); // Make the coin visible again
        }
    }

    internal void ResetCoin()
    {
        coin = 0;
    }
    
    public float HowManyCoin()
    {
        return coin;
    }
}
