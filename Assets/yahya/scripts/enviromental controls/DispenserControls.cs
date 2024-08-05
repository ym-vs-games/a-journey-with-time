using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispenserControls : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab; // Prefab of the projectile
    [SerializeField] private Transform shootPoint; // The point from where the projectile will be shot
    [SerializeField] private float ShootIncrement; //time between shots
    [SerializeField] private float SlowDownFactor;
    [SerializeField] AbilityController AbilityCheck;

    private bool Shooting;
    private float ShootDelay;

    void Update()
    {
        StartCoroutine(Shoot());
    }


    private IEnumerator Shoot()
    {
        if(!Shooting)
        {
            Shooting = true;
            if(AbilityCheck.SlowActive)
            {
                ShootDelay = ShootIncrement*SlowDownFactor;
            }
            else
            {
                ShootDelay = ShootIncrement;
            }
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
            projectile.SetActive(true);
            yield return new WaitForSeconds(ShootDelay);
            Shooting = false;
        }
        else
        {
            yield return null;
        }

    }
}
