using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnpoint;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float shootingInterval;

    private bool inRange = false;

    public bool InRange
    {
        get=>inRange;
        set
        {
            inRange = value;
            if (inRange) StartCoroutine(StartShootingRoutine());
        }
    }
    private IEnumerator StartShootingRoutine()
    {
        while(true)
        {
            Shoot();
            yield return new WaitForSeconds(shootingInterval);
        }
    }
    public void Shoot()
    {
        GameObject bullet = GameObject.Instantiate(bulletPrefab, bulletSpawnpoint.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
        StartCoroutine(DestroyAfterTimeRoutine(bullet, 1f));
    }


    private IEnumerator DestroyAfterTimeRoutine(GameObject objToDestroy, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(objToDestroy);
    }
}
