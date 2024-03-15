using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private PlayerRespawn respawn;
    private BoxCollider checkPointCollider;

    private void Awake() 
    {
        checkPointCollider = GetComponent<BoxCollider>();
        respawn = GameObject.FindGameObjectWithTag("Respawn").GetComponent<PlayerRespawn>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        // When a player enters the checkpoint's trigger collider, update the respawn point to this checkpoint  
        if (other.gameObject.CompareTag("Player"))
        {
            respawn.UpdateRespawnPoint(this.transform);
            checkPointCollider.enabled = false;
        }
    }
}
