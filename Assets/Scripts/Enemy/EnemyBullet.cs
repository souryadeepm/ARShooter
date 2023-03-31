using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("HomeBase"))
        {
            //apply damage to homebase
            other.GetComponent<Health>().Applydamage(10);
            
            Destroy(gameObject);
        }
    }
}
