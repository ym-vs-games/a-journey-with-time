using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollisionManager : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private Rigidbody2D Rb;
    [SerializeField] private Transform ShootDirection;

    void Start()
    {
        Move();
    }


    private void Move()
    {
        Rb.velocity = (ShootDirection.right * Speed);
        Debug.Log(ShootDirection.right);
    }

    void OnTriggerEnter2D(Collider2D Collide)
    {
        if(Collide.CompareTag("Player") || (Collide.gameObject.layer == 3))
        {
            Destroy(gameObject);
        }
    }

}

