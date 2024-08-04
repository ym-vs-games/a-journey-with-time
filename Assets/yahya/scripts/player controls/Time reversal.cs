using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timereversal : MonoBehaviour
{
    [SerializeField] private GameObject WaypointPrefab; // The waypoint object
    [SerializeField] private GameObject CurrentCheckpoint; // Last checkpoint player touched
    [SerializeField] private bool Dead; // Flag to check if player died
    [SerializeField] private float ReversalSpeed; // How fast they retrace steps

    private Stack<GameObject> WaypointStack;  // Stack to hold all waypoints created
    private GameObject LastWaypoint; // Previous waypoint created

    private bool IsReversing = false; // To track if reversal is in progress

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
        if (!Dead)
        {
            CreateWaypoint();
        }
        else if (!IsReversing)
        {
            // Only start the coroutine if it is not already running
            StartCoroutine(Reversal());
        }
    }

    private void CreateWaypoint()
    {
        if (CurrentCheckpoint == null) return; // Early exit if CurrentCheckpoint is not set

        // Check distance and add waypoints to the stack if conditions are met
        if ((LastWaypoint == null && Vector2.Distance(CurrentCheckpoint.transform.position, transform.position) > 8f) ||
            (LastWaypoint != null && Vector2.Distance(LastWaypoint.transform.position, transform.position) > 5f))
        {
            LastWaypoint = Instantiate(WaypointPrefab, transform.position, transform.rotation);
            WaypointStack.Push(LastWaypoint);
            Debug.Log("Waypoint added. Stack size: " + WaypointStack.Count);
        }
    }

    private IEnumerator Reversal()
    {
        IsReversing = true; // Mark that reversal has started

        while (LastWaypoint != null)
        {
            if (LastWaypoint.transform == null)
            {
                Debug.LogError("LastWaypoint transform is null!");
                break;
            }

            // Move towards the LastWaypoint position
            while (Vector2.Distance(transform.position, LastWaypoint.transform.position) > 0.01f)
            {
                transform.position = Vector2.MoveTowards(transform.position, LastWaypoint.transform.position, ReversalSpeed * Time.deltaTime);
                yield return null; // Wait for the next frame
            }

            // Destroy the LastWaypoint and pop the next waypoint from the stack
            Destroy(LastWaypoint);
            if (WaypointStack.Count > 0)
            {
                LastWaypoint = WaypointStack.Pop();
                Debug.Log("Moving to next waypoint. Stack size: " + WaypointStack.Count);
            }
            else
            {
                LastWaypoint = null; 
            }
        }

        // Move towards the CurrentCheckpoint position
        while (Vector2.Distance(transform.position, CurrentCheckpoint.transform.position) > 0.01f)
        {
            if (CurrentCheckpoint == null || CurrentCheckpoint.transform == null)
            {
                Debug.LogError("CurrentCheckpoint or its transform is null!");
                break;
            }

            transform.position = Vector2.MoveTowards(transform.position, CurrentCheckpoint.transform.position, ReversalSpeed * Time.deltaTime);
            yield return null; // Wait for the next frame
        }

        Dead = false;
        IsReversing = false; // Mark that reversal has ended
    }
}
