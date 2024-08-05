
using System.Collections;

using System.Collections.Generic;

using UnityEngine;

  

public class MoveRight : MonoBehaviour

{

    public float moveSpeed = 5.0f; // You can adjust this speed in the Inspector

  

    void Update()

    {

        // Move the object to the right using transform.right

        transform.position += transform.right * moveSpeed * Time.deltaTime;

    }

}
