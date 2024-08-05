using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    private bool activated;

    void OnTriggerEnter2D(Collider2D Checkpoint)
    {
        if(Checkpoint.CompareTag("Player") && activated == false)
        {
            activated = true;
            TimeReversal SetCheckpoint = Checkpoint.gameObject.GetComponent<TimeReversal>();
            SetCheckpoint.CurrentCheckpoint = gameObject;
        }
        
    }
}
