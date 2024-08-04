using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timereversal : MonoBehaviour
{
    [SerializeField] private GameObject WaypointPrefab;
    [SerializeField] private GameObject CurrentCheckpoint;

    private Stack<GameObject> WaypointStack;  // Declare the stack
    private GameObject LastWaypoint;

    void Start()
    {
        // Initialize the stack in the Start method
        WaypointStack = new Stack<GameObject>();
        
        // Ensure CurrentCheckpoint is assigned, handle null cases here
        if (CurrentCheckpoint == null)
        {
            Debug.LogError("CurrentCheckpoint is not assigned in the inspector.");
        }
    }

    void Update()
    {
        CreateWaypoint();
    }

    private void CreateWaypoint()
    {
        if (CurrentCheckpoint == null) return; // Early exit if CurrentCheckpoint is not set

        // Check distance and add waypoints to the stack if conditions are met
        if ((LastWaypoint == null && Vector2.Distance(CurrentCheckpoint.transform.position, transform.position) > 2f) ||
            (LastWaypoint != null && Vector2.Distance(LastWaypoint.transform.position, transform.position) > 2f))
        {
            LastWaypoint = Instantiate(WaypointPrefab, transform.position, transform.rotation);
            WaypointStack.Push(LastWaypoint);
        }
    }
}
