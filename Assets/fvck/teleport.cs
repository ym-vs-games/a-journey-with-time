using System.Collections;

using System.Collections.Generic;

using UnityEngine;

  

public class teleports : MonoBehaviour

{

    [SerializeField] private GameObject GameObjectPrefab1; // Prefab 1

  

    private void OnTriggerEnter2D(Collider2D other)

    {

        if (other.CompareTag("cloud")) // Check if the collided object has the "cloud" tag

        {

            // Log the positions before teleporting

            Debug.Log("Current Position of other: " + other.transform.position);

            Debug.Log("Target Position of GameObjectPrefab1: " + GameObjectPrefab1.transform.position);

  

            // Lock the y-axis position

            Vector3 newPosition = new Vector3(GameObjectPrefab1.transform.position.x, other.transform.position.y, GameObjectPrefab1.transform.position.z);

  

            // Move the collided object to the modified position

            other.transform.position = newPosition;

  

            // Log the positions after teleporting

            Debug.Log("New Position of other: " + other.transform.position);

            Debug.Log("Object moved to Prefab position with y-axis locked");

        }

    }

}