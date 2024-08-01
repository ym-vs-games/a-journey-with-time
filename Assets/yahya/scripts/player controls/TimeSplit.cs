using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeSplit : MonoBehaviour
{
    [SerializeField] private GameObject PlayerPast; // Reference to the past player GameObject
    [SerializeField] private PlayerInput PlayerInput; // PlayerInput for the present player
    [SerializeField] private PlayerInput PlayerPastInput; // PlayerInput for the past player
    
    
    public bool SplitActive = false; // Flag to indicate if the split is active
    private PlayerInput CurrentInput; // Reference to the currently active PlayerInput


    void Start()
    {
        SetCurrentInput(PlayerInput); // Set the initial input to the present player
        PlayerPast.SetActive(false); // Ensure the past player is initially inactive
    }


    public void ActivateSplit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SplitActive = true;
            PlayerPast.transform.position = transform.position; // Position the past player at the current player's position
            SetCurrentInput(PlayerPastInput); // Switch input control to the past player
            PlayerPast.SetActive(true);
        }
    }


    private void SetCurrentInput(PlayerInput newInput)
    {
        if (CurrentInput != null)
        {
            CurrentInput.enabled = false; // Disable current input
        }

        CurrentInput = newInput;
        CurrentInput.enabled = true; // Enable the new input
    }
}
