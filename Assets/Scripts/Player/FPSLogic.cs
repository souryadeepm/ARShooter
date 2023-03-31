using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FPSLogic : MonoBehaviour
{
    [SerializeField] GameObject HUDCanvas;
    [SerializeField] GameObject mainCamera;

    bool locked;
    bool hitAnything;


    RaycastHit hit;

    public event Action<GameObject> OnEnemyLocked;
    public event Action OnEnemyUnlocked;

    private void FixedUpdate()
    {
        hitAnything = Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, 10);

        if(hitAnything)
        {
            if(hit.transform.gameObject.CompareTag("Enemy") && !locked)
            {
                locked = true;
                OnEnemyLocked.Invoke(hit.transform.gameObject);
                Debug.Log("--LOCKED--");
            }
            if (!hit.transform.gameObject.CompareTag("Enemy") && locked)
            {
                locked = false;
                OnEnemyUnlocked.Invoke();
                Debug.Log("--UNLOCKED--");
            }
        }
        if (!hitAnything && locked)
        {
            locked = false;
            OnEnemyUnlocked.Invoke();
        }
    }

}
