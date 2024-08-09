using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Add this line

public class AbilityController : MonoBehaviour
{
    public bool SlowUnlocked;
    public bool AccelerateUnlocked;
    public bool StopUnlocked;
    public bool StopActive;
    public bool SlowActive;
    public bool AccelerateActive;

    public Button slowButton;
    public Button accelerateButton;
    public Button stopButton;

    void Start()
    {
        slowButton.onClick.AddListener(ActivateSlow);
        accelerateButton.onClick.AddListener(ActivateAccelerate);
        stopButton.onClick.AddListener(ActivateStop);
    }

    void ActivateSlow()
    {
        if (SlowUnlocked)
        {
            SlowActive = true;
            AccelerateActive = false;
            StopActive = false;
        }
    }

    void ActivateAccelerate()
    {
        if (AccelerateUnlocked)
        {
            AccelerateActive = true;
            SlowActive = false;
            StopActive = false;
        }
    }

    void ActivateStop()
    {
        if (StopUnlocked)
        {
            StopActive = true;
            SlowActive = false;
            AccelerateActive = false;
        }
    }
}
