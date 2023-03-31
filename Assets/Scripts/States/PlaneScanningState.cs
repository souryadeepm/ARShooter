using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Linq;
using UnityEngine.XR.ARSubsystems;

public class PlaneScanningState : IState
{
    private GameObject planeScanningCanvas;
    private GameObject enemyBase;
    private GameObject homeBase;
    private ARPlaneManager arPlaneManager;
    private List<ARPlane> planesFound;

    private GameObject enemyBasePrefab;
    private GameObject homeBasePrefab;

    
    public PlaneScanningState(GameObject planeScanningCanvas, GameObject enemyBase, GameObject homeBase)
    {
        this.planeScanningCanvas = planeScanningCanvas;
        this.enemyBasePrefab = enemyBase;
        this.homeBasePrefab = homeBase;
    }
    public void EnterState()
    {
        Debug.Log("--Entering Plane Detection State--");
        planeScanningCanvas.SetActive(true);
        arPlaneManager = GameObject.FindObjectOfType<ARPlaneManager>();
        arPlaneManager.planesChanged += PlanesChanged;
        planesFound = new List<ARPlane>();

    }

    public void ExecuteState()
    {
        Debug.Log("--Executing Plane Detection State--");
       
    }

    private void PlanesChanged(ARPlanesChangedEventArgs data)
    {
        if (data.added != null && data.added.Count > 0)
            planesFound.AddRange(data.added);

        foreach (ARPlane plane in planesFound /*.Where(plane => plane.extents.x * plane.extents.y >= 0.2f)*/) 
        {
            if (plane.alignment.IsVertical() && enemyBase == null)
            {
                enemyBase = GameObject.Instantiate(enemyBasePrefab);
                enemyBase.transform.position = plane.center;
                enemyBase.transform.forward = plane.normal;

                GameManager.instance.EnemyBase = enemyBase;
                
            }
            if (plane.alignment.IsHorizontal() && enemyBase != null && homeBase == null)
            {
                homeBase = GameObject.Instantiate(homeBasePrefab);
                homeBase.transform.position = plane.center;
                homeBase.transform.forward = plane.normal;

                GameManager.instance.HomeBase = homeBase;
               
            }
        }
    }
     
    public void ExitState()
    {
        Debug.Log("--Exiting Plane Detection State--");
        planeScanningCanvas.SetActive(false);
        arPlaneManager.planesChanged -= PlanesChanged;

        foreach (var plane in planesFound)
        {
            plane.gameObject.SetActive(false);
        }
    }

    
   
}
