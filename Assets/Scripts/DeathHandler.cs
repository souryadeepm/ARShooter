using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DeathHandler : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Health>().OnDeath += HandleDeath;
    }

    private void OnDisable()
    {
        GetComponent<Health>().OnDeath -= HandleDeath;
    }

    private void HandleDeath()
    {
        if(gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
        if (gameObject.CompareTag("HomeBase"))
        {
            Debug.Log("-- GAME OVER --");
            GameManager.instance.StartGameOver();
        }
    }
    
}
