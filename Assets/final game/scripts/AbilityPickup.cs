using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPickup : MonoBehaviour
{
    [SerializeField] private bool IsStop;
    [SerializeField] private bool IsSlow;
    [SerializeField] private bool IsAccelerate;


    void OnTriggerEnter2D(Collider2D GiveAbility)
    {
        if(GiveAbility.CompareTag("Player"))
        {
            AbilityController Ability = GiveAbility.gameObject.GetComponent<AbilityController>();
            if (IsAccelerate)
            {
                Ability.AccelerateUnlocked = true;
            }
            else if (IsSlow)
            {
                Ability.SlowUnlocked = true;
            }
            else if (IsStop)
            {
                Ability.StopUnlocked = true;
            }
            Destroy(gameObject);
        }
    }
}
