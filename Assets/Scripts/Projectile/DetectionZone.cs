using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public List<Collider> detectedObjs = new List<Collider>(); 

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player")
        {
            detectedObjs.Add(other);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player")
        {
            detectedObjs.Remove(other);
        }
    }
}
