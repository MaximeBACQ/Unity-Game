using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed = 1f;
    private Vector3 moveDirection = Vector3.left;
   
    void Update()
    {
        // Moves the projectile each frame based on moveSpeed and moveDirection
        transform.position += moveSpeed * moveDirection * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
