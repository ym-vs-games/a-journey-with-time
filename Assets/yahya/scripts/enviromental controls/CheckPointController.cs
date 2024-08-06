using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    [SerializeField] private Sprite newSprite;


    private SpriteRenderer spriteRenderer;


    void Start()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void OnTriggerEnter2D(Collider2D Checkpoint)
    {
        if(Checkpoint.CompareTag("Player"))
        {
            TimeReversal SetCheckpoint = Checkpoint.gameObject.GetComponent<TimeReversal>();
            SetCheckpoint.CurrentCheckpoint = gameObject;
            // Change the sprite of the current GameObject
            if (spriteRenderer != null && newSprite != null)
            {
                spriteRenderer.sprite = newSprite;
            }
        }
    }

    
}
