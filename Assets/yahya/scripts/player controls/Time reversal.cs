using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timereversal : MonoBehaviour
{
    [SerializeField] private GameObject WaypointPrefab; //the waypoint object
    [SerializeField] private GameObject CurrentCheckpoint; //last checkpoint player touched
    [SerializeField] private bool Dead; //flag to check if player died
    [SerializeField] private float ReversalSpeed; //how fast they retrace steps

    private Stack<GameObject> WaypointStack;  // Declare the stack
    private GameObject LastWaypoint; //previous waypoint created

    void Start()
    {
        // Initialize the stack in the Start method
        WaypointStack = new Stack<GameObject>(); //stack holds all waypoint created
        
        // Ensure CurrentCheckpoint is assigned, handle null cases here
        if (CurrentCheckpoint == null)
        {
            Debug.LogError("CurrentCheckpoint is not assigned in the inspector.");
        }
    }

    void Update()
    {
        if(!Dead)
        {
            CreateWaypoint();
        }
        else
        {
            StartCoroutine(Reversal());
        }
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

    private IEnumerator Reversal()
    {
        while (LastWaypoint != null)
        {
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
        while (transform.position != CurrentCheckpoint.transform.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, CurrentCheckpoint.transform.position, ReversalSpeed * Time.deltaTime);
            yield return null; // Wait for the next frame
        }

        Dead = false;
    } 
}
