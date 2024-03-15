using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : MonoBehaviour
{
    public static Invincibility instance;
    public Transform associatedCheckPoint;
    private Vector3 potionStartPosition; 
    private bool isCollected = false; 

    public float invincibilityDuration = 10f;

    public bool isInvincible = false;

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
            gameObject.SetActive(false);
            isCollected = true;
            MakePlayerInvincible();
        }
    }

    public void ResetInvincibilityPosition()
    {
        if (isCollected)
        {
            transform.position = potionStartPosition;
            isCollected = false;
            gameObject.SetActive(true);
        }
    }

    void MakePlayerInvincible()
    {
        isInvincible = true;
        Invoke("DisableInvincibility", invincibilityDuration); // Schedules invincibility to end after a duration
    }

    void DisableInvincibility()
    {
        isInvincible = false;
    }
}
