using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LeverDoorControls : MonoBehaviour
{
    [SerializeField] private Rigidbody2D Rb;
    [SerializeField] private SpriteRenderer LeverSprite;
    [SerializeField] private Sprite On;
    [SerializeField] private Sprite Off;
    [SerializeField] private float OpenSpeed;
    [SerializeField] private float CloseSpeed;


    private bool IsOn;
    private bool AtLever;


    void OnTriggerEnter2D(Collider2D Touching)
    {
        if(Touching.CompareTag("Player"))
        {
            Debug.Log("At lever");
            AtLever = true;
        }
    }


    void OnTriggerExit2D(Collider2D Touching)
    {
        if(Touching.CompareTag("Player"))
        {
            Debug.Log("Left lever");
            AtLever = false;
        }
    }


    public void Switch(InputAction.CallbackContext context)
    {
        Debug.Log("Interact");
        if(AtLever && context.performed)
        {
            if(IsOn)
            {
                Debug.Log("Opening");
                LeverSprite.sprite = Off;
                Close();
            }
            else
            {
                Debug.Log("Closing");
               LeverSprite.sprite = On;
               Open(); 
            }
            IsOn = !IsOn;
        }
    }


    private void Open()
    {
        Vector2 Vector = new Vector2(0, OpenSpeed);
        Rb.velocity = Vector;
    }


    private void Close()
    {
        Vector2 Vector = new Vector2(0, -CloseSpeed);
        Rb.velocity = Vector;
    }
}


