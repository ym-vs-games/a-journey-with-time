using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D Checkpoint)
    {
        if(Checkpoint.CompareTag("Player"))
        {
            TimeReversal SetCheckpoint = Checkpoint.gameObject.GetComponent<TimeReversal>();
            SetCheckpoint.CurrentCheckpoint = gameObject;
        }
        
    }
}
