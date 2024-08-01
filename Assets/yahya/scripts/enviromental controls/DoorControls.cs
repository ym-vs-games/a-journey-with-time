using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControls : MonoBehaviour
{
    [SerializeField] private float OpenSpeed;
    [SerializeField] private float CloseSpeed;
    [SerializeField] private Rigidbody2D Rb;


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

    
    void OnTriggerStay2D(Collider2D Press)
    {
        if(Press.gameObject.CompareTag("Player"))
        {
            Open();
            Debug.Log("Opening");
        }
    }

    void OnTriggerExit2D(Collider2D Press)
    {
        Close();
    }

}
