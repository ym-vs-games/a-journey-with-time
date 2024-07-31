using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform GroundChecker;
    [SerializeField] private LayerMask Ground;
    [SerializeField] private float Speed;
    [SerializeField] private float JumpForce;
    [SerializeField] private Rigidbody2D Rb;
    
    private float Horizontal;

    //runs every frame
    void Update()
    {
        Move();
    }


    //checks if player touching the ground
    private bool Grounded()
    {  
        //checks any object with ground layer within circle around player feet
        return  Physics2D.OverlapCircle(GroundChecker.position, 0.2f, Ground);
    }


    //set x direction
    public void HorizontalDirection(InputAction.CallbackContext context)
    {
        //takes player a & d input for direction
        Horizontal = context.ReadValue<Vector2>().x;
    }


    //sets player x velocity based on speed and direction
    private void Move()
    {
        Rb.velocity = new Vector2(Horizontal * Speed, Rb.velocity.y);
    }
    

    //causes player to jump when button pressed
    public void Jump(InputAction.CallbackContext context)
    {
        //jumps if button pressed and not touching ground
        if(context.performed && Grounded())
        {
            Rb.velocity = new Vector2(Rb.velocity.x, JumpForce);

        }
        //if jump button held player jumps higher
        if(context.canceled && Rb.velocity.y > 0f)
        {
            Rb.velocity = new Vector2(Rb.velocity.x, JumpForce * 0.5f);
        }
    }
}
