using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private LineRenderer laser;
    [SerializeField] private GameObject arCamera;
    [SerializeField] private float laserDmg;

    private FPSLogic fpsLogic;

    private GameObject target;


  


    private void OnEnable()
    {
        fpsLogic = GetComponent<FPSLogic>();
        fpsLogic.OnEnemyLocked += EnemyLocked;
        fpsLogic.OnEnemyUnlocked += EnemyUnlocked;
    }

    private void OnDisable()
    {
        fpsLogic.OnEnemyLocked -= EnemyLocked;
        fpsLogic.OnEnemyUnlocked -= EnemyUnlocked;
    }
    public void ShootLaser()
    {
        laser.enabled = true;
        if(target!=null)
        {

            //Apply damage to target
            target.GetComponentInParent<Health>().Applydamage(laserDmg);
        }
        StartCoroutine(DisableLaserRoutine());

    }



    private IEnumerator DisableLaserRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        laser.enabled = false;
    }

    private void EnemyLocked(GameObject enemy)
    {
        target = enemy;
    }

    private void EnemyUnlocked()
    {
        target = null;
    }
}
