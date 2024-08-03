using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispenserControls : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab; // Prefab of the projectile
    [SerializeField] private Transform shootPoint; // The point from where the projectile will be shot
    [SerializeField] private float ShootIncrement; //time between shots


    void Start()
    {
        StartCoroutine(Shoot());
    }


    private IEnumerator Shoot()
    {
        while (true)
        {
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
            projectile.SetActive(true);
            yield return new WaitForSeconds(ShootIncrement);
        }
    }
}
