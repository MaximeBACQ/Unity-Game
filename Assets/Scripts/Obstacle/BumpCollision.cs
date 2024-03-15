using UnityEngine;

public class BumpCollision : MonoBehaviour
{
    public float bounceSpeed = 50f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CharacterController controller = collision.gameObject.GetComponent<CharacterController>();
            if (controller != null)
            {
                // Calculate the bounce direction based on the collision normal and bounce speed.
                Vector3 bounceDirection = -collision.contacts[0].normal * bounceSpeed;
                // Move the player in the bounce direction, scaled by deltaTime to ensure smooth movement.
                controller.Move(bounceDirection * Time.deltaTime);
            }
        }   
    }
}
