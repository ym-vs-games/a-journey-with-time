using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeReversal : MonoBehaviour
{
    [SerializeField] private GameObject WaypointPrefab; // The waypoint object
    [SerializeField] private bool Dead; // Flag to check if player died
    [SerializeField] private float ReversalSpeed; // How fast they retrace steps

    public GameObject CurrentCheckpoint; // Last checkpoint player touched
    private Stack<GameObject> WaypointStack;  // Stack to hold all waypoints created
    private List<GameObject> AllWaypoints;    // List to track all waypoints
    private GameObject LastWaypoint; // Previous waypoint created
    private bool IsReversing = false; // To track if reversal is in progress

    void Start()
    {
        // Initialize the stack and list in the Start method
        WaypointStack = new Stack<GameObject>();
        AllWaypoints = new List<GameObject>();
    }

    void Update()
    {
        if (!Dead && CurrentCheckpoint != null)
        {
            CreateWaypoint();
        }
        else if (!IsReversing && CurrentCheckpoint != null)
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
            LastWaypoint.name = "ReversalWaypoint"; // Ensure the correct name is set
            WaypointStack.Push(LastWaypoint);
            AllWaypoints.Add(LastWaypoint); // Track all waypoints
        }
    }

    private IEnumerator Reversal()
    {
        IsReversing = true; // Mark that reversal has started

        while (LastWaypoint != null)
        {
            if (LastWaypoint.transform == null)
            {
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
                break;
            }

            transform.position = Vector2.MoveTowards(transform.position, CurrentCheckpoint.transform.position, ReversalSpeed * Time.deltaTime);
            yield return null; // Wait for the next frame
        }

        Dead = false;
        IsReversing = false; // Mark that reversal has ended
    }

    void OnTriggerEnter2D(Collider2D Clear)
    {
        if (Clear.CompareTag("Respawn"))
        {
            
            WaypointStack.Clear();
            // Destroy all waypoints
            foreach (GameObject waypoint in AllWaypoints)
            {
                if (waypoint != null)
                {
                    Destroy(waypoint);
                }
            }
            // Clear the list of all waypoints
            AllWaypoints.Clear();
        }
    }
}
