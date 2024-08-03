using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeSplit : MonoBehaviour
{
    [SerializeField] private GameObject PlayerPast; // Reference to the past player GameObject
    [SerializeField] private PlayerInput PlayerInput; // PlayerInput for the present player
    [SerializeField] private PlayerInput PlayerPastInput; // PlayerInput for the past player
    
    
    public bool SplitActive; // Flag to indicate if the split is active
    private PlayerInput CurrentInput; // Reference to the currently active PlayerInput


    public void ActivateSplit(InputAction.CallbackContext context)
    {
        if (context.performed && !SplitActive)
        {
            StartCoroutine(Split(SplitActive, PlayerPast, PlayerInput, PlayerPastInput));
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


    private IEnumerator Split(bool SplitActive, GameObject PlayerPast, PlayerInput PlayerInput,PlayerInput PlayerPastInput)
    {
        PlayerPast.transform.position = transform.position; // Position the past player at the current player's position
        SetCurrentInput(PlayerPastInput); // Switch input control to the past player
        PlayerPast.SetActive(true);
        SplitActive = true;

        yield return new WaitForSeconds(10f);

        PlayerPast.SetActive(false);
        SetCurrentInput(PlayerInput);
        SplitActive = false;
    }
}
