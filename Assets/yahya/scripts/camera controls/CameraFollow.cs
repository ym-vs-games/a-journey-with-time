using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollow : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private float FollowSpeed; 
    

    private Vector3 Offset; 


    void Start()
    {
        Offset = transform.position - Player.position;
    }


    void Update()
    {
        Vector3 DesiredPosition = Player.position + Offset;
        transform.position = Vector3.Lerp(transform.position, DesiredPosition, FollowSpeed * Time.deltaTime);
    }

}