using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollow : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private Transform PlayerPast;
    [SerializeField] private float FollowSpeed; 
    [SerializeField] private TimeSplit SplitScript;
    

    private Transform ActiveFollow;
    private Vector3 Offset; 


    void Start()
    {
        Offset = transform.position - Player.position;
    }


    void Update()
    {
        if(SplitScript.SplitActive)
        {
            ActiveFollow = PlayerPast;
        }
        else
        {
            ActiveFollow = Player;
        }
        Vector3 DesiredPosition = ActiveFollow.position + Offset;
        transform.position = Vector3.Lerp(transform.position, DesiredPosition, FollowSpeed * Time.deltaTime);
    }

}