using UnityEngine;
public class Potion : MonoBehaviour
{
    public static Potion instance;
    public Transform associatedCheckPoint;
    private Vector3 potionStartPosition; 
    private bool isCollected = false;

    private void Awake() {
        if(instance != null)
        {
            return;
        }

        instance = this;
    }

    private void Start()
    {
        // Save the starting position of the potion
        potionStartPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            isCollected = true;
        }
    }

    public void ResetPotionPosition()
    {
        // If the potion has been collected, reset its position
        if (isCollected)
        {
            transform.position = potionStartPosition;
            isCollected = false;
            gameObject.SetActive(true);
        }
    }
}
