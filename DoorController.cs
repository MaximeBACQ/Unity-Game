using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator doorAnim;
    private bool isOpened = false;

    void Start()
    {
        doorAnim = GetComponent<Animator>();
    }

    public void UseKey()
    {
        //if door not open,ed
        if (!isOpened)
        {
            isOpened = true;
            doorAnim.SetBool("open",true); // make "open" true in animator to trigger transition
        }
    }
}
