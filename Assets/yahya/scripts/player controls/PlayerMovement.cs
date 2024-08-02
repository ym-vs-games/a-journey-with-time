using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform GroundChecker; //object used for checking if player touching ground
    [SerializeField] private LayerMask Ground;  //layer applied to all ground objects
    [SerializeField] private float TopSpeed; //fastest horizontal velocity
    [SerializeField] private float JumpForce; //how high player can jump
    [SerializeField] private Rigidbody2D Rb; //rigid body system
    [SerializeField] private float Acceleration; // acceleration factor rate
    [SerializeField] private float Decceleration; //decceleration factor rate
    [SerializeField] private float PowerFactor; //used to tweak how fast player accerates/decelerates
    

    private float JumpLeniency; //amount of time after touching ground player can still jump
    private float Horizontal;


    void Update()
    {
        Grounded();
        Timers();
    }


    //runs every physics update
    void FixedUpdate()
    {
        Move();
        Flip();
    }


    //checks if player touching the ground
    private void Grounded()
    {  
        //checks any object with ground layer within circle around player feet
        if(Physics2D.OverlapCircle(GroundChecker.position, 0.2f, Ground))
        {
            JumpLeniency = 0.1f;
        }
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
        float TargetSpeed = Horizontal*TopSpeed; //speed player wants to be at
        float SpeedDiff = (TargetSpeed - Rb.velocity.x); // difference between target speed and current speed
        float AccelerationRate = (Mathf.Abs(TargetSpeed) > 0.01f) ? Acceleration : Decceleration; //sets rate based on target speed
        float ForceVector = Mathf.Pow(Mathf.Abs(SpeedDiff)*AccelerationRate, PowerFactor) * Mathf.Sign(SpeedDiff); //determines direction and magnitude of the force
        Rb.AddForce(ForceVector* Vector2.right); //applies a force on the player
    }
    

    //causes player to jump when button pressed
    public void Jump(InputAction.CallbackContext context)
    {
        //jumps if button pressed and not touching ground
        if(context.performed && JumpLeniency > 0)
        {
            Rb.velocity = new Vector2(Rb.velocity.x, JumpForce);
        }
        //if jump button held player jumps higher
        if(context.canceled && Rb.velocity.y >= (JumpForce*0.5f))
        {
            Rb.velocity = new Vector2(Rb.velocity.x, JumpForce * 0.5f);
        }
    }


    //makes sure character faces direction it is moving
    private void Flip()
    {
        Vector3 CurrentScale = transform.localScale;
        if((Horizontal > 0) && (transform.localScale.x < 0))
        {
            CurrentScale.x *= -1;
        }
        else if(((Horizontal < 0) && (transform.localScale.x > 0)))
        {
            CurrentScale.x *= -1;
        }
        transform.localScale = CurrentScale;

    }

    private void Timers()
    {
        JumpLeniency -= Time.deltaTime;
    }
}
