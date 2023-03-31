using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private Vector3 targetOffset;
    private EnemyShoot enemyShoot;
    private float distanceToTarget;

    private Vector3 target;
    private Transform homeBase;


    public float DistanceToTarget { get => distanceToTarget; set => distanceToTarget = value; }
    void Start()
    {
        //homeBase = GameManager.instance.HomeBase.transform;
        homeBase = GameObject.FindGameObjectWithTag("HomeBase").transform;
        target = homeBase.position + targetOffset;
        RandomizeMovements();
        enemyShoot = GetComponent<EnemyShoot>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(homeBase);
        distanceToTarget = Vector3.Distance(transform.position, target);


        if(distanceToTarget < 0.45f)
        {
            //shoot();

            transform.RotateAround(target, Vector3.up, 20 * Time.deltaTime);
            if (!enemyShoot.InRange)
                enemyShoot.InRange = true;
        }
        else
        {
            //keep moving towards target

            transform.position += (target - transform.position) * movementSpeed * Time.deltaTime ;
        }
    }

    private void RandomizeMovements()
    {
        target += new Vector3(Random.Range(-0.13f, 0.13f), Random.Range(-0.13f, 0.13f), Random.Range(-0.13f, 0.13f));
        movementSpeed += Random.Range(-0.13f, 0.13f);
    }
}
