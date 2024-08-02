using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] private Transform[] Waypoints; //list of location of waypoints
    [SerializeField] private float Speed; //how fast platform moves
    [SerializeField] private Rigidbody2D Rb;
    
    private Rigidbody2D PlayerRb;
    private Transform CurrentWaypoint; //waypoint platform currently moving to
    private int Index = 0;  //index counter for Waypoints list

    void Start()
    {
        //sets current waypoint to first in list
        CurrentWaypoint = Waypoints[Index];
    }


    void FixedUpdate()
    {
        Circle();
    }
    

    private void Circle()
    {   
        transform.position = Vector2.MoveTowards(transform.position, CurrentWaypoint.position, Speed * Time.deltaTime);
        if(Vector2.Distance(transform.position, CurrentWaypoint.position) < 0.1f) //change waypoint once it reaches current one
        {
            if(Index == (Waypoints.Length - 1)) //checks to see if it reached end of waypoint list
            {
                Index = 0; //resets to begining
            }
            else
            {
                Index++; //increments to next waypoint
            }
            CurrentWaypoint = Waypoints[Index]; //sets new current waypoint to next waypoint in list
        }
    }

    void OnTriggerEnter2D(Collider2D Touching)
    {
        if(Touching.CompareTag("Player"))
        {
            Touching.transform.SetParent(transform);
        }
    }

    void OnTriggerExit2D(Collider2D Touching)
    {
        if(Touching.CompareTag("Player"))
        {
            Touching.transform.SetParent(null);
        }
    }
}
