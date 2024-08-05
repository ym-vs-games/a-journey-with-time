using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollisionManager : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private Rigidbody2D Rb;
    [SerializeField] private Transform ShootDirection;
    [SerializeField] private float SlowDownFactor;
    [SerializeField] AbilityController AbilityCheck;

    void Start()
    {
        Move();
    }


    private void Move()
    {
        if(AbilityCheck.SlowActive == true)
        {
            Rb.velocity = (Vector2.down * Speed / SlowDownFactor);
        }
        else
        {
            Rb.velocity = (Vector2.down * Speed);
        }   
    }

    void OnTriggerEnter2D(Collider2D Collide)
    {
        if(Collide.CompareTag("Player") || (Collide.gameObject.layer == 3))
        {
            Destroy(gameObject);
        }
    }

}

